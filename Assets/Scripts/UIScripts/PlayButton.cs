using UnityEngine;
using System.Collections;

public class PlayButton : MonoBehaviour {

    public GameObject gameManager;
    private bool paused = true;

    public void Start()
    {
        gameManager.SetActive(true);
    }

    public void startGame()
    {
        paused = false;
        gameManager.GetComponent<PauseGame>().unpause();
        gameObject.SetActive(false);
    }
}
