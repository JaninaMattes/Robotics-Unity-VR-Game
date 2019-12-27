Shader "SensorShader/LidarShader/LidarShader" {
	
	// All Unity related properties
	Properties{
		_MinColor("Color in Minimal (RGB)", Color) = (0, 1, 0, 1)
		_MaxColor("Color in Maxmal (RGB)", Color) = (1, 0, 0, 1)
		_MaxDistance("Max Lidar Distance", Float) = 1100
	}
		SubShader{
			Pass
			{
				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				// For better functionality
				#include "UnityCG.cginc"

				struct appdata {
					float4 vertex : POSITION;
					float4 color: COLOR;
				};

				// Shader properties
			    // Color for the Lidar Dots
				float _MaxDistance;
				float4 _MinColor;
				float4 _MaxColor;

				struct v2f {
					float4 vertex : SV_POSITION;
					float4 col : COLOR;
				};

				// vertex
				v2f vert(appdata v) {
					v2f o;
					o.vertex = UnityObjectToClipPos(v.vertex);
					o.col = v.color;
					return o;
				}

				// framgent
				// calculate distance to color dots accordingly
				float4 frag(v2f o) : COLOR {
					float mag = abs(length(o.vertex)) / (1200); 
					float4 color = lerp(_MinColor, _MaxColor, mag);
					return color;
				}
					ENDCG
				}
		}

		FallBack "Diffuse"
}