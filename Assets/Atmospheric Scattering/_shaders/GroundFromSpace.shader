Shader "Custom/GroundFromSpace" {
	Properties {
		_CameraPos ("Camera position", Vector) = (20, 20, 0, 1)
		_SpherePos ("Sphere position (center)", Vector) = (0, 0, 0, 1)
		_LightDir ("Light direction", Vector) = (0, 1, 0, 0)
		_InvWavelength ("Light wavelength inverses for R, G and B channels", Vector) = (0.0001, 0.0001, 0.0001, 0)
		_CameraHeight ("Camera height over ground", Float) = 10
		_CameraHeight2 ("Camera height over ground ^ 2", Float) = 100
		_OuterRadius ("Atmosphere radius", Float) = 5.15
		_OuterRadius2 ("Atmosphere radius ^ 2", Float) = 26.5225
		_InnerRadius ("Planet radius", Float) = 5
		_InnerRadius2 ("Planet radius ^ 2", Float) = 25
		_KrESun ("Kr * ESun", Float) = 0.0375
		_KmESun ("Km * ESun", Float) = 0.0225
		_Kr4PI ("Kr * 4 * PI", Float) = 0.0314
		_Km4PI ("Km * 4 * PI", Float) = 0.01884
		_Scale ("Scale: 1 / (AtmRadius - PlanetRadius)", Float) = 6.66
		_ScaleDepth ("Scale depth: altitude with atmosphere's average density", Float) = 0.25
		_InvScaleDepth ("1.0 / _ScaleDepth", Float) = 4
		_ScaleOverScaleDepth ("Scale / ScaleDepth", Float) = 26.66
		_NumSamples ("Number of samples", Float) = 2
		
		_GValue ("G value", Float) = -0.95
		_GValue2 ("G value ^ 2", Float) = 0.9025
		
		_MainTex ("Planet texture", 2D) = "black" {}
		_NightTex ("Planet night texture", 2D) = "black" {}
	}
	SubShader {
		Pass {
			Cull Back
			
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"
			#include "Atmosphere.cginc"
			
			sampler2D _MainTex;
			sampler2D _NightTex;
			
			struct v2f {
			    float4 pos : SV_POSITION;
			   	float3 c0 : COLOR0;			// The Rayleigh color
				float3 c1 : COLOR1;			// The Mie color
				float2 tex : TEXCOORD1;
			};
			
			v2f vert (appdata_base v)
			{
				const float _NumSamples = 2.0f;
				
			    v2f o;
			    
			    // spatial values are transformed to coordinate space, where sphere is centered to origin
			    _CameraPos -= _SpherePos;
			    float3 vertPos = mul (_Object2World, v.vertex).xyz - _SpherePos;
			    
			    // Get the ray from the camera to the vertex, and its length (which is the far point of the ray passing through the atmosphere)
			    float3 ray = vertPos - _CameraPos.xyz;
			    float far = length(ray);
			    ray /= far;
			    
			    float3 sphCenterToVert = normalize(vertPos);
			    
			    // Calculate the closest intersection of the ray with the outer atmosphere (which is the near point of the ray passing through the atmosphere)
				float near = getNearIntersection(_CameraPos, ray, _CameraHeight2, _OuterRadius2);
			    
			    // Calculate the ray's starting position, then calculate its scattering offset
				float3 start = _CameraPos.xyz + ray * near;
				far -= near;
				float depth = exp((_InnerRadius - _OuterRadius) * _InvScaleDepth);
				float cameraAngle = dot(-ray, sphCenterToVert);
				float lightAngle = dot(_LightDir.xyz, sphCenterToVert);
				float cameraScale = scale(cameraAngle, _ScaleDepth);
				float lightScale = scale(lightAngle, _ScaleDepth);
				float cameraOffset = depth * cameraScale;
				float temp = (lightScale + cameraScale);
			    
			    // Initialize the scattering loop variables
				float sampleLength = far / _NumSamples;
				float scaledLength = sampleLength * _Scale;
				float3 sampleRayMove = ray * sampleLength;
				float3 samplePoint = start + sampleRayMove * 0.5;
				
				
				// Now loop through the sample rays
				float3 frontColor = float3(0.0, 0.0, 0.0);
				float3 attenuate;
				for(float i = 0; i < _NumSamples; i += 1.0)
				{
					float height = length(samplePoint);
					float depth = exp(_ScaleOverScaleDepth * (_InnerRadius - height));
					float scatter = depth * temp - cameraOffset;
					attenuate = exp(-scatter * (_InvWavelength * _Kr4PI + _Km4PI));
					frontColor += attenuate * (depth * scaledLength);
					samplePoint += sampleRayMove;
				}

				// Finally, scale the Mie and Rayleigh colors and set up the varying variables for the pixel shader
				o.pos = mul (UNITY_MATRIX_MVP, v.vertex);
				o.c0 = frontColor * (_InvWavelength * _KrESun + _KmESun);
				o.c1 = attenuate;
				o.tex = v.texcoord.xy;
			    
			    return o;
			}
			
			half4 frag (v2f i) : COLOR
			{
				float3 dayColor = i.c1 * tex2D(_MainTex, i.tex);
				
				float3 nightColor = float3(1.0f, 1.0f, 1.0f) - i.c1;
				nightColor *= nightColor;
				nightColor *= nightColor;
				nightColor *= nightColor;
				nightColor *= tex2D(_NightTex, i.tex) * 0.75f;
				
				return float4(i.c0 + dayColor + nightColor, 1.0f);
			}
			ENDCG
		}

	} 
	FallBack "Diffuse"
}
