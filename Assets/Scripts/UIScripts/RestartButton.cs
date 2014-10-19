using UnityEngine;
using System.Collections;

public class RestartButton : MonoBehaviour {
    public GameObject startButton;

    public void restartGame()
    {
        Application.LoadLevel(0);
    }
}
