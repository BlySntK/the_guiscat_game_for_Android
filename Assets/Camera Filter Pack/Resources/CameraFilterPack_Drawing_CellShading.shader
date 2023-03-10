////////////////////////////////////////////////////////////////////////////////////
//  CameraFilterPack v2.0 - by VETASOFT 2015 //////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////

Shader "CameraFilterPack/Drawing_CellShading" {
	Properties 
	{
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_TimeX ("Time", Range(0.0, 1.0)) = 1.0
		_Distortion ("_Distortion", Range(0.0, 1.0)) = 0.3
		_ScreenResolution ("_ScreenResolution", Vector) = (0.,0.,0.,0.)
		_EdgeSize ("_EdgeSize", Range(0.0, 1.0)) = 0
		_ColorLevel ("_ColorLevel", Range(0.0, 10.0)) = 7
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
			#pragma glsl
			#include "UnityCG.cginc"
			
			uniform sampler2D _MainTex;
			uniform float _TimeX;
			uniform float _Distortion;
			uniform float4 _ScreenResolution;
			uniform float _EdgeSize;
			uniform float _ColorLevel;
			
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
			#define tex2D(sampler,uvs)  tex2Dlod( sampler , float4( ( uvs ) , 0.0f , 0.0f) )
			
			
			float4 edgeFilter(in int px, in int py, v2f i) 
			{

				fixed4 color = 0.0;
				
				for(int a = -1 ; a <= 1 ; a++) {
					for(int b = -1 ; b <= 1 ; b++) {
						color += tex2D(_MainTex, ((i.texcoord.xy*_ScreenResolution.xy) + float2(px + -b, py + -a)) / _ScreenResolution.xy);
					}
				}

				return color / 9.0;

			}

			
			float4 frag (v2f i) : COLOR
			{
				
				fixed4 color = 0;
				
				for(int a = -1 ; a <= 1 ; a++) {
					for(int b = -1 ; b <= 1 ; b++) {
						color += tex2D(_MainTex, ((i.texcoord.xy*_ScreenResolution.xy) + float2(-b , -a)) / _ScreenResolution.xy);
					}
				}
				
				color /= 9.0;
				
				for(int a = 0 ; a < 3 ; a++ ) {
					color[a] = floor(7.0 * color[a]) / _ColorLevel;
				}

				fixed4 sum = abs(edgeFilter(0, 1, i) - edgeFilter(0, -1, i));
				sum += abs(edgeFilter(1, 0, i) - edgeFilter(-1, 0, i));
				sum /= 2.0;

				float edgsum = _EdgeSize + 0.05;
				if(length(sum) > edgsum) {
					color.rgb = 0.0;
				}

				return color;	
			}
			
			ENDCG
		}
		
	}
}