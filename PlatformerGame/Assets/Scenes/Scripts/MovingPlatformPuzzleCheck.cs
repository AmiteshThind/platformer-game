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


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (pressureSwitch.IsActivated())
        {
            Vector2 v = startPos;
            if (MoveY)
            {
                v.y += DistanceY * Mathf.Sin(Time.time * speedY);
            }
            if (MoveX)
            {
                v.x += DistanceX * Mathf.Sin(Time.time * speedX);
            }
            transform.position = v;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.collider.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.collider.transform.SetParent(null);
        }
    }
}
