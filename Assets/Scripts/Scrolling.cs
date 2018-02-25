using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrolling : MonoBehaviour
{
	public GameObject background;
	public float speed;
	
	void Start()
	{}

	void Update()
	{
		background.transform.Translate(0, -(speed * Time.deltaTime), 0);	// Move background down
		if (background.transform.position.y <= -10.75)						// If background under a certain value
			background.transform.Translate(0, 20.75f, 0);					// Move it above the scene
	}
}
