Shader "Custom/YellowPulse"
{
	Properties
	{
		_MainTex("Base (RGBA)", 2D) = "white" {}
	_FallOffTex("FallOff (A)", 2D) = "white" {}
	}
		SubShader
	{
		Tags{ "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" }
		LOD 200

		CGPROGRAM
#pragma surface surf Lambert alpha

		sampler2D _MainTex;
	sampler2D _FallOffTex;

	struct Input
	{
		float2 uv_MainTex;
		float3 worldPos;
	};

	void surf(Input IN, inout SurfaceOutput o)
	{
		half4 c = tex2D(_MainTex, IN.uv_MainTex);
		half fo = tex2D(_FallOffTex, IN.uv_MainTex).a;

		fixed illum = fo*((sin(IN.worldPos.x + _Time.g * 5) + cos(IN.worldPos.z + _Time.g * 5)) / 2 + 1) * 0.1f;//try use here some random value from vertex color
		o.Emission = fixed3(1.0f, 0.9294f, 0.0f)*(illum + pow(illum, 2) * 5);

		o.Albedo = lerp(c.rgb, fixed3(1.0f, 0.9294f, 0.0f), illum);

		o.Alpha = c.a;
	}
	ENDCG
	}
}