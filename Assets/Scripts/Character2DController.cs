using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character2DController : MonoBehaviour {
    public float MovementSpeed = 1;
    public float JumpForce = 1;
    public float climbSpeed = 2f;

    private Rigidbody2D _rb;
    private bool _crouchHeld = false,
    _isCloseToLadder = false,
    _climbHeld = false, 
    _hasStartedClimb = false;

    private Transform ladder;
    private float vertical = 0f;

    // Start is called before the first frame update
    void Start() {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
        vertical = Input.GetAxisRaw("Vertical") * climbSpeed;
        //if (Mathf.Abs(_rb.velocity.y) < 0.001f && movement.Equals(0) && !isCloseToLadder && (crouchHeld || isUnderPlatform))
        //    GetComponent<Animator>().Play("CharacterCrouchIdle");
        //    else if (Mathf.Abs(_rb.velocity.y) < 0.001f && !isCloseToLadder && (movement > 0 || movement < 0) && (crouchHeld || isUnderPlatform))
        //    GetComponent<Animator>().Play("CharacterCrouch");
        //    else if (Mathf.Abs(_rb.velocity.y) < 0.001f && !hasStartedClimb && movement.Equals(0))
        //    GetComponent<Animator>().Play("CharacterIdle");
        //    else if (Mathf.Abs(_rb.velocity.y) < 0.001f && !hasStartedClimb && (movement > 0 || movement < 0))
        //    GetComponent<Animator>().Play("CharacterWalk");

        //_crouchHeld = (Mathf.Abs(_rb.velocity.y) < 0.001f && !_isCloseToLadder && Input.GetButton("Crouch")) ? true : false;
        _climbHeld = (_isCloseToLadder && Input.GetButton("Climb")) ? true : false;

        if (_climbHeld) {
            if (!_hasStartedClimb) {
                _hasStartedClimb = true;
            }
        } else {
            if (_hasStartedClimb) {
                //GetComponent<Animator>().Play("CharacterClimbIdle");
            }
        }
    }

    // Update is called once per frame, fixed for physics or whatever
    void FixedUpdate() {
        var movement = Input.GetAxis("Horizontal");
        _rb.velocity = new Vector2(movement * Time.deltaTime * MovementSpeed, _rb.velocity.y);

        if (Input.GetButtonDown("Jump") && Mathf.Abs(_rb.velocity.y) < 0.001f) {
            _rb.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
        }

        if (_hasStartedClimb && !_climbHeld) {
            if (movement > 0 || movement < 0) {
                ResetClimbing();
            }
        } else if (_hasStartedClimb && _climbHeld) {
            float height = GetComponent<SpriteRenderer>().size.y;
            float topHandlerY = Half(ladder.transform.GetChild(0).transform.position.y + height);
            float bottomHandlerY = Half(ladder.transform.GetChild(1).transform.position.y + height);
            float transformY = Half(transform.position.y);
            float transformVY = transformY + vertical;

            if (transformVY > topHandlerY || transformVY < bottomHandlerY) {
                ResetClimbing();
            } else if (transformY <= topHandlerY && transformY >= bottomHandlerY) {
                _rb.bodyType = RigidbodyType2D.Kinematic;
                if (!transform.position.x.Equals(ladder.transform.position.x))
                    transform.position = new Vector3(ladder.transform.position.x, transform.position.y, transform.position.z);

                //GetComponent<Animator>().Play("CharacterClimb");
                Vector3 forwardDirection = new Vector3(0, transformVY, 0);
                Vector3 newPos = Vector3.zero;
                if (vertical > 0)
                    newPos = transform.position + forwardDirection * Time.deltaTime * climbSpeed;
                else if (vertical < 0)
                    newPos = transform.position - forwardDirection * Time.deltaTime * climbSpeed;
                if (newPos != Vector3.zero) _rb.MovePosition(newPos);
            }
        }
    }

    public static float Half(float value)
    {
        return Mathf.Floor(value) + 0.5f;
    }

    private void ResetClimbing()
    {
        if (_hasStartedClimb) {
            _hasStartedClimb = false;
            _rb.bodyType = RigidbodyType2D.Dynamic;
            transform.position = new Vector3(transform.position.x, Half(transform.position.y), transform.position.z);
        }
    }

    // this happens when the object this is attached to collides with something else
    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.gameObject.tag.Equals("Ladder")) {
            _isCloseToLadder = true;
            this.ladder = collision.transform;
        }
    }
}
