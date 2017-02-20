// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'

Shader "Curve/Standard surface" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Lambert vertex:vert addshadow
		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0
 
        //uniform properties adjustable by controller CS script
        uniform half3 _CurveOrigin; // origin to start curve
        uniform half _Curvature; // curve power
        uniform fixed3 _Scale; // flatt scale
        uniform half _FlatMargin; // flat margin from origin

        //standard properties
        sampler2D _MainTex;
		fixed4 _Color;
 
        struct Input {
            float2 uv_MainTex;
        };

        half4 Bend(half4 v){
       		half4 wpos = mul(unity_ObjectToWorld, v); // world pos from vertex

       		half2 xzDistance = (wpos.xz - _CurveOrigin.xz) / _Scale.xz; // x and z distance divided by scale for flatting the x or z scale
       		half dist = length(xzDistance);

       		dist = max(0, dist - _FlatMargin);

       		wpos.y -= dist * dist * _Curvature; // calculate y curve

       		wpos = mul(unity_WorldToObject, wpos); // convert back to object space

       		return wpos;
        }
 
        void vert( inout appdata_full v) {
        	half4 vpos = Bend(v.vertex);

        	v.vertex = vpos;
        }

		void surf (Input IN, inout SurfaceOutput o) {
			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
