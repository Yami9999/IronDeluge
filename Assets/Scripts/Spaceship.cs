using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Spaceship : MonoBehaviour
{
	// Move speed.
	public float speed;
	
	// Shoot delay.
	public float shotDelay;
	
	// Bullet Prefab.
	public GameObject bullet;
	
	// Whether shoot or on't shoot.
	public bool canShot;
	
	// explosion Prefab.
	public GameObject explosion;
	
	// Make explosion.
	public void Explosion ()
	{
		Instantiate (explosion, transform.position, transform.rotation);
	}
	
	// Make bullet.
	public void Shot (Transform origin)
	{
		Instantiate (bullet, origin.position, origin.rotation);
	}
	
	// Move spaceship.
	public void Move (Vector2 direction)
	{
		GetComponent<Rigidbody2D>().velocity = direction * speed;
	}
}
