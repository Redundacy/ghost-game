using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class platformer : MonoBehaviour
{
    // Robert Spatz
    // created following https://craftgames.co/unity-2d-platformer-movement/

    public float speed;
    Rigidbody2D rb;
    public float jumpForce;
    bool isGrounded = false;
    public Transform isGroundedChecker;
    public float checkGroundRadius;
    public LayerMask groundLayer;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
        CheckIfGrounded();
    }

    void FixedUpdate() {
    }

    // horizontal movement function
    void Move(){
        float x = Input.GetAxisRaw("Horizontal");
        float moveBy = x * speed;
        rb.velocity = new Vector2(moveBy, rb.velocity.y);
        if (x < 0) {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (x > 0) {
            GetComponent<SpriteRenderer>().flipX = false;
        }
    }
    // jump function
    void Jump(){
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded){
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }
    // checks if the player is grounded
    void CheckIfGrounded(){
        Collider2D collider = Physics2D.OverlapCircle(isGroundedChecker.position, checkGroundRadius, groundLayer);
    
        if (collider != null) {
            isGrounded = true;
        } else {
            isGrounded = false;
        }
    }

}
