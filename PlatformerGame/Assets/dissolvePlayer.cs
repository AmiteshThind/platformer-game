using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dissolvePlayer : MonoBehaviour
{
    // Start is called before the first frame update
    public bool dissolvingPlayer;
    public Dissolve[] dissolves;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (dissolvingPlayer)
        {
            foreach (var dissolve in dissolves)
            {
                dissolve.InitiateDissolve();
            }
        }
    }
}
