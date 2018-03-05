using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	public float speed;
	public float lifeTime;
	
	void Start ()
	{
		Destroy(gameObject, lifeTime);	// Delete bullet after some seconds
	}

	void Update()
	{
		GetComponent<Rigidbody2D>().AddForce(transform.up * speed * Time.deltaTime, ForceMode2D.Force); // Move forward
	}

	void OnTriggerEnter2D(Collider2D thing)																		// On hit function
	{
		string layerName = LayerMask.LayerToName(thing.gameObject.layer);										// Get layer name
		if(layerName == "Player" || layerName == "Wall" || layerName == "Bomb" || layerName == "BulletWall")	// If layer is a player or a wall or a bomb or the bullet wall
			Destroy(gameObject);																				// Delete bullet
	}
}
