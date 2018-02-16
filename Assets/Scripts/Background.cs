using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
	public GameObject background;
	public float speed;
	
	void Start()
	{}

	void Update()
	{
		background.transform.Translate(0, -(speed * Time.deltaTime), 0);
		if (background.transform.position.y <= -10.75)
			background.transform.Translate(0, 20.75f, 0);
	}
}
