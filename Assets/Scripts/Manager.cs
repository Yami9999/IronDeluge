using UnityEngine;

public class Manager : MonoBehaviour
{
    // Player Prefab.
    public GameObject player1;
    public GameObject player2;

    // Title.
    private GameObject title;

    void Start ()
    {
        // Reference Title GameObject and acquire.
        title = GameObject.Find ("Title");
    }

    void Update ()
    {
        // When isn't during the game, if press X key, return true.
        if (IsPlaying () == false && Input.GetKeyDown(KeyCode.X)) {
            GameStart ();
        }
    }

    void GameStart ()
    {
        // When start the game, hidden title and appear the player.
        title.SetActive (false);
        Instantiate (player1, player1.transform.position, player1.transform.rotation);
        Instantiate (player2, player2.transform.position, player2.transform.rotation);
    }

    public void GameOver ()
    {
        // When the game is over, display title.
        title.SetActive (true);
    }

    public bool IsPlaying ()
    {
        // Judge whether or not during the game, title is display or non-display.
        return title.activeSelf == false;
    }
}
