using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPink : MonoBehaviour
{
    public float speed;
    public float fireRate;
	public float turnRate;
	public int score;
    public GameObject bullet;
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
		GetComponent<Rigidbody2D>().AddForce(Vector2.down * speed * Time.deltaTime, ForceMode2D.Force);		// Move down
		GetComponent<Rigidbody2D>().AddForce(turnDirection * speed * Time.deltaTime, ForceMode2D.Force);	// Move left
		if (Time.time > lastTurn + turnRate)																// If actual time is past last turn time + turn rate
		{
			turnDirection = -turnDirection;																	// Inverse horizontal direction
			lastTurn = Time.time;																			// Update last turn time
		}
		if (Time.time > lastShot + fireRate)																// If actual time is past last shot time + fire rate
		{
			Instantiate(bullet, transform.position, transform.rotation);									// Shoot
			lastShot = Time.time;																			// Update last shot time
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
