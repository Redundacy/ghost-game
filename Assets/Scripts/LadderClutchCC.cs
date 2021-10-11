using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderClutchCC : MonoBehaviour {
    public float MovementSpeed = 1;
    public float JumpForce = 1;
    public float climbSpeed = 2f;
    public float distance;
    bool isGrounded = false;
    public Transform isGroundedChecker;
    public float checkGroundRadius;
    public LayerMask groundLayer;
    public LayerMask whatIsLadder;

    private Rigidbody2D _rb;
    private float inputHorizontal;
    private float inputVertical;
    private bool isClimbing;

    // Start is called before the first frame update
    void Start() {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
        CheckIfGrounded();
    }

    // Update is called once per frame, fixed for physics or whatever
    void FixedUpdate() {
        inputHorizontal = Input.GetAxis("Horizontal");
        _rb.velocity = new Vector2(inputHorizontal * Time.deltaTime * MovementSpeed, _rb.velocity.y);

        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.up, distance, whatIsLadder);

        if (hitInfo.collider != null) {
            if (Input.GetKey(KeyCode.G)) { // We should probably add some sort of grab/clutch button so we can add in some nice rolling ladders
                isClimbing = true;
            }
        }
        else {
            isClimbing = false;
        }

        if (isClimbing) {
            inputVertical = Input.GetAxisRaw("Vertical");
            _rb.velocity = new Vector2(_rb.velocity.x, inputVertical * climbSpeed);
            _rb.gravityScale = 0;
        }
        else {
            _rb.gravityScale = 1;
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) {
            _rb.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
        }
        if (Input.GetKeyDown(KeyCode.Space) && isClimbing) {
            isClimbing = false;
            _rb.gravityScale = 1;
            _rb.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
        }
    }

    // this happens when the object this is attached to collides with something else
    private void OnTriggerStay2D(Collider2D collision) {
    }

    void CheckIfGrounded()
    {
        Collider2D collider = Physics2D.OverlapCircle(isGroundedChecker.position, checkGroundRadius, groundLayer);

        if (collider != null) {
            isGrounded = true;
        } else {
            isGrounded = false;
        }
    }
}
