
Shader "Shader/GridShader/GridShaderTEST" {
	Properties{
		_GridThickness("Grid Thickness", Float) = 0.1
		_GridSpacing("Grid Spacing", Float) = 0.5
		_GridColour("Grid Colour", Color) = (0.5, 1.0, 1.0, 1.0)
		_BaseColour("Base Colour", Color) = (0.0, 0.0, 0.0, 0.0)
		_Radius("Sphere Radius", Range(0, 1)) = 0.0
		_Position("Player Position", Vector) = (0, 0, 0, 0)
		_Softness("Sphere Softness", Range(0,100)) = 0
	}

		SubShader{
		Tags{ "RenderType" = "Opaque" "Queue" = "Transparent" }
		LOD 100

		Pass{
			ZWrite Off
			Blend SrcAlpha OneMinusSrcAlpha

			CGPROGRAM

			// Define the vertex and fragment shader functions
			#pragma vertex vert
			#pragma fragment frag
			// for important shader functions
			#include "UnityCG.cginc"

			// Access Shaderlab properties
			float _GridThickness;
			float _GridSpacing;
			float4 _GridColour;
			float4 _BaseColour;

			//Spherical Mask
			float4 _Position;
			half _Radius;
			half _Softness;

			// Input into the vertex shader
			struct vertexInput {
				float4 vertex : POSITION;
			};

			// Output from vertex shader into fragment shader
			struct vertexOutput {
				float4 pos : SV_POSITION;
				float4 worldPos : TEXCOORD0;
			};
		
			// VERTEX SHADER
			vertexOutput vert(vertexInput input) {
				vertexOutput output;
				output.pos = UnityObjectToClipPos(input.vertex);
				// Calculate the world position coordinates to pass to the fragment shader
				output.worldPos = mul(unity_ObjectToWorld, input.vertex);

				output.worldPos.x -= _GridThickness / 2;
				output.worldPos.z -= _GridThickness / 2;

				return output;
			}

			// FRAGMENT SHADER
			float4 frag(vertexOutput input) : COLOR{
			half d = distance(_Position, _Position);
			// Black color
			fixed3 black = fixed3(0, 0, 0);

			half sum = saturate((d - _Radius) / -_Softness);
			fixed4 lerpColor = lerp(fixed4(black, 1), _GridColour, sum);

				if (frac(input.worldPos.x / _GridSpacing) < _GridThickness || frac(input.worldPos.z / _GridSpacing) < _GridThickness) {
					return lerpColor;
				}
				else {
					return _BaseColour;
				}
			}
		ENDCG
		}
	}

		Fallback "Diffuse"
}