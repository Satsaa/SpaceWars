Shader "Hidden/Depth Pass"
{
    Properties
    {
        _DepthPow("Depth Power", Float) = 1.0  
    }

    CGINCLUDE

    #include "UnityCG.cginc"

    float _DepthPow;
    sampler2D _CameraDepthTexture;

    half4 do_sum(v2f_img i) : SV_Target
    {
        return (pow(tex2D(_CameraDepthTexture, i.uv.xy), _DepthPow)).xxxx;
    }

    ENDCG

    Subshader
    {
        Pass
        {
            ZTest Always Cull Off ZWrite Off
            CGPROGRAM
            #pragma vertex vert_img
            #pragma fragment do_sum
            ENDCG
        }
    }
}
