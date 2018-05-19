// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Outlined/ModelProjection" {

	/*These variables define the width and color of the outline*/
	Properties{
		_OutlineColor("Outline Color", Color) = (0,0,0,1)
		_Outline("Outline width", Range(0.0, 0.99)) = .005
	}

		CGINCLUDE
#include "UnityCG.cginc"

	struct appdata {
		float4 vertex : POSITION;
		float3 normal : NORMAL;
	};

	struct v2f {
		float4 pos   : POSITION;
		float4 color : COLOR;
	};

	uniform float  _Outline;
	uniform float4 _OutlineColor;

	v2f vert(appdata vertexData) {
		// just make a copy of incoming vertex data but scaled according to normal direction
		v2f theObj;

		/*Multiply the vertex data by the modelview projection matrix*/
		theObj.pos = UnityObjectToClipPos(vertexData.vertex);

		/*Find normal by multiplying current modelview matrix by the data's normal*/
		float3 norm = mul((float3x3)UNITY_MATRIX_IT_MV, vertexData.normal);
		float2 offset = TransformViewToProjection(norm.xy);

		theObj.pos.xy += offset * theObj.pos.z * _Outline;

		/*Set the color that we want to paint the outline*/
		theObj.color = _OutlineColor;
		return theObj;
	}
	ENDCG

		SubShader{
		Tags{ "Queue" = "Transparent" }

		Pass{
		Name "BASE"
		/*Don't cull backfaces, since the model is mostly transparent*/
		Cull Off
		Blend Zero One

		/*The following line allows inner lines of the model to be outlined as well*/
		//Offset -8, -8

		SetTexture[_OutlineColor]{
		ConstantColor(0,0,0,0)
		Combine constant
	}
	}

		Pass{
		Name "OUTLINE"
		Tags{ "LightMode" = "Always" }
		Cull Front

		/*Blending modes*/
		/*Only enable one at a time!*/

		//Blend SrcAlpha OneMinusSrcAlpha /*Normal blending mode*/
		//Blend One One					  /*Additive blending mode*/
		Blend One OneMinusDstColor        /*Soft additive blending*/
        //Blend DstColor Zero             /*Multiplicative blending*/
	    //Blend DstColor SrcColor         /*2x Multiplicative*/

		CGPROGRAM
#pragma vertex vert
#pragma fragment frag

		half4 frag(v2f i) :COLOR{
		return i.color;
	}
		ENDCG
	}


	}

		/*If there is some sort of error, just use a standard Diffuse shader*/
		Fallback "Diffuse"
}