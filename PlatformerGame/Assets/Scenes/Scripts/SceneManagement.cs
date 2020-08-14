using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;


public class SceneManagement : MonoBehaviour
{
    PlayerMoveController playerMoveController;
   public CinemachineVirtualCamera cinemachine;
    public static float sceneTimer = 0f; 
 
    // Start is called before the first frame update
    void Start()
    {

        cinemachine = FindObjectOfType<CinemachineVirtualCamera>();
        playerMoveController = GetComponent<PlayerMoveController>();
        
    }

    IEnumerator MyMethod()
    {
        Debug.Log("Before Waiting 2 seconds");
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator nextLevel()
    {
        Debug.Log("Before Waiting 1 seconds");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }

    // Update is called once per frame
    void Update()
    {
        if (playerMoveController.playerDead)
        {
            StartCoroutine(MyMethod());
        }
        sceneTimer +=Time.deltaTime; 
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "DeadGround" || collision.gameObject.tag == "SlopeRock")
        {
            playerMoveController.playerDead = true; 

            StartCoroutine(MyMethod());
        }

        if(collision.gameObject.tag == "reachedEnd")
        {
            StartCoroutine(nextLevel());
        }

        
    }

 private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "DeadGround" || collision.gameObject.tag == "SlopeRock")
        {
            playerMoveController.playerDead = true;
            Destroy(cinemachine);

            StartCoroutine(MyMethod());
        }

        if (collision.gameObject.tag == "reachedEnd")
        {
            StartCoroutine(nextLevel());
        }
    }


}
