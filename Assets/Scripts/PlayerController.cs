using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Caleb Rosenboom
//made using  https://craftgames.co/unity-2d-platformer-movement/ and https://www.youtube.com/watch?v=QGDeafTx5ug
public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    public float PlayerMovement;

    private Rigidbody2D rb;
    private bool facingRight = true;
    //
    private bool isGrounded;
    public Transform IsGroundChecker;
    public float GroundCheckRadius;
    public LayerMask whatIsGround;

    private int extraJumps;
    public int extraJumpsValue;

    void Start()
    {
        extraJumps = extraJumpsValue;
        rb = GetComponent<Rigidbody2D>();
    }
    //Controls player movement
   void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, GroundCheckRadius, whatIsGround);
        PlayerMovement = Input.GetAxisRaw("Horizontal");
        Debug.Log(PlayerMovement);
        rb.velocity = new Vector2(PlayerMovement * PlayerSpeed, rb.velocity.y);
        //
        if(facingRight == false && PlayerMovement > 0)
        {
            Flip();
        } else if (facingRight == true && PlayerMovement < 0)
        {
            Flip();
        }
    }
    //Makes sure character is facing correct way when pushing the arrow keys
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }

    //Controls Jumping 
    void Update()
    {
        if (isGrounded == true)
        {
            extraJumps = extraJumpsValue;
        }
        if(Input.GetKeyDown(KeyCode.UpArrow) && extraJumps > 0)
        {
            rb.velocity = Vector2.up * JumpForce;
            extraJumps--;
        }else if (Input.GetKeyDown(KeyCode.UpArrow) && extraJumps == 0 && isGrounded == true)
        {
            rb.velocity = Vector2.up * JumpForce;
        }
    }
}