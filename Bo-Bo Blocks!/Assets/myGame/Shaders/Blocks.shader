Shader "Custom/Blocks" {
	Properties {
		_BorderColor("Border Color",Color) = (0,0,0,1)
		_Color("Main Color",Color) = (1,1,1,1)
		//_MainTex("Base(RGB)",2D) = "white"{}
		_Width("Border Width",Float) = 0.2
		//_Glonesses

	}
	SubShader {
	Pass{
		Tags { "RenderType"="Opaque""Queue"="Geometry"}
		Cull Back 
		Blend SrcAlpha OneMinusSrcAlpha
		LOD 200
		
		CGPROGRAM
		#pragma vertex vert 
		#pragma fragment frag 
		#include "UnityCG.cginc"
		#include "Lighting.cginc"

		fixed4 _Color;
		//sampler2D _MainTex;
		uniform float4 _BorderColor;
		uniform float _Width;

		struct v2f{
			float4 pos : SV_POSITION;
			float3 worldNormal : TEXCOORD0;
			float2 uv : TEXCOORD1;
			float4 texcoord: TEXCOORD2;
		};

		v2f vert(appdata_full v){
			v2f o;
			o.pos = mul(UNITY_MATRIX_MVP,v.vertex);
			o.uv = v.texcoord;
			o.worldNormal = UnityObjectToWorldNormal(v.normal);
			o.texcoord = v.texcoord;
			return o;
		}

		fixed4 frag(v2f i):SV_Target{
			float3 worldNormal = normalize(i.worldNormal);
			float3 lightDir = normalize(_WorldSpaceLightPos0.xyz);
			float halfLambert = dot(worldNormal,lightDir)*0.5 +0.5;
			fixed3 diffuse = _LightColor0.rgb * _Color.rgb * halfLambert;
			fixed3 ambient = UNITY_LIGHTMODEL_AMBIENT.rgb;
			fixed3 color = diffuse + ambient;
			float lx = smoothstep(0,1, i.texcoord.x-_Width);
			float ly = smoothstep(0,1, i.texcoord.y-_Width);
			float hx = smoothstep(0,1, 1.0 - _Width-i.texcoord.x);
			float hy = smoothstep(0,1, 1.0 - _Width-i.texcoord.y);
			fixed3 color1 = lerp(_BorderColor, color, lx*ly*hx*hy).rgb;
			return fixed4(color1,1.0);
			//return fixed4(color,1.0);
		}
		ENDCG
		}
		
	}
	FallBack "Diffuse"
}
