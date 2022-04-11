using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Caleb Rosenboom, Eric Dundas, Robert Spatz
//Consolidated by Caleb Rosenboom
public class PlayerController : MonoBehaviour
{
    public float speed = 10;
    public float inputHorizontal;

    public float jumpForce = 5.5f;
    private int extraJumps;
    public int extraJumpsValue = 1;
    private int playerExtraJumps;
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

    public BookBehavior BookPrefab;
    public Transform LaunchOffset;

    public LayerMask whatIsLadder;
    private float inputVertical;
    private bool isClimbing;
    public float climbSpeed = 2f;
    public float distance;

    //Making the game feel more fluid stuff
    public float[] maxSpeed = {
        10, 10
    };
    public float[] acceleration = {
        5, 5
    };
    public float[] deceleration = {
        5, 5
    };
    private float currentSpeed = 0;

    public float localGravityScale = 20;

    public GameObject pauseMenu;

    //Start called before first update
    void Start()
    {
        extraJumps = extraJumpsValue;
        playerExtraJumps = extraJumpsValue;
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Move();
        Jump();
        groundCheck();
        jumpImprovement();
        Throw();
        Climb();
        Menu();
    }
   
    //Controls Throwing of Book Platform
    private void Throw()
    {
        if (Input.GetButtonDown("Fire1") && gameObject.GetComponent<interactPlayer>().player == 1)
        {
            if (GameObject.FindGameObjectWithTag("Book") != null) {
                GameObject.FindGameObjectWithTag("Book").GetComponent<BookBehavior>().Fall();
            }
            Instantiate(BookPrefab, LaunchOffset.position, transform.rotation);
        }
    }

    //Controls player movement
    void Move()
    {
        inputHorizontal = Input.GetAxisRaw("Horizontal");
        //_rb.velocity = new Vector2(inputHorizontal * speed, _rb.velocity.y);

        //Below is Wally's movement improvements.
        switch (inputHorizontal)
        {
            case 0:
                if (currentSpeed != 0)
                {
                    if (currentSpeed > 0)
                    {
                        currentSpeed = currentSpeed - deceleration[gameObject.GetComponent<interactPlayer>().player];
                    } else if (currentSpeed < 0)
                    {
                        currentSpeed = currentSpeed + deceleration[gameObject.GetComponent<interactPlayer>().player];
                    }
                }
                break;
            case 1:
                if (currentSpeed < maxSpeed[gameObject.GetComponent<interactPlayer>().player])
                {
                    currentSpeed = currentSpeed + acceleration[gameObject.GetComponent<interactPlayer>().player];
                }
                break;
            case -1:
                if (currentSpeed > (-1 * maxSpeed[gameObject.GetComponent<interactPlayer>().player]))
                {
                    currentSpeed = currentSpeed + (-1 * acceleration[gameObject.GetComponent<interactPlayer>().player]);
                }
                break;
            default:
                break;
        }     
        _rb.velocity = new Vector2(currentSpeed, _rb.velocity.y);
        //Above is Wally's movement improvements.

        if (facingRight == false && inputHorizontal > 0)
        {
            Flip();
        }
        else if (facingRight == true && inputHorizontal < 0)
        {
            Flip();
        }
    }

    //Character Flipping Left or Right Based on Input
    void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    //Controls jumping
    void Jump()
    {
        if (gameObject.GetComponent<interactPlayer>().player == 0) {
            playerExtraJumps = extraJumpsValue;
        }
        else {
            playerExtraJumps = 0;
        }
        if (isGrounded || isClimbing)
        {
            extraJumps = playerExtraJumps;
        }
        if (Input.GetButtonDown("Jump") && (isGrounded || Time.time - lastTimeGrounded <= rememberGroundedFor || extraJumps > 0 || isClimbing))

        {
            _rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            extraJumps--;
        }
        else if (Input.GetButtonDown("Jump") && (isGrounded || Time.time - lastTimeGrounded <= rememberGroundedFor) && extraJumps == 0)
        {
            _rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
    }

    //Checks if the player is grounded
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
        else if (_rb.velocity.y > 0 && Input.GetButton("Jump"))
        {
            _rb.velocity += Vector2.up * Physics2D.gravity * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    //Controls Ladder Climbing
    void Climb()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.up, distance, whatIsLadder);
        Rigidbody2D ladder;

        if (hitInfo.collider != null)
        {
            if (Input.GetButtonDown("Vertical"))
            {
                isClimbing = true; 
            }
            ladder = hitInfo.transform.GetComponent<Rigidbody2D>();
        }
        else
        {
            isClimbing = false;
            ladder = null;
        }

        if (isClimbing)
        {
            inputVertical = Input.GetAxisRaw("Vertical");
            _rb.velocity = new Vector2(_rb.velocity.x, inputVertical * climbSpeed);
            _rb.gravityScale = 0;
            if (hitInfo.transform.GetComponent<LadderHandler>().MovingLadder)
            {
                ladder.transform.position = new Vector3(Mathf.Clamp(_rb.position.x,
                    hitInfo.transform.GetComponent<LadderHandler>().LadderBoundsLeft,
                    hitInfo.transform.GetComponent<LadderHandler>().LadderBoundsRight),
                    ladder.transform.position.y, ladder.transform.position.z);
            }
        }
        else
        {
            _rb.gravityScale = localGravityScale;
        }
        if (Input.GetButtonDown("Jump") && isClimbing)
        {
            Jump();
            isClimbing = false;
            _rb.gravityScale = localGravityScale;
        }
    }

    void Menu() {
        if(Input.GetButtonDown("Cancel"))
        {
            pauseMenu.SetActive(!pauseMenu.activeSelf);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
    }
}
