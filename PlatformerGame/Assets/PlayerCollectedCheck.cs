using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollectedCheck : MonoBehaviour
{

    public bool playerCollectedActivationKey = false; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            playerCollectedActivationKey = true;
            Destroy(GetComponent<SpriteRenderer>());
            Destroy(GetComponent<BoxCollider2D>());
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }

        }
    }


}
