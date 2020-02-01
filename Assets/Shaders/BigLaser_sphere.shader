// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "BigLaser_sphere"
{
	Properties
	{
		_Cutoff( "Mask Clip Value", Float ) = 0.5
		_TextureSample0("Texture Sample 0", 2D) = "white" {}
		_Speed("Speed", Range( 0 , 20)) = 5
		_gradient1("gradient1", 2D) = "white" {}
		_Texture0("Texture 0", 2D) = "white" {}
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "TransparentCutout"  "Queue" = "AlphaTest+0" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Unlit keepalpha noshadow 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _TextureSample0;
		uniform float _Speed;
		uniform sampler2D _Texture0;
		uniform sampler2D _gradient1;
		uniform float4 _gradient1_ST;
		uniform float _Cutoff = 0.5;


		float4 CalculateContrast( float contrastValue, float4 colorTarget )
		{
			float t = 0.5 * ( 1.0 - contrastValue );
			return mul( float4x4( contrastValue,0,0,t, 0,contrastValue,0,t, 0,0,contrastValue,t, 0,0,0,1 ), colorTarget );
		}

		inline half4 LightingUnlit( SurfaceOutput s, half3 lightDir, half atten )
		{
			return half4 ( 0, 0, 0, s.Alpha );
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			float4 color3 = IsGammaSpace() ? float4(0,1,0,0) : float4(0,1,0,0);
			float4 appendResult16 = (float4(0.0 , _Speed , 0.0 , 0.0));
			float2 uv_TexCoord10 = i.uv_texcoord * float2( 3,1 );
			float2 panner11 = ( _Time.y * appendResult16.xy + uv_TexCoord10);
			float4 tex2DNode1 = tex2D( _TextureSample0, panner11 );
			float2 panner22 = ( _Time.y * float2( 0,2.2 ) + i.uv_texcoord);
			float2 uv_TexCoord31 = i.uv_texcoord * float2( 0.5,0.5 );
			float2 panner27 = ( _Time.y * float2( 0,1 ) + uv_TexCoord31);
			float2 uv_gradient1 = i.uv_texcoord * _gradient1_ST.xy + _gradient1_ST.zw;
			float4 temp_cast_1 = (( tex2D( _gradient1, uv_gradient1 ).r + -0.01 )).xxxx;
			float4 temp_output_41_0 = ( tex2DNode1 * ( ( tex2D( _Texture0, panner22 ).r + tex2D( _Texture0, panner27 ).r ) * CalculateContrast(5.0,temp_cast_1) ) );
			float4 temp_output_58_0 = (float4( 0.6981132,0.6981132,0.6981132,0 ) + (( tex2DNode1 * ( 1.0 - temp_output_41_0 ) ) - float4( 0,0,0,0 )) * (float4( 1,1,1,0 ) - float4( 0.6981132,0.6981132,0.6981132,0 )) / (float4( 0.4056604,0.4056604,0.4056604,0 ) - float4( 0,0,0,0 )));
			o.Emission = ( ( color3 * temp_output_58_0 ) + temp_output_58_0 ).rgb;
			o.Alpha = 1;
			clip( temp_output_41_0.r - _Cutoff );
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=17101
-1049;81;601;919;780.6031;292.9905;1.299393;False;False
Node;AmplifyShaderEditor.Vector2Node;25;-3096.189,390.2044;Inherit;False;Constant;_Vector0;Vector 0;5;0;Create;True;0;0;False;0;0,2.2;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.TextureCoordinatesNode;31;-3205.344,866.501;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;0.5,0.5;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleTimeNode;56;-3172.43,540.9832;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;32;-3213.571,628.2632;Inherit;False;Constant;_Vector1;Vector 1;5;0;Create;True;0;0;False;0;0,1;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.TextureCoordinatesNode;24;-3453.703,502.4934;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PannerNode;27;-2881.116,629.0723;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;19;-2691.975,765.7029;Inherit;True;Property;_gradient1;gradient1;3;0;Create;True;0;0;False;0;31f7d96c7c1556344979ed90b687f2a6;31f7d96c7c1556344979ed90b687f2a6;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TexturePropertyNode;23;-2877.975,239.7766;Inherit;True;Property;_Texture0;Texture 0;4;0;Create;True;0;0;False;0;f9ca7308f7fd1a64aafe77cd75ad8881;None;False;white;Auto;Texture2D;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.PannerNode;22;-2893.14,472.6219;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;15;-2805.716,-26.02588;Inherit;False;Property;_Speed;Speed;2;0;Create;True;0;0;False;0;5;0;0;20;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;13;-2361.806,169.2103;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;46;-2436.989,1094.623;Inherit;False;Constant;_Float1;Float 1;5;0;Create;True;0;0;False;0;5;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;26;-2367.683,528.0916;Inherit;True;Property;_TextureSample1;Texture Sample 1;4;0;Create;True;0;0;False;0;f9ca7308f7fd1a64aafe77cd75ad8881;f9ca7308f7fd1a64aafe77cd75ad8881;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;21;-2334.748,272.6591;Inherit;True;Property;_Perlin_noise;Perlin_noise;4;0;Create;True;0;0;False;0;f9ca7308f7fd1a64aafe77cd75ad8881;f9ca7308f7fd1a64aafe77cd75ad8881;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;10;-2446.138,-143.1986;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;3,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DynamicAppendNode;16;-2433.716,-5.025879;Inherit;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SimpleAddOpNode;47;-2382.022,972.2787;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;-0.01;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleContrastOpNode;45;-2247.266,929.7241;Inherit;False;2;1;COLOR;0,0,0,0;False;0;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;37;-1882.125,351.1976;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;11;-2115.963,-25.97362;Inherit;True;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;34;-1480.364,673.8889;Inherit;True;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;1;-1654.907,-85.59396;Inherit;True;Property;_TextureSample0;Texture Sample 0;1;0;Create;True;0;0;False;0;acdd4e6a999f9134b8df609ae9795e8f;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;41;-955.6121,249.9091;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.OneMinusNode;57;-697.3973,169.3649;Inherit;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;48;-596.0648,-13.93632;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;3;-992.5067,-178.0298;Inherit;False;Constant;_Color0;Color 0;2;0;Create;True;0;0;False;0;0,1,0,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TFHCRemapNode;58;-549.7069,58.01181;Inherit;True;5;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0.4056604,0.4056604,0.4056604,0;False;3;COLOR;0.6981132,0.6981132,0.6981132,0;False;4;COLOR;1,1,1,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;59;-305.3248,-30.61679;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;60;-15.26071,47.45045;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;165.7486,49.0582;Float;False;True;2;ASEMaterialInspector;0;0;Unlit;BigLaser_sphere;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Masked;0.5;True;False;0;False;TransparentCutout;;AlphaTest;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;False;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;0;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;27;0;31;0
WireConnection;27;2;32;0
WireConnection;27;1;56;0
WireConnection;22;0;24;0
WireConnection;22;2;25;0
WireConnection;22;1;56;0
WireConnection;26;0;23;0
WireConnection;26;1;27;0
WireConnection;21;0;23;0
WireConnection;21;1;22;0
WireConnection;16;1;15;0
WireConnection;47;0;19;1
WireConnection;45;1;47;0
WireConnection;45;0;46;0
WireConnection;37;0;21;1
WireConnection;37;1;26;1
WireConnection;11;0;10;0
WireConnection;11;2;16;0
WireConnection;11;1;13;0
WireConnection;34;0;37;0
WireConnection;34;1;45;0
WireConnection;1;1;11;0
WireConnection;41;0;1;0
WireConnection;41;1;34;0
WireConnection;57;0;41;0
WireConnection;48;0;1;0
WireConnection;48;1;57;0
WireConnection;58;0;48;0
WireConnection;59;0;3;0
WireConnection;59;1;58;0
WireConnection;60;0;59;0
WireConnection;60;1;58;0
WireConnection;0;2;60;0
WireConnection;0;10;41;0
ASEEND*/
//CHKSM=E77CB25E908B783655A6172EAB965D980293655B