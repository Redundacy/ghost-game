using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character2DController : MonoBehaviour {
    public float MovementSpeed = 1;
    public float JumpForce = 1;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() {
        var movement = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(movement * Time.deltaTime * MovementSpeed, rb.velocity.y);
        

        if (Input.GetButtonDown("Jump") && Mathf.Abs(rb.velocity.y) < 0.001f) {
            rb.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
        }
    }
}
