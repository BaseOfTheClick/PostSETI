using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/**
Add class description please
*/
public class IntelligentPlanet : MonoBehaviour
{
    //rate of the change of the resources on this planet
    public float spawnRate;

    //speed of the planet orbit
    public float orbitSpeed = 1f;

    //number of resources on the planet
    [SerializeField]
    private int resources;

    public void Start()
    {
        this.GetComponent<ResourceSpawner>().timeInterval = this.spawnRate;
    }

    //default constructor
    //does nothing
    public IntelligentPlanet() { }

    //gives default values to fields
    public IntelligentPlanet(float rateOfChange, int resources)
    {
        this.spawnRate = rateOfChange;
        this.resources = resources;
    }

    public float getSpawnRate()
    {
        return spawnRate;
    }

    public int getResources()
    {
        return resources;
    }

    public void setResources(int res)
    {
        this.resources = res;
    }

    public void setSpawnRate(float rateOfChange)
    {
        this.spawnRate = rateOfChange;
        this.GetComponent<ResourceSpawner>().timeInterval = this.spawnRate;
    }
    public void addToSpawnRate(float toAdd)
    {
        this.spawnRate += toAdd;
        this.GetComponent<ResourceSpawner>().timeInterval = this.spawnRate;
    }
    public void multiplySpawnRate(float toMultiply)
    {
        this.spawnRate *= toMultiply;
        this.GetComponent<ResourceSpawner>().timeInterval = this.spawnRate;
    }
    public int takeResources(int packageSize)
    {
        resources -= packageSize;

        return packageSize;
    }

    public void spinPlanet()
    {
        this.GetComponent<CameraController>().DoCamOrbit(orbitSpeed * Time.deltaTime, 0.0f);
    }

    public void Update()
    {
        spinPlanet();
    }
}