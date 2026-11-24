Shader "Impfany/ArcSwoosh"
{
    Properties
    {
        _Color ("Color", Color) = (1, 1, 1, 1)
        _TrailLength ("Trail Length", Range(0.05, 1)) = 0.5
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

                float insideRange = step(radius, _Range);
                float insideAngle = step(abs(angle), _Angle * 0.5);
                
                float along = angle / _Angle + 0.5;
                float head = _Progress * (1.0 + _TrailLength);
                float behind = head - along;
                float insideTrail = step(0.0, behind) * step(behind, _TrailLength);

                half4 color = _Color;
                color.a *= insideRange * insideAngle * insideTrail;
                return color;
            }
            ENDHLSL
        }
    }
}