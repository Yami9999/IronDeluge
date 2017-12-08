using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
	// Scroll speed
	public float speed = 0.1f;
	
	void Update ()
	{
		// Value of Y change from 0 to 1 by time. return to 0 if it becomes 1 and repeat.
		float y = Mathf.Repeat (Time.time * speed, 1);
		
		// Create offset that shift value of Y
		Vector2 offset = new Vector2 (0, y);
		
		// Set up offset to materials
		GetComponent<Renderer>().sharedMaterial.SetTextureOffset ("_MainTex", offset);
	}
}
