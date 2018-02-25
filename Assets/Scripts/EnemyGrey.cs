using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGrey : MonoBehaviour
{
    public float speed;
    public float fireRate;
	public int score;
    public GameObject bullet1;
	public GameObject bullet2;
    public GameObject explosion;
	private float lastShot;
	private float lastTurn;
	private Vector2 turnDirection;

	void Start()
	{
		lastShot = 0;
		lastTurn = 0;
		turnDirection = Vector2.left;
	}

	void Update()
	{
		if (transform.position.y > 4.4)																				// If enemy is above a certain height
			GetComponent<Rigidbody2D>().AddForce(Vector2.down * (speed / 2) * Time.deltaTime, ForceMode2D.Force);	// Move down
		if (transform.position.y <= 4.4)																			// If enemy go cross the limit
			Strafe();																								// Strafe
		if (Time.time >= lastShot + fireRate)																		// If actual time is past last shot time + fire rate
		{
			Instantiate(bullet1, transform.position, transform.rotation);											// Shoot
			Instantiate(bullet2, transform.position, transform.rotation);
			lastShot = Time.time;																					// Update last shot time
		}
	}
	
	void Strafe()																							// Strafe function
	{
		GetComponent<Rigidbody2D>().AddForce(turnDirection * speed * Time.deltaTime, ForceMode2D.Force);	// Move left
		if (transform.position.x < 0)																		// If enemy is on the left screen
		{
			if (transform.position.x <= -7 || transform.position.x >= -2)									// If X position is too much left or right
			{
				if (Time.time >= lastTurn + 1)																// Wait 1 second after last turn before turning again
				{
					turnDirection = -turnDirection;															// Inverse horizontal direction
					lastTurn = Time.time;																	// Update last turn time
				}
			}
		}
		if (transform.position.x > 0)																		// Same if enemy is on the right
		{
			if (transform.position.x >= 7 || transform.position.x <= 2)
			{
				if (Time.time >= lastTurn + 1)
				{
					turnDirection = -turnDirection;
					lastTurn = Time.time;
				}
			}
		}
	}

	void OnTriggerEnter2D (Collider2D thing)												// On hit function
	{
		string layerName = LayerMask.LayerToName(thing.gameObject.layer);					// Get layer name
		if (layerName == "Bullet (Player)" || layerName == "Player" || layerName == "Wall")	// If layer is a player bullet or a player or a side wall
		{
			Instantiate(explosion, transform.position, transform.rotation);					// Explode
			if (SoundManager.solo)															// If game is solo
			ScoreScreen.score += score;														// Update score
			Destroy(gameObject);															// Delete enemy
		}
		if (layerName == "OutOfBoundsWall")													// If layer is the out of bound wall
			Destroy(gameObject);															// Delete enemy
	}
}
