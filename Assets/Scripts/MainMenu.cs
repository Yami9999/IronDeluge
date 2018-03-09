using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class MainMenu : MonoBehaviour
{
	public bool isSoloButton;
	public bool isVersusButton;
	public bool isSettingsButton;
	public bool isExitButton;
	public GameObject SettingsMenu;
	public GameObject SoloButton;
	public GameObject VersusButton;
	public GameObject ExitButton;
	public static bool settingsMenuIsActive;
	public static GameObject SettingsMenuInstance;
	private AudioSource[] sound;


	void Start()
	{
		sound = GameObject.Find("SoundManager").GetComponents<AudioSource>();	// Get sounds from the sound manager
		settingsMenuIsActive = false;
		GameManager.soloPlay = false;
	}

	void OnMouseUp()																		// On click function	
	{
		GameObject.Find("SoundManager").GetComponent<SoundManager>().PlaySound(sound[17]);	// Play menu sound
		if(isSoloButton)																	// If button is "Solo"
		{
			GameManager.soloPlay = true;													// Warn the game manager
			SceneManager.LoadScene(2);														// Load game
		}
		if(isVersusButton)																	// If button is "Versus"
		{
			GameManager.soloPlay = false;													// Warn the game manager
			SceneManager.LoadScene(2);														// Load game
		}
		if(isSettingsButton)																// If button is "Settings"
		{
			if(!GameObject.Find("SettingsMenu(Clone)"))										// If settings menu doesn't exist
				OpenSettings();																// Open settings
			else																			// If settings menu is on
				CloseSettings();															// Close settings
		}
		if(isExitButton)																	// If button is "Exit"
			Application.Quit();																// Quit the game
	} 

	void OpenSettings()																										// Open settings function
	{
		GetComponent<Renderer>().material.color = new Color32(200,0,0,255);													// Maintain red color on button
		SettingsMenuInstance = Instantiate(SettingsMenu,SettingsMenu.transform.position,SettingsMenu.transform.rotation);	// Turn on settings menu
		settingsMenuIsActive = true;																						// Warn MouseHover.cs that settings are now active
		SoloButton.GetComponent<BoxCollider2D>().enabled = false;															// Turn off other buttons' hitboxes
		VersusButton.GetComponent<BoxCollider2D>().enabled = false;
		ExitButton.GetComponent<BoxCollider2D>().enabled = false;
		SoloButton.GetComponent<Renderer>().material.color = new Color32(50,50,50,255);										// Change other buttons to grey
		VersusButton.GetComponent<Renderer>().material.color = new Color32(50,50,50,255);
		ExitButton.GetComponent<Renderer>().material.color = new Color32(50,50,50,255);
		return;																												// Avoid open/close on a same tick
	}

	public void CloseSettings()																// Close settings function
	{
		Destroy(SettingsMenuInstance);														// Turn off settings menu
		settingsMenuIsActive = false;														// Warn MouseHover.cs that settings are now inactive
		SoloButton.GetComponent<BoxCollider2D>().enabled = true;							// Restore other buttons' hitboxes
		VersusButton.GetComponent<BoxCollider2D>().enabled = true;
		ExitButton.GetComponent<BoxCollider2D>().enabled = true;
		SoloButton.GetComponent<Renderer>().material.color = new Color32(0,150,200,255);	// Restore other buttons' color
		VersusButton.GetComponent<Renderer>().material.color = new Color32(0,150,200,255);
		ExitButton.GetComponent<Renderer>().material.color = new Color32(0,150,200,255);
		return;
	}
}


