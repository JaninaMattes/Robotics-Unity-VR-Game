Shader "Shader/SensorShader/SonarShader/AdvancedRadarShader"
{
	// Unity properties
	Properties
	{
		_Color("Standard Color (RGB)", Color) = (1,1,1,1)
		_PointColor("Point Color (RGB)", Color) = (1, 0, 0, 1)
		_MainTex("Albedo (RGB)", 2D) = "white" {}
		_ImpactSize("Smoothness", Float) = 0.5 // Linear progression
	}
		SubShader
	{
		Tags{ "RenderType" = "Opaque" }
		LOD 200

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows
		// Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0
	    #include "UnityCG.cginc"

		sampler2D _MainTex;

	struct Input
	{
		float2 uv_MainTex;
		// worldspace of a certain point 
		// can be turned into an object position
		float3 worldPos;
	};

	// Actual Shader properties
	fixed4 _Color;
	// Passing Array to Shader
	int _PointsSize;
	fixed4 _Points[50]; // Max amount of Sonarpositions is 50
	fixed4 _PointColor;
	float _ImpactSize;

	// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
	// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
	// #pragma instancing_options assumeuniformscaling
	UNITY_INSTANCING_BUFFER_START(Props)
		// put more per-instance properties here
		UNITY_INSTANCING_BUFFER_END(Props)

		void surf(Input IN, inout SurfaceOutputStandard o)
	{
		// Albedo comes from a texture tinted by color
		fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
		// Passing an Array to make multiple SonarRings possible
		fixed4 emissive = 0;
		float3 objPos = mul(unity_WorldToObject, float4(IN.worldPos, 1)).xyz;

		for (int i = 0; i < _PointsSize; ++i) {
			emissive += max(0, frac(1.0 - max(0, (_Points[i].w * _ImpactSize) - distance(_Points[i].xyz, objPos.xyz)) / _ImpactSize) * (1 - _Points[i].w));
		}
	    
		o.Albedo = c.rgb;
		o.Emission = emissive * _PointColor;
		o.Metallic = 0;
		o.Smoothness = 0;
		o.Alpha = c.a;
	}
	ENDCG
	}
		FallBack "Diffuse"
}
