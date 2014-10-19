using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GlobalRateController : MonoBehaviour
{
    public GameObject restartButton;

    public float initialResourceSpawnRate = 1;
    public float initialBarDepletionRate = 1.5f;
    public float initialRateRange = 0.5f;

    public float alertQuantity = 25;

    public Color scaryColor;
    public float colorSmooth = 0.5f;

    private Color startColor;

    private const string planetTag = "planet";
    private const string societyBarTag = "barbox";

    private GameObject planet;
    private GameObject[] societyBars;

    private bool scienceIsLow = false;
    private bool econIsLow = false;

    private float econAverage = 0;
    private int econsFound = 0;

    void Awake()
    {
        planet = GameObject.FindGameObjectWithTag(planetTag);
        societyBars = GameObject.FindGameObjectsWithTag(societyBarTag);

        planet.GetComponent<IntelligentPlanet>().setSpawnRate(initialResourceSpawnRate);

        

        foreach (GameObject obj in societyBars)
        {
            obj.GetComponent<SocietyBar>().setRateOfChange(initialBarDepletionRate);
            obj.GetComponent<SocietyBar>().setRateRange(initialRateRange);
        }
    }

    public bool isScienceLow()
    {
        return scienceIsLow;
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject obj in societyBars)
        {
            float resources = obj.GetComponent<SocietyBar>().getResources();

            if (resources <= 0)
            {
                if (this.GetComponent<ScienceGuy>().isScienceGuy(obj))
                {
                    this.scienceIsLow = true;
                }
                if (this.GetComponent<EconGuy>().isEconomist(obj))
                {
                    this.econIsLow = true;
                }
                if (this.GetComponent<PoliticsGuy>().isPolitician(obj))
                {
                    Debug.Log("Politicians are in danger");
                    this.GetComponent<PauseGame>().pause();
                    restartButton.SetActive(true);
                }
            }

            if (this.GetComponent<EconGuy>().isEconomist(obj))
            {
                econAverage += resources;
                econsFound++;

                if (econsFound == 3)
                {
                    this.GetComponent<ChangeGlobalRates>().setResourceSpawnRate((2.5f * (1 - (econAverage / 300)) + 0.5f));
                    econAverage = 0;
                    econsFound = 0;
                }
            }
        }
    }
}
