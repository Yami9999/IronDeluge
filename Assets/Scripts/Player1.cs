using UnityEngine;
using System.Collections;

public class Player1 : MonoBehaviour
{
	// Spaceship component.
	Spaceship spaceship;
	Rigidbody2D rb;
	
	IEnumerator Start ()
	{
		// Acquire Spaceship component.
		spaceship = GetComponent<Spaceship>();
		rb = GetComponent<Rigidbody2D>();

		while (true) {
			
			// Make bullet at the same Player as a location and angle.
			spaceship.Shot(transform);
			
			// Sound shot SE.
			GetComponent<AudioSource>().Play();
			
			// Wait for input shotDelay second.
			yield return new WaitForSeconds (spaceship.shotDelay);
		}
	}
	
	void FixedUpdate ()
	{
		if (Input.GetKey("z"))
           rb.AddForce(Vector2.up * spaceship.speed * Time.deltaTime, ForceMode2D.Force);
        if (Input.GetKey("s"))
            rb.AddForce(Vector2.down * spaceship.speed * Time.deltaTime, ForceMode2D.Force);
        if (Input.GetKey("q"))
            rb.AddForce(Vector2.left * spaceship.speed * Time.deltaTime, ForceMode2D.Force);
        if (Input.GetKey("d"))
            rb.AddForce(Vector2.right * spaceship.speed * Time.deltaTime, ForceMode2D.Force);
	}
	
	// Invoke when hit in a moment.
	void OnTriggerEnter2D (Collider2D c)
	{
		// Acquire layer name.
		string layerName = LayerMask.LayerToName(c.gameObject.layer);
		
		// If layer name is "Bullet (Enemy)", delete bullet.
		if( layerName == "Bullet (Enemy)")
		{
			// Delete bullet.
			Destroy(c.gameObject);
		}
		
		// If layer name is "Bullet (Enemy)" or "Enemy", explode Player.
		if( layerName == "Bullet (Enemy)" || layerName == "Enemy")
		{
			// Reference Manager component in Scene and acquire, invoke GameOver method.
			FindObjectOfType<Manager>().GameOver();
			
			// Explosion.
			spaceship.Explosion();
			
			// Delete Player.
			Destroy (gameObject);
		}

		if( layerName == "Wall")
		{
			GetComponent<Rigidbody>().velocity = Vector3.zero;
		}
	}
}