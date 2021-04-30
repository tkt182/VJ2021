Shader "Custom/MixShader" {
    Properties {
        [HideInInspector] _Chan_0 ("Channel 0", 2D) = "black" {}
        [HideInInspector] _Chan_1 ("Channel 1", 2D) = "black" {}
        [HideInInspector] _Chan_2 ("Channel 2", 2D) = "black" {}
        [HideInInspector] _Chan_3 ("Channel 3", 2D) = "black" {}

        [HideInInspector] _Gain_0 ("Gain 0", Range(0, 1)) = 0
        [HideInInspector] _Gain_1 ("Gain 1", Range(0, 1)) = 0
        [HideInInspector] _Gain_2 ("Gain 2", Range(0, 1)) = 0
        [HideInInspector] _Gain_3 ("Gain 3", Range(0, 1)) = 0

        [HideInInspector] _MasterGain ("Master Gain", Range(0, 1)) = 1

    }
    SubShader {

        // No culling or depth
        Cull Off ZWrite Off ZTest Always

        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _Chan_0;
            sampler2D _Chan_1;
            sampler2D _Chan_2;
            sampler2D _Chan_3;
            float _Gain_0;
            float _Gain_1;
            float _Gain_2;
            float _Gain_3;
            float _MasterGain;

            fixed4 frag (v2f i) : SV_Target {
                fixed4 col = fixed4(0,0,0,1);
                col += tex2D(_Chan_0, i.uv) * _Gain_0;
                col += tex2D(_Chan_1, i.uv) * _Gain_1;
                col += tex2D(_Chan_2, i.uv) * _Gain_2;
                col += tex2D(_Chan_3, i.uv) * _Gain_3;

                return col * _MasterGain;
            }
            ENDCG
        }
    }
}
