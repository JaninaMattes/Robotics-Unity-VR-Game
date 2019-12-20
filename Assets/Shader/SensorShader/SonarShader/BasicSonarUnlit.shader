Shader "Shader/SensorShader/SonarShader/BasicSonarUnlit"
{
	// Unity properties
	Properties
	{
		_Color("Color", Color) = (1,1,1,1)
		_MainTex("Albedo (RGB)", 2D) = "white" {}
	_Glossiness("Smoothness", Range(0,1)) = 0.5
		_Metallic("Metallic", Range(0,1)) = 0.0
		_SonarOrigin("Sonar Origin", Vector) = (0,0,0,0)
		_SonarDistance("Sonar Distance", Float) = 0
		_SonarWidth("Sonar Width", Float) = 0.1
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

		sampler2D _MainTex;

	struct Input
	{
		float2 uv_MainTex;
		// worldspace of a certain point
		float3 worldPos;
	};

	// Actual Shader properties
	half _Glossiness;
	half _Metallic;
	fixed4 _Color;
	fixed4 _SonarOrigin;
	float _SonarDistance;
	float _SonarWidth;

	// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
	// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
	// #pragma instancing_options assumeuniformscaling
	UNITY_INSTANCING_BUFFER_START(Props)
		// put more per-instance properties here
		UNITY_INSTANCING_BUFFER_END(Props)

		void surf(Input IN, inout SurfaceOutputStandard o)
	{
		// By distracting the distance everything will be inverted
		// The fourth component of the vector "w" measures the time to animate the sonar effect
		float distance = length(IN.worldPos.xyz - _SonarOrigin.xyz) - _SonarDistance * _SonarOrigin.w;
		float halfWidth = _SonarWidth * 0.5;
		// Create two distances
		float lowerDistance = distance - halfWidth;
		float upperDistance = distance + halfWidth;

		// Albedo comes from a texture tinted by color
		fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
		//fixed4 c = fixed4(pow(1 - (abs(distance) / halfWidth), 8),0, 0, 1);
		float ringStrength = pow(1 - (abs(distance) / halfWidth), 8)
			* (lowerDistance < 0 && upperDistance > 0);
		// As the sonar ring goes further away it should get dimmer and fade out
		o.Albedo = ringStrength * c.rgb * (1 - _SonarOrigin.w);
		o.Emission = o.Albedo;
		// Metallic and smoothness come from slider variables
		o.Metallic = _Metallic;
		o.Smoothness = _Glossiness;
		o.Alpha = c.a;
	}
	ENDCG
	}
		FallBack "Diffuse"
}
