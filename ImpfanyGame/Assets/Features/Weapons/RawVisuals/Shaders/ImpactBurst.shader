Shader "Impfany/ImpactBurst"
{
    Properties
    {
        _Color ("Color", Color) = (1, 1, 1, 1)
        _TailColor ("Center Color", Color) = (1, 1, 1, 0.2)
        _RimColor ("Rim Color", Color) = (1, 1, 1, 1)
        [IntRange] _Steps ("Color Steps", Range(2, 8)) = 3
        _RimWidth ("Rim Width", Range(0, 0.5)) = 0.09
        [IntRange] _Spikes ("Spikes", Range(3, 32)) = 12
        _SpikeDepth ("Spike Depth", Range(0, 0.6)) = 0.18
        _ExpandTime ("Expand Time", Range(0.05, 0.95)) = 0.35
    }

    SubShader
    {
        Tags { "Queue" = "Transparent" "RenderType" = "Transparent" "RenderPipeline" = "UniversalPipeline" }

        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off
        ZWrite Off

        Pass
        {
            HLSLPROGRAM
            #pragma vertex Vert
            #pragma fragment Frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            #define PIXELS_PER_UNIT 32.0
            #define FULL_TURN 6.28318530718

            struct Attributes
            {
                float3 positionOS : POSITION;
            };

            struct Varyings
            {
                float4 positionCS : SV_POSITION;
                float2 localPosition : TEXCOORD0;
            };

            CBUFFER_START(UnityPerMaterial)
                half4 _Color;
                half4 _TailColor;
                half4 _RimColor;
                float _Steps;
                float _RimWidth;
                float _Spikes;
                float _SpikeDepth;
                float _ExpandTime;
                float _Range;
                float _Progress;
            CBUFFER_END

            Varyings Vert(Attributes input)
            {
                Varyings output;
                output.positionCS = TransformObjectToHClip(input.positionOS);
                output.localPosition = input.positionOS.xy;
                return output;
            }

            half4 Frag(Varyings input) : SV_Target
            {
                float2 p = input.localPosition;
                p = (floor(p * PIXELS_PER_UNIT) + 0.5) / PIXELS_PER_UNIT;

                float radius = length(p);

                // Einmal rundherum von 0 bis 1.
                float around = atan2(p.y, p.x) / FULL_TURN + 0.5;

                // Erster Teil der Phase: der Kreis platzt auf (schnell los, bremst am Ende ab).
                float expand = saturate(_Progress / _ExpandTime);
                expand = 1.0 - pow(1.0 - expand, 3.0);

                // Restliche Phase: die Mitte leert sich von innen nach aussen.
                float clear = saturate((_Progress - _ExpandTime) / (1.0 - _ExpandTime));

                // Zickzack rund um den Kreis: 0 = Zackenspitze, 1 = Tal zwischen zwei Zacken.
                float zigzag = abs(frac(around * _Spikes) * 2.0 - 1.0);
                float outer = _Range * expand * (1.0 - _SpikeDepth * zigzag);

                // Wie tief der Punkt innerhalb der Aussenkante liegt.
                float depth = outer - radius;
                float inside = step(0.0, depth);
                float insideRim = inside * step(depth, _RimWidth);

                // 0 = Mitte, 1 = Aussenkante.
                float toEdge = saturate(radius / max(outer, 0.0001));
                float insideFill = inside * step(clear, toEdge);

                float band = min(floor(toEdge * _Steps), _Steps - 1.0);
                float shade = band / (_Steps - 1.0);
                half4 color = lerp(_TailColor, _Color, shade);
                color.a *= insideFill;

                // Die harte Kante liegt immer obenauf.
                color = lerp(color, _RimColor, insideRim);
                return color;
            }
            ENDHLSL
        }
    }
}
