Shader "Impfany/ImpactBurst"
{
    Properties
    {
        _Color ("Center Color", Color) = (0.72, 0.72, 0.72, 0.1)
        _EdgeColor ("Edge Color", Color) = (0.5, 0.5, 0.5, 0.28)
        _RimColor ("Rim Color", Color) = (0.72, 0.72, 0.72, 0.5)
        [IntRange] _Steps ("Color Steps", Range(2, 16)) = 8
        _RimWidth ("Rim Width", Range(0, 0.5)) = 0.0625
        _ExpandTime ("Expand Time", Range(0.01, 0.95)) = 0.08
        _FadeSoftness ("Fade Softness", Range(0.01, 1)) = 0.5
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
                half4 _EdgeColor;
                half4 _RimColor;
                float _Steps;
                float _RimWidth;
                float _ExpandTime;
                float _FadeSoftness;
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

                // Erster Teil der Phase: der Kreis waechst von der Mitte bis zur Range
                // (schnell los, bremst am Ende ab).
                float expand = saturate(_Progress / _ExpandTime);
                expand = 1.0 - pow(1.0 - expand, 3.0);
                float outer = _Range * expand;

                // Restliche Phase: laeuft von 0 auf 1 und blendet die Flaeche aus.
                float clear = saturate((_Progress - _ExpandTime) / (1.0 - _ExpandTime));

                // Wie tief der Punkt innerhalb der Aussenkante liegt.
                float depth = outer - radius;
                float inside = step(0.0, depth);
                float insideRim = inside * step(depth, _RimWidth);

                // 0 = Mitte, 1 = Aussenkante.
                float toEdge = saturate(radius / max(outer, 0.0001));

                // Krater: zur Kante hin in Stufen dunkler.
                float band = min(floor(toEdge * _Steps), _Steps - 1.0);
                float shade = band / (_Steps - 1.0);
                half4 color = lerp(_Color, _EdgeColor, shade);

                // Ausblenden von der Mitte nach aussen, in Stufen statt hartem Loch.
                float fade = saturate((toEdge + _FadeSoftness - clear * (1.0 + _FadeSoftness)) / _FadeSoftness);
                fade = ceil(fade * _Steps) / _Steps;
                color.a *= inside * fade;

                // Die harte Kontur bleibt bis zum Ende der Phase stehen.
                color = lerp(color, _RimColor, insideRim);
                return color;
            }
            ENDHLSL
        }
    }
}
