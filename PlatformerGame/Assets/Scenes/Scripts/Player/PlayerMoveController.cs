﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMoveController : MonoBehaviour
{
    // Start is called before the first frame update

    private Joystick joystick;
    private JumpJoyButton jumpJoyButton;
    Rigidbody2D playerRigidBody;
    public bool jumpPressed;
    public float jumpVelocity=42f;
    public float fallMultipler = 0.022f;// gravity factor when player reaches peak
    public float lowJumpMultiplier = 0.1f; // gravity factor for when player performs a low jump 
    public bool isGrounded;
    public float maxSpeed = 10f;
    public bool playerIsMoving;
    private bool isFacingRight;
    public float gravity = -265f;
    private int ExtraJumpCount = 0;
	public bool playerDead = false; 
    public int ExtraJumpsInAir = 0;
    [Range(0,1f)]public float glideFactor = 0.003f;
    Animator animator;
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;  // How much to smooth out the movement

	bool isTouchingWall;
	public Transform wallCheck;
	bool wallSliding;
	public float wallSlidingSpeed;
	bool wallJumping;
	public float xWallForce;
	public float yWallForce;
	public float wallJumpTime; 


	/* Dashing Vars*/
	public bool isDashing;
	public float dashSpeed;
	private Vector2 dashDir;
	private int groundDashCount;
	private int airDashCount;
	public int maxAirDashes = 1;
	private float dashDuration;
	public float maxDashDuration;
	public float dashRechargeTime;
	private float groundDashRechargeTime;
	public float maxGroundDashes;
	bool jumpHeld;
	public float input;
	void Start()
    {
        isFacingRight = true;
        playerIsMoving = false;
        isGrounded = false;
        joystick = FindObjectOfType<Joystick>();
        jumpJoyButton = FindObjectOfType<JumpJoyButton>();
        animator = GetComponent<Animator>();
        playerRigidBody = GetComponent<Rigidbody2D>();
        //Physics2D.gravity = new Vector2(0f,gravity);
    }

    // Update is called once per frame
    void Update()
    {
		if (!playerDead)
		{

			

			jumpHeld = (jumpJoyButton.Pressed || Input.GetButton("Jump"));
			input = joystick.Horizontal + Input.GetAxis("Horizontal");

			bool isMoving = Mathf.Abs(input) > 0;
			bool movingRight = input > 0;
			if (jumpJoyButton.Pressed || Input.GetButtonDown("Jump"))
				jumpPressed = true;

			animator.SetBool("isMoving", isMoving);
			if (isMoving)
			{
				if (!movingRight && isFacingRight)
					FlipPlayer();
				else if (movingRight && !isFacingRight)
					FlipPlayer();
			}

			if (groundDashCount == maxGroundDashes && groundDashRechargeTime > 0)
			{
				groundDashRechargeTime -= Time.deltaTime;
			}
			if (groundDashCount == maxGroundDashes && groundDashRechargeTime <= 0)
			{
				groundDashCount = 0;
				groundDashRechargeTime = dashRechargeTime;
			}
			if(isTouchingWall && input != 0)
            {
				wallSliding = true;
            }
            else
            {
				wallSliding = false;
            }

            if (wallSliding)
            {
				playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x, Mathf.Clamp(playerRigidBody.velocity.y, -wallSlidingSpeed, float.MaxValue));
            }


			
			if (!isDashing)
			{
				if (Input.GetKeyDown(KeyCode.LeftShift)) // p
				{
					Vector3 mousePosition = Input.mousePosition;
					mousePosition.z = Camera.main.nearClipPlane;
					mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
					dashDir = new Vector2(mousePosition.x - playerRigidBody.position.x, mousePosition.y - playerRigidBody.position.y);
					dashDir.Normalize();
					if (!isGrounded)
					{
						if (airDashCount < maxAirDashes)
						{
							airDashCount++;
							isDashing = true;
						}
					}
					else if (groundDashCount < maxGroundDashes)
					{
						isDashing = true;
						groundDashCount++;
					}
				}
			}
		}
	}

    // Used for handling any physics/manipulation of rigidbody
    private void FixedUpdate()
    {
		//shrinkPlayer();
		if (!playerDead)
		{
			// Handle Horizontal Movement
			if (isDashing)
			{
				if (dashDuration <= 0)
				{
					isDashing = false;
					dashDir = Vector2.zero;
					dashDuration = maxDashDuration;
					playerRigidBody.velocity = Vector2.zero;
				}
				else
				{
					dashDuration -= Time.deltaTime;
					bool airDash = !isGrounded;
					if (airDash)
					{
						playerRigidBody.velocity = dashDir * dashSpeed;
					}
					else
					{
						Vector2 groundDashDir = new Vector2(input > 0 ? 1 : -1, 0);
						playerRigidBody.velocity = groundDashDir * dashSpeed;
					}
				}
			}
			else
			{
				ApplyInput();
			} 

			if((Input.GetKey(KeyCode.Space) || jumpJoyButton.Pressed) && wallSliding == true)
            {
				wallJumping = true;
				Invoke("SetWallJumpingToFalse", wallJumpTime);

            }

            if (wallJumping)
            {
				playerRigidBody.velocity = new Vector2(xWallForce * -input, yWallForce);
            }

			//Replace jumpJoyButton.Pressed with Input.GetKeyDown(KeyCode.Space) for PC
			if (jumpPressed && isGrounded && !wallJumping)
			{
				print("jump");
				jumpPressed = false;
				playerRigidBody.AddForce(new Vector2(0f, jumpVelocity), ForceMode2D.Impulse);
				isGrounded = false;
				ExtraJumpCount--;
				animator.SetBool("inAir", true);
			}

			if (isGrounded)
			{
				jumpHeld = false;
				jumpJoyButton.Pressed = false;
				ExtraJumpCount = ExtraJumpsInAir;
			}

			print(jumpHeld);
			if (playerRigidBody.velocity.y <= 0 && jumpHeld)
			{
				 
				playerRigidBody.gravityScale = glideFactor * playerRigidBody.gravityScale;

			}
			else if(!wallJumping)
			{
				//playerRigidBody.velocity += Vector2.up * (lowJumpMultiplier);
				playerRigidBody.gravityScale = 5f;
			}
		}
	}

	public void ApplyInput()
	{
		float xInput = joystick.Horizontal + Input.GetAxisRaw("Horizontal");
		Vector2 playerVelocity = playerRigidBody.velocity;
		// Move the character by finding the target velocity
		Vector2 targetVelocity = new Vector2(xInput * maxSpeed, playerRigidBody.velocity.y);
		// And then smoothing it out and applying it to the character
		playerRigidBody.velocity = Vector2.SmoothDamp(targetVelocity, targetVelocity, ref playerVelocity, m_MovementSmoothing);
	}

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground" && isGrounded == false)
        {
			airDashCount = 0;
            isGrounded = true;
			jumpPressed = false;
			jumpJoyButton.Pressed = false;
			animator.SetBool("inAir", false);
        }
		if(other.gameObject.tag == "Wall")
        {
			isTouchingWall = true;
			//airDashCount = 0;
			print("touched");
		}
        else
        {
			isTouchingWall = false; 
        }
		
    }

    void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground" && isGrounded == true)
        {
			airDashCount = 0;
		//	jumpPressed = false;
		//	jumpJoyButton.Pressed = false;
			isGrounded = true;
           
        }
		if (other.gameObject.tag == "Wall" && isTouchingWall)
		{
			isTouchingWall = true;
			//airDashCount = 0;
		}
	}

    void OnCollisionExit2D(Collision2D other)
    {
		//  isGrounded = false;
		isTouchingWall = false;

       
    }

    void shrinkPlayer()
    {
        if (playerIsMoving)
        {
           // this.transform.localScale -= new Vector3(0.001f,0.001f,0);
           // print(this.transform.localScale);
           
        }
        
    }

	void SetWallJumpingToFalse()
    {
		wallJumping = false; 
    }

    void FlipPlayer()
    {
        isFacingRight = !isFacingRight;
		Vector3 scale = transform.localScale;
		scale.x *= -1;
		transform.localScale = scale;
    }


}
