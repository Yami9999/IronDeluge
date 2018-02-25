using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class MainMenu : MonoBehaviour
{
	public bool isPlayButton;
	public bool isSettingsButton;
	public bool isExitButton;
	public GameObject SettingsMenu;
	public GameObject PlayButton;
	public GameObject ExitButton;
	public static bool settingsMenuIsActive;

	void Start()
	{
		settingsMenuIsActive = false;
	}

	void OnMouseUp()									// On click function	
	{
		if(isPlayButton)								// If button is "Play"
			SceneManager.LoadScene(1);					// Load game
		if(isSettingsButton)							// If button is "Settings"
		{
			if(SettingsMenu.activeInHierarchy == false)	// If settings menu is off
				OpenSettings();							// Open settings
			else										// If settings menu is on
				CloseSettings();						// Close settings
		}
		if(isExitButton)								// If button is "Exit"
			Application.Quit();							// Quit the game
	} 

	void OpenSettings()																	// Open settings functio
	{
		GetComponent<Renderer>().material.color = new Color32(200,0,0,255);				// Maintain red color on button
		SettingsMenu.SetActive(true);													// Turn on settings menu
		settingsMenuIsActive = true;													// Warn MouseHover.cs that settings are now active
		PlayButton.GetComponent<BoxCollider2D>().enabled = false;						// Turn off other buttons' hitboxes
		ExitButton.GetComponent<BoxCollider2D>().enabled = false;
		PlayButton.GetComponent<Renderer>().material.color = new Color32(50,50,50,255);	// Change other buttons to grey
		ExitButton.GetComponent<Renderer>().material.color = new Color32(50,50,50,255);
		return;																			// Avoid open/close on a same tick
	}

	void CloseSettings()																	// Close settings function
	{
		SettingsMenu.SetActive(false);														// Turn off settings menu
		settingsMenuIsActive = false;														// Warn MouseHover.cs that settings are now inactive
		PlayButton.GetComponent<BoxCollider2D>().enabled = true;							// Restore other buttons' hitboxes
		ExitButton.GetComponent<BoxCollider2D>().enabled = true;
		PlayButton.GetComponent<Renderer>().material.color = new Color32(0,150,200,255);	// Restore other buttons' color
		ExitButton.GetComponent<Renderer>().material.color = new Color32(0,150,200,255);
		return;
	}
}


