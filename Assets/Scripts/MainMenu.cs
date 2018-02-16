using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class MainMenu : MonoBehaviour
{
	public bool isPlayButton;
	public bool isSettingsButton;
	public bool isExitButton;
	public GameObject SettingsWindow;
	public GameObject PlayButton;
	public GameObject ExitButton;
	public static bool settingsIsActive;

	void Start()
    {
		// Initialisation
		settingsIsActive = false;
	}

	// Quand clic
	void OnMouseUp()
	{
		// Si bouton Play
		if(isPlayButton)
		{
			// Lancer le jeu
			SceneManager.LoadScene(1);
		}

		// Si bouton d'options
		if(isSettingsButton)
		{
			// Maintenir la couleur rouge sur le bouton
			GetComponent<Renderer>().material.color = new Color32(200,0,0,255);

			// Inverser la visibilité des options
			SettingsWindow.GetComponent<Renderer>().enabled = !SettingsWindow.GetComponent<Renderer>().enabled;

			// Si options visibles
			if(SettingsWindow.GetComponent<Renderer>().enabled == true)
			{
				// Signaler à MouseHover.cs
				settingsIsActive = true;
				// Désactiver les hitbox des autres boutons
				PlayButton.GetComponent<BoxCollider2D>().enabled = false;
				ExitButton.GetComponent<BoxCollider2D>().enabled = false;
				// Couleur = gris
				PlayButton.GetComponent<Renderer>().material.color = new Color32(50,50,50,255);
				ExitButton.GetComponent<Renderer>().material.color = new Color32(50,50,50,255);
			}

			// Si options non visibles
			else
			{
				// Signaler à MouseHover.cs
				settingsIsActive = false;
				// Réactiver les hitbox des autres boutons
				PlayButton.GetComponent<BoxCollider2D>().enabled = true;
				ExitButton.GetComponent<BoxCollider2D>().enabled = true;
				// Rétablir les couleurs
				PlayButton.GetComponent<Renderer>().material.color = new Color32(0,150,200,255);
				ExitButton.GetComponent<Renderer>().material.color = new Color32(0,150,200,255);
			}
		}

		// Si bouton Exit
		if(isExitButton)
		{
			// Quitter le jeu
			Application.Quit();
		}
	} 
}
