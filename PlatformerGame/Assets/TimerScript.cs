using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{
    // Start is called before the first frame update
    Text time;
    string timerMin,timerSec; 

    void Start()
    {
        time = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        int minutes = Mathf.FloorToInt(SceneManagement.sceneTimer / 60F);
        int seconds = Mathf.FloorToInt(SceneManagement.sceneTimer - minutes * 60);
        string niceTime = string.Format("{0:0}:{1:00}", minutes, seconds);




        time.text = "Time: "+niceTime;
    }
}
