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
		background.transform.Translate(0, -(speed * Time.deltaTime), 0);	// Move background down
		if (background.transform.position.y <= -10.75)						// If background under a certain value
			background.transform.Translate(0, 30.80f, 0);					// Move it above the scene
	}

	public void Accelerate()
	{
		speed += 2f;	// Speed up background
	}

	public IEnumerator WhiteToYellow()											// Change backgrounds color gradually from neutral to yellow
    {
        GetComponent<SpriteRenderer>().color = new Color32(255,255,200,255);
        yield return new WaitForSeconds(0.5f);
       	GetComponent<SpriteRenderer>().color = new Color32(255,255,100,255);
        yield return new WaitForSeconds(0.5f);
        GetComponent<SpriteRenderer>().color = new Color32(255,255,0,255);
        yield return null;
    }

    public IEnumerator YellowToOrange()										// Change backgrounds color gradually from yellow to orange
    {
        GetComponent<SpriteRenderer>().color = new Color32(255,210,0,255);
        yield return new WaitForSeconds(0.5f);
        GetComponent<SpriteRenderer>().color = new Color32(255,170,0,255);
        yield return new WaitForSeconds(0.5f);
        GetComponent<SpriteRenderer>().color = new Color32(255,125,0,255);
        yield return null;
    }

    public IEnumerator OrangeToRed()										// Change backgrounds color gradually from orange to red
	{
        GetComponent<SpriteRenderer>().color = new Color32(255,85,0,255);
        yield return new WaitForSeconds(0.5f);
        GetComponent<SpriteRenderer>().color = new Color32(255,40,0,255);
        yield return new WaitForSeconds(0.5f);
        GetComponent<SpriteRenderer>().color = new Color32(255,0,0,255);
        yield return null;
	}
}
