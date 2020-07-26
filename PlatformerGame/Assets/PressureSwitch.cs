using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PressureSwitch : MonoBehaviour
{
	Animator animator;
	 
	private bool pressed;
	private int pressCount;
	public bool activated = false;
	public string activateTag;
	public bool wrongRock;
	public bool objectOnPlatform = false;

	// Start is called before the first frame update
	void Start()
    {
		animator = GetComponent<Animator>();
	}

    // Update is called once per frame
    void Update()
    {
		 
    }

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag != "Ground")
		{
			if (other.gameObject.tag == activateTag)
				activated = true;
				pressCount++;
			if (!pressed)
			{
				animator.SetBool("Press", true);
			}
			if(other.gameObject.tag!=activateTag && other.gameObject.tag != "Player"){
				wrongRock = true; 
            }
			if(other.gameObject.tag!="Ground" && other.gameObject.tag != "Player"){
			 objectOnPlatform = true;
}
		}
		
	}

	
	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.tag != "Ground")
		{
			if (other.gameObject.tag == activateTag)
				activated = false;
			if (pressCount == 1)
			{
				animator.SetBool("Press", false);
			}
			pressCount--;

			//wrongRock = false;
		objectOnPlatform = false;
		}
		 

	}
	public bool IsActivated()
	{
		return activated;
	}

 

 
}
