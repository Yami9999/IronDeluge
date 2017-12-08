using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
	// Spaceship component.
	Spaceship spaceship;
	
	IEnumerator Start ()
	{
		// Acquire Spaceship component.
		spaceship = GetComponent<Spaceship> ();
		
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
		// Move right and left.
		float x = Input.GetAxisRaw ("Horizontal");
		
		// Move right and left.
		float y = Input.GetAxisRaw ("Vertical");
		
		// Seek moving angle.
		Vector2 direction = new Vector2 (x, y).normalized;
		
		// Restrict moving.
		Move (direction);
		
	}
	
	// Move Player.
	void Move (Vector2 direction)
	{
		// Acquire world coordinate in the bottom left of the screen from Viewport.
		Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
		
		// Acquire world coordinate in the bottom left of the screen from Viewport.
		Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
		
		// Acquire Player coordinate.
		Vector2 pos = transform.position;
		
		// Add amount of movement.
		pos += direction  * spaceship.speed * Time.deltaTime;
		
		// Restrict so that the location of the player fit in the screen.
		pos.x = Mathf.Clamp (pos.x, min.x, max.x);
		pos.y = Mathf.Clamp (pos.y, min.y, max.y);
		
		// The numerical value with the restriction make the location of the player.
		transform.position = pos;
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