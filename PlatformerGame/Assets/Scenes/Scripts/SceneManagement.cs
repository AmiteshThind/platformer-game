using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;
using UnityEngine.UI;


public class SceneManagement : MonoBehaviour
{
    PlayerMoveController playerMoveController;
   public CinemachineVirtualCamera cinemachine;
    public static float sceneTimer = 0f;
    public float fireElementsToCollect;
    public float timeToCompleteLevelSeconds;
    public string levelName;
    public GameObject [] objectivesMetIcons;
    public GameObject[] objectivesNotMetIcons;
    private bool ObjectivesMet; 
    public GameObject Panel;
    public Text LevelTitle,CollectibleObjective, TimeObjective;
    
 
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
        sceneTimer = 0f;
        Coin.coinCount = 0;
    }

    public void nextLevel()
    {
        Debug.Log("Before Waiting 1 seconds");
        if (ObjectivesMet)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            sceneTimer = 0f;
            Coin.coinCount = 0;
        }
        else
        {
            print("Objectives Not Met");
        }

    }

    public void restartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        sceneTimer = 0f;
        Coin.coinCount = 0;
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
        if(collision.gameObject.tag == "deadlyObstacle")
        {
            playerMoveController.playerDead = true; 

            StartCoroutine(MyMethod());
        }

        

        
    }

 private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "DeadGround")
        {
            playerMoveController.playerDead = true;
            Destroy(cinemachine);

            StartCoroutine(MyMethod());
        }

        if (collision.gameObject.tag == "reachedEnd")
        {
            Panel.SetActive(true);
            
            CollectibleObjective.text = "Collect " + fireElementsToCollect;
            TimeObjective.text = "Complete level in under " +goalTimeInMins() + " Mins";

            if(Coin.coinCount >= fireElementsToCollect)
            {
                objectivesMetIcons[0].SetActive(true);
            }
            else
            {
                objectivesNotMetIcons[0].SetActive(true);
            }


             if(sceneTimer < timeToCompleteLevelSeconds)
            {
                objectivesMetIcons[1].SetActive(true);
            }
            else
            {
                objectivesNotMetIcons[1].SetActive(true);
            }

            if (Coin.coinCount >= fireElementsToCollect || sceneTimer<timeToCompleteLevelSeconds)
            {
                //StartCoroutine(nextLevel());
                LevelTitle.text = levelName + " Passed";
                ObjectivesMet = true; 
            }
            else
            {
                // StartCoroutine(MyMethod());
                LevelTitle.text = levelName + " Failed";
                ObjectivesMet = false; 

            }
        }
    }



    string goalTimeInMins()
    {
        int minutes = Mathf.FloorToInt(timeToCompleteLevelSeconds / 60F);
        int seconds = Mathf.FloorToInt(timeToCompleteLevelSeconds - minutes * 60);
        string niceTime = string.Format("{0:0}:{1:00}", minutes, seconds);

        return niceTime; 
    }
}
