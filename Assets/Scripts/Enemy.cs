using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
	// Spaceship component.
	Spaceship spaceship;
	
	IEnumerator Start ()
	{
		
		// Acquire spaceship component.
		spaceship = GetComponent<Spaceship> ();
		
		// Enemy move to the minus direction of a Y axis of a local coordinate.
		Move (transform.up * -1);
		
		// If canShot is false, coroutine finish.
		if (spaceship.canShot == false) {
			yield break;
		}
		
		while (true) {
			
			// Acquire all child elements.
			for (int i = 0; i < transform.childCount; i++) {
				
				Transform shotPosition = transform.GetChild (i);
				
				// Bullet shoot ShotPosition as a location and angle.
				spaceship.Shot (shotPosition);
			}
			
			// Wait for input shotDelay.
			yield return new WaitForSeconds (spaceship.shotDelay);
		}
	}
	
	// Move Spaceship.
	public void Move (Vector2 direction)
	{
		GetComponent<Rigidbody2D>().velocity = direction * spaceship.speed;
	}
	
	void OnTriggerEnter2D (Collider2D c)
	{
		// Acquire layer name.
		string layerName = LayerMask.LayerToName (c.gameObject.layer);
		
		// When layer name isn't Bullet (Player), do nothing.
		if (layerName != "Bullet (Player)") return;
		
		// Delete Bullet.
		Destroy(c.gameObject);
		
		// Explosion.
		spaceship.Explosion ();
		
		// Delete Enemy.
		Destroy (gameObject);
	}
}