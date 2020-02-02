// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Custom/BuildingShader"
{
	Properties
	{
		_BaseColor("BaseColor", 2D) = "white" {}
		_Color01("Color01", Color) = (0,0,0,0)
		_Color02("Color02", Color) = (0,0,0,0)
		_Saturation("Saturation", Range( 0 , 1)) = 0.5
		_ColorInEmissive("ColorInEmissive", Range( 0 , 1)) = 0
		_Emissive("Emissive", 2D) = "white" {}
		_EmissiveColor01("EmissiveColor01", Color) = (0,0,0,0)
		_EmissiveColor02("EmissiveColor02", Color) = (0,0,0,0)
		_OutlineColor("OutlineColor", Color) = (1,1,1,1)
		_OutlineWidth("OutlineWidth", Range( 0 , 2)) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ }
		Cull Front
		CGPROGRAM
		#pragma target 3.0
		#pragma surface outlineSurf Outline nofog  keepalpha noshadow noambient novertexlights nolightmap nodynlightmap nodirlightmap nometa noforwardadd vertex:outlineVertexDataFunc 
		
		
		
		struct Input
		{
			half filler;
		};
		uniform float _OutlineWidth;
		uniform float4 _OutlineColor;
		
		void outlineVertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			float outlineVar = _OutlineWidth;
			v.vertex.xyz += ( v.normal * outlineVar );
		}
		inline half4 LightingOutline( SurfaceOutput s, half3 lightDir, half atten ) { return half4 ( 0,0,0, s.Alpha); }
		void outlineSurf( Input i, inout SurfaceOutput o )
		{
			o.Emission = _OutlineColor.rgb;
		}
		ENDCG
		

		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Off
		CGPROGRAM
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows exclude_path:deferred vertex:vertexDataFunc 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform float4 _Color01;
		uniform float4 _Color02;
		uniform float _Saturation;
		uniform sampler2D _BaseColor;
		uniform float4 _BaseColor_ST;
		uniform sampler2D _Emissive;
		uniform float4 _Emissive_ST;
		uniform float4 _EmissiveColor01;
		uniform float4 _EmissiveColor02;
		uniform float _ColorInEmissive;


		float3 HSVToRGB( float3 c )
		{
			float4 K = float4( 1.0, 2.0 / 3.0, 1.0 / 3.0, 3.0 );
			float3 p = abs( frac( c.xxx + K.xyz ) * 6.0 - K.www );
			return c.z * lerp( K.xxx, saturate( p - K.xxx ), c.y );
		}


		float3 RGBToHSV(float3 c)
		{
			float4 K = float4(0.0, -1.0 / 3.0, 2.0 / 3.0, -1.0);
			float4 p = lerp( float4( c.bg, K.wz ), float4( c.gb, K.xy ), step( c.b, c.g ) );
			float4 q = lerp( float4( p.xyw, c.r ), float4( c.r, p.yzx ), step( p.x, c.r ) );
			float d = q.x - min( q.w, q.y );
			float e = 1.0e-10;
			return float3( abs(q.z + (q.w - q.y) / (6.0 * d + e)), d / (q.x + e), q.x);
		}

		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			v.vertex.xyz += 0;
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float3 hsvTorgb36 = RGBToHSV( _Color01.rgb );
			float3 hsvTorgb35 = RGBToHSV( _Color02.rgb );
			float4 transform14 = mul(unity_ObjectToWorld,float4( 0,0,0,1 ));
			float temp_output_17_0 = abs( sin( ( transform14.x + transform14.z ) ) );
			float lerpResult34 = lerp( hsvTorgb36.x , hsvTorgb35.x , temp_output_17_0);
			float clampResult49 = clamp( temp_output_17_0 , 0.25 , 0.5 );
			float3 hsvTorgb18 = HSVToRGB( float3(lerpResult34,_Saturation,clampResult49) );
			float2 uv_BaseColor = i.uv_texcoord * _BaseColor_ST.xy + _BaseColor_ST.zw;
			float4 temp_output_22_0 = ( float4( hsvTorgb18 , 0.0 ) * tex2D( _BaseColor, uv_BaseColor ) );
			o.Albedo = temp_output_22_0.rgb;
			float2 uv_Emissive = i.uv_texcoord * _Emissive_ST.xy + _Emissive_ST.zw;
			float3 hsvTorgb45 = RGBToHSV( _EmissiveColor01.rgb );
			float3 hsvTorgb46 = RGBToHSV( _EmissiveColor02.rgb );
			float lerpResult47 = lerp( hsvTorgb45.x , hsvTorgb46.x , temp_output_17_0);
			float3 hsvTorgb27 = HSVToRGB( float3(lerpResult47,10.0,abs( sin( ( transform14.x + transform14.y + transform14.z ) ) )) );
			float4 temp_output_10_0 = ( tex2D( _Emissive, uv_Emissive ) * float4( exp( hsvTorgb27 ) , 0.0 ) );
			float4 lerpResult8 = lerp( temp_output_10_0 , ( temp_output_22_0 + temp_output_10_0 ) , _ColorInEmissive);
			o.Emission = lerpResult8.rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=17101
