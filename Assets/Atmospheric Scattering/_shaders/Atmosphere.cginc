#ifndef ATMOSPHERE_CG_INCLUDED
#define ATMOSPHERE_CG_INCLUDED



float scale(float cos, float scaleDepth)
{
	float x = 1.0 - cos;
	return scaleDepth * exp(-0.00287 + x*(0.459 + x*(3.83 + x*(-6.80 + x*5.25))));
}



// Calculates the Rayleigh phase function
float getRayleighPhase(float cos2)
{
	//return 1.0;
	return 0.75 + 0.75*cos2;
}

// Calculates the Mie phase function
float getMiePhase(float cos, float cos2, float g, float g2)
{
	return 1.5 * ((1.0 - g2) / (2.0 + g2)) * (1.0 + cos2) / pow(1.0 + g2 - 2.0*g*cos, 1.5);
}


// Returns the near intersection point of a line and a sphere
float getNearIntersection(float3 pos, float3 ray, float distance2, float radius2)
{
	float B = 2.0 * dot(pos, ray);
	float C = distance2 - radius2;
	float det = max(0.0, B*B - 4.0 * C);
	return 0.5 * (-B - sqrt(det));
}

float4 _CameraPos;
float4 _SpherePos;
float4 _LightDir;
float4 _InvWavelength;
float _CameraHeight;
float _CameraHeight2;
float _OuterRadius;
float _OuterRadius2;
float _InnerRadius;
float _InnerRadius2;
float _KrESun;
float _KmESun;
float _Kr4PI;
float _Km4PI;
float _Scale;
float _ScaleDepth;
float _InvScaleDepth;
float _ScaleOverScaleDepth;

float _GValue;
float _GValue2;

#endif