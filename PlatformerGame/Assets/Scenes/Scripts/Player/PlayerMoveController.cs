using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMoveController : MonoBehaviour
{
    // Start is called before the first frame update

    private Joystick joystick;
    private JumpJoyButton jumpJoyButton;
    Rigidbody2D rb;
    public bool jump;
    [Range(1, 20)] public float jumpVelocity;
    public float fallMultipler = 2.5f;// gravity factor when player reaches peak
    public float lowJumpMultiplier = 2f; // gravity factor for when player performs a low jump 
    public bool isGrounded;
    public float playerSpeed;
    public bool playerIsMoving;
    private bool isFacingRight;
    Animator animator; 



    void Start()
    {
        transform.Rotate(0, 180f, 0);
        isFacingRight = true;
        playerIsMoving = false;
        playerSpeed = 10f;
        isGrounded = false;
        joystick = FindObjectOfType<Joystick>();
        jumpJoyButton = FindObjectOfType<JumpJoyButton>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        

        if (joystick.Horizontal !=0 || Input.GetAxis("Horizontal")!=0)
        {
            playerIsMoving = true;
            animator.SetBool("isMoving",true);
        }
        else
        {
            playerIsMoving = false;
            animator.SetBool("isMoving", false);
        }

        if(isFacingRight && (joystick.Horizontal<0 || Input.GetAxis("Horizontal")<0))
        {
            FlipPlayer();
            print("FLIP");
        }else if(!isFacingRight && (joystick.Horizontal > 0 || Input.GetAxis("Horizontal") > 0))
        {
            FlipPlayer();
        }



    }

    // Used for handling any physics/manipulation of rigidbody
    private void FixedUpdate()
    {
        shrinkPlayer();
        rb.velocity = new Vector2(joystick.Horizontal *playerSpeed +Input.GetAxis("Horizontal")*playerSpeed , rb.velocity.y);//set horizontal player speed 
        if (!jump & jumpJoyButton.Pressed && isGrounded)
        {
            
            jump = true; 
            rb.velocity += Vector2.up * jumpVelocity;
        }
        if (jump && (!jumpJoyButton.Pressed))
        {
            jump = false;
        }

        if (rb.velocity.y <= 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultipler);
        }else if(rb.velocity.y>0 && !jump){
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier);
        }


        

    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground" && isGrounded == false)
        {
            isGrounded = true;
            animator.SetBool("inAir", false);
        }
    }

    void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground" && isGrounded == true)
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
                isGrounded = false;
        animator.SetBool("inAir", true);
    }

    void shrinkPlayer()
    {
        if (playerIsMoving)
        {
           // this.transform.localScale -= new Vector3(0.001f,0.001f,0);
           // print(this.transform.localScale);
           
        }
        
    }

    void FlipPlayer()
    {
        print("Player");
        isFacingRight = !isFacingRight;
        transform.Rotate(0f, 180f, 0f);
    }


}
