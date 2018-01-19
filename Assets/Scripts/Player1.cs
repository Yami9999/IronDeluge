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
		
		while (true) {
			
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
		if (Input.GetKey("z"))
        {
            transform.Translate(Vector3.up * spaceship.speed * Time.deltaTime);
        }
        if (Input.GetKey("s"))
        {
            transform.Translate(Vector3.down * spaceship.speed * Time.deltaTime);
        }
        if (Input.GetKey("q"))
        {
            transform.Translate(Vector3.left * spaceship.speed * Time.deltaTime);
        }
        if (Input.GetKey("d"))
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

		if( layerName == "Wall")
		{
			GetComponent<Rigidbody>().velocity = Vector3.zero;
		}
	}
}