using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class Login : MonoBehaviour {
    public string playerName = string.Empty;

    [SerializeField]
    private InputField input = null;

    [SerializeField]
    private Button button = null;

    // Use this for initialization
	void Start () {
        DontDestroyOnLoad(GetComponent<Login>());

        input.onSubmit.AddListener((value) => startGame(value));
        button.onClick.AddListener(() => startGame(input.value));

    }

    public void startGame(string name) {
        playerName = input.value;

        Application.LoadLevel(1);
    }
	// Update is called once per frame
	void Update () {
	
	}
}
