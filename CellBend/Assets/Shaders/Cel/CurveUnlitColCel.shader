// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'

Shader "Curve/Cel/Colored"
{
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MaxDiffuse("Maximum diffuse", Range(0.0,1.0)) = 0.5
		_Levels("Cel levels", Range(1.0,10.0)) = 3.0
		_Brightness("Brightness",Range(0.0,1.0)) = 0.0

	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 100

		Pass {
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata {
				float4 vertex : POSITION;
				float3 normal : NORMAL;
			};

			struct v2f {
				float4 vertex : SV_POSITION;
				float3 normal : NORMAL;
				float4 posWorld : TEXCOORD1;
			};

        	//uniform properties adjustable by controller CS script
	        uniform half3 _CurveOrigin; // origin to start curve
	        uniform half _Curvature; // curve power
	        uniform fixed3 _Scale; // flatt scale
	        uniform half _FlatMargin; // flat margin from origin

       		//standard properties
			fixed4 _Color;
			half _MaxDiffuse;
			half _Brightness;
			float _Levels;
			float4 _LightColor0;

	        half4 Bend(half4 v){
	       		half4 wpos = mul(unity_ObjectToWorld, v); // world pos from vertex

	       		half2 xzDistance = (wpos.xz - _CurveOrigin.xz) / _Scale.xz; // x and z distance divided by scale for flatting the x or z scale
	       		half dist = length(xzDistance);

	       		dist = max(0, dist - _FlatMargin);

	       		wpos.y -= dist * dist * _Curvature; // calculate y curve

	       		wpos = mul(unity_WorldToObject, wpos); // convert back to object space

	       		return wpos;
	        }
			
			v2f vert (appdata v) {
				//curve
	        	half4 vpos = Bend(v.vertex);
	        	v.vertex = vpos;

				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.posWorld = mul(unity_ObjectToWorld, v.vertex);
				o.normal = mul(float4(v.normal, 0.0), unity_ObjectToWorld).xyz;
				return o;
			}
			
			fixed4 frag (v2f i) : COLOR {
				fixed4 col = _Color;

				float3 normalDirection = normalize(i.normal);
				float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
				float3 viewDirection = normalize(_WorldSpaceCameraPos - i.posWorld.xyz);

				float brightness = dot(lightDirection, normalDirection) + _Brightness;
				float level = floor(brightness * _Levels);
				brightness = level / _Levels;
				float3 diffuseL = _LightColor0.rgb * max(_MaxDiffuse, brightness);

				fixed4 finalColor = fixed4(diffuseL, 1.0)*col;

				return finalColor;
			}
			ENDCG
		}
	}
}
