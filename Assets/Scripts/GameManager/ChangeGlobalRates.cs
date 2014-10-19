using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChangeGlobalRates : MonoBehaviour
{
    private const string societyBarTag = "barbox";
    private const string planetTag = "planet";
    private GameObject[] societyBars;
    private GameObject planet;

    // Use this for initialization
    void Start()
    {
        societyBars = GameObject.FindGameObjectsWithTag(societyBarTag);
        planet = GameObject.FindGameObjectWithTag(planetTag);
    }

    //bar rate modifiers
    public void increaseBarRate(float toAdd)
    {
        foreach (GameObject obj in societyBars)
        {
            obj.GetComponent<SocietyBar>().increaseRateOfChange(toAdd);
        }
    }
    public void increaseBarRateBy(float toMultiply)
    {
        foreach (GameObject obj in societyBars)
        {
            obj.GetComponent<SocietyBar>().increaseRateOfChangeBy(toMultiply);
        }
    }
    public void changeRateRange(float toAdd)
    {
        foreach (GameObject obj in societyBars)
        {
            obj.GetComponent<SocietyBar>().addToRateRange(toAdd);
        }
    }
    public void changeRateRangeBy(float toMultiply)
    {
        foreach (GameObject obj in societyBars)
        {
            obj.GetComponent<SocietyBar>().multiplyRateRange(toMultiply);
        }
    }
    // end bar rate modifiers

    //planet rate modifiers
    public void changeResourceSpawnRate(float toAdd)
    {
        planet.GetComponent<IntelligentPlanet>().addToSpawnRate(toAdd);
    }
    public void changeResourceSpawnRateBy(float toMultiply)
    {
        planet.GetComponent<IntelligentPlanet>().multiplySpawnRate(toMultiply);
    }
    public void setResourceSpawnRate(float value)
    {
        planet.GetComponent<IntelligentPlanet>().setSpawnRate(value);
    }
    // end planet rate modifiers
}
