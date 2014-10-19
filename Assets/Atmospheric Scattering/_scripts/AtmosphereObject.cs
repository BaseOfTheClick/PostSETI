using UnityEngine;
using System.Collections;

public class AtmosphereObject : MonoBehaviour {

	public Material materialFromSpace;
	public Material materialFromAtmosphere;
	
	internal Vector3 lightDir = new Vector3(0, 1, 0);
	
	internal float wavelengthRed;
	internal float wavelengthGreen;
	internal float wavelengthBlue;
	
	internal float innerRadius;
	internal float outerRadius;
	
	internal float kr, km, eSun, gValue;
	
	internal float rayleighScaleDepth;
	
	internal Texture planetTexture = null;
	internal Texture planetNightTexture = null;
	
	private bool constantMaterialPropertyChanged = true;
	
	// Use this for initialization
	void Start () {
				
		SetMaterialProperties(materialFromSpace);
		SetMaterialProperties(materialFromAtmosphere);
		
		if(planetTexture != null)
		{
			materialFromSpace.mainTexture = planetTexture;
			materialFromAtmosphere.mainTexture = planetTexture;
		}
		if(planetNightTexture != null)
		{
			materialFromSpace.SetTexture("_NightTex", planetNightTexture);
			materialFromAtmosphere.SetTexture("_NightTex", planetNightTexture);	
		}
	}
	
	// Update is called once per frame
	void Update () {
				
		Vector3 cameraPos = Camera.mainCamera.transform.position;
		Vector3 camToSphCenter = this.transform.position - cameraPos;
		
		bool materialChanged = false;
		
		Material prevMaterial = this.renderer.sharedMaterial;
		
		if(camToSphCenter.magnitude < outerRadius)
			this.renderer.sharedMaterial = materialFromAtmosphere;
		else
			this.renderer.sharedMaterial = materialFromSpace;
		
		if(prevMaterial != this.renderer.sharedMaterial)
				materialChanged = true;
		
		SetMaterialProperties(this.renderer.sharedMaterial);
		if(constantMaterialPropertyChanged || materialChanged) SetConstantMaterialProperties(this.renderer.sharedMaterial);
		
		constantMaterialPropertyChanged = false;
	}
	
	void SetConstantMaterialProperties(Material material)
	{
		material.SetVector("_InvWavelength", new Vector3(1f / Mathf.Pow(wavelengthRed, 4.0f),
		                                                 1f / Mathf.Pow(wavelengthGreen, 4.0f),
		                                                 1f / Mathf.Pow(wavelengthBlue, 4.0f)));
		material.SetFloat("_InnerRadius", innerRadius);
		material.SetFloat("_InnerRadius2", innerRadius * innerRadius);
		material.SetFloat("_OuterRadius", outerRadius);
		material.SetFloat("_OuterRadius2", outerRadius * outerRadius);
		material.SetFloat("_KrESun", kr * eSun);
		material.SetFloat("_KmESun", km * eSun);
		material.SetFloat("_Km4PI", km * 4.0f * Mathf.PI);
		material.SetFloat("_Kr4PI", kr * 4.0f * Mathf.PI);
		material.SetFloat("_Scale", 1f / (outerRadius - innerRadius));
		material.SetFloat("_ScaleDepth", rayleighScaleDepth);
		material.SetFloat("_InvScaleDepth", 1f / rayleighScaleDepth);
		material.SetFloat("_ScaleOverScaleDepth", 1.0f / ( (outerRadius - innerRadius) * rayleighScaleDepth) );
		material.SetFloat("_GValue", gValue);
		material.SetFloat("_GValue2", gValue * gValue);
	}
	
	void SetMaterialProperties(Material material)
	{
		Vector3 cameraPos = Camera.mainCamera.transform.position;
		Vector3 camToSphCenter = this.transform.position - cameraPos;
		
		material.SetVector("_CameraPos", cameraPos);
		material.SetVector("_SpherePos", this.transform.position);
		material.SetFloat("_CameraHeight", camToSphCenter.magnitude);
		material.SetFloat("_CameraHeight2",camToSphCenter.sqrMagnitude);
		material.SetVector("_LightDir", lightDir.normalized);
	}
	
	public void SetLightDirection(Vector3 lightDirection)
	{
		if(lightDir != lightDirection)
		{
			lightDir = lightDirection;
		}
	}
	
	public void SetLightWavelength(float waveRed, float waveGreen, float waveBlue)
	{
		if(wavelengthRed != waveRed || wavelengthGreen != waveGreen || wavelengthBlue != waveBlue)
		{
			wavelengthRed = waveRed;
			wavelengthGreen = waveGreen;
			wavelengthBlue = waveBlue;

			constantMaterialPropertyChanged = true;
		}
	}
	
	public void SetInnerRadius(float radius)
	{
		if(innerRadius != radius)
		{
			innerRadius = radius;
			constantMaterialPropertyChanged = true;
		}
	}
	
	public void SetOuterRadius(float radius)
	{
		if(outerRadius != radius)
		{
			outerRadius = radius;
			constantMaterialPropertyChanged = true;
		}
	}
	
	public void SetRayleighScatteringConstant(float constant)
	{
		if(kr != constant)
		{
			kr = constant;
			constantMaterialPropertyChanged = true;
		}
	}
	
	public void SetMieScatteringConstant(float constant)
	{
		if(km != constant)
		{
			km = constant;
			constantMaterialPropertyChanged = true;
		}
	}
	
	public void SetSunIntensity(float sun)
	{
		if(eSun != sun)
		{
			eSun = sun;
			constantMaterialPropertyChanged = true;
		}
	}
	
	public void SetGValue(float g)
	{
		if(gValue != g)
		{
			gValue = g;
			constantMaterialPropertyChanged = true;
		}
	}
	
	public void SetRayleighScaleDepth(float scaleDepth)
	{
		if(rayleighScaleDepth != scaleDepth)
		{
			rayleighScaleDepth = scaleDepth;	
			constantMaterialPropertyChanged = true;
		}
	}
	
	public void SetPlanetDayTexture(Texture texture)
	{
		if(planetTexture != texture)
		{
			planetTexture = texture;
			materialFromSpace.mainTexture = planetTexture;
			materialFromAtmosphere.mainTexture = planetTexture;
		}
	}
	
	public void SetPlanetNightTexture(Texture texture)
	{
		if(planetNightTexture != texture)
		{
			planetNightTexture = texture;
			materialFromSpace.SetTexture("_NightTex", planetNightTexture);
			materialFromAtmosphere.SetTexture("_NightTex", planetNightTexture);	
		}
	}
}
