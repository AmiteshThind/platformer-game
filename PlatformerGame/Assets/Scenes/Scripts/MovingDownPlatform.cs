﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingDownPlatform : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isActivated;
    public float rateOfDescend = 10f;
    public checkPlatformActivated checkPlatformActivated;
   
    Rigidbody2D rb;
    void Start()
    {
         
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }
    void FixedUpdate()

    {
        if (checkPlatformActivated.MovingPlatformDownActivated)
        {
            //rb.gravityScale = rateOfDescend/100f;
            rb.velocity = new Vector2(0, -rateOfDescend);
        }
        else
        {
            rb.velocity = new Vector2(0, 0);
        }




    }
   

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Player")
        {
            checkPlatformActivated.MovingPlatformDownActivated = false; 
        }
    }
}
