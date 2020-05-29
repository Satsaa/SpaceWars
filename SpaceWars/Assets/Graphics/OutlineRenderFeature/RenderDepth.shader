Shader "Hidden/Render Depth"
{
    Properties
    {
    }

    CGINCLUDE

    #include "UnityCG.cginc"

    float _DepthMult;
    sampler2D _CameraDepthTexture;
    float4 _MainTex_TexelSize;

    // Separable Gaussian filters
    half4 get_depth(v2f_img i) : SV_Target
    {
        return tex2D(_CameraDepthTexture, i.uv.xy);
    }

    ENDCG

    Subshader
    {
        Pass
        {
            ZTest Always Cull Off ZWrite Off
            CGPROGRAM
            #pragma vertex vert_img
            #pragma fragment get_depth
            ENDCG
        }
    }
}
