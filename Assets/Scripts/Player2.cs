using UnityEngine;
using System.Collections;

public class Player2 : MonoBehaviour
{
	// Spaceship component.
	Spaceship spaceship;
	
	IEnumerator Start ()
	{
		// Acquire Spaceship component.
		spaceship = GetComponent<Spaceship> ();
		
		while (true)
        {
			
			// Make bullet at the same Player as a location and angle.
			spaceship.Shot (transform);
			
			// Sound shot SE.
			GetComponent<AudioSource>().Play();
			
			// Wait for input shotDelay second.
			yield return new WaitForSeconds (spaceship.shotDelay);
		}
	}
	
	void Update ()
	{
		// Movements
		if (Input.GetKey("up"))
        {
            transform.Translate(Vector3.up * spaceship.speed * Time.deltaTime);
        }
        if (Input.GetKey("down"))
        {
            transform.Translate(Vector3.down * spaceship.speed * Time.deltaTime);
        }
        if (Input.GetKey("left"))
        {
            transform.Translate(Vector3.left * spaceship.speed * Time.deltaTime);
        }
        if (Input.GetKey("right"))
        {
            transform.Translate(Vector3.right * spaceship.speed * Time.deltaTime);
        }
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
	}
}