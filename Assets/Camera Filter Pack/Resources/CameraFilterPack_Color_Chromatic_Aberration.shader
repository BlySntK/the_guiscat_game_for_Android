////////////////////////////////////////////////////////////////////////////////////
//  CameraFilterPack v2.0 - by VETASOFT 2015 //////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////

Shader "CameraFilterPack/Color_Chromatic_Aberration" {
	Properties 
	{
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_TimeX ("Time", Range(0.0, 1.0)) = 1.0
		_Distortion ("_Distortion", Range(-0.02, 0.02)) = 0.02
		_ScreenResolution ("_ScreenResolution", Vector) = (0.,0.,0.,0.)
	}
	SubShader 
	{
		Pass
		{
			ZTest Always
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma fragmentoption ARB_precision_hint_fastest
			#pragma target 3.0
			#include "UnityCG.cginc"
			
			
			uniform sampler2D _MainTex;
			uniform float _TimeX;
			uniform float _Distortion;
			uniform float4 _ScreenResolution;
			
		       struct appdata_t
            {
                float4 vertex   : POSITION;
                float4 color    : COLOR;
                float2 texcoord : TEXCOORD0;
            };
 
            struct v2f
            {
                  half2 texcoord  : TEXCOORD0;
                  float4 vertex   : SV_POSITION;
                  fixed4 color    : COLOR;
           };   
             
  			v2f vert(appdata_t IN)
            {
                v2f OUT;
                OUT.vertex = mul(UNITY_MATRIX_MVP, IN.vertex);
                OUT.texcoord = IN.texcoord;
                OUT.color = IN.color;
                
                return OUT;
            }
            

half4 frag (v2f i) : COLOR
{
 	float2 p = i.texcoord.xy;
	half4 col = tex2D(_MainTex, p);
	
	float2 offset = float2(_Distortion,.0);

	col.r = tex2D(_MainTex, p+offset.xy).r;
	col.b = tex2D(_MainTex, p+offset.yx).b;
	
    return col;

}
			
			ENDCG
		}
		
	}
}