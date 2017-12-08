using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	// Bullet move speed.
	public int speed = 10;
	
	// Time from GameObject generate to delete.
	public float lifeTime = 5;
	
	void Start ()
	{
		// Enemy move to the minus direction of a Y axis of a local coordinate.
		GetComponent<Rigidbody2D>().velocity = transform.up.normalized * speed;
		
		// GameObject delete after lifeTime second.
		Destroy(gameObject, lifeTime);
	}
}
