using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseHover : MonoBehaviour
{
	void Start()
	{
		// Couleur initiale = bleu
		GetComponent<Renderer>().material.color = new Color32(0,150,200,255);
	}

	// Quand la souris sur bouton
	void OnMouseEnter()
	{
		// Changer la couleur pour bleu clair
		GetComponent<Renderer>().material.color = new Color32(200,0,0,255);
	}

	// Quand la souris quitte le bouton
	void OnMouseExit()
	{	
		// Si les options ne sont pas affichées
		if(MainMenu.settingsIsActive == false)
		{
			// Rétablir la couleur pour bleu
			GetComponent<Renderer>().material.color = new Color32(0,150,200,255);
		}
	}
}
