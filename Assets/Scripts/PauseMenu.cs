using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class PauseMenu : MonoBehaviour
{
	public bool isResume;
	public bool isReturn;

	private bool isPaused;

	void OnMouseUp()
	{
		isPaused = Manager.isPaused;

		if(isPaused == true && isResume == true)
		{
			Time.timeScale = 1;
			Manager.isPaused = false;
			Destroy(Manager.pauseInstance);
		}

		if(isReturn == true)
		{
			SceneManager.LoadScene(0);
		}
	} 
}
