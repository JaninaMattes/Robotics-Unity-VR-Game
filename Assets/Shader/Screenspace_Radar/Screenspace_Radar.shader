// Shader created with Shader Forge v1.40 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.40;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,cpap:True,lico:1,lgpr:1,limd:3,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:3138,x:33322,y:32747,varname:node_3138,prsc:2|emission-2878-OUT;n:type:ShaderForge.SFN_SceneDepth,id:8075,x:32217,y:32732,varname:node_8075,prsc:2|UV-2928-UVOUT;n:type:ShaderForge.SFN_ScreenPos,id:2928,x:32029,y:32752,varname:node_2928,prsc:2,sctp:2;n:type:ShaderForge.SFN_Step,id:9510,x:32497,y:32765,varname:node_9510,prsc:2|A-8075-OUT,B-352-OUT;n:type:ShaderForge.SFN_ValueProperty,id:352,x:31991,y:32945,ptovrint:False,ptlb:dist,ptin:_dist,varname:node_352,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:5;n:type:ShaderForge.SFN_Step,id:4228,x:32497,y:32912,varname:node_4228,prsc:2|A-8075-OUT,B-3759-OUT;n:type:ShaderForge.SFN_OneMinus,id:28,x:32649,y:32829,varname:node_28,prsc:2|IN-4228-OUT;n:type:ShaderForge.SFN_Multiply,id:3919,x:32945,y:32807,varname:node_3919,prsc:2|A-9510-OUT,B-28-OUT,C-632-G,D-4923-OUT,E-4509-OUT;n:type:ShaderForge.SFN_Subtract,id:3759,x:32208,y:32945,varname:node_3759,prsc:2|A-352-OUT,B-7157-OUT;n:type:ShaderForge.SFN_ValueProperty,id:7157,x:32002,y:33020,ptovrint:False,ptlb:width,ptin:_width,varname:_node_352_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Tex2d,id:632,x:32334,y:33128,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:node_632,prsc:2,glob:False,taghide:True,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:3,isnm:False;n:type:ShaderForge.SFN_RemapRangeAdvanced,id:4923,x:32441,y:32622,varname:node_4923,prsc:2|IN-8075-OUT,IMIN-3759-OUT,IMAX-352-OUT,OMIN-818-OUT,OMAX-4609-OUT;n:type:ShaderForge.SFN_ValueProperty,id:818,x:32141,y:32584,ptovrint:False,ptlb:node_818,ptin:_node_818,varname:node_818,prsc:2,glob:False,taghide:True,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_ValueProperty,id:4609,x:32157,y:32666,ptovrint:False,ptlb:falloff,ptin:_falloff,varname:node_4609,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_ValueProperty,id:4509,x:32901,y:32970,ptovrint:False,ptlb:strength,ptin:_strength,varname:node_4509,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:2;n:type:ShaderForge.SFN_Color,id:1203,x:32747,y:33115,ptovrint:False,ptlb:Tint,ptin:_Tint,varname:node_1203,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Multiply,id:2878,x:33126,y:32870,varname:node_2878,prsc:2|A-3919-OUT,B-1203-RGB;n:type:ShaderForge.SFN_SceneDepth,id:2866,x:32482,y:32417,varname:node_2866,prsc:2|UV-8661-UVOUT;n:type:ShaderForge.SFN_ScreenPos,id:8661,x:32294,y:32437,varname:node_8661,prsc:2,sctp:2;n:type:ShaderForge.SFN_RemapRangeAdvanced,id:7662,x:32905,y:32394,varname:node_7662,prsc:2|IN-2866-OUT,IMIN-4684-OUT,IMAX-8262-OUT,OMIN-5647-OUT,OMAX-3445-OUT;n:type:ShaderForge.SFN_ValueProperty,id:8262,x:32635,y:32265,ptovrint:False,ptlb:max,ptin:_max,varname:_node_2671_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_ValueProperty,id:3445,x:32701,y:32576,ptovrint:False,ptlb:outputMax,ptin:_outputMax,varname:node_3445,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_Multiply,id:1719,x:33105,y:32572,varname:node_1719,prsc:2|A-7662-OUT,B-632-RGB;n:type:ShaderForge.SFN_ValueProperty,id:4684,x:32599,y:32362,ptovrint:False,ptlb:inputMin,ptin:_inputMin,varname:_outputMin_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_SwitchProperty,id:9191,x:33206,y:32415,ptovrint:False,ptlb:node_9191,ptin:_node_9191,varname:node_9191,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,on:False|A-7662-OUT,B-1719-OUT;n:type:ShaderForge.SFN_ValueProperty,id:5647,x:32655,y:32451,ptovrint:False,ptlb:outputMin,ptin:_outputMin,varname:node_5647,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_Distance,id:9673,x:32371,y:33320,varname:node_9673,prsc:2|A-681-XYZ,B-8650-UVOUT;n:type:ShaderForge.SFN_ObjectPosition,id:681,x:31858,y:33443,varname:node_681,prsc:2;n:type:ShaderForge.SFN_Vector3,id:6654,x:32084,y:33502,varname:node_6654,prsc:2,v1:1,v2:1,v3:1;n:type:ShaderForge.SFN_FragmentPosition,id:1640,x:32195,y:33397,varname:node_1640,prsc:2;n:type:ShaderForge.SFN_ScreenPos,id:8650,x:31886,y:33277,varname:node_8650,prsc:2,sctp:2;n:type:ShaderForge.SFN_ScreenPos,id:8818,x:31817,y:33064,varname:node_8818,prsc:2,sctp:2;proporder:352-7157-632-818-4609-4509-1203-8262-3445-4684-9191-5647;pass:END;sub:END;*/

