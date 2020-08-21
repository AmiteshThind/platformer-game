﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlamingRock : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			GameObject player = GameObject.Find("Player");
			if (player!=null)
			{
				PlayerMoveController playerObj = player.GetComponent<PlayerMoveController>();
				playerObj.KillPlayer();
			}
		}
		Destroy(this.gameObject);
	}
}