using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMoveController : MonoBehaviour
{
    // Start is called before the first frame update

    private Joystick joystick;
    private JumpJoyButton jumpJoyButton;
    Rigidbody2D playerRigidBody;
    public bool jump;
    [Range(1, 20)] public float jumpVelocity;
    public float fallMultipler = 2.5f;// gravity factor when player reaches peak
    public float lowJumpMultiplier = 2f; // gravity factor for when player performs a low jump 
    public bool isGrounded;
    public float maxSpeed = 10f;
    public bool playerIsMoving;
    private bool isFacingRight;
    Animator animator;
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;  // How much to smooth out the movement

	void Start()
    {
        isFacingRight = true;
        playerIsMoving = false;
        isGrounded = false;
        joystick = FindObjectOfType<Joystick>();
        jumpJoyButton = FindObjectOfType<JumpJoyButton>();
        animator = GetComponent<Animator>();
        playerRigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
		float input = joystick.Horizontal + Input.GetAxis("Horizontal");
		bool isMoving = Mathf.Abs(input) > 0;
		bool movingRight = input > 0;

        animator.SetBool("isMoving", isMoving);
		if (isMoving)
		{
			if (!movingRight && isFacingRight)
				FlipPlayer();
			else if (movingRight && !isFacingRight)
				FlipPlayer();
		}
		
	}

    // Used for handling any physics/manipulation of rigidbody
    private void FixedUpdate()
    {
		//shrinkPlayer();

		// Handle Horizontal Movement
		ApplyInput();

		if (!jump & Input.GetKeyDown(KeyCode.Space) && isGrounded)
		{

			jump = true;
			playerRigidBody.velocity += Vector2.up * jumpVelocity;
		}
		if (jump && (!Input.GetKeyDown(KeyCode.Space)))
		{
			jump = false;
		}

		if (playerRigidBody.velocity.y <= 0)
		{
			playerRigidBody.velocity += Vector2.up * Physics2D.gravity * (fallMultipler - 1) * Time.deltaTime;
		}
		else if (playerRigidBody.velocity.y > 0 && !jump)
		{
			playerRigidBody.velocity += Vector2.up * (lowJumpMultiplier);
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
        isFacingRight = !isFacingRight;
		Vector3 scale = transform.localScale;
		scale.x *= -1;
		transform.localScale = scale;
    }


}
