using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    public GameObject EnemyPink;
    public GameObject EnemyRed;
    public GameObject EnemyGrey;
    public double spawnRate;
    private float lastEnemySpawned;
    private float randomX;
    private int randomEnemy;

    void Start()
    {
        lastEnemySpawned = 0;
    }

    void Update()
    {
        if(Time.time > lastEnemySpawned + spawnRate)                                                // If actual time is past the last enemy spawned time + the spawn rate
        {
            randomEnemy = Random.Range(0,3);                                                        // Take a random enemy from the array
            randomX = Random.Range(-7.5f,-1f);                                                      // Take a random value above the left terrain
            if (randomEnemy == 0)                                                                   // Select enemy type
            {
                Instantiate(EnemyPink, new Vector2(randomX, 6), EnemyPink.transform.rotation);      // Create an enemy above the left terrain
                if (SoundManager.solo == false)                                                     // If Game is not solo
                    Instantiate(EnemyPink, new Vector2(-randomX, 6), EnemyPink.transform.rotation); // Create the mirror enemy above the right terrain
            }
            if (randomEnemy == 1)
            {
                Instantiate(EnemyRed, new Vector2(randomX, 6), EnemyRed.transform.rotation);
                if (SoundManager.solo == false)
                    Instantiate(EnemyRed, new Vector2(-randomX, 6), EnemyRed.transform.rotation);
            }
            if (randomEnemy == 2)
            {
                Instantiate(EnemyGrey, new Vector2(randomX, 6), EnemyGrey.transform.rotation);
                if (SoundManager.solo == false)
                    Instantiate(EnemyGrey, new Vector2(-randomX, 6), EnemyGrey.transform.rotation);                                                
            }
            lastEnemySpawned = Time.time;                                                           // Update the time of the last enemy spawned
            spawnRate -= 0.1;                                                                       // Decrease spawnrate
        }
    }
}