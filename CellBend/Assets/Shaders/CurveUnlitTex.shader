// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'

Shader "Curve/Unlit Textured"
{
	Properties {
		_MainTex ("Texture", 2D) = "white" {}
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
				float2 uv : TEXCOORD0;
			};

			struct v2f {
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

        	//uniform properties adjustable by controller CS script
	        uniform half3 _CurveOrigin; // origin to start curve
	        uniform half _Curvature; // curve power
	        uniform fixed3 _Scale; // flatt scale
	        uniform half _FlatMargin; // flat margin from origin

       		//standard properties
			sampler2D _MainTex;
			float4 _MainTex_ST;

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
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target {
				fixed4 col = tex2D(_MainTex, i.uv);
				return col;
			}
			ENDCG
		}
	}
}
