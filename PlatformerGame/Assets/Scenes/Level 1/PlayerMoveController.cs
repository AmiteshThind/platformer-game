using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    void Start()
    {
        playerSpeed = 10f;
        isGrounded = false;
        joystick = FindObjectOfType<Joystick>();
        jumpJoyButton = FindObjectOfType<JumpJoyButton>(); 
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    // Used for handling any physics/manipulation of rigidbody
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(joystick.Horizontal *playerSpeed +Input.GetAxis("Horizontal")*playerSpeed , rb.velocity.y);//set horizontal player speed 
        if (!jump & jumpJoyButton.Pressed && isGrounded)
        {
            jump = true; 
            rb.velocity += Vector2.up * jumpVelocity;
        }
        if (jump && !jumpJoyButton.Pressed)
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
    }


}
