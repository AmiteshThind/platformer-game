using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    public bool DashCharged = true; 
    public float DashForceValue = 20f;
    private float DashForce; 
    public float StartDashTimer = 0.05f;
    float CurrentDashTimer;
    Rigidbody2D playerRigidBody; 
    private float moveHorizontalKeyBoardInput,moveHorizontalJoyStickInput;
    private float moveVerticalKeyBoardInput, moveVerticalJoyStickInput;
    public bool isDashing;
    public float DashRechargeTimeCounter =2f;
    public float DashRechargeTime = 2f; 
    Joystick joystick;
    Rigidbody2D rb;
    DashButton dashbutton;

    // Start is called before the first frame update
    void Start()
    {
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
        moveHorizontalJoyStickInput = joystick.Horizontal;
        moveVerticalJoyStickInput = joystick.Vertical;

        if (joystick.Horizontal > 0.2f)
        {
            moveHorizontalJoyStickInput = 1; 
        }else if(joystick.Horizontal < -0.2f)
        {
            moveHorizontalJoyStickInput = -1; 
        }
        else { moveHorizontalJoyStickInput = 0f;  }


        if (joystick.Vertical > 0.4f)
        {
            moveVerticalJoyStickInput = 1;
        }
        else if (joystick.Vertical < -0.4f)
        {
            moveVerticalJoyStickInput = -1;
        }
        else
        {
            moveVerticalJoyStickInput = 0f; 
        }

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


        if ((Input.GetKeyDown(KeyCode.LeftShift) || dashbutton.Pressed) && DashCharged)
        {
            isDashing = true;
            DashCharged = false;
            CurrentDashTimer = StartDashTimer;
            rb.velocity = Vector2.zero;
             
            //DashDirection = (int)moveHorizontalJoyStickInput; // Change To joystick if using joystick

        }

        if (isDashing)
        { 
            
            Vector2 dir = new Vector2(joystick.Horizontal, joystick.Vertical);
            print("dir"+dir.normalized);


            if (Mathf.Abs(joystick.Vertical) > 0.5f && Mathf.Abs(joystick.Horizontal)<0.2f)
            {
                DashForce = DashForceValue-4f;
            }
            else {
                DashForce = DashForceValue;
            }

            rb.velocity = dir.normalized*DashForce;
            CurrentDashTimer -= Time.deltaTime;
            print("horizonal "+moveHorizontalJoyStickInput);
            print("vertical"+ moveVerticalJoyStickInput);
        }
         

        if (CurrentDashTimer <= 0)
        {
            isDashing = false;
           
        }

    }

   }
 