Shader "Shader Forge/Screenspace_Radar" {
    Properties {
        _dist ("dist", Float ) = 5
        _width ("width", Float ) = 1
        [HideInInspector]_MainTex ("MainTex", 2D) = "bump" {}
        [HideInInspector]_node_818 ("node_818", Float ) = 0
        _falloff ("falloff", Float ) = 1
        _strength ("strength", Float ) = 2
        _Tint ("Tint", Color) = (0.5,0.5,0.5,1)
        _max ("max", Float ) = 0
        _outputMax ("outputMax", Float ) = 0
        _inputMin ("inputMin", Float ) = 0
        [MaterialToggle] _node_9191 ("node_9191", Float ) = 0
        _outputMin ("outputMin", Float ) = 0
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_instancing
            #include "UnityCG.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma target 3.0
            uniform sampler2D _CameraDepthTexture;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            UNITY_INSTANCING_BUFFER_START( Props )
                UNITY_DEFINE_INSTANCED_PROP( float, _dist)
                UNITY_DEFINE_INSTANCED_PROP( float, _width)
                UNITY_DEFINE_INSTANCED_PROP( float, _node_818)
                UNITY_DEFINE_INSTANCED_PROP( float, _falloff)
                UNITY_DEFINE_INSTANCED_PROP( float, _strength)
                UNITY_DEFINE_INSTANCED_PROP( float4, _Tint)
            UNITY_INSTANCING_BUFFER_END( Props )
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float4 projPos : TEXCOORD3;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                UNITY_SETUP_INSTANCE_ID( v );
                UNITY_TRANSFER_INSTANCE_ID( v, o );
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                o.projPos = ComputeScreenPos (o.pos);
                COMPUTE_EYEDEPTH(o.projPos.z);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                UNITY_SETUP_INSTANCE_ID( i );
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
                float2 sceneUVs = (i.projPos.xy / i.projPos.w);
////// Lighting:
////// Emissive:
                float node_8075 = max(0, LinearEyeDepth(SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, sceneUVs.rg)) - _ProjectionParams.g);
                float _dist_var = UNITY_ACCESS_INSTANCED_PROP( Props, _dist );
                float _width_var = UNITY_ACCESS_INSTANCED_PROP( Props, _width );
                float node_3759 = (_dist_var-_width_var);
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float _node_818_var = UNITY_ACCESS_INSTANCED_PROP( Props, _node_818 );
                float _falloff_var = UNITY_ACCESS_INSTANCED_PROP( Props, _falloff );
                float _strength_var = UNITY_ACCESS_INSTANCED_PROP( Props, _strength );
                float4 _Tint_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Tint );
                float3 emissive = ((step(node_8075,_dist_var)*(1.0 - step(node_8075,node_3759))*_MainTex_var.g*(_node_818_var + ( (node_8075 - node_3759) * (_falloff_var - _node_818_var) ) / (_dist_var - node_3759))*_strength_var)*_Tint_var.rgb);
                float3 finalColor = emissive;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
