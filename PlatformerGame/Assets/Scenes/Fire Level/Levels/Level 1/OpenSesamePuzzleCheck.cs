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
	public GameObject[] rocks;
	Vector3[] rocksOriginalPosition;
	Quaternion [] rocksOriginalRotation;
	public float unlockTime = 3f;
	private bool unlocked = false;
	private bool unlocking = false;
	public bool resetPuzzle = false;

	void Start()
    {
		rocksOriginalPosition = new Vector3[rocks.Length];
		rocksOriginalRotation = new Quaternion[rocks.Length];

	   for (int i = 0; i < rocks.Length; i++)
        {
			rocksOriginalPosition[i] = rocks[i].transform.position;
			rocksOriginalRotation[i] = rocks[i].transform.rotation;
		}
 
    }

    // Update is called once per frame
    void Update()

    {

		if (!PuzzleCompleted() && rocksOnAllPlatforms())
		{
			resetRocks();

			foreach (var pressureSwitch in pressureSwitches)
			{
				pressureSwitch.objectOnPlatform = false;
				pressureSwitch.activated = false;
			}

		}


		else if (PuzzleCompleted() && !unlocked && !unlocking )
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

	public bool PuzzleCompleted()
    {
		bool allSwitchesActivated = true;

					 
			foreach (var pressureSwitch in pressureSwitches)
			{
				allSwitchesActivated &= pressureSwitch.activated;
			}  
 

        return allSwitchesActivated;
    }


	public  bool rocksOnAllPlatforms()

	{
		bool rocksOnAllPlatforms = true;
		foreach(var pressureSwitch in pressureSwitches)
        {
			rocksOnAllPlatforms &= pressureSwitch.objectOnPlatform;
        }
		return rocksOnAllPlatforms;
    }


	void resetRocks()
    {
		for(int i =0; i < rocks.Length; i++)
        {
			rocks[i].transform.position = rocksOriginalPosition[i];
			rocks[i].transform.rotation = rocksOriginalRotation[i];
        }
    }

}
