//Edited by Laurence Schinina
Shader "Custom/DepthIgnoreWithShadow" {
	Properties 
 {
     _Color ("Ignore Depth Color", Color) = (1,1,1,1)
     _MainTex ("Base (RGB) Gloss (A)", 2D) = "white" {}
	 _Normal("Normal Map", 2D) = "bump" {}
	 
 }
 
 Category 
 {
     SubShader 
     { 
         Tags
		 { 
			"Queue" = "Geometry"
			"RenderType"="Opaque" 
		 }
         Pass
         {
             ZWrite Off
             ZTest Greater
             Lighting Off
             Color [_Color]
         }
         Pass 
         {
             ZTest Less          
             SetTexture [_MainTex] {combine texture}
         }
		 CGPROGRAM
		#pragma surface surf Lambert

		sampler2D _MainTex;
		sampler2D _Normal;
		struct Input {
			float2 uv_MainTex;
			float2 uv_Normal;
		};

		void surf (Input IN, inout SurfaceOutput o) {
			 o.Albedo = tex2D (_MainTex, IN.uv_MainTex).rgb;
			 o.Normal = UnpackNormal(tex2D(_Normal, IN.uv_Normal));
		}
		ENDCG
     }


 }
 
 Fallback "VertexLit", 1
}
