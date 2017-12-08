using UnityEngine;
using System.Collections;

public class Emitter : MonoBehaviour
{
    // Store Wave Prefab.
    public GameObject[] waves;

    // Current Wave.
    private int currentWave;

    // Manager component.
    private Manager manager;

    IEnumerator Start ()
    {

        // If Wave don't exist, coroutine finish.
        if (waves.Length == 0) {
            yield break;
        }

        // Seek Manager component in the Scene and acquire.
        manager = FindObjectOfType<Manager>();

        while (true) {

            // During title displaying stands by.
            while(manager.IsPlaying() == false) {
                yield return new WaitForEndOfFrame ();
            }

            // Make Wave.
            GameObject g = (GameObject)Instantiate (waves [currentWave], transform.position, Quaternion.identity);

            // Wait for all Enemy child elements is deleted.
            g.transform.parent = transform;

            // Wait until all Enemy in child elements is deleted.
            while (g.transform.childCount != 0) {
                yield return new WaitForEndOfFrame ();
            }

            // Delete Wave.
            Destroy (g);

            // When Wave stored have executed all, currentWave makes 0.（Initialize -> loop）
            if (waves.Length <= ++currentWave) {
                currentWave = 0;
            }

        }
    }
}