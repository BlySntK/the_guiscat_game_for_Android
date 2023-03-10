////////////////////////////////////////////////////////////////////////////////////
//  CameraFilterPack v2.0 - by VETASOFT 2015 //////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////

Shader "CameraFilterPack/Colors_Brightness" {
	Properties 
	{
		_MainTex ("Base (RGB)", 2D) = "white" {}
	    _Val("Value", Float) = 1
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
			#include "UnityCG.cginc"
			
			
			uniform sampler2D _MainTex;
			uniform float _Val;
			
			
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
            };


	float4 frag (v2f i) : COLOR
	{

		fixed3 c = tex2D(_MainTex, i.texcoord);
		if (_Val<1.) c*=_Val; else c+=_Val-1;
		return float4(c, 1.0);
			
	}
			
			ENDCG
		}
		
	}
}