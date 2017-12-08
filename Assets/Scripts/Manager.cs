using UnityEngine;

public class Manager : MonoBehaviour
{
    // Player Prefab.
    public GameObject player;

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
        if (IsPlaying () == false && Input.GetKeyDown (KeyCode.X)) {
            GameStart ();
        }
    }

    void GameStart ()
    {
        // When start the game, hidden title and appear the player.
        title.SetActive (false);
        Instantiate (player, player.transform.position, player.transform.rotation);
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
