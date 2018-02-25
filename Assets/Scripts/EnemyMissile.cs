﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMissile : MonoBehaviour
{
    public float speed;
    public GameObject explosion;

	void Start()
	{}

	void Update()
	{
		GetComponent<Rigidbody2D>().AddForce(transform.up * speed * Time.deltaTime, ForceMode2D.Force); // Go forward
	}
	
	void OnTriggerEnter2D (Collider2D thing)								// On hit function
	{
		string layerName = LayerMask.LayerToName(thing.gameObject.layer);	// Get layer name
		if (layerName == "Bullet (Player)" || layerName == "Player")		// If layer is a player bullet or a player
		{
			Instantiate(explosion, transform.position, transform.rotation);	// Explode
			Destroy(gameObject);											// Delete enemy
		}
		if (layerName == "OutOfBoundsWall")									// If layer is the out of bound wall
			Destroy(gameObject);											// Delete enemy
	}
}