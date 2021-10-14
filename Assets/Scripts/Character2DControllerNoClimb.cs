using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character2DControllerNoClimb : MonoBehaviour {
    public float MovementSpeed = 1;
    public float JumpForce = 1;

    private Rigidbody2D _rb;
    private float inputHorizontal;
    private float inputVertical;

    // Start is called before the first frame update
    void Start() {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
    }

    // Update is called once per frame, fixed for physics or whatever
    void FixedUpdate() {
        inputHorizontal = Input.GetAxis("Horizontal");
        _rb.velocity = new Vector2(inputHorizontal * Time.deltaTime * MovementSpeed, _rb.velocity.y);

        if (Input.GetButtonDown("Jump") && Mathf.Abs(_rb.velocity.y) < 0.001f) {
            _rb.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
        }
    }

    // this happens when the object this is attached to collides with something else
    private void OnTriggerStay2D(Collider2D collision) {
    }
}
