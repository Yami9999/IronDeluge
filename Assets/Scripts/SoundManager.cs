using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
	public static SoundManager instance;
	private AudioSource[] music;
	private int index;
	public static bool isMuted;

	void Start()
	{
		DontDestroyOnLoad(gameObject);	// Make the sound manager persistent
		isMuted = false;				// Game is not muted
	}

	public void PlaySound(AudioSource sound)			// Play sound function
	{
		sound.Play();
	}

	public void StopSound(AudioSource sound)			// Stop sound function
	{
		sound.Stop();
	}

	public void SetVolume(float val)					// Volume control function
    {
        GetComponent<AudioSource>().volume = val;
    }

	public void Mute()							// Mute function
	{
		index = 0;
		music = GetComponents<AudioSource>();	// GetComponent musics
		if(!isMuted)							// If game is note muted
		{
			while(index != 19)					// Take all audio sources
			{
				music[index].mute = true;		// Mute them
				++index;						// Increment index
			}
			isMuted = true;						// Game is now muted
			return;								// Avoid mute/unmute on same tick
		}
		if(isMuted)								// If game is muted
		{
			while(index != 19)					// Take all audio sources
			{
				music[index].mute = false;		// Unmute them
				++index;						// Increment index
			}
			isMuted = false;					// Game is now unmuted
			return;								// Avoid mute/unmute on same tick
		}
	}
}
