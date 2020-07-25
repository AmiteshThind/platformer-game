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
		}
		
	}
	public bool IsActivated()
	{
		return activated;
	}

 
}
