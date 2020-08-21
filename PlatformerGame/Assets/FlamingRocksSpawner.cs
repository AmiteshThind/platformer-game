using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlamingRocksSpawner : MonoBehaviour
{
	public Rigidbody2D playerRb;
	public GameObject flamingRockPrefab;
	public float spawnInterval = 5f;
	private float interval = 0;
	// Start is called before the first frame update
    void Start()
    {
		spawnRocks();
    }

    // Update is called once per frame
    void Update()
    {
        if(interval >= spawnInterval)
		{
			interval = 0;
			spawnRocks();
		}
		else
		{
			interval += Time.deltaTime;
		}
    }

	void spawnRocks()
	{
		Instantiate(flamingRockPrefab, new Vector3(playerRb.position.x - Random.Range(30, -30), playerRb.position.y + Random.Range(150, 175), 0), Quaternion.identity);
		Instantiate(flamingRockPrefab, new Vector3(playerRb.position.x - Random.Range(-30, 30), playerRb.position.y + Random.Range(150, 175), 0), Quaternion.identity);
	 
	}
}
