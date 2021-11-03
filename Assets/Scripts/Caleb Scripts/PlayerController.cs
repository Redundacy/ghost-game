using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Caleb Rosenboom, Eric Dundas, Robert Spatz
//Consolidated by Caleb Rosenboom
//made using  https://craftgames.co/unity-2d-platformer-movement/ and https://www.youtube.com/watch?v=QGDeafTx5ug
public class PlayerController : MonoBehaviour
{
    public float speed = 10;
    public float inputHorizontal;

    public float jumpForce = 5.5f;
    private int extraJumps;
    public int extraJumpsValue = 1;
    public float fallMultiplier = 1.25f;
    public float lowJumpMultiplier = 0.5f;

    private Rigidbody2D _rb;
    public float rememberGroundedFor = 0.1f;
    float lastTimeGrounded;
    private bool facingRight = true;
    private bool isGrounded;
    public Transform isGroundedCheck;
    public float checkGroundRadius = 0.25f;
    public LayerMask groundLayer;

    //Start called before first update
    void Start()
    {
        extraJumps = extraJumpsValue;
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Move();
        Jump();
        groundCheck();
        jumpImprovement();
    }

    //Controls player movement
    void Move()
    {
        inputHorizontal = Input.GetAxisRaw("Horizontal");
        _rb.velocity = new Vector2(inputHorizontal * speed, _rb.velocity.y);
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
        if (isGrounded)
        {
            extraJumps = extraJumpsValue;
        }
        if (Input.GetButtonDown("Jump") && (isGrounded || Time.time -lastTimeGrounded <= rememberGroundedFor || extraJumps > 0))

        {
            _rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            extraJumps--;
        }
        else if (Input.GetButtonDown("Jump") && (isGrounded || Time.time - lastTimeGrounded <= rememberGroundedFor) && extraJumps == 0)
        {
            _rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
    }

    //checks if player is Grounded
    void groundCheck()
    {
        Collider2D collider = Physics2D.OverlapCircle(isGroundedCheck.position, checkGroundRadius, groundLayer);
        if (collider != null)
        {
            isGrounded = true;
        }
        else
        {
            if (isGrounded)
            {
                lastTimeGrounded = Time.time;
            }
            isGrounded = false;
        }
    }

    //This improves the feeling of jump
    void jumpImprovement()
    {
        if (_rb.velocity.y < 0)
        {
            _rb.velocity += Vector2.up * Physics2D.gravity * (fallMultiplier - 1) * Time.deltaTime;
        }
    //controls the short jump
        else if (_rb.velocity.y > 0 && Input.GetButton("Jump"))
        {
            _rb.velocity += Vector2.up * Physics2D.gravity * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }
}