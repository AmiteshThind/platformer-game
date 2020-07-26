using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenSesamePuzzleCheck : MonoBehaviour
{
    // Start is called before the first frame update

    public PressureSwitch [] pressureSwitches;
	public Dissolve[] dissolves; // TODO: Change class name if more behaviour is required from the rocks, 
	public GameObject unlockPivot;
	public GameObject door;
    public float unlockTime = 3f;
	private bool unlocked = false;
	private bool unlocking = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PuzzleCompleted() && !unlocked && !unlocking)
        {
			Unlock();
        }
    }

	private void Unlock()
	{
		// test for dissolve
		foreach(var dissolve in dissolves)
		{
			dissolve.InitiateDissolve();
		}
		unlocking = true;
	}

	private void FixedUpdate()
	{
		if (unlocking)
		{
			OpenDoor();
		}	
	}

	private void OpenDoor()
	{
		door.transform.RotateAround(unlockPivot.transform.position, Vector3.back, 20 * Time.deltaTime);
		if (door.transform.rotation.z <= 0) // stop when platform is flat
		{
			unlocked = true;
			unlocking = false;
		}
	}

	bool PuzzleCompleted()
    {
        bool allSwitchesActivated = true; 
        foreach (var pressureSwitch in pressureSwitches)
        {
            allSwitchesActivated &= pressureSwitch.activated;
        }

        return allSwitchesActivated;
    }


}
