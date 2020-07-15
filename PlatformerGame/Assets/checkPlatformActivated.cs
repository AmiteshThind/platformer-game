using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkPlatformActivated : MonoBehaviour
{
    // Start is called before the first frame update
    MovingDownPlatform movingDownPlatform;
    void Start()
    {
        movingDownPlatform = FindObjectOfType<MovingDownPlatform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            movingDownPlatform.isActivated = true; 
        }
    }
}
