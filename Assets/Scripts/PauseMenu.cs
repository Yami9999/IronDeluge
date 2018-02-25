using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class PauseMenu : MonoBehaviour
{
	public bool isResumeButton;
	public bool isReturnButton;
	private bool isPaused;
	private AudioSource[] music;

	void OnMouseUp()						// On click function
	{
		isPaused = GameManager.isPaused;	// Get game state from Manager.cs
		if(isResumeButton && isPaused)		// If button is "Resume" and game is paused
			Unpause();						// Unpause
		if(isReturnButton)					// If button is "Return to main menu"
			ReturnToMenu();					// Return to menu
	} 

	void Unpause()								// Unpause function
	{
		Time.timeScale = 1;						// Unlock time
		GameManager.isPaused = false;			// Game is not paused anymore
		Destroy(GameManager.PauseInstance);		// Destroy pause menu
	}

	void ReturnToMenu()																				// Return to menu function
	{
		Time.timeScale = 1;																			// Unlock time
		music = GameObject.Find("SoundManager").GetComponents<AudioSource>();						// Get musics from the sound manager
		GameObject.Find("SoundManager").GetComponent<SoundManager>().StopSound(music[3]);			// Stop battle musics
		GameObject.Find("SoundManager").GetComponent<SoundManager>().StopSound(music[4]);
		if (Random.value < 0.5)																		// Randomly play menu music 1 or 2
				GameObject.Find("SoundManager").GetComponent<SoundManager>().PlaySound(music[0]);
			else
				GameObject.Find("SoundManager").GetComponent<SoundManager>().PlaySound(music[1]);
		SceneManager.LoadScene(0);																	// Load main menu scene
	}
}
