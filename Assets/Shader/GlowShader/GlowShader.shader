Shader "Shader/GlowShader"
{
	Properties
	{
		_Color("Color", Color) = (1,1,1,1)
		_MainTex("Albedo (RGB)", 2D) = "white" {}
		_BumpMap("Normal Map", 2D) = "bump" {}
		_RimColor("Rim Color", Color) = (1,1,1,1) //creates color on the edges
		_RimPower("Rim Power", Range (1.0, 6.0)) = 3.0 //range that can be adjusted
	}
		// defines the subshaders in case a shader ain't supported by a spec. platform
		SubShader
		{
			Tags { "RenderType" = "Opaque" }

			//CGPROGRAM is a Nvidia language
			CGPROGRAM
			// Physically based Standard lighting model, and enable shadows on all light types
			#pragma surface surf Standard fullforwardshadows

			// Use shader model 3.0 target, to get nicer looking lighting
			#pragma target 3.0

			struct Input
			{
				float4 color: Color;
				float2 uv_MainTex;
				float2 uv_BumpMap;
				float3 viewDir; // x, y, z direction for camera
			};

			float4 _Color;
			sampler2D _MainTex;
			sampler2D _BumpMap;
			float4 _RimColor;
			float _RimPower;

			// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
			// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
			// #pragma instancing_options assumeuniformscaling
			UNITY_INSTANCING_BUFFER_START(Props)
				// put more per-instance properties here
				UNITY_INSTANCING_BUFFER_END(Props)

				void surf(Input IN, inout SurfaceOutputStandard o)
			{
				// color definition
				IN.color = _Color;
				o.Albedo = tex2D(_MainTex, IN.uv_MainTex).rgb * IN.color;
				o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));

				// create the rim effect 
				half rim = 1.0 - saturate(dot(normalize(IN.viewDir), o.Normal));
				o.Emission = _RimColor.rgb * pow(rim, _RimPower);
			}
			ENDCG
		}
			// defines the fallback shader in case this ain't supported
			FallBack "Diffuse"
}
