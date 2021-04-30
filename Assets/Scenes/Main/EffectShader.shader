Shader "Custom/EffectShader" {
    Properties {
        _MainTex ("Texture", 2D) = "black" {}
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

            sampler2D _MainTex;
            int _effect_0_shiftRGB;
            float _audioValue;

            // Utils
            float2 rot(float2 st, float t) {
                float c = cos(t), s = sin(t);
                return mul(float2x2(c, -s, s, c), st);
            }

            float random(float2 st) {
                return frac(sin(dot(st, float2(329., 4938.))) * 23043.);
            }

            float noise(float2 st) {
                float2 i = floor(st);
                float2 f = frac(st);

                float a = random(i);
                float b = random(i + float2(1.0, 0.0));
                float c = random(i + float2(0.0, 1.0));
                float d = random(i + float2(1.0, 1.0));

                float2 u = f*f*(3.0-2.0*f);
                return lerp(a, b, u.x) +
                        (c - a)* u.y * (1.0 - u.x) +
                        (d - b) * u.x * u.y;
            }

            float3 rgb2hsv(float3 c) {
              float4 K = float4(0.0, -1.0 / 3.0, 2.0 / 3.0, -1.0);
              float4 p = lerp(float4(c.bg, K.wz), float4(c.gb, K.xy), step(c.b, c.g));
              float4 q = lerp(float4(p.xyw, c.r), float4(c.r, p.yzx), step(p.x, c.r));

              float d = q.x - min(q.w, q.y);
              float e = 1.0e-10;
              return float3(abs(q.z + (q.w - q.y) / (6.0 * d + e)), d / (q.x + e), q.x);
            }

            float3 hsv2rgb(float3 c) {
              c = float3(c.x, clamp(c.yz, 0.0, 1.0));
              float4 K = float4(1.0, 2.0 / 3.0, 1.0 / 3.0, 3.0);
              float3 p = abs(frac(c.xxx + K.xyz) * 6.0 - K.www);
              return c.z * lerp(K.xxx, clamp(p - K.xxx, 0.0, 1.0), c.y);
            }

            // Effects
            inline void postRGBShift(inout float2 uv, inout float4 color, in float a) {
                float d = 0.1 * sin(_Time.y * 30000.) * _audioValue * a;
                color.r = tex2D(_MainTex, uv + float2(-d, 0)).r;
                color.g = tex2D(_MainTex, uv + float2(0, 0)).g;
                color.b = tex2D(_MainTex, uv + float2(d, 0)).b;
            }

            fixed4 frag (v2f i) : SV_Target {
                fixed4 col = fixed4(0,0,0,1);
                col = tex2D(_MainTex, i.uv);

                float2 uv = i.uv;
                // Post FX
                if (_effect_0_shiftRGB) { postRGBShift(uv, col, 1.5); }

                return col;
            }
            ENDCG
        }
    }
}
