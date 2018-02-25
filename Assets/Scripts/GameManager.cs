using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public GameObject Player1;
    public GameObject Player2;
    public GameObject EnemySpawner;
    public GameObject PauseMenu;
    public static bool isPaused;
    public static GameObject PauseInstance;
    private GameObject PauseIntro;
    private SoundManager SoundManager;
    private AudioSource[] music;

    void Start()
    {
        isPaused = false;                                                                   // Game is unpaused
        PauseIntro = GameObject.Find("PauseIntro");                                         // Get the intro pause
        music = GameObject.Find("SoundManager").GetComponents<AudioSource>();               // Get musics
        GameObject.Find("SoundManager").GetComponent<SoundManager>().StopSound(music[0]);   // Stop menu music via the sound manager
        GameObject.Find("SoundManager").GetComponent<SoundManager>().StopSound(music[1]);
        GameObject.Find("SoundManager").GetComponent<SoundManager>().PlaySound(music[2]);   // Play pre battle music
    }

    void Update()
    {
        if (PauseIntro.activeSelf == true && Input.GetKeyDown(KeyCode.X))                               // If intro pause is active and X is pressed
            StartGame();                                                                                // Start game
        if (PauseIntro.activeSelf == false && isPaused == false && Input.GetKeyDown(KeyCode.Escape))    // If intro pause is inactive and game is not paused and ESC is pressed
        {
            Pause();                                                                                    // Pause game
            return;                                                                                     // Avoid pause/unpause on a same tick
        }                                                                          
        if (PauseIntro.activeSelf == false && isPaused == true && Input.GetKeyDown(KeyCode.Escape))     // If intro pause is inactive and game is paused and ESC is pressed
        {
            Unpause();                                                                                  // Unpause game
            return;                                                                                     // Avoid pause/unpause on a same tick
        }
    }

    public void StartGame()                                                                             // Start game function
    {
        PauseIntro.SetActive(false);                                                                    // Turn off the intro pause
        GameObject.Find("SoundManager").GetComponent<SoundManager>().StopSound(music[2]);               // Stop pre battle music
        if (Random.value < 0.5)
            GameObject.Find("SoundManager").GetComponent<SoundManager>().PlaySound(music[3]);           // Randomly play battle music 1 or 2
        else
            GameObject.Find("SoundManager").GetComponent<SoundManager>().PlaySound(music[4]);
        Instantiate(Player1, Player1.transform.position, Player1.transform.rotation);                   // Create player 1
        Instantiate(Player2, Player2.transform.position, Player2.transform.rotation);                   // Create player 2
        Instantiate(EnemySpawner, EnemySpawner.transform.position, EnemySpawner.transform.rotation);    // Create the enemy spawner
    }

    public void Pause()                                                                                     // Pause function
    {
        PauseInstance = Instantiate(PauseMenu, PauseMenu.transform.position, PauseMenu.transform.rotation); // Create a pause instance based on the pause menu
        Time.timeScale = 0;                                                                                 // Stop time
        isPaused = true;                                                                                    // Update game state
    }

    public void Unpause()       // Unpause function       
    {
        Time.timeScale = 1;     // Resume time
        isPaused = false;       // Game is paused
        Destroy(PauseInstance); // Delete pause instance
    }
}
