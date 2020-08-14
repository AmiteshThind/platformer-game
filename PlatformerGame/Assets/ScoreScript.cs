using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
   
    // Start is called before the first frame update
    Text score;
    SceneManagement sceneManagement;
    void Start()
    {
        sceneManagement = FindObjectOfType<SceneManagement>();
        score = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        score.text = ""+Coin.coinCount+"/"+sceneManagement.fireElementsToCollect;
       
    }
}
