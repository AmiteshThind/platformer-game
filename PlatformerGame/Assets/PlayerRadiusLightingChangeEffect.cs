using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRadiusLightingChangeEffect : MonoBehaviour
{
    // Start is called before the first frame update
    public UnityEngine.Experimental.Rendering.Universal.Light2D light2d;
    public float counter = 0f;
    public float duration = 300f;
    public float InitialOuterRadius;
    public float FinalOuterRadius;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        counter += Time.deltaTime;

        light2d.pointLightOuterRadius = Mathf.Lerp(InitialOuterRadius, FinalOuterRadius, counter / duration);
    }
}
