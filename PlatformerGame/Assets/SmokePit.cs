using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokePit : MonoBehaviour
{
    public int minRandomIntervalSeconds;
    public int maxRandomIntervalSeconds; 
    int randomNum;
    ParticleSystem ps;

    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        ps.Stop();
       
        GenerateRandomNum();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if ((Mathf.RoundToInt(Time.time) % randomNum )== 0)
        {
            ps.Play();

        }
       

    }

    void GenerateRandomNum()
    {
        randomNum = Random.Range(minRandomIntervalSeconds, minRandomIntervalSeconds);
    }

 


}
