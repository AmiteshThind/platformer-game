using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenSesamePuzzleCheck : MonoBehaviour
{
    // Start is called before the first frame update

    public PressureSwitch [] pressureSwitches;
    

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PuzzleCompleted())
        {
            print("everything is great");
        }
    }


    bool PuzzleCompleted()
    {
        bool allSwitchesActivated = true; 
        foreach (var pressureSwitch in pressureSwitches)
        {
            if(pressureSwitch.activated == false)
            {
                allSwitchesActivated = false;
            }
        }

        return allSwitchesActivated;
    }


}
