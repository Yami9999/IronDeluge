using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    public GameObject EnemyRed;
    public float spawnRate;
    private float lastEnemySpawned;
    private float randomX;

    void Start()
    {
        lastEnemySpawned = 0;
    }

    void Update()
    {
        if(Time.time > lastEnemySpawned + spawnRate)                                        // If actual time is past the last enemy spawned time + the spawn rate
        {
            randomX = Random.Range(-7.5f,-1f);                                              // Take a random value above the left terrain
            Instantiate(EnemyRed, new Vector2(randomX, 6), EnemyRed.transform.rotation);    // Create enemy above the left terrain
            Instantiate(EnemyRed, new Vector2(-randomX, 6), EnemyRed.transform.rotation);   // Create the mirror enemy above the right terrain
            lastEnemySpawned = Time.time;                                                   // Update the tmie f the last enemy spawned
        }
    }
}