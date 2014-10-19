using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class YearCounter : MonoBehaviour {

	public float yearElapseRate = 50.0f;

	public RectTransform yearCounterGUIText;
	public GameObject GALAXY;

    public float scaleSpeed = 0.4f;

	private Text text;
	private string startingText;
	public float currentYear = 0;


	// Use this for initialization
	void Start () {
	
		if (yearCounterGUIText.GetComponent<Text>() != null) {
			text = yearCounterGUIText.GetComponent<Text>();
			startingText = text.text;
		} else {

			Debug.LogError ( gameObject.name + "does not appear to have a GUI Text component attached." );

		}
	}
	
	// Update is called once per frame
	void Update () {

		currentYear += Time.deltaTime * 50;

		text.text = startingText + "\n" + Mathf.RoundToInt(currentYear).ToString();

        if (Time.timeScale != 0)
        {
            GALAXY.transform.localScale = GALAXY.transform.localScale * /*(1 - 4f)*/ (1 -(Time.deltaTime * scaleSpeed));
        }

	
	}
}
