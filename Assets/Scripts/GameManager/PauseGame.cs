using UnityEngine;
using System.Collections;

public class PauseGame : MonoBehaviour {
    public bool isPaused;

    public void pause()
    {
        Time.timeScale = 0;
        isPaused = true;
    }
    public void unpause()
    {
        Time.timeScale = 1;
        isPaused = false;
    }

    public void Update()
    {
        if (isPaused)
        {
            pause();
        }
        else
        {
            unpause();
        }
    }
}
