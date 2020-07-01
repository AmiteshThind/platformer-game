using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    public bool DashCharged = true; 
    public float DashForceX = 50f;
    public float DashForceY = 25f; 
    public float StartDashTimer = 0.15f;
    float CurrentDashTimer;
    float DashDirection; 
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

    }

    // Update is called once per frame
    void Update()
    {

        moveHorizontalKeyBoardInput = Input.GetAxis("Horizontal");
        moveVerticalKeyBoardInput = Input.GetAxis("Vertical");
        moveHorizontalJoyStickInput = joystick.Horizontal;
        moveVerticalJoyStickInput = joystick.Vertical;
       

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
            rb.velocity = new Vector2(DashForceX*moveHorizontalJoyStickInput,DashForceY*moveVerticalJoyStickInput);
            CurrentDashTimer -= Time.deltaTime;
        }

        if (CurrentDashTimer <= 0)
        {
            isDashing = false;
           
        }

    }

   }
 