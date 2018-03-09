using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseHover : MonoBehaviour
{
	public bool isButtonInSettings;

	void Start()
	{
		GetComponent<Renderer>().material.color = new Color32(0,150,200,255);	// Set color to blue
	}

	void OnMouseEnter()															// Mouse enter function
	{
		GetComponent<Renderer>().material.color = new Color32(200,0,0,255);		// Change color to red
	}

	void OnMouseExit()																// Mouse exit function
	{	
		if(!MainMenu.settingsMenuIsActive || isButtonInSettings)					// If settings menu is inactive or button is from settings menu
			GetComponent<Renderer>().material.color = new Color32(0,150,200,255);	// Change back color to blue
	}
}
