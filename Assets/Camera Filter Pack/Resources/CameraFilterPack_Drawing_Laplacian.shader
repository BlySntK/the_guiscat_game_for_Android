////////////////////////////////////////////////////////////////////////////////////
//  CameraFilterPack v2.0 - by VETASOFT 2015 //////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////

Shader "CameraFilterPack/Drawing_Laplacian" {
	Properties 
	{
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_TimeX ("Time", Range(0.0, 1.0)) = 1.0
		_Distortion ("_Distortion", Range(0.0, 1.0)) = 0.3
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


fixed4 frag (v2f i) : COLOR
{
	float2 SR1 = 1. / _ScreenResolution.xy;
	float2 SR2 = 2. / _ScreenResolution.xy;
	float2 uv 	= i.texcoord.xy;
	fixed4 color= 0.0;

	color -= tex2D(_MainTex, (uv + float2(-SR2.x,0)));
	color -= tex2D(_MainTex, (uv + float2(-SR2.x,SR1.y)));
	color -= tex2D(_MainTex, (uv + float2(-SR2.x,SR2.y)));

	color -= tex2D(_MainTex, (uv + float2(-SR1.x,-SR2.y)));
	color -= tex2D(_MainTex, (uv + float2(-SR1.x,-SR1.y)));
	color -= tex2D(_MainTex, (uv + float2(-SR1.x,0)));

	color -= tex2D(_MainTex, (uv + float2(0,-SR2.y)));
	color -= tex2D(_MainTex, (uv + float2(0,-SR1.y)));
	color += 16. * tex2D(_MainTex, (uv + float2(0,0)));
	color -= tex2D(_MainTex, (uv + float2(0,SR1.y)));
	color -= tex2D(_MainTex, (uv + float2(0,SR2.y)));

	color -= tex2D(_MainTex, (uv + float2(SR1.x,-SR2.y)));
	color -= tex2D(_MainTex, (uv + float2(SR1.x,-SR1.y)));
	color -= tex2D(_MainTex, (uv + float2(SR1.x,0)));

	color -= tex2D(_MainTex, (uv + float2(SR2.x,-SR2.y)));
	color -= tex2D(_MainTex, (uv + float2(SR2.x,-SR1.y)));
	color -= tex2D(_MainTex, (uv + float2(SR2.x,0)));

	if((color.r+color.g+color.b)/3. < .8)
	{
		color = 0.;
	}

	return color;	
}
			
			ENDCG
		}
		
	}
}