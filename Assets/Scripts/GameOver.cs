using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class GameOver : MonoBehaviour
{
	public bool isRematchButton;
	public bool isReturnButton;
	private bool isPaused;
	private AudioSource[] music;

	void Start()
	{
		music = GameObject.Find("SoundManager").GetComponents<AudioSource>();	// Get musics from the sound manager
		Time.timeScale = 0;														// Time stop
	}

	void OnMouseUp()		// On click function
	{
		if(isRematchButton)	// If button is "Rematch"
			Rematch();		// Rematch
		if(isReturnButton)	// If button is "Return to main menu"
			ReturnToMenu();	// Return to menu
	}

	void Rematch()					
	{
		Time.timeScale = 1;																		// Time resume
		GameObject.Find("SoundManager").GetComponent<SoundManager>().StopSound(music[5]);		// Stop game over musics
		GameObject.Find("SoundManager").GetComponent<SoundManager>().StopSound(music[6]);
		SceneManager.LoadScene(1);																// Reload game scene
	}

	void ReturnToMenu()																			// Return to menu function
	{
		Time.timeScale = 1;																		// Time resume
		GameObject.Find("SoundManager").GetComponent<SoundManager>().StopSound(music[5]);		// Stop game over musics
		GameObject.Find("SoundManager").GetComponent<SoundManager>().StopSound(music[6]);
		if (Random.value < 0.5)																	// Randomly play menu music 1 or 2
			GameObject.Find("SoundManager").GetComponent<SoundManager>().PlaySound(music[0]);
		else
			GameObject.Find("SoundManager").GetComponent<SoundManager>().PlaySound(music[1]);
		SceneManager.LoadScene(0);																// Load main menu scene
	}
}
