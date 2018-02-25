using UnityEngine;
using System.Collections;

public class EnemyRed : MonoBehaviour
{
    public float speed;
    public float fireRate;
    public GameObject bullet;
    public GameObject explosion;
	private float lastShot;

	void Start()
	{
		lastShot = 0;
	}

	void Update()
	{
		GetComponent<Rigidbody2D>().AddForce(Vector2.down * speed * Time.deltaTime, ForceMode2D.Force);	// Move down
		if(Time.time > lastShot + fireRate)																// If actual time is past last shot time + fire rate
		{
			Instantiate(bullet, transform.GetChild(0).position, transform.GetChild(0).rotation);		// Shoot
			Instantiate(bullet, transform.GetChild(1).position, transform.GetChild(1).rotation);
			Instantiate(bullet, transform.GetChild(2).position, transform.GetChild(2).rotation);
			lastShot = Time.time;																		// Update last shot time
		}
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