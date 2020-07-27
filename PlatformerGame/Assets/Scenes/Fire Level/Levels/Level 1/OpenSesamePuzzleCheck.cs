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
	Vector3[] rocksOriginalLocation;

    public float unlockTime = 3f;
	private bool unlocked = false;
	private bool unlocking = false;
	public bool misplacedRock;
	public bool resetPuzzle = false;

	void Start()
    {
		rocksOriginalLocation = new Vector3[rocks.Length];
       for(int i = 0; i < rocks.Length; i++)
        {
			
			rocksOriginalLocation[i] = rocks[i].transform.position;
		}
		print(rocksOriginalLocation);
		print(rocks);
    }

    // Update is called once per frame
    void Update()

    {

		//PuzzleCheck();

		if (!PuzzleCompleted() && rocksOnAllPlatforms())
		{
			resetRocks();
			//misplacedRock = false;
			foreach (var pressureSwitch in pressureSwitches)
			{
				pressureSwitch.objectOnPlatform = false;
				//pressureSwitch.wrongRock = false;
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

	 void PuzzleCheck()
    {
		 
		foreach (var pressureSwitch in pressureSwitches)
		{
			if (pressureSwitch.wrongRock)
			{
				misplacedRock  = true;
			}
		}

		if (!PuzzleCompleted() && rocksOnAllPlatforms())
		{
			resetRocks();  
			//misplacedRock = false;
			foreach (var pressureSwitch in pressureSwitches)
			{
				pressureSwitch.objectOnPlatform = false;
				pressureSwitch.wrongRock = false;
				pressureSwitch.activated = false;
			}
			
			 
		}


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
			print("NEW"+rocks[i].transform.position);
			print("ORIGNIAL"+rocksOriginalLocation[i]); 
			rocks[i].transform.position = rocksOriginalLocation[i];
        }
    }

}
