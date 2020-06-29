using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    public bool DashCharged = true; 
    public float DashForce = 100f;
    public float StartDashTimer = 0.15f;
    float CurrentDashTimer;
    float DashDirection; 
    private float moveKeyBoardInput,moveJoyStickInput;
    public bool isDashing;
    public float DashRechargeTime =5f; 
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
         
        moveKeyBoardInput = Input.GetAxis("Horizontal");
        moveJoyStickInput = joystick.Horizontal;

        if ((moveKeyBoardInput >0 || moveJoyStickInput > 0))
        {
            DashDirection = 1;
        }
        else if ((moveKeyBoardInput < 0 || moveJoyStickInput < 0))
        {
            DashDirection = -1;
        }
        else
        {
            DashDirection = 0; 
        }
      
     
    }

    void FixedUpdate()
    {

        if (!DashCharged)
        {
            DashRechargeTime -= Time.deltaTime;
            if (DashRechargeTime <= 0f)
            {
                DashRechargeTime = 5f;
                DashCharged = true;
            }
        }


        if ((Input.GetKeyDown(KeyCode.LeftShift) || dashbutton.Pressed) && DashCharged)
        {
            isDashing = true;
            DashCharged = false;
            CurrentDashTimer = StartDashTimer;
            rb.velocity = Vector2.zero;
            DashDirection = (int)moveKeyBoardInput; // Change To joystick if using joystick 
        }

        if (isDashing)
        {
        
            rb.velocity = new Vector2(DashForce*DashDirection,0);
            CurrentDashTimer -= Time.deltaTime;
        }

        if (CurrentDashTimer <= 0)
        {
            isDashing = false;
           
        }

    }

   }
 