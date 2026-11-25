Shader "Impfany/Shockwave"
{
    Properties
    {
        _Color ("Color", Color) = (1, 1, 1, 0.8)
        _TailColor ("Tail Color", Color) = (1, 1, 1, 0.08)
        [IntRange] _Steps ("Color Steps", Range(2, 8)) = 5
        _TrailLength ("Trail Length", Range(0.05, 1)) = 0.45
        _Front ("Front Softness", Range(0.01, 0.9)) = 0.25
        _EdgeSoftness ("Side Softness (Degrees)", Range(0, 90)) = 20
        _Fade ("Fade Over Distance", Range(0, 1)) = 0.5
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
                half4 _TailColor;
                float _Steps;
                float _TrailLength;
                float _Front;
                float _EdgeSoftness;
                float _Fade;
                float _Range;
                float _Angle;
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
                float angle = degrees(atan2(p.y, p.x));

                // Wie viel Grad der Punkt vom seitlichen Rand des Faechers entfernt ist.
                float toSide = _Angle * 0.5 - abs(angle);
                float insideAngle = step(0.0, toSide);

                // Zum seitlichen Rand hin ausblenden. Bei einem vollen Kreis gibt es keinen Rand.
                float fullCircle = step(359.5, _Angle);
                float sideFade = max(fullCircle, saturate(toSide / max(_EdgeSoftness, 0.001)));

                // 0 = beim Spieler, 1 = an der Range.
                float distance01 = radius / _Range;
                float insideRange = step(distance01, 1.0);

                // Die Wellenfront laeuft vom Spieler bis zur Range und noch um die Schweiflaenge weiter.
                float head = _Progress * (1.0 + _TrailLength);
                float behind = head - distance01;
                float insideTrail = step(0.0, behind) * step(behind, _TrailLength);

                // 1 = an der Front, 0 = am Schweifende.
                float tail = 1.0 - behind / _TrailLength;

                // Weiche Front: ganz vorne schwach, kurz dahinter am staerksten.
                float lead = saturate((1.0 - tail) / _Front);
                float strength = saturate(lead * tail / (1.0 - _Front));

                strength *= sideFade * (1.0 - _Fade * distance01);

                float band = min(floor(strength * _Steps), _Steps - 1.0);
                float shade = band / (_Steps - 1.0);
                half4 color = lerp(_TailColor, _Color, shade);
                color.a *= insideAngle * insideRange * insideTrail;
                return color;
            }
            ENDHLSL
        }
    }
}
