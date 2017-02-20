Shader "Curve/Cel/Unlit textured color" {
	Properties {
		_Color ("Main Color", Color) = (1,1,1,1)
		_MainTex("Texture", 2D) = "white" {}
		_MaxDiffuse("MaxDiffuse", Range(0.0,1.0)) = 0.5
		_Levels ("Cel levels" , Range(1.0, 10.0)) = 3.0
		_Brighten("Brighten" , Range(0.0, 1.0)) = 0.0
    }
    SubShader {
   	 Tags{ "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" }
   	 ZWrite Off
   	 LOD 200

   	 Pass {
   		 CGPROGRAM
   		 #pragma vertex vert
   		 #pragma fragment frag
   		 
   		 #include "UnityCG.cginc"


   		 struct appdata {
   			 float4 pos : POSITION;
   			 float3 normal : NORMAL;
   			 float2 texcoord : TEXCOORD0;
   		 };


   		 struct v2f {
   			 float4 pos : SV_POSITION;
   			 float3 normal : NORMAL;
   			 float2 texcoord : TEXCOORD0;
   			 float4 posWorld : TEXCOORD1;
   		 };


   		 sampler2D _MainTex;
   		 float4 _MainTex_ST;
   		 fixed4 _Color;


   		 float _MaxDiffuse;
   		 float4 _LightColor0;
   		 float _Levels;
   		 float _Brighten;


   		 
   		 v2f vert (appdata v) {
   			 v2f o;
   			 o.pos = mul(UNITY_MATRIX_MVP, v.pos);
   			 o.posWorld = mul(unity_ObjectToWorld, v.pos);
   			 o.normal = mul(float4(v.normal, 0.0), unity_ObjectToWorld).xyz;
   			 o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
   			 return o;
   		 }
   		 
   		 float4 frag (v2f i) : COLOR {
   			 float4 tex = tex2D(_MainTex, i.texcoord) * _Color;

   			 float3 normalDirection = normalize(i.normal);
   			 float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
   			 float3 viewDirection = normalize(_WorldSpaceCameraPos - i.posWorld.xyz);


   			 float brightness = dot(lightDirection, -viewDirection) + _Brighten;
   			 float level = floor(brightness * _Levels);
   			 brightness = level / _Levels;
   			 float3 diffuse = _LightColor0.rgb * max(_MaxDiffuse, brightness);

   			 float4 finalColor = float4(diffuse, 1.0) * tex;

   			 return finalColor;
   		 }
   		 ENDCG
   	 }
    }
}


