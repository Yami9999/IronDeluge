using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ScoreScreen : MonoBehaviour
{
	public bool isTimer;
	public bool isScore;
	public GameObject text;
	public static int score;
	private float timer;

	void Start ()
	{
		timer = 0;
		score = 0;
	}
	
	void Update ()
	{
		if (isTimer)																// If digit is timer
		{
			text.GetComponent<TextMesh>().text = Math.Round(timer,2).ToString();	// Display the round value of the time spend since the creation of the score screen
			timer += Time.deltaTime;												// Update timer
		}
		if (isScore)																// If digit is score
			text.GetComponent<TextMesh>().text = score.ToString();					// Deisplay score
	}
}
