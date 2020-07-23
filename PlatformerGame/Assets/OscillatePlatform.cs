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
    private bool moving;

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
         if(collision.gameObject.tag == "Player")
        {
            moving = true;
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
