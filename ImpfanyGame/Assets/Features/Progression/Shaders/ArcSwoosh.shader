Shader "Impfany/ArcSwoosh"
{
    Properties
    {
        _Color ("Color", Color) = (1, 1, 1, 1)
        _TailColor ("Tail Color", Color) = (1, 1, 1, 0.2)
        [IntRange] _Steps ("Color Steps", Range(2, 8)) = 3
        _TrailLength ("Trail Length", Range(0.05, 1)) = 0.5
        _Thickness ("Thickness", Range(0.05, 1)) = 0.4
        _Skew ("Skew", Range(0, 1)) = 0.2
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
                float _Range;
                float _Angle;
                float _Progress;
                float _TrailLength;
                float _Thickness;
                half4 _TailColor;
                float _Steps;
                float _Skew;
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

                float insideAngle = step(abs(angle), _Angle * 0.5);
                
                float along = angle / _Angle + 0.5;
                float lag = _Skew * (1.0 - radius / _Range);
                float head = _Progress * (1.0 + _TrailLength + _Skew * _Thickness);
                float behind = head - along - lag;
                float insideTrail = step(0.0, behind) * step(behind, _TrailLength);
                
                float tail = 1.0 - behind / _TrailLength;
                float innerRadius = _Range * (1.0 - _Thickness * tail);
                float insideBlade = step(innerRadius, radius) * step(radius, _Range);

                float band = min(floor(tail * _Steps), _Steps - 1.0);
                float shade = band / (_Steps - 1.0);
                half4 color = lerp(_TailColor, _Color, shade);
                color.a *= insideBlade * insideAngle * insideTrail;
                return color;
            }
            ENDHLSL
        }
    }
}