100;345;1600;839;2117.046;973.3228;1;True;True
Node;AmplifyShaderEditor.ObjectToWorldTransfNode;14;-1936.501,-772.1887;Inherit;False;1;0;FLOAT4;0,0,0,1;False;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;15;-1577.987,-750.2948;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SinOpNode;16;-1386.415,-736.6111;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;23;-1617.795,-147.7396;Inherit;False;3;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;43;-1775.514,-571.8166;Inherit;False;Property;_EmissiveColor01;EmissiveColor01;7;0;Create;True;0;0;False;0;0,0,0,0;1,0,0.4689345,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;44;-1779.733,-378.1546;Inherit;False;Property;_EmissiveColor02;EmissiveColor02;8;0;Create;True;0;0;False;0;0,0,0,0;0,0.7149541,1,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RGBToHSVNode;45;-1430.039,-558.8505;Inherit;False;1;0;FLOAT3;0,0,0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.ColorNode;29;-1589.709,-1165.213;Inherit;False;Property;_Color01;Color01;1;0;Create;True;0;0;False;0;0,0,0,0;1,0,0.2771311,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;30;-1593.928,-971.551;Inherit;False;Property;_Color02;Color02;2;0;Create;True;0;0;False;0;0,0,0,0;0.3278966,0,1,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.AbsOpNode;17;-1172.949,-731.1376;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RGBToHSVNode;46;-1430.039,-313.7133;Inherit;False;1;0;FLOAT3;0,0,0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SinOpNode;24;-1426.223,-134.0559;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;47;-1113.461,-380.9508;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;28;-1220.86,-210.8187;Inherit;False;Constant;_Float0;Float 0;6;0;Create;True;0;0;False;0;10;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RGBToHSVNode;36;-1244.234,-1152.247;Inherit;False;1;0;FLOAT3;0,0,0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RGBToHSVNode;35;-1244.234,-907.1097;Inherit;False;1;0;FLOAT3;0,0,0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.AbsOpNode;25;-1212.757,-128.5824;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.HSVToRGBNode;27;-901.5804,-200.5264;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;21;-1325.275,-660.6541;Inherit;False;Property;_Saturation;Saturation;3;0;Create;True;0;0;False;0;0.5;0.463;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;34;-927.6569,-974.3472;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;49;-1485.553,-684.2092;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0.25;False;2;FLOAT;0.5;False;1;FLOAT;0
Node;AmplifyShaderEditor.ExpOpNode;48;-801.9165,0.8977051;Inherit;False;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SamplerNode;1;-989.8218,-451.8093;Inherit;True;Property;_BaseColor;BaseColor;0;0;Create;True;0;0;False;0;None;14ee75341e6b286438cc27bd7308a924;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;2;-1293.353,44.74643;Inherit;True;Property;_Emissive;Emissive;6;0;Create;True;0;0;False;0;None;b34464a50219a7844bfcb1dccca5eee0;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.HSVToRGBNode;18;-919.8322,-739.1919;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;22;-540.7947,-494.9656;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;10;-603.0404,63.09216;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT3;0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;5;-484.507,497.9063;Inherit;False;Property;_ColorInEmissive;ColorInEmissive;5;0;Create;True;0;0;False;0;0;0.202;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;13;-822.0558,696.6074;Inherit;False;Property;_OutlineWidth;OutlineWidth;10;0;Create;True;0;0;False;0;0;0.5;0;2;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;7;-395.4474,158.8288;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;12;-960.9952,505.1957;Inherit;False;Property;_OutlineColor;OutlineColor;9;0;Create;True;0;0;False;0;1,1,1,1;0.692,0.9069962,1,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.OutlineNode;11;-522.508,402.8705;Inherit;False;0;True;None;0;0;Front;3;0;FLOAT3;0,0,0;False;2;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.LerpOp;8;-218.7013,58.54232;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;20;-1263.788,-553.5269;Inherit;False;Property;_Value;Value;4;0;Create;True;0;0;False;0;1;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;369.787,-68.23452;Float;False;True;2;ASEMaterialInspector;0;0;Standard;Custom/BuildingShader;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Off;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;ForwardOnly;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0.3537736,0.8554136,1,1;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;15;0;14;1
WireConnection;15;1;14;3
WireConnection;16;0;15;0
WireConnection;23;0;14;1
WireConnection;23;1;14;2
WireConnection;23;2;14;3
WireConnection;45;0;43;0
WireConnection;17;0;16;0
WireConnection;46;0;44;0
WireConnection;24;0;23;0
WireConnection;47;0;45;1
WireConnection;47;1;46;1
WireConnection;47;2;17;0
WireConnection;36;0;29;0
WireConnection;35;0;30;0
WireConnection;25;0;24;0
WireConnection;27;0;47;0
WireConnection;27;1;28;0
WireConnection;27;2;25;0
WireConnection;34;0;36;1
WireConnection;34;1;35;1
WireConnection;34;2;17;0
WireConnection;49;0;17;0
WireConnection;48;0;27;0
WireConnection;18;0;34;0
WireConnection;18;1;21;0
WireConnection;18;2;49;0
WireConnection;22;0;18;0
WireConnection;22;1;1;0
WireConnection;10;0;2;0
WireConnection;10;1;48;0
WireConnection;7;0;22;0
WireConnection;7;1;10;0
WireConnection;11;0;12;0
WireConnection;11;1;13;0
WireConnection;8;0;10;0
WireConnection;8;1;7;0
WireConnection;8;2;5;0
WireConnection;0;0;22;0
WireConnection;0;2;8;0
WireConnection;0;11;11;0
ASEEND*/
//CHKSM=FBC23DD3C6A6E7E902FF233435B3CDA661C07DCE