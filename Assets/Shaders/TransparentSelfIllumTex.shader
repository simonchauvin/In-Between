Shader "Custom/TransparentSelfIllumTex"
{
	Properties
	{
		_Color ("Color", Color) = (1.0, 1.0, 1.0, 1.0)
		_Transparency ("Transparency", Range(0,1)) = 1
		_AlphaMask ("Color Mask (A)", 2D) = "white" {}
	}
	SubShader
	{
		Tags
		{
			"Queue"="Transparent"
			"IgnoreProjector"="True"
			"RenderType"="Transparent"
		}
		LOD 200
		
		CGPROGRAM
		#pragma surface surf Lambert alpha
		
		fixed4 _Color;
		fixed _Transparency;
		sampler2D _AlphaMask;
		
		struct Input
		{
			float2 uv_AlphaMask;
		};
		
		void surf (Input IN, inout SurfaceOutput o)
		{
			half4 c = tex2D (_AlphaMask, IN.uv_AlphaMask);
			o.Alpha = c.a * _Transparency;
			o.Emission = _Color.rgb * c.rgb;
		}
		ENDCG
	}
	FallBack "Transparent/VertexLit"
}