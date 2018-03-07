using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	public bool isPink;
	public bool isRed;
	public bool isGrey;
	public bool isMissile;
    public float speed;
    public float fireRate;
	public float pinkTurnRate;
	public int score;
    public GameObject bullet;
	public GameObject greyExtraBullet;
    public GameObject explosion;
	private float lastShot;
	private float lastTurn;
	private Vector2 turnDirection;
	private AudioSource[] sound;

	void Start()
	{
		sound = GameObject.Find("SoundManager").GetComponents<AudioSource>();					// Get sounds from the sound manager
		if (isMissile)																			// If ennemy is missile
			GameObject.Find("SoundManager").GetComponent<SoundManager>().PlaySound(sound[11]);	// Play missile sound
		lastShot = 0;																			// Initialize last shot time
		lastTurn = 0;																			// Initialize last turn time
		if (transform.position.x >= -8.5 && transform.position.x < 0)							// If enemy is on left screen
			turnDirection = Vector2.left;														// Initialize turn direction to left
		else																					// If enemy is on right screen
			turnDirection = Vector2.right;														// Initialize turn direction to left
	}

	void Update()
	{
		if (isPink)			// If enemy is pink
			PinkIA();		// Use pink IA
		if (isRed)			// If enemy is red
			RedIA();		// Use red IA
		if (isGrey)			// If enemy is grey
			GreyIA();		// Use grey IA
		if (isMissile)		// If enemy is missile
			MissileIA();	// Use missile IA
	}

	void OnTriggerEnter2D (Collider2D thing)	// On hit function
	{
		KillEnemy(thing);						// Kill enemy															
	}
	
	void PinkIA()
	{
		GetComponent<Rigidbody2D>().AddForce(Vector2.down * speed * Time.deltaTime, ForceMode2D.Force);		// Move down
		GetComponent<Rigidbody2D>().AddForce(turnDirection * speed * Time.deltaTime, ForceMode2D.Force);	// Move left
		if (Time.time > lastTurn + pinkTurnRate)															// If actual time is past last turn time + turn rate
		{
			turnDirection = -turnDirection;																	// Inverse horizontal direction
			lastTurn = Time.time;																			// Update last turn time
		}
		if (transform.position.y <= 4.4)																	// If enemy reach a certain height
		{
			if (Time.time > lastShot + fireRate)															// If actual time is past last shot time + fire rate
			{
				Instantiate(bullet, transform.position, transform.rotation);								// Shoot
				lastShot = Time.time;																		// Update last shot time
			}
		}
	}

	void RedIA()
	{
		GetComponent<Rigidbody2D>().AddForce(Vector2.down * speed * Time.deltaTime, ForceMode2D.Force);	// Move down
		if (transform.position.y <= 4.4)																// If enemy reach a certain height
		{
			if (Time.time > lastShot + fireRate)														// If actual time is past last shot time + fire rate
			{
				Instantiate(bullet, transform.GetChild(0).position, transform.GetChild(0).rotation);	// Shoot
				Instantiate(bullet, transform.GetChild(1).position, transform.GetChild(1).rotation);
				Instantiate(bullet, transform.GetChild(2).position, transform.GetChild(2).rotation);
				lastShot = Time.time;																	// Update last shot time
			}
		}
	}

	void GreyIA()																									// Grey enemies IA function
	{
		if (transform.position.y > 4.4)																				// If enemy is above a certain height
			GetComponent<Rigidbody2D>().AddForce(Vector2.down * (speed / 2) * Time.deltaTime, ForceMode2D.Force);	// Move down
		if (transform.position.y <= 4.4)																			// If enemy reach a certain height
		{
			Strafe();																								// Strafe
			if (Time.time >= lastShot + fireRate)																	// If actual time is past last shot time + fire rate
			{
				Instantiate(bullet, transform.position, transform.rotation);										// Shoot
				Instantiate(greyExtraBullet, transform.position, transform.rotation);
				lastShot = Time.time;																				// Update last shot time
			}
		}
	}

	void MissileIA()																					// Missiles IA function
	{
		GetComponent<Rigidbody2D>().AddForce(transform.up * speed * Time.deltaTime, ForceMode2D.Force); // Go forward
	}

	void Strafe()																							// Strafe function
	{
		GetComponent<Rigidbody2D>().AddForce(turnDirection * speed * Time.deltaTime, ForceMode2D.Force);	// Move left
		if (transform.position.x < 0)																		// If enemy is on the left screen
		{
			if (transform.position.x <= -8 || transform.position.x >= -0.5)									// If X position is too much left or right
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
			if (transform.position.x >= 8 || transform.position.x <= 0.5)
			{
				if (Time.time >= lastTurn + 1)
				{
					turnDirection = -turnDirection;
					lastTurn = Time.time;
				}
			}
		}
	}

	void KillEnemy(Collider2D thing)																				// Kill enemy function
	{
		string layerName = LayerMask.LayerToName(thing.gameObject.layer);											// Get layer name
		if (layerName == "Bullet (Player)" || layerName == "Player" || layerName == "Wall" || layerName== "Bomb")	// If layer is a player bullet or a player or a side wall
		{
			Instantiate(explosion, transform.position, transform.rotation);											// Explode
			GiveScore();																							// Give score
			Destroy(gameObject);																					// Delete enemy
		}
		if (layerName == "OutOfBoundsWall")																			// If layer is the out of bound wall
			Destroy(gameObject);																					// Delete enemy
	}

	void GiveScore()																							// Give score function
	{
		if (GameManager.soloPlay)																				// If game is solo
			ScoreScreen.score += score;																			// Update score
		else																									// If game is versus
		{
			if (transform.position.x >= -8.5 && transform.position.x < 0)										// If enemy is on left screen
				Player.killCount1 += 1;																			// Inscrease player 1 kill count
			if (transform.position.x > 0 && transform.position.x <= 8.5)										// If enemy is on right screen
				Player.killCount2 += 1;																			// Inscrease player 2 kill count
		}
	}
}
