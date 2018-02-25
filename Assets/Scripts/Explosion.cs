using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
	private AudioSource[] music;

    void Start()
    {
        music = GameObject.Find("SoundManager").GetComponents<AudioSource>(); 	            // Get sounds
        GameObject.Find("SoundManager").GetComponent<SoundManager>().PlaySound(music[7]);	// Play explosion sound
    }

    void OnAnimationFinish()    // End of animation function
    {
        Destroy(gameObject);    // Delete explosion
    }
}
