using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyLightingChangeEffect : MonoBehaviour
{
    // Start is called before the first frame update
    public UnityEngine.Experimental.Rendering.Universal.Light2D light2d;
    public float counter =0f;
    public float duration = 300f;
    public float InitialIntensity;
    public float FinalIntensity; 

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        counter += Time.deltaTime;

        light2d.intensity = Mathf.Lerp(InitialIntensity,FinalIntensity, counter / duration);
    }
}
