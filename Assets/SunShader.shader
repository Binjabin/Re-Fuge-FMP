Shader "Binjabin/SunShader"
{
    Properties
    {
        [HDR] _Color ("Color", Color) = (1,1,1,1)
        _CellDensity ("CellDensity", Float) = 1
        [HDR] _CellColor ("CellColor", Color) = (1,1,1,1)
        _VoronoiTexture("VoronoiTexture", 2D) = "white" {}
        _SolorFlare("SolorFlare", Float) = 3
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0


        struct Input
        {
            float4 color : COLOR;
            float2 uv_MainTex;
        };

        fixed4 _CellColor;
        fixed4 _Color;
        sampler2D _VoronoiTexture;
        float4 _VoronoiTexture_ST;
        float _SolorFlare;
        float _CellDensity;


        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        float2 Unity_RadialShear_float(float2 UV, float2 Center, float Strength, float2 Offset)
        {
            float2 delta = UV - Center;
            float delta2 = dot(delta.xy, delta.xy);
            float2 delta_offset = delta2 * Strength;
            float2 Out = UV + float2(delta.y, -delta.x) * delta_offset + Offset;
            return Out;
        }

        inline float2 unity_voronoi_noise_randomVector(float2 UV, float offset)
        {
            float2x2 m = float2x2(15.27, 47.63, 99.41, 89.98);
            UV = frac(sin(mul(UV, m)) * 46839.32);
            return float2(sin(UV.y * +offset) * 0.5 + 0.5, cos(UV.x * offset) * 0.5 + 0.5);
        }

        float Unity_Voronoi_float(float2 UV, float AngleOffset, float CellDensity)
        {
            float2 g = floor(UV * CellDensity);
            float2 f = frac(UV * CellDensity);
            float t = 8.0;
            float3 res = float3(8.0, 0.0, 0.0);
            float Out;

            for (int y = -1; y <= 1; y++)
            {
                for (int x = -1; x <= 1; x++)
                {
                    float2 lattice = float2(x, y);
                    float2 offset = unity_voronoi_noise_randomVector(lattice + g, AngleOffset);
                    float d = distance(lattice + offset, f);
                    if (d < res.x)
                    {
                        res = float3(d, offset.x, offset.y);
                        Out = res.x;
                        float Cells = res.y;
                    }
                }
            }
            return Out;
        }

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
            float2 shearUV = Unity_RadialShear_float(IN.uv_MainTex, float2(0, 0), float2(1, 1), float2(0, 0));
            float angleOffset = _Time * 30;
            float voronoi = Unity_Voronoi_float(shearUV, angleOffset, _CellDensity);
            float4 powerVoronoi = pow(voronoi, _SolorFlare);
            float4 colorVoronoi = powerVoronoi * _CellColor;
            fixed4 c = _Color + colorVoronoi;
            o.Emission = c;
            // Metallic and smoothness come from slider variables
            //o.Emission = colorVoronoi;


        }
        ENDCG
    }
    FallBack "Diffuse"
}
