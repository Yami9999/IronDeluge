using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    static public bool soloPlay;
    public GameObject Player1;
    public GameObject Player2;
    public GameObject EnemySpawner;
    public GameObject PauseIntro;
    public GameObject PauseMenu;
    public GameObject ScoreScreen;
    public GameObject CountDown;
    public static bool isPaused;
    public static GameObject PauseInstance;
    private bool introHasEnded;
    private GameObject PauseIntroInstance;
    private GameObject CountDownInstance;
    private SoundManager SoundManager;
    private AudioSource[] music;

    void Start()
    {
        if (instance == null)           // Singleton pattern
            instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;  // Wait for scene load
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;  // Stop waiting for scene load
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Main_Scene")                                                 // If in main scene
        {
            if (GameObject.Find("PauseIntro(Clone)") != null && Input.GetKeyDown(KeyCode.X))                    // If intro pause exist and X is pressed
                StartCoroutine(Countdown());                                                                    // Start countdown
            if (GameObject.Find("PauseIntro(Clone)") == null && GameObject.Find("Countdown(Clone)") == null)    // If both intro pause and countdown are destroyed
            {
                if (introHasEnded && !isPaused && Input.GetKeyDown(KeyCode.Escape))                             // If game is not paused and ESC is pressed
                {
                    Pause();                                                                                    // Pause game
                    return;                                                                                     // Avoid pause/unpause on a same tick
                }                                                                          
                if (introHasEnded && isPaused && Input.GetKeyDown(KeyCode.Escape))                              // If game is paused and ESC is pressed
                {
                    Unpause();                                                                                  // Unpause game
                    return;                                                                                     // Avoid pause/unpause on a same tick
                }
            }
        }
    }

    void OnSceneLoaded(Scene scene,LoadSceneMode mode)                                                                  // On scene loaded function
    {   
        if (scene.name == "Main_Scene")                                                                                 // If main scene
        {
            music = GameObject.Find("SoundManager").GetComponents<AudioSource>();                                       // Get musics
            GameObject.Find("SoundManager").GetComponent<SoundManager>().StopSound(music[0]);                           // Stop menu musics via the sound manager
            GameObject.Find("SoundManager").GetComponent<SoundManager>().StopSound(music[1]);
            GameObject.Find("SoundManager").GetComponent<SoundManager>().PlaySound(music[2]);                           // Play pre battle music
            isPaused = false;                                                                                           // Initialize to unpaused
            introHasEnded = false;                                                                                      // Initialize to no ended
            PauseIntroInstance = Instantiate(PauseIntro,PauseIntro.transform.position,PauseIntro.transform.rotation);   // Create the intro pause
        }
    }

    public IEnumerator Countdown()                                                                                      // Start countdown function
    {
        Destroy(PauseIntroInstance);                                                                                    // Delete the intro pause
        GameObject.Find("SoundManager").GetComponent<SoundManager>().StopSound(music[2]);                               // Stop pre battle music
        CountDownInstance = Instantiate(CountDown,CountDown.transform.position,CountDown.transform.rotation);           // Create a countdown
        yield return new WaitForSeconds(1f);                                                                            // Wait a second
        CountDownInstance.transform.GetChild(0).transform.GetChild(1).GetComponent<TextMesh>().text = "2";              // Display 2
        CountDownInstance.transform.GetChild(1).transform.GetChild(1).GetComponent<TextMesh>().text = "2";
        CountDownInstance.transform.GetChild(0).transform.GetChild(1).GetComponent<TextMesh>().color = Color.yellow;    // Change color to yellow
        CountDownInstance.transform.GetChild(1).transform.GetChild(1).GetComponent<TextMesh>().color = Color.yellow;
        yield return new WaitForSeconds(1f);                                                                            // Wait a second
        CountDownInstance.transform.GetChild(0).transform.GetChild(1).GetComponent<TextMesh>().text = "1";              // Display 1
        CountDownInstance.transform.GetChild(1).transform.GetChild(1).GetComponent<TextMesh>().text = "1";
        CountDownInstance.transform.GetChild(0).transform.GetChild(1).GetComponent<TextMesh>().color = Color.red;       // Change color to orange
        CountDownInstance.transform.GetChild(1).transform.GetChild(1).GetComponent<TextMesh>().color = Color.red;
        yield return new WaitForSeconds(1f);                                                                            // Wait a second
        Destroy(CountDownInstance);                                                                                     // Delete countdown
        StartGame();
    }

    void StartGame()                                                                                    // Start game function
    {
        if (Random.value < 0.5)                                                                         // Randomly play battle music 1 or 2
            GameObject.Find("SoundManager").GetComponent<SoundManager>().PlaySound(music[3]);
        else
            GameObject.Find("SoundManager").GetComponent<SoundManager>().PlaySound(music[4]);
        Instantiate(Player1, Player1.transform.position, Player1.transform.rotation);                   // Create player 1
        if (soloPlay)                                                                                   // If game is solo
            Instantiate(ScoreScreen, ScoreScreen.transform.position, ScoreScreen.transform.rotation);   // Create score screen
        //else                                                                                          // If game is versus
            //Instantiate(Player2, Player2.transform.position, Player2.transform.rotation);             // Create player 2
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
