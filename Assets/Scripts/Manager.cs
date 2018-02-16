using UnityEngine;

public class Manager : MonoBehaviour
{
    // Players
    public GameObject player1;
    public GameObject player2;
    public GameObject pauseMenu;

    public static bool isPaused;
    public static GameObject pauseInstance;

    // Title.
    private GameObject title;

    void Start()
    {
        // Reference Title GameObject and acquire.
        title = GameObject.Find("Title");
        isPaused = false;
    }

    void Update()
    {
        // When isn't during the game, if press X key, return true.
        if (IsPlaying() == false && isPaused == false && Input.GetKeyDown(KeyCode.X))
        {
            GameStart();
        }

        if (isPaused == false && Input.GetKeyDown(KeyCode.Escape))
        {
            pauseInstance = Instantiate(pauseMenu, pauseMenu.transform.position, pauseMenu.transform.rotation);
            Time.timeScale = 0;
            isPaused = true;
            return;
        }

        if (isPaused == true && Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 1;
            isPaused = false;
            Destroy(pauseInstance);
            return;
        }
    }

    void GameStart()
    {
        // When start the game, hidden title and appear the player.
        title.SetActive(false);
        Instantiate(player1, player1.transform.position, player1.transform.rotation);
        Instantiate(player2, player2.transform.position, player2.transform.rotation);
    }

    public void GameOver()
    {
        // When the game is over, display title.
        title.SetActive(true);
    }

    public bool IsPlaying()
    {
        // Judge whether or not during the game, title is display or non-display.
        return title.activeSelf == false;
    }
}
