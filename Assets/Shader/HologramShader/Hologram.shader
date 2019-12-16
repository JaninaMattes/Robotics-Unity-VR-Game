Shader "Shader/Hologram" {
    Properties {
      _RimColor ("Rim Color", Color) = (0,0.5,0.5,0.0)
      _RimPower ("Rim Power", Range(0.5,8.0)) = 3.0

		_SRef("Stencil Ref", Float) = 1
		[Enum(UnityEngine.Rendering.CompareFunction)]	_SComp("Stencil Comp", Float) = 8
		[Enum(UnityEngine.Rendering.StencilOp)]	_SOp("Stencil Op", Float) = 2
    }
    SubShader {
      Tags{"Queue" = "Transparent"}

      //Pass {
       // ZWrite On
        //ColorMask 0
      //}
		Stencil
		{
			Ref[_SRef]
			Comp[_SComp]
			Pass[_SOp]
		}
		


      CGPROGRAM
      
      #pragma surface surf Lambert alpha:fade
      struct Input {
          float3 viewDir;
      };

      float4 _RimColor;
      float _RimPower;
      
      void surf (Input IN, inout SurfaceOutput o) {
          half rim = 1.0 - saturate(dot (normalize(IN.viewDir), o.Normal));
          o.Emission = _RimColor.rgb * pow (rim, _RimPower) * 10;
          o.Alpha = pow (rim, _RimPower);
      }
      ENDCG
    } 
    Fallback "Diffuse"
  }