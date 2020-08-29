using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballSpawner : MonoBehaviour
{
	public Rigidbody2D playerRb;
	public Rigidbody2D fireballPrefab;
	public GameObject spawnLocation;
	public float spawnInterval = 5f;
	public int spawnAmount = 4;
	private float interval = 0;
	public float gravityScale = 0;
	public float force = 1000;

	public float minAngle = 140;
	public float maxAngle = 230;

	// Start is called before the first frame update
	void Start()
	{
		SpawnFireballs();
	}

	// Update is called once per frame
	void Update()
	{
		if (interval >= spawnInterval)
		{
			interval = 0;
			SpawnFireballs();
		}
		else
		{
			interval += Time.deltaTime;
		}
	}

	void SpawnFireballs()
	{
		for (int i = 0; i < spawnAmount; i++)
		{
			Rigidbody2D f1 = Instantiate(fireballPrefab, new Vector3(spawnLocation.transform.position.x, spawnLocation.transform.position.y-(i*3), 0), Quaternion.identity);
			float slice = (maxAngle - minAngle) / spawnAmount;
			float minA = minAngle + (i*slice);
			float maxA = minA + slice;
			ApplyOptions(f1, minA, maxA);
		}
	}

	private void ApplyOptions(Rigidbody2D f1, float minA, float maxA)
	{
		TweakGravityScale(f1);
		ApplyForce(f1, minA, maxA);
	}

	private void ApplyForce(Rigidbody2D f1, float minA, float maxA)
	{
		float randomJitter = Random.Range(.1f * force, -.1f * force); // 10% random jitter
		float randomAngle = Random.Range(minA, maxA);
		GiveRotation(f1, randomAngle);
		f1.AddForce(new Vector2((force + randomJitter) * Mathf.Cos(Mathf.Deg2Rad * randomAngle), (force+randomJitter) * Mathf.Sin(Mathf.Deg2Rad * randomAngle)));
	}

	private void TweakGravityScale(Rigidbody2D f1)
	{
		f1.gravityScale = this.gravityScale;
	}

	private void GiveRotation(Rigidbody2D f1, float rotation)
	{
		
		f1.transform.Rotate(new Vector3(0, 0, rotation));
	}
}
