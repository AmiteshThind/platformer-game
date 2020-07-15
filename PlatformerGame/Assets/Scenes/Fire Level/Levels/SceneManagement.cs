using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneManagement : MonoBehaviour
{
    PlayerMoveController playerMoveController;
 
    // Start is called before the first frame update
    void Start()
    {
        playerMoveController = GetComponent<PlayerMoveController>();
    }

    IEnumerator MyMethod()
    {
        Debug.Log("Before Waiting 2 seconds");
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "DeadGround")
        {
            playerMoveController.playerDead = true; 

            StartCoroutine(MyMethod());
        }
    }


}
