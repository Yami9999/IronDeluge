using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    public GameObject EnemyPink;
    public GameObject EnemyRed;
    public GameObject EnemyGrey;
    public GameObject EnemyMissile;
    public double enemySpawnRate;
    public float missileSpawnRate;
    private float lastEnemySpawned;
    private float randomX;
    private int randomEnemy;

    void Start()
    {
        lastEnemySpawned = 0;
        StartCoroutine(Difficulty());
    }

    void Update()
    {
        if(Time.time > lastEnemySpawned + enemySpawnRate)                                               // If actual time is past the last enemy spawned time + the spawn rate
        {
            randomEnemy = Random.Range(0,3);                                                            // Take a random enemy from the array
            randomX = Random.Range(-8f,-0.5f);                                                          // Take a random value above the left terrain
            if (randomEnemy == 0)                                                                       // Select enemy type
            {
                Instantiate(EnemyPink, new Vector2(randomX, 6), EnemyPink.transform.rotation);          // Create an enemy above the left terrain
                if (!GameManager.soloPlay)                                                              // If Game is not solo
                    Instantiate(EnemyPink, new Vector2(-randomX, 6), EnemyPink.transform.rotation);     // Create the mirror enemy above the right terrain
            }
            if (randomEnemy == 1)
            {
                Instantiate(EnemyRed, new Vector2(randomX, 6), EnemyRed.transform.rotation);
                if (!GameManager.soloPlay)
                    Instantiate(EnemyRed, new Vector2(-randomX, 6), EnemyRed.transform.rotation);
            }
            if (randomEnemy == 2)
            {
                Instantiate(EnemyGrey, new Vector2(randomX, 6), EnemyGrey.transform.rotation);
                if (!GameManager.soloPlay)
                    Instantiate(EnemyGrey, new Vector2(-randomX, 6), EnemyGrey.transform.rotation);                                                
            }
            lastEnemySpawned = Time.time;                                                               // Update the time of the last enemy spawned
        }
    }

    public void RedirectSpawnMissiles(int missileCount, bool isPlayer1) // Access to the spawn missiles function
    {
        StartCoroutine(SpawnMissiles(missileCount,isPlayer1));
    }

    IEnumerator SpawnMissiles(int missileCount,bool isPlayer1)                                      // Spawn missiles function
    {
        yield return new WaitForSeconds(1f);                                                        // Wait one second
        while (missileCount > 0)                                                                    // While there are missiles to fire
        {
            if (isPlayer1)                                                                          // If the launcher is player 1
                randomX = Random.Range(0.5f,8f);                                                    // Take a random value above the right terrain
            else                                                                                    // If the launcher is player 2
                randomX = Random.Range(-8f,-0.5f);                                                  // Take a random value above the left terrain
   			Instantiate(EnemyMissile, new Vector2(randomX, 6), EnemyMissile.transform.rotation);    // Create a missile above the terrain
            yield return new WaitForSeconds(missileSpawnRate);                                      // Wait for missile spawn rate
            missileCount -= 1;                                                                      // Decrease the missile count
        }
    }

    IEnumerator Difficulty()                                                                        // Progressive difficulty function
    {
        yield return new WaitForSeconds(6);                                                         // Wait
        GameObject.Find("Background1").GetComponent<Background>().Accelerate();                     // Speed up background
        GameObject.Find("Background2").GetComponent<Background>().Accelerate();
        GameObject.Find("Background3").GetComponent<Background>().Accelerate();
        enemySpawnRate -= 1;                                                                        // Decrease spawn rate 2 -> 1
        yield return new WaitForSeconds(14);
        GameObject.Find("Background1").GetComponent<Background>().Accelerate();
        GameObject.Find("Background2").GetComponent<Background>().Accelerate();
        GameObject.Find("Background3").GetComponent<Background>().Accelerate();
        StartCoroutine(GameObject.Find("Background1").GetComponent<Background>().WhiteToYellow());  // Change backgrounds color
        StartCoroutine(GameObject.Find("Background2").GetComponent<Background>().WhiteToYellow());
        StartCoroutine(GameObject.Find("Background3").GetComponent<Background>().WhiteToYellow());
        enemySpawnRate -= 0.5;                                                                      // 1 -> 0.5
        yield return new WaitForSeconds(20);
        GameObject.Find("Background1").GetComponent<Background>().Accelerate(); 
        GameObject.Find("Background2").GetComponent<Background>().Accelerate();
        GameObject.Find("Background3").GetComponent<Background>().Accelerate();
        StartCoroutine(GameObject.Find("Background1").GetComponent<Background>().YellowToOrange());
        StartCoroutine(GameObject.Find("Background2").GetComponent<Background>().YellowToOrange());
        StartCoroutine(GameObject.Find("Background3").GetComponent<Background>().YellowToOrange());
        enemySpawnRate -= 0.25;                                                                     // 1 -> 0.25
        yield return new WaitForSeconds(20);
        GameObject.Find("Background1").GetComponent<Background>().Accelerate();
        GameObject.Find("Background2").GetComponent<Background>().Accelerate();
        GameObject.Find("Background3").GetComponent<Background>().Accelerate();
        StartCoroutine(GameObject.Find("Background1").GetComponent<Background>().OrangeToRed());
        StartCoroutine(GameObject.Find("Background2").GetComponent<Background>().OrangeToRed());
        StartCoroutine(GameObject.Find("Background3").GetComponent<Background>().OrangeToRed());
        enemySpawnRate -= 0.15;                                                                     // 1 -> 0.1
        if (GameManager.soloPlay && Player.bombCount == 0 && Input.GetKeyDown("l"))                 // If game is solo and player is out of bombs and player press bomb button
        {
            enemySpawnRate -= 0.1;                                                                  // 0.1 -> 0
            yield return null;                                                                      // Return
        }
    }
}