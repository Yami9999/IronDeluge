﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
	public static SoundManager instance;
	private AudioSource[] music;
	//PlayerPrefs.SetFloat("soundVolume", currentVolume);  //Note that you need to keep the volume in a variable called currentVolume or whatever you name it.

	void Start()
	{
		//currentVolume = PlayerPrefs.GetFloat("soundVolume");
		//SetVolume(currentVolume);    //This function has been defined above
		if (instance == null)					// If no sound manager exist
			instance = this;					// This sound manager exist
		else									// If a sound manager already exist
			Destroy(gameObject);				// Destroy this sound manager
		DontDestroyOnLoad(gameObject);			// Stay alive after scene loadings
		music = GetComponents<AudioSource>();
		if (Random.value < 0.5)					// Randomly play menu music 1 or 2
			PlaySound(music[0]);
		else
			PlaySound(music[1]);
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

	public static void Mute(AudioSource sound)			// Mute function
	{
		sound.volume = 0f;
	}
}
