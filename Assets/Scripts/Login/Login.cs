using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class Login : MonoBehaviour {
    public static string playerName = string.Empty;

    // Use this for initialization
	void Start () {
        DontDestroyOnLoad(gameObject);
        GetComponent<InputField>().onSubmit.AddListener(startGame);
    }

    public void startGame(string name) {
        playerName = GetComponent<InputField>().value;
        Application.LoadLevel(Application.loadedLevel + 1);
    }

    IEnumerator ChangeLevel() {
        InputField input = GetComponent<InputField>();
        float sec = GetComponent<SceneFading>().BeginFade(1);
        yield return new WaitForSeconds(sec);
        Application.LoadLevel(Application.loadedLevel + 1);
    }

	// Update is called once per frame
	void Update () {
	
	}
}

