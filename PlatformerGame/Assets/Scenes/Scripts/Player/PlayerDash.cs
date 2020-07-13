using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    public bool DashCharged = true; 
    public float DashForceValueX = 26f;
    public float DashForceValueY = 21f;
    private Vector2 DashForce; 
    public float GroundDashDuration = 0.05f;
    float CurrentDashTimer;
    float CurrentAirDashTimer;
    public float AirDashDuration = 0.05f;
    public ParticleSystem DashEffect;

    public bool AirDashUsed = false; 
    Rigidbody2D playerRigidBody; 
    private float moveHorizontalKeyBoardInput,moveHorizontalJoyStickInput;
    private float moveVerticalKeyBoardInput, moveVerticalJoyStickInput;
    public bool isDashing;
    public bool isDashingInAir;
    public float DashRechargeTimeCounter =2f;
    public float DashRechargeTime = 2f; 
    Joystick joystick;
    Rigidbody2D rb;
    DashButton dashbutton;
    PlayerMoveController playerMoveController;

    // Start is called before the first frame update
    void Start()
    {
        playerMoveController = GetComponent<PlayerMoveController>();
        rb = GetComponent<Rigidbody2D>();
        joystick = FindObjectOfType<Joystick>();
        dashbutton = FindObjectOfType<DashButton>();
        playerRigidBody = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {

        moveHorizontalKeyBoardInput = Input.GetAxis("Horizontal");
        moveVerticalKeyBoardInput = Input.GetAxis("Vertical");
        
 

        if (playerMoveController.isGrounded && joystick.Horizontal>0.9f)
        {
            moveHorizontalJoyStickInput = 1;
            moveVerticalJoyStickInput = 0; 
        }
        else if(playerMoveController.isGrounded && joystick.Horizontal < -0.9f)
        {
            moveHorizontalJoyStickInput = -1;
            moveVerticalJoyStickInput = 0;
        }
        else if (!playerMoveController.isGrounded)
        {
            moveHorizontalJoyStickInput = joystick.Horizontal;
            moveVerticalJoyStickInput = joystick.Vertical;
        }
        else
        {
            moveHorizontalJoyStickInput = 0;
            moveVerticalJoyStickInput = 0; 
        }


        //if (joystick.Horizontal > 0.02f)
        //{
        //    moveHorizontalJoyStickInput = 1; 
        //}else if(joystick.Horizontal < -0.02f)
        //{
        //    moveHorizontalJoyStickInput = -1; 
        //}
        //else { moveHorizontalJoyStickInput = 0f;  }


        //if (joystick.Vertical > 0.4f)
        //{
        //    moveVerticalJoyStickInput = 1;
        //}
        //else if (joystick.Vertical < -0.4f)
        //{
        //    moveVerticalJoyStickInput = -1;
        //}
        //else
        //{
        //    moveVerticalJoyStickInput = 0f; 
        //}



        //if ((moveHorizontalKeyBoardInput >0 || moveHorizontalJoyStickInput > 0))
        //{
        //    DashDirection = 1;
        //}
        //else if ((moveHorizontalKeyBoardInput < 0 || moveHorizontalJoyStickInput < 0))
        //{
        //    DashDirection = -1;
        //}
        //else
        //{
        //    DashDirection = 0; 
        //}


    }

    void FixedUpdate()
    {

        if (!DashCharged)
        {
            DashRechargeTimeCounter -= Time.deltaTime;
            if (DashRechargeTimeCounter <= 0f)
            {
                DashRechargeTimeCounter = DashRechargeTime;
                DashCharged = true;
            }
        }


        if ((Input.GetKeyDown(KeyCode.LeftShift) || dashbutton.Pressed))
        {
            print(playerMoveController.isGrounded);
            if (DashCharged && playerMoveController.isGrounded)
            {
                isDashing = true;
                DashCharged = false;
                CurrentDashTimer = GroundDashDuration;
            }

            else if (!playerMoveController.isGrounded)
            {
                if (!AirDashUsed)
                {
                    isDashingInAir = true;
                    CurrentAirDashTimer = AirDashDuration;
                }
            }


        }




        if (isDashing)
        {
            if (playerMoveController.isGrounded && moveHorizontalJoyStickInput != 0)
            {
                CreateDashTrail();
                rb.velocity = new Vector2(DashForceValueX * moveHorizontalJoyStickInput, 0);
                //  print("worked");
            }



            //  print("horizonal " + moveHorizontalJoyStickInput);
            //  print("vertical" + moveVerticalJoyStickInput);
            CurrentDashTimer -= Time.deltaTime;
        }




        if (isDashingInAir)
        {
            print("DASHINAIRDAHSINRIAR");
            AirDashUsed = true;
            CreateDashTrail();
            Vector2 dir = new Vector2(joystick.Horizontal, joystick.Vertical);
            //  print("dir" + dir.normalized);
            DashForce = new Vector2(DashForceValueX, DashForceValueY);
            print("dir" + dir);
            print(dir.normalized);
            if (dir.normalized == dir)
            {
                rb.velocity = dir.normalized * DashForce;
            }
            CurrentAirDashTimer -= Time.deltaTime;
        }


        if (CurrentDashTimer <= 0)
        {
            isDashing = false;

        }

        if (CurrentAirDashTimer <= 0)
        {
            isDashingInAir = false;
        }

        if (playerMoveController.isGrounded)
        {
            AirDashUsed = false; 
        }


    }


    void CreateDashTrail()
    {
        DashEffect.Play();
    }
   }
 