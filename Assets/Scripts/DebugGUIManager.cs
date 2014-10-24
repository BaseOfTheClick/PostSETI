using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DebugGUIManager : MonoBehaviour {

	// Time vars
	[SerializeField]
	private Text timeText;
	private string timeTextStartText;
	private int lastSecond;

	// Use this for initialization
	void Start () {
	
		// Prep time
		timeText = GameObject.Find("Canvas_DebugGUI/Text_Time").GetComponent<Text>();
		timeTextStartText = timeText.text;


	}
	
	// Update is called once per frame
	void Update () {

		// Update Time
		timeText.text = timeTextStartText + Time.time.ToString();
		// If we're in a new second, flash red;
		if ( lastSecond != Mathf.FloorToInt(Time.time) ) {
			StartCoroutine( flashTextColor (timeText) );
		}
		// Set second-mark checker
		lastSecond = Mathf.FloorToInt(Time.time);

	}

	IEnumerator flashTextColor ( Text inputText ) {

		inputText.color = Color.cyan;

		yield return new WaitForSeconds(0.1f);

		inputText.color = Color.white;

	}

}
