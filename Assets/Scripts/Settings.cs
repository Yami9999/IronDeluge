using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
	public bool isCloseButton;
	public bool isMuteButton;
	public GameObject SettingsMenu;
	public GameObject SoloButton;
	public GameObject VersusButton;
	public GameObject ExitButton;
	public SpriteRenderer Checkbox;
	private AudioSource[] sound;

	void Start ()
	{
		sound = GameObject.Find("SoundManager").GetComponents<AudioSource>();	// Get musics from the sound manager
		if(SoundManager.isMuted)												// If game is muted
			Checkbox.enabled = true;											// Show checkbox
	}
	
	void Update ()
	{
		
	}

	void OnMouseUp()
	{
		GameObject.Find("SoundManager").GetComponent<SoundManager>().PlaySound(sound[17]);					// Play menu sound
		if(isCloseButton)																					// If button is "Close"
			GameObject.Find("Interface").transform.GetChild(3).GetComponent<MainMenu>().CloseSettings();	// Close settings via the main menu function
		if(isMuteButton)																					// If button is "Mute"
		{
			GameObject.Find("SoundManager").GetComponent<SoundManager>().Mute();							// Mute sounds via the sound manager
			if(Checkbox.enabled)																			// If checkbox is visible
			{
				Checkbox.enabled = false;																	// Hide it
				return;																						// Avoid hide/unhide on same tick
			}
			if(!Checkbox.enabled)																			// If checkbox is hidden
			{
				Checkbox.enabled = true;																	// Show it
				return;																						// Avoid hide/unhide on same tick
			}
		}
	}
}
