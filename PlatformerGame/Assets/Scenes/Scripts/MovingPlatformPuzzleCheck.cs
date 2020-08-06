using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformPuzzleCheck : MonoBehaviour
{
    // Start is called before the first frame update
    public PressureSwitch pressureSwitch;
    public float DistanceX;
    public float DistanceY;
    public float speedX;
    public float speedY;
    private Vector2 startPos;
    public bool MoveX;
    public bool MoveY;
    public float time;
    

    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if (pressureSwitch.IsActivated())
        {
            time += Time.fixedDeltaTime;
            Vector2 v = startPos;
            if (MoveY)
            {
                v.y += DistanceY * Mathf.Sin(time * speedY);
            }
            if (MoveX)
            {
                v.x += DistanceX * Mathf.Sin(time * speedX);
            }
            transform.position = v;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        
            collision.collider.transform.SetParent(transform);
        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        
            collision.collider.transform.SetParent(null);
      
    }
}
