﻿using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
	public float speed;
	public bool isPlayer1;
	public int killsForMissiles;
	public int missileCount;
	public GameObject Bullet;
	public GameObject Bomb;
	public GameObject Explosion;
	public GameObject GameOverScreen;
	public GameObject VictoryScreen1;
	public GameObject VictoryScreen2;
	static public int killCount1;
	static public int killCount2;
	static public int bombCount;
	private AudioSource[] music;
	private bool isDead;
	private bool missilesReady;
	private float lastMissileSpawned;
	private float randomX;
	
	void Start()
	{
		music = GameObject.Find("SoundManager").GetComponents<AudioSource>(); 	// Get musics and sounds
		bombCount = 3;															// Set 3 bombs
		killCount1 = 0;
		killCount2 = 0;
	}
	
	void Update()
	{
		if (isPlayer1)													// If it's player 1
			InputsPlayer1();											// Use player 1's controls
		else															// If it's player 2
			InputsPlayer2();											// Use player 2's controls
		if (!GameManager.soloPlay)										// If game is not solo
		{
			if (isPlayer1)												// If player 1
			{
				if (killCount1 >= killsForMissiles && !missilesReady)	// If the player 1 kill count reach the threshold and missiles are not ready
				StartCoroutine(AllowMissiles());						// Allow missiles usage
			}
			else 														// If player 2
			{
				if (killCount2 >= killsForMissiles && !missilesReady)	// If the player 2 kill count reach the threshold and missiles are not ready
				StartCoroutine(AllowMissiles());						// Allow missiles usage
			}
		}
	}

	void OnTriggerEnter2D(Collider2D thing)														// On hit function
	{
		string layerName = LayerMask.LayerToName(thing.gameObject.layer);						// Get layer name
		if(layerName == "Bullet (Enemy)" || layerName == "Enemy")								// If layer is an enemy bullet or an enemy
		{
			isDead = true;																		// Avoid firing and bombing while waiting for the game over
			Instantiate(Explosion, transform.position, transform.rotation);						// Explode
			GetComponent<SpriteRenderer>().enabled = false;										// Hide player sprite
			GetComponent<BoxCollider2D>().enabled = false;										// Turn off player hitbox
			transform.GetChild(1).transform.GetComponent<SpriteRenderer>().enabled = false;		// Hide player aura
			transform.GetChild(2).transform.GetComponent<MeshRenderer>().enabled = false;		// Hide the text "Missiles Ready"
			GameObject.Find("SoundManager").GetComponent<SoundManager>().StopSound(music[3]);	// Stop battle musics
			GameObject.Find("SoundManager").GetComponent<SoundManager>().StopSound(music[4]);
			StartCoroutine(GameOver());															// Start the game over timer
		}
	}

	void InputsPlayer1()
	{
		if (Input.GetKey("z") || Input.GetKey("w"))																// Movements
        	GetComponent<Rigidbody2D>().AddForce(Vector2.up * speed * Time.deltaTime, ForceMode2D.Force);
        if (Input.GetKey("s"))
            GetComponent<Rigidbody2D>().AddForce(Vector2.down * speed * Time.deltaTime, ForceMode2D.Force);
        if (Input.GetKey("q") || Input.GetKey("a"))
            GetComponent<Rigidbody2D>().AddForce(Vector2.left * speed * Time.deltaTime, ForceMode2D.Force);
        if (Input.GetKey("d"))
            GetComponent<Rigidbody2D>().AddForce(Vector2.right * speed * Time.deltaTime, ForceMode2D.Force);
		if (!isDead)																							// If player is not dead
		{
			if (Input.GetKeyDown("k"))																			// If shoot button is pressed
				Shoot();																						// Shoot
			if (Input.GetKeyDown("l") && bombCount >= 1)														// If bomb button is pressed and there are bombs remaining
				ThrowBomb();																					// Throw bomb
			if ((Input.GetKeyDown("m") || Input.GetKeyDown(",")) && missilesReady)								// If missiles buttons are pressed and missiles are allowed
				LaunchMissiles(true);																			// Launch missiles while warning it's from player 1
		}
	}

	void InputsPlayer2()
	{
		if (Input.GetKey("up"))																					// Movements
        	GetComponent<Rigidbody2D>().AddForce(Vector2.up * speed * Time.deltaTime, ForceMode2D.Force);
        if (Input.GetKey("down"))
            GetComponent<Rigidbody2D>().AddForce(Vector2.down * speed * Time.deltaTime, ForceMode2D.Force);
        if (Input.GetKey("left"))
            GetComponent<Rigidbody2D>().AddForce(Vector2.left * speed * Time.deltaTime, ForceMode2D.Force);
        if (Input.GetKey("right"))
            GetComponent<Rigidbody2D>().AddForce(Vector2.right * speed * Time.deltaTime, ForceMode2D.Force);
		if (!isDead)																							// If player is not dead
		{
			if (Input.GetKeyDown("[1]"))																		// If shoot button is pressed
				Shoot();																						// Shoot
			if (Input.GetKeyDown("[2]") && bombCount >= 1)														// If bomb button is pressed and there are bombs remaining
				ThrowBomb();																					// Throw bomb
			if ((Input.GetKeyDown("[3]") || Input.GetKeyDown(",")) && missilesReady)							// If missiles buttons are pressed and missiles are allowed
				LaunchMissiles(true);																			// Launch missiles
		}
	}

	IEnumerator AllowMissiles()															// Missile ready indicators display function
	{
		missilesReady = true;															// Missiles are ready
		transform.GetChild(1).transform.GetComponent<SpriteRenderer>().enabled = true;	// Show the player aura
		transform.GetChild(2).transform.GetComponent<MeshRenderer>().enabled = true;	// Show the text "Missiles Ready"
		yield return new WaitForSeconds(2);												// Wait 2 seconds
		transform.GetChild(2).transform.GetComponent<MeshRenderer>().enabled = false;	// Hide the text "Missiles Ready"
		yield return null;																// Return
	}

	void Shoot()																			// Shoot function
	{
		GameObject.Find("SoundManager").GetComponent<SoundManager>().PlaySound(music[8]);	// Play fire sound
		Instantiate(Bullet, transform.position, transform.rotation);						// Create bullet
	}

	void ThrowBomb()																		// Bomb throw function
	{
		GameObject.Find("SoundManager").GetComponent<SoundManager>().PlaySound(music[7]);	// Play explosion sound
		if (bombCount == 3)
		GameObject.Find("BombIcon1").GetComponent<SpriteRenderer>().enabled = false;		// Remove an icon from the interface
		if (bombCount == 2)
		GameObject.Find("BombIcon2").GetComponent<SpriteRenderer>().enabled = false;
		if (bombCount == 1)
		GameObject.Find("BombIcon3").GetComponent<SpriteRenderer>().enabled = false;
		bombCount -= 1;																		// Decrease the bomb count
		Instantiate(Bomb, Bomb.transform.position, Bomb.transform.rotation);				// Create bomb
	}

	void LaunchMissiles(bool player1Launch)																					// Launch missiles function
	{
		if (player1Launch)																									// If player 1 is the launcher
			GameObject.Find("EnemySpawner(Clone)").GetComponent<EnemySpawner>().RedirectSpawnMissiles(missileCount,true);	// Ask the enemy spawner to spawn missiles on player 2
		else																												// If player 2 is the launcher
			GameObject.Find("EnemySpawner(Clone)").GetComponent<EnemySpawner>().RedirectSpawnMissiles(missileCount,false);	// Ask the enemy spawner to spawn missiles on player 1
		transform.GetChild(1).transform.GetComponent<SpriteRenderer>().enabled = false;										// Hide player aura
		transform.GetChild(2).transform.GetComponent<MeshRenderer>().enabled = false;										// Hide the text "Missiles Ready"
		if (isPlayer1)																										// If player 1
			killCount1 = 0;																									// Reinitialize kill count 1
		else																												// If player 2
			killCount2 = 0;																									// Reinitialize kill count 2
		missilesReady = false;																								// Missiles are not ready
	}

	IEnumerator GameOver()																							// Game over function
	{
		yield return new WaitForSeconds(1);																			// Wait one second
		if (GameManager.soloPlay)                                                     								// If game is solo
		{
			Instantiate(GameOverScreen, GameOverScreen.transform.position, GameOverScreen.transform.rotation);		// Create a solo game over screen
			GameObject.Find("SoundManager").GetComponent<SoundManager>().PlaySound(music[5]);						// Play player 2 victory music
		}
		else																										// If game is versus
		{
			if (isPlayer1)																							// If it's player 1
			{
				Instantiate(VictoryScreen2, VictoryScreen2.transform.position, VictoryScreen2.transform.rotation);	// Create player 2 victory screen
				GameObject.Find("SoundManager").GetComponent<SoundManager>().PlaySound(music[5]);					// Play player 2 victory music
			}
			else																									// If it's player 2
			{
				Instantiate(VictoryScreen1, VictoryScreen1.transform.position, VictoryScreen1.transform.rotation);	// Create player 1 victory screen
				GameObject.Find("SoundManager").GetComponent<SoundManager>().PlaySound(music[6]);					// Play player 1 victory music
			}
		}
		Destroy(gameObject);																						// Delete player
	}
}