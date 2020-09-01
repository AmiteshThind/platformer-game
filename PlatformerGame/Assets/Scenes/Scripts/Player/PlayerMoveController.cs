using System.Collections;
using System.Collections.Generic;
using DitzeGames.Effects;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMoveController : MonoBehaviour
{
	// Start is called before the first frame update

	private Joystick joystick;
	private JumpJoyButton jumpJoyButton;
	Rigidbody2D playerRigidBody;
	public bool jumpPressed;
	public float jumpVelocity = 42f;
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
	[Range(0, 1f)] public float glideFactor = 0.003f;
	Animator animator;
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;  // How much to smooth out the movement
	public float hangTime;
	public float hangCounter;
	public bool isOnSlantedWall;

	public Transform groundCheckPoint;
	public Vector2 groundCheckSize;
	public LayerMask groundLayer;

	[Header("Wall Jumping")]

	public bool isTouchingWall;
	public bool wallJumping;
	public Transform wallCheckPoint;
	public Vector2 wallCheckSize;
	public LayerMask wallLayer;
	public float wallJumpTime;

	public float wallSlideSpeed;
	public bool isWallSliding;
	public float xWallForce;
	public float yWallForce;
	public float walljumpDirection = -1;
	public float airSpeed = 0f;

	[Header("Camera Shake")]
	public float duration = 1f;
	public float speed = 10f;
	public Vector3 CameraForce = new Vector3(2f, 2f, 2f);
	





	/* Dashing Vars*/
	DashButton dashbutton;
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
	public bool isGrounded2;
	void Start()
	{
		 
		isFacingRight = true;
		playerIsMoving = false;
		isGrounded = false;
		joystick = FindObjectOfType<Joystick>();
		jumpJoyButton = FindObjectOfType<JumpJoyButton>();
		dashbutton = FindObjectOfType<DashButton>();
		animator = GetComponent<Animator>();
		playerRigidBody = GetComponent<Rigidbody2D>();
		//Physics2D.gravity = new Vector2(0f,gravity);
	}

	// Update is called once per frame
	void Update()
	{

    

		if (!playerDead)
		{


			isTouchingWall = Physics2D.OverlapBox(wallCheckPoint.position, wallCheckSize, 0, wallLayer);
			isGrounded2 = Physics2D.OverlapBox(groundCheckPoint.position, groundCheckSize, 0, groundLayer);
			jumpHeld = (jumpJoyButton.Pressed || Input.GetButton("Jump"));
			input = joystick.Horizontal + Input.GetAxis("Horizontal");

			bool isMoving = Mathf.Abs(input) > 0;
			bool movingRight = input > 0;
			if (jumpJoyButton.Pressed || Input.GetButtonDown("Jump"))
			{
				jumpPressed = true;
				
			}


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

            if (!isGrounded2)
            {
				isGrounded = false;
            }

			if (!isDashing)
			{
				bool keyboardDash = Input.GetKeyDown(KeyCode.LeftShift);
				bool joyDash = dashbutton.Pressed;
				if (keyboardDash || joyDash) // p
				{
					if (keyboardDash) // get mouse position
					{
						Vector3 mousePosition = Input.mousePosition;
						mousePosition.z = Camera.main.nearClipPlane;
						mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
						dashDir = new Vector2(mousePosition.x - playerRigidBody.position.x, mousePosition.y - playerRigidBody.position.y);
						dashDir.Normalize();
						
					}
					if (joyDash)
					{
						Vector2 dir = new Vector2(joystick.Horizontal, joystick.Vertical);
						dashDir = dir.normalized;
						
					}

					if (!isGrounded)
					{
						if (airDashCount < maxAirDashes)
						{
							airDashCount++;
							isDashing = true;
						}
					}
					else if (groundDashCount < maxGroundDashes && input != 0)
					{
						isDashing = true;
						groundDashCount++;
					}
				}

			}

           

			if((Input.GetKeyDown(KeyCode.Space) || jumpJoyButton.Pressed) && isWallSliding)
            {
				wallJumping = true;
				jumpJoyButton.Pressed = false;
				Invoke("SetWallJumpingToFalse", wallJumpTime);
				AudioManager.instance.Play("PlayerJump");

			}

			if(isTouchingWall && !isGrounded)
            {
				isWallSliding = true;
            }
            else
            {
				isWallSliding = false; 
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
					AudioManager.instance.Play("PlayerDash");
					CameraEffects.ShakeOnce(duration,speed,CameraForce);
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


			//Replace jumpJoyButton.Pressed with Input.GetKeyDown(KeyCode.Space) for PC
			if (jumpPressed && hangCounter>0 && !isTouchingWall)
			{
				jumpPressed = false;
				//playerRigidBody.AddForce(new Vector2(0f, jumpVelocity), ForceMode2D.Impulse);
				playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x, jumpVelocity);
				isGrounded = false;
				ExtraJumpCount--;
				animator.SetBool("inAir", true);
				if (hangCounter == hangTime)
				{
					AudioManager.instance.Play("PlayerJump");
				}
			}

			if (isGrounded)
			{
				jumpHeld = false;
				jumpJoyButton.Pressed = false;
				ExtraJumpCount = ExtraJumpsInAir;
				hangCounter = hangTime; 

			}
			 
			else
			{
				hangCounter -= Time.fixedDeltaTime;
			}

			
			if (playerRigidBody.velocity.y <= 0 && jumpHeld)
			{

				playerRigidBody.gravityScale = glideFactor * playerRigidBody.gravityScale;

			}
			else
			{
				//playerRigidBody.velocity += Vector2.up * (lowJumpMultiplier);
				playerRigidBody.gravityScale = 8f;
			}

			if (isWallSliding)
			{
				playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x, Mathf.Clamp(playerRigidBody.velocity.y, -wallSlideSpeed, float.MaxValue));
			}



			if (wallJumping)
			{
				if (isFacingRight)
				{

					playerRigidBody.velocity = new Vector2(xWallForce * -1, yWallForce);
				}
                else if(!isFacingRight) // for animations created two conditions if needed 
                {
					playerRigidBody.velocity = new Vector2(xWallForce * 1, yWallForce);
				}
				 
				print("WALLJUMp");
			}


		}
	}

	public void ApplyInput()
	{
		if (!isTouchingWall)
		{
			 
			float xInput = joystick.Horizontal + Input.GetAxisRaw("Horizontal");
			Vector2 playerVelocity = playerRigidBody.velocity;
			// Move the character by finding the target velocity
			Vector2 targetVelocity = new Vector2(xInput * maxSpeed, playerRigidBody.velocity.y);
			// And then smoothing it out and applying it to the character
			playerRigidBody.velocity = Vector2.SmoothDamp(targetVelocity, targetVelocity, ref playerVelocity, m_MovementSmoothing);


		}

			//else if (!isGrounded && !isWallSliding && input != 0)
			//{
			 
	  //          if (Mathf.Abs(playerRigidBody.velocity.x) > maxSpeed)
	  //          {
			//		//playerRigidBody.velocity = new Vector2(input * maxSpeed, playerRigidBody.velocity.y);
	  //          }

			//}

	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if ((other.gameObject.tag == "Ground" || other.gameObject.tag=="OpenSesameCircleRock" || other.gameObject.tag == "OpenSesameSquareRock" ||other.gameObject.tag == "OpenSesameTriangleRock" || other.gameObject.tag == "BlankRock" ) && isGrounded == false)
		{
			airDashCount = 0;
			isGrounded = true;
			jumpPressed = false;
			jumpJoyButton.Pressed = false;
			animator.SetBool("inAir", false);
			isOnSlantedWall = false;
		 
		}
		if(other.gameObject.tag == "Wall")
        {
			isGrounded = false;
			isOnSlantedWall = true;
        }


		if (other.gameObject.tag == "MovingPlatform")
		{
			 
			isGrounded = true;
			jumpPressed = false;
			jumpJoyButton.Pressed = false;
			animator.SetBool("inAir", false);
		}



	}

	void OnCollisionStay2D(Collision2D other)
	{
		if ((other.gameObject.tag == "Ground" || other.gameObject.tag == "OpenSesameCircleRock" || other.gameObject.tag == "OpenSesameSquareRock" || other.gameObject.tag == "OpenSesameTriangleRock" || other.gameObject.tag == "BlankRock") && isGrounded == true)
		{ 
			airDashCount = 0;
			jumpPressed = false;
			jumpJoyButton.Pressed = false;
			isGrounded = true;

		}


		if(other.gameObject.tag == "MovingPlatform")
        {
			transform.parent = other.transform;
			isGrounded = true;
			jumpPressed = false;
			jumpJoyButton.Pressed = false;
			animator.SetBool("inAir", false);
		}
         
	}

	void OnCollisionExit2D(Collision2D other)
	{
		// isGrounded = false;
		isOnSlantedWall = false;

		if (other.gameObject.tag == "MovingPlatform")
		{
			transform.parent = null;
			isGrounded = false;
			animator.SetBool("inAir", true);
		}

	}

	private void OnParticleCollision(GameObject other)
	{
		if (other.tag == "Smoke")
		{
			playerDead = true;
			glideFactor = 1;
			playerRigidBody.gravityScale = 8f;
		}
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
        if (!isWallSliding) { 
			walljumpDirection *= -1;

			isFacingRight = !isFacingRight;
			Vector3 scale = transform.localScale;
			scale.x *= -1;
			transform.localScale = scale;
		 }
		}




	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawCube(wallCheckPoint.position, wallCheckSize);

		Gizmos.color = Color.red;
		Gizmos.DrawCube(groundCheckPoint.position, groundCheckSize);

	}

	void SetWallJumpingToFalse()
    {
		wallJumping = false;

    }

	void JumpPressedIsFalse()
    {
		jumpPressed = false; 
    }

	public void KillPlayer()
	{
		playerDead = true;
	}

}