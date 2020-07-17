using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OscillatePlatform : MonoBehaviour
{
    // Start is called before the first frame update

    public float Distance;
    public float speed;
    private Vector2 startPos;
    PlayerMoveController playerMoveController;

    void Start()
    {
        playerMoveController = FindObjectOfType<PlayerMoveController>();
        startPos = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!playerMoveController.playerDead)
        {
            Vector2 v = startPos;
        v.y += Distance * Mathf.Sin(Time.time * speed);
        transform.position = v;
        }
    }

     


    
}
