using UnityEngine;
using System.Collections;

public class Player1 : MonoBehaviour
{
	public float speed;
	public GameObject Bullet;
	public GameObject Explosion;
	public GameObject GameOverScreenSolo;
	public GameObject GameOverScreenVersus;
	private AudioSource[] music;
	
	void Start()
	{
		music = GameObject.Find("SoundManager").GetComponents<AudioSource>(); 	// Get musics and sounds
	}
	
	void Update()
	{
		if (Input.GetKey("z"))																					// Movements
        	GetComponent<Rigidbody2D>().AddForce(Vector2.up * speed * Time.deltaTime, ForceMode2D.Force);
        if (Input.GetKey("s"))
            GetComponent<Rigidbody2D>().AddForce(Vector2.down * speed * Time.deltaTime, ForceMode2D.Force);
        if (Input.GetKey("q"))
            GetComponent<Rigidbody2D>().AddForce(Vector2.left * speed * Time.deltaTime, ForceMode2D.Force);
        if (Input.GetKey("d"))
            GetComponent<Rigidbody2D>().AddForce(Vector2.right * speed * Time.deltaTime, ForceMode2D.Force);
		if (Input.GetKeyDown("k"))																				// If shoot button is pressed
		{
			Instantiate(Bullet, transform.position, transform.rotation);										// Create bullet
			GameObject.Find("SoundManager").GetComponent<SoundManager>().PlaySound(music[8]);					// Play fire sound
		}
	}

	void OnTriggerEnter2D(Collider2D thing)														// On hit function
	{
		string layerName = LayerMask.LayerToName(thing.gameObject.layer);						// Get layer name
		if(layerName == "Bullet (Enemy)" || layerName == "Enemy")								// If layer is an enemy bullet or an enemy
		{
			Instantiate(Explosion, transform.position, transform.rotation);						// Explode
			GetComponent<SpriteRenderer>().enabled = false;										// Hide player sprite
			GameObject.Find("SoundManager").GetComponent<SoundManager>().StopSound(music[3]);	// Stop battle musics
			GameObject.Find("SoundManager").GetComponent<SoundManager>().StopSound(music[4]);
			StartCoroutine(GameOver());															// Start the game over timer
		}
	}

	IEnumerator GameOver()																											// Game over function
	{
		yield return new WaitForSeconds(1);																							// Wait one second
		if (SoundManager.solo)                                                     													// If game is solo
		{
			Instantiate(GameOverScreenSolo, GameOverScreenSolo.transform.position, GameOverScreenSolo.transform.rotation);			// Create a solo game over screen
			GameObject.Find("SoundManager").GetComponent<SoundManager>().PlaySound(music[6]);										// Play player 2 game over music
		}	
		else																														// If game is versus
		{
			Instantiate(GameOverScreenVersus, GameOverScreenVersus.transform.position, GameOverScreenVersus.transform.rotation);	// Create a game over screen
			GameObject.Find("SoundManager").GetComponent<SoundManager>().PlaySound(music[5]);										// Play player 1 game over music
		}
		Destroy(gameObject);																										// Delete player
	}
}