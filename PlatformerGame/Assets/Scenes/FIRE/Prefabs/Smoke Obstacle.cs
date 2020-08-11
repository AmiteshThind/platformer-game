using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeObstacle : MonoBehaviour
{
    // Start is called before the first frame update

    int randomNum;
    ParticleSystem ps;
    public float duration = 2f;
    public float timer = 0f;

    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        GenerateRandomNum();
    }

    // Update is called once per frame
    void Update()
    {
        if(Mathf.RoundToInt(Time.time)%randomNum == 0)
        {
            ps.Play();

        }
        else if(Mathf.RoundToInt(Time.time) == 20)
        {
            ps.Stop();
        }

    }

    void GenerateRandomNum()
    {
        randomNum = Random.Range(0, 10);
    }
}
