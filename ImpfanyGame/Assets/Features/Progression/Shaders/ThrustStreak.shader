Shader "Impfany/ThrustStreak"
{
    Properties
    {
        _Color ("Color", Color) = (1, 1, 1, 1)
        _TailColor ("Tail Color", Color) = (1, 1, 1, 0.2)
        [IntRange] _Steps ("Color Steps", Range(2, 8)) = 3
        _TrailLength ("Trail Length", Range(0.05, 1)) = 0.5
        _Width ("Width", Range(0.03, 2)) = 0.25
        _Tip ("Tip Length", Range(0.01, 0.9)) = 0.2
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
                float _Width;
                float _Tip;
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

                // Wie weit vorne der Punkt liegt (0 = Waffe, 1 = Range) und wie weit seitlich.
                float forward = p.x / _Range;
                float side = abs(p.y);

                float insideRange = step(0.0, p.x) * step(p.x, _Range);

                // Die Spitze läuft von der Waffe bis zur Range und noch um die Schweiflänge weiter.
                float head = _Progress * (1.0 + _TrailLength);
                float behind = head - forward;
                float insideTrail = step(0.0, behind) * step(behind, _TrailLength);

                // 1 = an der Spitze, 0 = am Schweifende.
                float tail = 1.0 - behind / _TrailLength;

                // Vorne spitz, kurz dahinter am breitesten, nach hinten wieder schmal.
                float shape = min((1.0 - tail) / _Tip, tail) * (1.0 + _Tip);
                float insideStreak = step(side, _Width * 0.5 * shape);

                float band = min(floor(tail * _Steps), _Steps - 1.0);
                float shade = band / (_Steps - 1.0);
                half4 color = lerp(_TailColor, _Color, shade);
                color.a *= insideRange * insideTrail * insideStreak;
                return color;
            }
            ENDHLSL
        }
    }
}
