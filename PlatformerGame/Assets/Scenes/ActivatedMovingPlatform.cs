using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatedMovingPlatform : MonoBehaviour
{
    // Start is called before the first frame update

    public PlayerCollectedCheck playerCollectedCheck;


    public float DistanceX;
    public float DistanceY;
    public float speedX;
    public float newDistanceX; 
    public float speedY;
    private Vector2 startPos;
    public bool MoveX;
    public bool MoveY;
    public Vector2 pos1;
    public Vector2 pos2;
    public UnityEngine.Experimental.Rendering.Universal.Light2D light2d;
    private bool movePlatform = false;
    public float timerlocal = 0f; 
    void Start()
    {
      
        startPos = transform.position;
        pos1 = transform.position;
        pos2.x = transform.position.x + DistanceX;
        pos2.y = transform.position.y + DistanceY;
    }

    // Update is called once per frame
    void FixedUpdate()
    { 
        if (playerCollectedCheck.playerCollectedActivationKey)
        {
 
            light2d.intensity = 1.2f;
            if (movePlatform)
            {

                timerlocal += Time.fixedDeltaTime;
                if (MoveY)
                {
                    transform.position = Vector2.Lerp(pos1, pos2, Mathf.PingPong(timerlocal * speedY, 1.0f));
                }
                if (MoveX)
                {
                    transform.position = Vector2.Lerp(pos1, pos2, Mathf.PingPong(timerlocal * speedX, 1.0f));
                }
            }
          
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
             collision.collider.transform.SetParent(transform);
            

            movePlatform = true; 
        }
    }



    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.collider.transform.SetParent(null);
            movePlatform = false; 
        }
    }


}
