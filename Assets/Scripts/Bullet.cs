using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	// Bullet move speed.
	public float speed;
	// Time from GameObject generate to delete.
	public float lifeTime;
	
	void Start ()
	{
		// GameObject delete after lifeTime second.
		Destroy(gameObject, lifeTime);
	}

	void Update()
	{
		// Enemy move to the minus direction of a Y axis of a local coordinate.
		GetComponent<Rigidbody2D>().AddForce(transform.up * speed * Time.deltaTime, ForceMode2D.Force);
	}

	// On hit
	void OnTriggerEnter2D(Collider2D thing)
	{
		// Get layer name
		string layerName = LayerMask.LayerToName(thing.gameObject.layer);
		// If layer is a player or a wall, destroy bullet
		if(layerName == "Player" || layerName == "Wall")
			Destroy(gameObject);
	}
}
