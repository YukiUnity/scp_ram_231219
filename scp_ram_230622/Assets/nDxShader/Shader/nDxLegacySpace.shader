// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/

Shader "nDxShader/nDxLegacySpace" {
    Properties {
        _Color ("Color", Color) = (0.4039216,0.4039216,0.4039216,1)
        _Frequency ("Frequency", Float ) = 64
        _Velocity ("Velocity", Range(0, 0.1)) = 0.002
        _W ("W", Range(0.001, 0.5)) = 0.5
        _H ("H", Range(0.001, 0.5)) = 0.5
        _Spin ("Spin", Range(0, 10)) = 10
        [MaterialToggle] _BitCrush ("BitCrush", Float ) = 0.0085
        _NoiseA ("Noise(A)", 2D) = "white" {}
        _TransparencyA ("Transparency(A)", Range(0, 1)) = 0.15
        _ThresholdA ("Threshold(A)", Range(0, 10)) = 5
        _PowerA ("Power(A)", Range(0, 10)) = 0.4
        _VCMultiplyA ("VC Multiply(A)", Range(0.1, 1)) = 0.5
        _Refraction ("Refraction", Range(0, 1)) = 0.03
        [MaterialToggle] _Scroll ("Scroll", Float ) = 1
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        GrabPass{ }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            Cull Off
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 metal xboxone ps4 
            #pragma target 3.0
            uniform sampler2D _GrabTexture;
            uniform float4 _Color;
            uniform float _Frequency;
            uniform float _Velocity;
            uniform float _W;
            uniform float _Spin;
            uniform float _H;
            uniform float _ThresholdA;
            uniform float _PowerA;
            uniform float _VCMultiplyA;
            uniform float _Refraction;
            uniform float _TransparencyA;
            uniform sampler2D _NoiseA; uniform float4 _NoiseA_ST;
            uniform fixed _Scroll;
            uniform fixed _BitCrush;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float4 vertexColor : COLOR;
                float4 projPos : TEXCOORD3;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                float4 node_4507 = _Time;
                float node_7865_ang = node_4507.a;
                float node_7865_spd = _Spin;
                float node_7865_cos = cos(node_7865_spd*node_7865_ang);
                float node_7865_sin = sin(node_7865_spd*node_7865_ang);
                float2 node_7865_piv = float2(0.5,0.5);
                float node_490 = ((_W*_H)/2.0);
                float2 node_6064_tc_rcp = float2(1.0,1.0)/float2( _W, _H );
                float node_6064_ty = floor(node_490 * node_6064_tc_rcp.x);
                float node_6064_tx = node_490 - _W * node_6064_ty;
                float4 node_6238 = _Time;
                float3 node_546 = cos((o.vertexColor.rgb*(node_6238.g*_Frequency)));
                float2 node_6064 = ((node_546+node_546).rg + float2(node_6064_tx, node_6064_ty)) * node_6064_tc_rcp;
                float2 node_7865 = (mul(node_6064-node_7865_piv,float2x2( node_7865_cos, -node_7865_sin, node_7865_sin, node_7865_cos))+node_7865_piv);
                float2 node_2000 = (node_7865*_Velocity);
                v.vertex.xyz += float3(lerp( node_2000, ((floor((clamp(node_2000,0,1)*5.0))/5.0)*2.0), _BitCrush ),0.0);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                o.projPos = ComputeScreenPos (o.pos);
                COMPUTE_EYEDEPTH(o.projPos.z);
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                i.normalDir = normalize(i.normalDir);
                i.normalDir *= faceSign;
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float node_7978 = (dot(viewDirection,i.normalDir).r*_Refraction);
                float2 sceneUVs = (i.projPos.xy / i.projPos.w) + float2(node_7978,node_7978);
                float4 sceneColor = tex2D(_GrabTexture, sceneUVs);
////// Lighting:
////// Emissive:
                float4 node_9291 = _Time;
                float node_5329 = 10.0;
                float node_8912 = 0.1;
                float2 node_388_tc_rcp = float2(1.0,1.0)/float2( node_5329, node_8912 );
                float node_388_ty = floor(node_8912 * node_388_tc_rcp.x);
                float node_388_tx = node_8912 - node_5329 * node_388_ty;
                float2 node_388 = (i.uv0 + float2(node_388_tx, node_388_ty)) * node_388_tc_rcp;
                float2 node_1942 = (node_388+node_9291.g*float2(0,1));
                float4 node_7101 = tex2D(_NoiseA,TRANSFORM_TEX(node_1942, _NoiseA));
                float3 node_3906 = pow(i.vertexColor.rgb,_PowerA).rgb;
                float3 emissive = (lerp( 1.0, node_7101.rgb, _Scroll )*(pow((saturate(node_3906.r)*saturate(node_3906.g)),_VCMultiplyA)*_Color.rgb));
                float3 finalColor = emissive;
                float4 node_6562 = _Time;
                float node_4029 = 100.0;
                float2 node_7953_tc_rcp = float2(1.0,1.0)/float2( node_4029, node_4029 );
                float node_7953_ty = floor(node_4029 * node_7953_tc_rcp.x);
                float node_7953_tx = node_4029 - node_4029 * node_7953_ty;
                float2 node_7953 = (i.uv0 + float2(node_7953_tx, node_7953_ty)) * node_7953_tc_rcp;
                float2 node_8697 = (node_7953+node_6562.a*float2(1,1));
                float4 node_7221 = tex2D(_NoiseA,TRANSFORM_TEX(node_8697, _NoiseA));
                return fixed4(lerp(sceneColor.rgb, finalColor,(clamp(((i.vertexColor.rgb.rgb*node_7221.rgb).r*_ThresholdA),0.4,1.0)*_TransparencyA)),1);
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Offset 1, 1
            Cull Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCASTER
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 metal xboxone ps4 
            #pragma target 3.0
            uniform float _Frequency;
            uniform float _Velocity;
            uniform float _W;
            uniform float _Spin;
            uniform float _H;
            uniform fixed _BitCrush;
            struct VertexInput {
                float4 vertex : POSITION;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
                float4 vertexColor : COLOR;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.vertexColor = v.vertexColor;
                float4 node_4507 = _Time;
                float node_7865_ang = node_4507.a;
                float node_7865_spd = _Spin;
                float node_7865_cos = cos(node_7865_spd*node_7865_ang);
                float node_7865_sin = sin(node_7865_spd*node_7865_ang);
                float2 node_7865_piv = float2(0.5,0.5);
                float node_490 = ((_W*_H)/2.0);
                float2 node_6064_tc_rcp = float2(1.0,1.0)/float2( _W, _H );
                float node_6064_ty = floor(node_490 * node_6064_tc_rcp.x);
                float node_6064_tx = node_490 - _W * node_6064_ty;
                float4 node_6238 = _Time;
                float3 node_546 = cos((o.vertexColor.rgb*(node_6238.g*_Frequency)));
                float2 node_6064 = ((node_546+node_546).rg + float2(node_6064_tx, node_6064_ty)) * node_6064_tc_rcp;
                float2 node_7865 = (mul(node_6064-node_7865_piv,float2x2( node_7865_cos, -node_7865_sin, node_7865_sin, node_7865_cos))+node_7865_piv);
                float2 node_2000 = (node_7865*_Velocity);
                v.vertex.xyz += float3(lerp( node_2000, ((floor((clamp(node_2000,0,1)*5.0))/5.0)*2.0), _BitCrush ),0.0);
                o.pos = UnityObjectToClipPos( v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
