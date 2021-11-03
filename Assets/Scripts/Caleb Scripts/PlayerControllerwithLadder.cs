using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Caleb Rosenboom made with help from videos from Blackthronprod on Youtube
public class PlayerControllerwithLadder : MonoBehaviour
{
    public float speed = 10;
    public float inputHorizontal;
    public float inputVertical;

    public float jumpForce = 5;
    private int extraJumps;
    public int extraJumpsValue = 1;
    public float fallMultiplier = 1.25f;
    public float lowJumpMultiplier = 0.5f;

    private Rigidbody2D rb;
    public float rememberGroundedFor = 0.1f;
    float lastTimeGrounded;
    private bool facingRight = true;
    private bool isGrounded;
    public Transform isGroundedCheck;
    public float checkGroundRadius = 0.25f;
    public LayerMask groundLayer;

    public float distance;
    public LayerMask whatIsLadder;
    public bool isClimbing;
    public float climbSpeed;
    //Start called before first update
    void Start()
    {
        extraJumps = extraJumpsValue;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update(){
        Move();
        Jump();
        GroundCheck();
        JumpImprovement();
        Climb();
    }

    void Climb() {
        climbSpeed = speed * 0.5f;
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.up, distance, whatIsLadder);

        if (hitInfo.collider != null) {
            if (Input.GetKey(KeyCode.UpArrow)) {
                isClimbing = true;
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                isClimbing = false;
            }
        }
        if (isClimbing == true){
            inputVertical = Input.GetAxisRaw("Vertical");
            rb.velocity = new Vector2(rb.velocity.x, inputVertical * climbSpeed);
        }
        }
       //Controls player movement
        void Move()
        {
            inputHorizontal = Input.GetAxisRaw("Horizontal");
            rb.velocity = new Vector2(inputHorizontal * speed, rb.velocity.y);
            if (facingRight == false && inputHorizontal > 0)
            {
                Flip();
            }
            else if (facingRight == true && inputHorizontal < 0)
            {
                Flip();
            }
        }

        //Flips character
        void Flip()
        {
            facingRight = !facingRight;
            Vector3 Scaler = transform.localScale;
            Scaler.x *= -1;
            transform.localScale = Scaler;
        }

        //Controls jumping
        void Jump()
        {
            if (isGrounded == true)
            {
                extraJumps = extraJumpsValue;
            }
            if (Input.GetButtonDown("Jump") && (isGrounded || Time.time - lastTimeGrounded <= rememberGroundedFor || extraJumps > 0))

            {
                rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                extraJumps--;
            }
            else if (Input.GetButtonDown("Jump") && (isGrounded || Time.time - lastTimeGrounded <= rememberGroundedFor) && extraJumps == 0)
            {
                rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            }
        }

        //checks if player is Grounded
        void GroundCheck()
        {
            Collider2D collider = Physics2D.OverlapCircle(isGroundedCheck.position, checkGroundRadius, groundLayer);
            if (collider != null)
            {
                isGrounded = true;
            }
            else {
                if (isGrounded == true){
                    lastTimeGrounded = Time.time;
                }
                isGrounded = false;
            }
        }

        //This improves the feeling of jump
        void JumpImprovement()
        {
            if (rb.velocity.y < 0)
            {
                rb.velocity += Vector2.up * Physics2D.gravity * (fallMultiplier - 1) * Time.deltaTime;
            }
            else if (rb.velocity.y > 0 && Input.GetButton("Jump"))
            {
                rb.velocity += Vector2.up * Physics2D.gravity * (lowJumpMultiplier - 1) * Time.deltaTime;
            }
        }
  }

