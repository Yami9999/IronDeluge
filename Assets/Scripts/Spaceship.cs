using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Spaceship : MonoBehaviour
{
    // Move speed.
    public float speed;

    // Delay shoot bullet.
    public float shotDelay;

    // Bullet Prefab.
    public GameObject bullet;

    // Whether shoot or don't shoot.
    public bool canShot;

    // Explosion Prefab.
    public GameObject explosion;

    // Make Explosion.
    public void Explosion ()
    {
        Instantiate (explosion, transform.position, transform.rotation);
    }

    // Make Bullet.
    public void Shot (Transform origin)
    {
        Instantiate (bullet, origin.position, origin.rotation);
    }
}
