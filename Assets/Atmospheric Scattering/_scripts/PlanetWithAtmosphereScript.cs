using UnityEngine;
using System.Collections;

public class PlanetWithAtmosphereScript : MonoBehaviour {
	
	public AtmosphereObject planet = null;
	public AtmosphereObject atmosphere = null;
	
	public Vector3 lightDirection = new Vector3(0, 1, 0);
	
	public float wavelengthRed;
	public float wavelengthGreen;
	public float wavelengthBlue;
	
	private float innerRadius;
	private float outerRadius;
	
	public float rayleighScatteringConstant, mieScatteringConstant, sunIntensity, gValue;
	
	public float rayleighScaleDepth;
	
	public Texture planetDayTexture = null;
	public Texture planetNightTexture = null;
	
	// Use this for initialization
	void Start () {
		
		SetAtmosphereObjectValues();
	}
	
	// Update is called once per frame
	void Update () {
		
		innerRadius = 49.2126f * this.transform.localScale.x;
		outerRadius = 50.44292f * this.transform.localScale.x;
		SetAtmosphereObjectValues();
	}
	
	void SetAtmosphereObjectValues() {
		
		planet.SetPlanetDayTexture(planetDayTexture);
		planet.SetPlanetNightTexture(planetNightTexture);
		
		planet.SetLightDirection(lightDirection);
		planet.SetLightWavelength(wavelengthRed, wavelengthGreen, wavelengthBlue);
		planet.SetInnerRadius(innerRadius);
		planet.SetOuterRadius(outerRadius);
		planet.SetRayleighScatteringConstant(rayleighScatteringConstant);
		planet.SetMieScatteringConstant(mieScatteringConstant);
		planet.SetSunIntensity(sunIntensity);
		planet.SetGValue(gValue);
		planet.SetRayleighScaleDepth(rayleighScaleDepth);
		
		atmosphere.SetLightDirection(lightDirection);
		atmosphere.SetLightWavelength(wavelengthRed, wavelengthGreen, wavelengthBlue);
		atmosphere.SetInnerRadius(innerRadius);
		atmosphere.SetOuterRadius(outerRadius);
		atmosphere.SetRayleighScatteringConstant(rayleighScatteringConstant);
		atmosphere.SetMieScatteringConstant(mieScatteringConstant);
		atmosphere.SetSunIntensity(sunIntensity);
		atmosphere.SetGValue(gValue);
		atmosphere.SetRayleighScaleDepth(rayleighScaleDepth);
		
	}
}
