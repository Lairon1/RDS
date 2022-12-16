Shader "T4MShaders/ShaderModel2/BumpDLM/T4M 3 Textures Bumped DLM" {
Properties {
	_Splat0 ("Layer 1", 2D) = "white" {}
	_Splat1 ("Layer 2", 2D) = "white" {}
	_Splat2 ("Layer 3", 2D) = "white" {}
	_BumpSplat0 ("Layer1Normalmap", 2D) = "bump" {}
	_BumpSplat1 ("Layer2Normalmap", 2D) = "bump" {}
	_BumpSplat2 ("Layer3Normalmap", 2D) = "bump" {}
	_Control ("Control (RGBA)", 2D) = "white" {}
	_MainTex ("Never Used", 2D) = "white" {}
}

SubShader {
	Tags {
		"SplatCount" = "3"
		"RenderType" = "Opaque"
	}
	
	Pass {
		Name "FORWARD"
		Tags { "LightMode" = "ForwardBase" }
Program "vp" {
// Vertex combos: 8
//   opengl - ALU: 9 to 73
//   d3d9 - ALU: 9 to 76
SubProgram "opengl " {
Keywords { "DIRECTIONAL" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "SHADOWS_OFF" }
Bind "vertex" Vertex
Bind "tangent" ATTR14
Bind "normal" Normal
Bind "texcoord" TexCoord0
Vector 13 [unity_Scale]
Vector 14 [_WorldSpaceLightPos0]
Matrix 5 [_Object2World]
Matrix 9 [_World2Object]
Vector 15 [unity_SHAr]
Vector 16 [unity_SHAg]
Vector 17 [unity_SHAb]
Vector 18 [unity_SHBr]
Vector 19 [unity_SHBg]
Vector 20 [unity_SHBb]
Vector 21 [unity_SHC]
Vector 22 [_Control_ST]
Vector 23 [_Splat0_ST]
Vector 24 [_Splat1_ST]
Vector 25 [_Splat2_ST]
"!!ARBvp1.0
# 37 ALU
PARAM c[26] = { { 1 },
		state.matrix.mvp,
		program.local[5..25] };
TEMP R0;
TEMP R1;
TEMP R2;
TEMP R3;
MUL R1.xyz, vertex.normal, c[13].w;
DP3 R2.w, R1, c[6];
DP3 R0.x, R1, c[5];
DP3 R0.z, R1, c[7];
MOV R0.y, R2.w;
MUL R1, R0.xyzz, R0.yzzx;
MOV R0.w, c[0].x;
DP4 R2.z, R0, c[17];
DP4 R2.y, R0, c[16];
DP4 R2.x, R0, c[15];
MUL R0.y, R2.w, R2.w;
DP4 R3.z, R1, c[20];
DP4 R3.y, R1, c[19];
DP4 R3.x, R1, c[18];
MOV R1.xyz, vertex.attrib[14];
ADD R3.xyz, R2, R3;
MAD R0.x, R0, R0, -R0.y;
MUL R2.xyz, R0.x, c[21];
MUL R0.xyz, vertex.normal.zxyw, R1.yzxw;
MAD R0.xyz, vertex.normal.yzxw, R1.zxyw, -R0;
ADD result.texcoord[3].xyz, R3, R2;
MOV R1, c[14];
DP4 R2.z, R1, c[11];
DP4 R2.y, R1, c[10];
DP4 R2.x, R1, c[9];
MUL R0.xyz, R0, vertex.attrib[14].w;
DP3 result.texcoord[2].y, R2, R0;
DP3 result.texcoord[2].z, vertex.normal, R2;
DP3 result.texcoord[2].x, R2, vertex.attrib[14];
MAD result.texcoord[0].zw, vertex.texcoord[0].xyxy, c[23].xyxy, c[23];
MAD result.texcoord[0].xy, vertex.texcoord[0], c[22], c[22].zwzw;
MAD result.texcoord[1].zw, vertex.texcoord[0].xyxy, c[25].xyxy, c[25];
MAD result.texcoord[1].xy, vertex.texcoord[0], c[24], c[24].zwzw;
DP4 result.position.w, vertex.position, c[4];
DP4 result.position.z, vertex.position, c[3];
DP4 result.position.y, vertex.position, c[2];
DP4 result.position.x, vertex.position, c[1];
END
# 37 instructions, 4 R-regs
"
}

SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "SHADOWS_OFF" }
Bind "vertex" Vertex
Bind "tangent" TexCoord2
Bind "normal" Normal
Bind "texcoord" TexCoord0
Matrix 0 [glstate_matrix_mvp]
Vector 12 [unity_Scale]
Vector 13 [_WorldSpaceLightPos0]
Matrix 4 [_Object2World]
Matrix 8 [_World2Object]
Vector 14 [unity_SHAr]
Vector 15 [unity_SHAg]
Vector 16 [unity_SHAb]
Vector 17 [unity_SHBr]
Vector 18 [unity_SHBg]
Vector 19 [unity_SHBb]
Vector 20 [unity_SHC]
Vector 21 [_Control_ST]
Vector 22 [_Splat0_ST]
Vector 23 [_Splat1_ST]
Vector 24 [_Splat2_ST]
"vs_2_0
; 40 ALU
def c25, 1.00000000, 0, 0, 0
dcl_position0 v0
dcl_tangent0 v1
dcl_normal0 v2
dcl_texcoord0 v3
mul r1.xyz, v2, c12.w
dp3 r2.w, r1, c5
dp3 r0.x, r1, c4
dp3 r0.z, r1, c6
mov r0.y, r2.w
mul r1, r0.xyzz, r0.yzzx
mov r0.w, c25.x
dp4 r2.z, r0, c16
dp4 r2.y, r0, c15
dp4 r2.x, r0, c14
mul r0.y, r2.w, r2.w
dp4 r3.z, r1, c19
dp4 r3.y, r1, c18
dp4 r3.x, r1, c17
mad r0.x, r0, r0, -r0.y
mul r1.xyz, r0.x, c20
add r2.xyz, r2, r3
mov r0.xyz, v1
add oT3.xyz, r2, r1
mul r1.xyz, v2.zxyw, r0.yzxw
mov r0.xyz, v1
mad r1.xyz, v2.yzxw, r0.zxyw, -r1
mov r0, c10
dp4 r3.z, c13, r0
mul r2.xyz, r1, v1.w
mov r0, c9
mov r1, c8
dp4 r3.y, c13, r0
dp4 r3.x, c13, r1
dp3 oT2.y, r3, r2
dp3 oT2.z, v2, r3
dp3 oT2.x, r3, v1
mad oT0.zw, v3.xyxy, c22.xyxy, c22
mad oT0.xy, v3, c21, c21.zwzw
mad oT1.zw, v3.xyxy, c24.xyxy, c24
mad oT1.xy, v3, c23, c23.zwzw
dp4 oPos.w, v0, c3
dp4 oPos.z, v0, c2
dp4 oPos.y, v0, c1
dp4 oPos.x, v0, c0
"
}

SubProgram "gles " {
Keywords { "DIRECTIONAL" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "SHADOWS_OFF" }
"!!GLES
#define SHADER_API_GLES 1
#define tex2D texture2D


#ifdef VERTEX
#define gl_ModelViewProjectionMatrix glstate_matrix_mvp
uniform mat4 glstate_matrix_mvp;

varying lowp vec3 xlv_TEXCOORD3;
varying lowp vec3 xlv_TEXCOORD2;
varying highp vec4 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;
uniform highp vec4 unity_Scale;
uniform highp vec4 unity_SHC;
uniform highp vec4 unity_SHBr;
uniform highp vec4 unity_SHBg;
uniform highp vec4 unity_SHBb;
uniform highp vec4 unity_SHAr;
uniform highp vec4 unity_SHAg;
uniform highp vec4 unity_SHAb;

uniform lowp vec4 _WorldSpaceLightPos0;
uniform highp mat4 _World2Object;
uniform highp vec4 _Splat2_ST;
uniform highp vec4 _Splat1_ST;
uniform highp vec4 _Splat0_ST;
uniform highp mat4 _Object2World;
uniform highp vec4 _Control_ST;
attribute vec4 _glesTANGENT;
attribute vec4 _glesMultiTexCoord0;
attribute vec3 _glesNormal;
attribute vec4 _glesVertex;
void main ()
{
  vec4 tmpvar_1;
  tmpvar_1.xyz = normalize (_glesTANGENT.xyz);
  tmpvar_1.w = _glesTANGENT.w;
  vec3 tmpvar_2;
  tmpvar_2 = normalize (_glesNormal);
  highp vec3 shlight;
  highp vec4 tmpvar_3;
  highp vec4 tmpvar_4;
  lowp vec3 tmpvar_5;
  lowp vec3 tmpvar_6;
  tmpvar_3.xy = ((_glesMultiTexCoord0.xy * _Control_ST.xy) + _Control_ST.zw);
  tmpvar_3.zw = ((_glesMultiTexCoord0.xy * _Splat0_ST.xy) + _Splat0_ST.zw);
  tmpvar_4.xy = ((_glesMultiTexCoord0.xy * _Splat1_ST.xy) + _Splat1_ST.zw);
  tmpvar_4.zw = ((_glesMultiTexCoord0.xy * _Splat2_ST.xy) + _Splat2_ST.zw);
  mat3 tmpvar_7;
  tmpvar_7[0] = _Object2World[0].xyz;
  tmpvar_7[1] = _Object2World[1].xyz;
  tmpvar_7[2] = _Object2World[2].xyz;
  highp mat3 tmpvar_8;
  tmpvar_8[0] = tmpvar_1.xyz;
  tmpvar_8[1] = (cross (tmpvar_2, tmpvar_1.xyz) * _glesTANGENT.w);
  tmpvar_8[2] = tmpvar_2;
  mat3 tmpvar_9;
  tmpvar_9[0].x = tmpvar_8[0].x;
  tmpvar_9[0].y = tmpvar_8[1].x;
  tmpvar_9[0].z = tmpvar_8[2].x;
  tmpvar_9[1].x = tmpvar_8[0].y;
  tmpvar_9[1].y = tmpvar_8[1].y;
  tmpvar_9[1].z = tmpvar_8[2].y;
  tmpvar_9[2].x = tmpvar_8[0].z;
  tmpvar_9[2].y = tmpvar_8[1].z;
  tmpvar_9[2].z = tmpvar_8[2].z;
  highp vec3 tmpvar_10;
  tmpvar_10 = (tmpvar_9 * (_World2Object * _WorldSpaceLightPos0).xyz);
  tmpvar_5 = tmpvar_10;
  highp vec4 tmpvar_11;
  tmpvar_11.w = 1.0;
  tmpvar_11.xyz = (tmpvar_7 * (tmpvar_2 * unity_Scale.w));
  mediump vec3 tmpvar_12;
  mediump vec4 normal;
  normal = tmpvar_11;
  mediump vec3 x3;
  highp float vC;
  mediump vec3 x2;
  mediump vec3 x1;
  highp float tmpvar_13;
  tmpvar_13 = dot (unity_SHAr, normal);
  x1.x = tmpvar_13;
  highp float tmpvar_14;
  tmpvar_14 = dot (unity_SHAg, normal);
  x1.y = tmpvar_14;
  highp float tmpvar_15;
  tmpvar_15 = dot (unity_SHAb, normal);
  x1.z = tmpvar_15;
  mediump vec4 tmpvar_16;
  tmpvar_16 = (normal.xyzz * normal.yzzx);
  highp float tmpvar_17;
  tmpvar_17 = dot (unity_SHBr, tmpvar_16);
  x2.x = tmpvar_17;
  highp float tmpvar_18;
  tmpvar_18 = dot (unity_SHBg, tmpvar_16);
  x2.y = tmpvar_18;
  highp float tmpvar_19;
  tmpvar_19 = dot (unity_SHBb, tmpvar_16);
  x2.z = tmpvar_19;
  mediump float tmpvar_20;
  tmpvar_20 = ((normal.x * normal.x) - (normal.y * normal.y));
  vC = tmpvar_20;
  highp vec3 tmpvar_21;
  tmpvar_21 = (unity_SHC.xyz * vC);
  x3 = tmpvar_21;
  tmpvar_12 = ((x1 + x2) + x3);
  shlight = tmpvar_12;
  tmpvar_6 = shlight;
  gl_Position = (gl_ModelViewProjectionMatrix * _glesVertex);
  xlv_TEXCOORD0 = tmpvar_3;
  xlv_TEXCOORD1 = tmpvar_4;
  xlv_TEXCOORD2 = tmpvar_5;
  xlv_TEXCOORD3 = tmpvar_6;
}



#endif
#ifdef FRAGMENT

varying lowp vec3 xlv_TEXCOORD3;
varying lowp vec3 xlv_TEXCOORD2;
varying highp vec4 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;
uniform sampler2D _Splat2;
uniform sampler2D _Splat1;
uniform sampler2D _Splat0;
uniform lowp vec4 _LightColor0;
uniform sampler2D _Control;
uniform sampler2D _BumpSplat2;
uniform sampler2D _BumpSplat1;
uniform sampler2D _BumpSplat0;
void main ()
{
  lowp vec4 c;
  highp vec2 tmpvar_1;
  tmpvar_1 = xlv_TEXCOORD0.xy;
  highp vec2 tmpvar_2;
  tmpvar_2 = xlv_TEXCOORD0.zw;
  highp vec2 tmpvar_3;
  tmpvar_3 = xlv_TEXCOORD1.xy;
  highp vec2 tmpvar_4;
  tmpvar_4 = xlv_TEXCOORD1.zw;
  lowp vec3 tmpvar_5;
  lowp vec4 tmpvar_6;
  tmpvar_6 = texture2D (_Control, tmpvar_1);
  tmpvar_5 = (((texture2D (_Splat0, tmpvar_2).xyz * tmpvar_6.x) + (texture2D (_Splat1, tmpvar_3).xyz * tmpvar_6.y)) + (texture2D (_Splat2, tmpvar_4).xyz * tmpvar_6.z));
  lowp vec4 c_i0;
  c_i0.xyz = ((tmpvar_5 * _LightColor0.xyz) * (max (0.0, dot ((((((texture2D (_BumpSplat0, tmpvar_2).xyz * 2.0) - 1.0) * tmpvar_6.x) + (((texture2D (_BumpSplat1, tmpvar_3).xyz * 2.0) - 1.0) * tmpvar_6.y)) + (((texture2D (_BumpSplat2, tmpvar_4).xyz * 2.0) - 1.0) * tmpvar_6.z)), xlv_TEXCOORD2)) * 2.0));
  c_i0.w = 0.0;
  c = c_i0;
  c.xyz = (c_i0.xyz + (tmpvar_5 * xlv_TEXCOORD3));
  gl_FragData[0] = c;
}



#endif"
}

SubProgram "glesdesktop " {
Keywords { "DIRECTIONAL" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "SHADOWS_OFF" }
"!!GLES
#define SHADER_API_GLES 1
#define tex2D texture2D


#ifdef VERTEX
#define gl_ModelViewProjectionMatrix glstate_matrix_mvp
uniform mat4 glstate_matrix_mvp;

varying lowp vec3 xlv_TEXCOORD3;
varying lowp vec3 xlv_TEXCOORD2;
varying highp vec4 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;
uniform highp vec4 unity_Scale;
uniform highp vec4 unity_SHC;
uniform highp vec4 unity_SHBr;
uniform highp vec4 unity_SHBg;
uniform highp vec4 unity_SHBb;
uniform highp vec4 unity_SHAr;
uniform highp vec4 unity_SHAg;
uniform highp vec4 unity_SHAb;

uniform lowp vec4 _WorldSpaceLightPos0;
uniform highp mat4 _World2Object;
uniform highp vec4 _Splat2_ST;
uniform highp vec4 _Splat1_ST;
uniform highp vec4 _Splat0_ST;
uniform highp mat4 _Object2World;
uniform highp vec4 _Control_ST;
attribute vec4 _glesTANGENT;
attribute vec4 _glesMultiTexCoord0;
attribute vec3 _glesNormal;
attribute vec4 _glesVertex;
void main ()
{
  vec4 tmpvar_1;
  tmpvar_1.xyz = normalize (_glesTANGENT.xyz);
  tmpvar_1.w = _glesTANGENT.w;
  vec3 tmpvar_2;
  tmpvar_2 = normalize (_glesNormal);
  highp vec3 shlight;
  highp vec4 tmpvar_3;
  highp vec4 tmpvar_4;
  lowp vec3 tmpvar_5;
  lowp vec3 tmpvar_6;
  tmpvar_3.xy = ((_glesMultiTexCoord0.xy * _Control_ST.xy) + _Control_ST.zw);
  tmpvar_3.zw = ((_glesMultiTexCoord0.xy * _Splat0_ST.xy) + _Splat0_ST.zw);
  tmpvar_4.xy = ((_glesMultiTexCoord0.xy * _Splat1_ST.xy) + _Splat1_ST.zw);
  tmpvar_4.zw = ((_glesMultiTexCoord0.xy * _Splat2_ST.xy) + _Splat2_ST.zw);
  mat3 tmpvar_7;
  tmpvar_7[0] = _Object2World[0].xyz;
  tmpvar_7[1] = _Object2World[1].xyz;
  tmpvar_7[2] = _Object2World[2].xyz;
  highp mat3 tmpvar_8;
  tmpvar_8[0] = tmpvar_1.xyz;
  tmpvar_8[1] = (cross (tmpvar_2, tmpvar_1.xyz) * _glesTANGENT.w);
  tmpvar_8[2] = tmpvar_2;
  mat3 tmpvar_9;
  tmpvar_9[0].x = tmpvar_8[0].x;
  tmpvar_9[0].y = tmpvar_8[1].x;
  tmpvar_9[0].z = tmpvar_8[2].x;
  tmpvar_9[1].x = tmpvar_8[0].y;
  tmpvar_9[1].y = tmpvar_8[1].y;
  tmpvar_9[1].z = tmpvar_8[2].y;
  tmpvar_9[2].x = tmpvar_8[0].z;
  tmpvar_9[2].y = tmpvar_8[1].z;
  tmpvar_9[2].z = tmpvar_8[2].z;
  highp vec3 tmpvar_10;
  tmpvar_10 = (tmpvar_9 * (_World2Object * _WorldSpaceLightPos0).xyz);
  tmpvar_5 = tmpvar_10;
  highp vec4 tmpvar_11;
  tmpvar_11.w = 1.0;
  tmpvar_11.xyz = (tmpvar_7 * (tmpvar_2 * unity_Scale.w));
  mediump vec3 tmpvar_12;
  mediump vec4 normal;
  normal = tmpvar_11;
  mediump vec3 x3;
  highp float vC;
  mediump vec3 x2;
  mediump vec3 x1;
  highp float tmpvar_13;
  tmpvar_13 = dot (unity_SHAr, normal);
  x1.x = tmpvar_13;
  highp float tmpvar_14;
  tmpvar_14 = dot (unity_SHAg, normal);
  x1.y = tmpvar_14;
  highp float tmpvar_15;
  tmpvar_15 = dot (unity_SHAb, normal);
  x1.z = tmpvar_15;
  mediump vec4 tmpvar_16;
  tmpvar_16 = (normal.xyzz * normal.yzzx);
  highp float tmpvar_17;
  tmpvar_17 = dot (unity_SHBr, tmpvar_16);
  x2.x = tmpvar_17;
  highp float tmpvar_18;
  tmpvar_18 = dot (unity_SHBg, tmpvar_16);
  x2.y = tmpvar_18;
  highp float tmpvar_19;
  tmpvar_19 = dot (unity_SHBb, tmpvar_16);
  x2.z = tmpvar_19;
  mediump float tmpvar_20;
  tmpvar_20 = ((normal.x * normal.x) - (normal.y * normal.y));
  vC = tmpvar_20;
  highp vec3 tmpvar_21;
  tmpvar_21 = (unity_SHC.xyz * vC);
  x3 = tmpvar_21;
  tmpvar_12 = ((x1 + x2) + x3);
  shlight = tmpvar_12;
  tmpvar_6 = shlight;
  gl_Position = (gl_ModelViewProjectionMatrix * _glesVertex);
  xlv_TEXCOORD0 = tmpvar_3;
  xlv_TEXCOORD1 = tmpvar_4;
  xlv_TEXCOORD2 = tmpvar_5;
  xlv_TEXCOORD3 = tmpvar_6;
}



#endif
#ifdef FRAGMENT

varying lowp vec3 xlv_TEXCOORD3;
varying lowp vec3 xlv_TEXCOORD2;
varying highp vec4 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;
uniform sampler2D _Splat2;
uniform sampler2D _Splat1;
uniform sampler2D _Splat0;
uniform lowp vec4 _LightColor0;
uniform sampler2D _Control;
uniform sampler2D _BumpSplat2;
uniform sampler2D _BumpSplat1;
uniform sampler2D _BumpSplat0;
void main ()
{
  lowp vec4 c;
  highp vec2 tmpvar_1;
  tmpvar_1 = xlv_TEXCOORD0.xy;
  highp vec2 tmpvar_2;
  tmpvar_2 = xlv_TEXCOORD0.zw;
  highp vec2 tmpvar_3;
  tmpvar_3 = xlv_TEXCOORD1.xy;
  highp vec2 tmpvar_4;
  tmpvar_4 = xlv_TEXCOORD1.zw;
  lowp vec3 tmpvar_5;
  lowp vec4 tmpvar_6;
  tmpvar_6 = texture2D (_Control, tmpvar_1);
  tmpvar_5 = (((texture2D (_Splat0, tmpvar_2).xyz * tmpvar_6.x) + (texture2D (_Splat1, tmpvar_3).xyz * tmpvar_6.y)) + (texture2D (_Splat2, tmpvar_4).xyz * tmpvar_6.z));
  lowp vec3 normal;
  normal.xy = ((texture2D (_BumpSplat0, tmpvar_2).wy * 2.0) - 1.0);
  normal.z = sqrt (((1.0 - (normal.x * normal.x)) - (normal.y * normal.y)));
  lowp vec3 normal_i0;
  normal_i0.xy = ((texture2D (_BumpSplat1, tmpvar_3).wy * 2.0) - 1.0);
  normal_i0.z = sqrt (((1.0 - (normal_i0.x * normal_i0.x)) - (normal_i0.y * normal_i0.y)));
  lowp vec3 normal_i0_i1;
  normal_i0_i1.xy = ((texture2D (_BumpSplat2, tmpvar_4).wy * 2.0) - 1.0);
  normal_i0_i1.z = sqrt (((1.0 - (normal_i0_i1.x * normal_i0_i1.x)) - (normal_i0_i1.y * normal_i0_i1.y)));
  lowp vec4 c_i0;
  c_i0.xyz = ((tmpvar_5 * _LightColor0.xyz) * (max (0.0, dot ((((normal * tmpvar_6.x) + (normal_i0 * tmpvar_6.y)) + (normal_i0_i1 * tmpvar_6.z)), xlv_TEXCOORD2)) * 2.0));
  c_i0.w = 0.0;
  c = c_i0;
  c.xyz = (c_i0.xyz + (tmpvar_5 * xlv_TEXCOORD3));
  gl_FragData[0] = c;
}



#endif"
}

SubProgram "opengl " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "DIRLIGHTMAP_OFF" "SHADOWS_OFF" }
Bind "vertex" Vertex
Bind "texcoord" TexCoord0
Bind "texcoord1" TexCoord1
Vector 14 [unity_LightmapST]
Vector 15 [_Control_ST]
Vector 16 [_Splat0_ST]
Vector 17 [_Splat1_ST]
Vector 18 [_Splat2_ST]
"!!ARBvp1.0
# 9 ALU
PARAM c[19] = { program.local[0],
		state.matrix.mvp,
		program.local[5..18] };
MAD result.texcoord[0].zw, vertex.texcoord[0].xyxy, c[16].xyxy, c[16];
MAD result.texcoord[0].xy, vertex.texcoord[0], c[15], c[15].zwzw;
MAD result.texcoord[1].zw, vertex.texcoord[0].xyxy, c[18].xyxy, c[18];
MAD result.texcoord[1].xy, vertex.texcoord[0], c[17], c[17].zwzw;
MAD result.texcoord[2].xy, vertex.texcoord[1], c[14], c[14].zwzw;
DP4 result.position.w, vertex.position, c[4];
DP4 result.position.z, vertex.position, c[3];
DP4 result.position.y, vertex.position, c[2];
DP4 result.position.x, vertex.position, c[1];
END
# 9 instructions, 0 R-regs
"
}

SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "DIRLIGHTMAP_OFF" "SHADOWS_OFF" }
Bind "vertex" Vertex
Bind "texcoord" TexCoord0
Bind "texcoord1" TexCoord1
Matrix 0 [glstate_matrix_mvp]
Vector 12 [unity_LightmapST]
Vector 13 [_Control_ST]
Vector 14 [_Splat0_ST]
Vector 15 [_Splat1_ST]
Vector 16 [_Splat2_ST]
"vs_2_0
; 9 ALU
dcl_position0 v0
dcl_texcoord0 v3
dcl_texcoord1 v4
mad oT0.zw, v3.xyxy, c14.xyxy, c14
mad oT0.xy, v3, c13, c13.zwzw
mad oT1.zw, v3.xyxy, c16.xyxy, c16
mad oT1.xy, v3, c15, c15.zwzw
mad oT2.xy, v4, c12, c12.zwzw
dp4 oPos.w, v0, c3
dp4 oPos.z, v0, c2
dp4 oPos.y, v0, c1
dp4 oPos.x, v0, c0
"
}

SubProgram "gles " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "DIRLIGHTMAP_OFF" "SHADOWS_OFF" }
"!!GLES
#define SHADER_API_GLES 1
#define tex2D texture2D


#ifdef VERTEX
#define gl_ModelViewProjectionMatrix glstate_matrix_mvp
uniform mat4 glstate_matrix_mvp;

varying highp vec2 xlv_TEXCOORD2;
varying highp vec4 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;
uniform highp vec4 unity_LightmapST;

uniform highp vec4 _Splat2_ST;
uniform highp vec4 _Splat1_ST;
uniform highp vec4 _Splat0_ST;
uniform highp vec4 _Control_ST;
attribute vec4 _glesMultiTexCoord1;
attribute vec4 _glesMultiTexCoord0;
attribute vec4 _glesVertex;
void main ()
{
  highp vec4 tmpvar_1;
  highp vec4 tmpvar_2;
  tmpvar_1.xy = ((_glesMultiTexCoord0.xy * _Control_ST.xy) + _Control_ST.zw);
  tmpvar_1.zw = ((_glesMultiTexCoord0.xy * _Splat0_ST.xy) + _Splat0_ST.zw);
  tmpvar_2.xy = ((_glesMultiTexCoord0.xy * _Splat1_ST.xy) + _Splat1_ST.zw);
  tmpvar_2.zw = ((_glesMultiTexCoord0.xy * _Splat2_ST.xy) + _Splat2_ST.zw);
  gl_Position = (gl_ModelViewProjectionMatrix * _glesVertex);
  xlv_TEXCOORD0 = tmpvar_1;
  xlv_TEXCOORD1 = tmpvar_2;
  xlv_TEXCOORD2 = ((_glesMultiTexCoord1.xy * unity_LightmapST.xy) + unity_LightmapST.zw);
}



#endif
#ifdef FRAGMENT

varying highp vec2 xlv_TEXCOORD2;
varying highp vec4 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;
uniform sampler2D unity_Lightmap;
uniform sampler2D _Splat2;
uniform sampler2D _Splat1;
uniform sampler2D _Splat0;
uniform sampler2D _Control;
void main ()
{
  lowp vec4 c;
  highp vec2 tmpvar_1;
  tmpvar_1 = xlv_TEXCOORD0.xy;
  highp vec2 tmpvar_2;
  tmpvar_2 = xlv_TEXCOORD0.zw;
  highp vec2 tmpvar_3;
  tmpvar_3 = xlv_TEXCOORD1.xy;
  highp vec2 tmpvar_4;
  tmpvar_4 = xlv_TEXCOORD1.zw;
  lowp vec4 tmpvar_5;
  tmpvar_5 = texture2D (_Control, tmpvar_1);
  c = vec4(0.0, 0.0, 0.0, 0.0);
  c.xyz = ((((texture2D (_Splat0, tmpvar_2).xyz * tmpvar_5.x) + (texture2D (_Splat1, tmpvar_3).xyz * tmpvar_5.y)) + (texture2D (_Splat2, tmpvar_4).xyz * tmpvar_5.z)) * (2.0 * texture2D (unity_Lightmap, xlv_TEXCOORD2).xyz));
  c.w = 0.0;
  gl_FragData[0] = c;
}



#endif"
}

SubProgram "glesdesktop " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "DIRLIGHTMAP_OFF" "SHADOWS_OFF" }
"!!GLES
#define SHADER_API_GLES 1
#define tex2D texture2D


#ifdef VERTEX
#define gl_ModelViewProjectionMatrix glstate_matrix_mvp
uniform mat4 glstate_matrix_mvp;

varying highp vec2 xlv_TEXCOORD2;
varying highp vec4 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;
uniform highp vec4 unity_LightmapST;

uniform highp vec4 _Splat2_ST;
uniform highp vec4 _Splat1_ST;
uniform highp vec4 _Splat0_ST;
uniform highp vec4 _Control_ST;
attribute vec4 _glesMultiTexCoord1;
attribute vec4 _glesMultiTexCoord0;
attribute vec4 _glesVertex;
void main ()
{
  highp vec4 tmpvar_1;
  highp vec4 tmpvar_2;
  tmpvar_1.xy = ((_glesMultiTexCoord0.xy * _Control_ST.xy) + _Control_ST.zw);
  tmpvar_1.zw = ((_glesMultiTexCoord0.xy * _Splat0_ST.xy) + _Splat0_ST.zw);
  tmpvar_2.xy = ((_glesMultiTexCoord0.xy * _Splat1_ST.xy) + _Splat1_ST.zw);
  tmpvar_2.zw = ((_glesMultiTexCoord0.xy * _Splat2_ST.xy) + _Splat2_ST.zw);
  gl_Position = (gl_ModelViewProjectionMatrix * _glesVertex);
  xlv_TEXCOORD0 = tmpvar_1;
  xlv_TEXCOORD1 = tmpvar_2;
  xlv_TEXCOORD2 = ((_glesMultiTexCoord1.xy * unity_LightmapST.xy) + unity_LightmapST.zw);
}



#endif
#ifdef FRAGMENT

varying highp vec2 xlv_TEXCOORD2;
varying highp vec4 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;
uniform sampler2D unity_Lightmap;
uniform sampler2D _Splat2;
uniform sampler2D _Splat1;
uniform sampler2D _Splat0;
uniform sampler2D _Control;
uniform sampler2D _BumpSplat2;
uniform sampler2D _BumpSplat1;
uniform sampler2D _BumpSplat0;
void main ()
{
  lowp vec4 c;
  highp vec2 tmpvar_1;
  tmpvar_1 = xlv_TEXCOORD0.xy;
  highp vec2 tmpvar_2;
  tmpvar_2 = xlv_TEXCOORD0.zw;
  highp vec2 tmpvar_3;
  tmpvar_3 = xlv_TEXCOORD1.xy;
  highp vec2 tmpvar_4;
  tmpvar_4 = xlv_TEXCOORD1.zw;
  lowp vec4 tmpvar_5;
  tmpvar_5 = texture2D (_Control, tmpvar_1);
  lowp vec3 normal;
  normal.xy = ((texture2D (_BumpSplat0, tmpvar_2).wy * 2.0) - 1.0);
  normal.z = sqrt (((1.0 - (normal.x * normal.x)) - (normal.y * normal.y)));
  lowp vec3 normal_i0;
  normal_i0.xy = ((texture2D (_BumpSplat1, tmpvar_3).wy * 2.0) - 1.0);
  normal_i0.z = sqrt (((1.0 - (normal_i0.x * normal_i0.x)) - (normal_i0.y * normal_i0.y)));
  lowp vec3 normal_i0_i1;
  normal_i0_i1.xy = ((texture2D (_BumpSplat2, tmpvar_4).wy * 2.0) - 1.0);
  normal_i0_i1.z = sqrt (((1.0 - (normal_i0_i1.x * normal_i0_i1.x)) - (normal_i0_i1.y * normal_i0_i1.y)));
  c = vec4(0.0, 0.0, 0.0, 0.0);
  lowp vec4 tmpvar_6;
  tmpvar_6 = texture2D (unity_Lightmap, xlv_TEXCOORD2);
  c.xyz = ((((texture2D (_Splat0, tmpvar_2).xyz * tmpvar_5.x) + (texture2D (_Splat1, tmpvar_3).xyz * tmpvar_5.y)) + (texture2D (_Splat2, tmpvar_4).xyz * tmpvar_5.z)) * ((8.0 * tmpvar_6.w) * tmpvar_6.xyz));
  c.w = 0.0;
  gl_FragData[0] = c;
}



#endif"
}

SubProgram "opengl " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "DIRLIGHTMAP_ON" "SHADOWS_OFF" }
Bind "vertex" Vertex
Bind "texcoord" TexCoord0
Bind "texcoord1" TexCoord1
Vector 14 [unity_LightmapST]
Vector 15 [_Control_ST]
Vector 16 [_Splat0_ST]
Vector 17 [_Splat1_ST]
Vector 18 [_Splat2_ST]
"!!ARBvp1.0
# 9 ALU
PARAM c[19] = { program.local[0],
		state.matrix.mvp,
		program.local[5..18] };
MAD result.texcoord[0].zw, vertex.texcoord[0].xyxy, c[16].xyxy, c[16];
MAD result.texcoord[0].xy, vertex.texcoord[0], c[15], c[15].zwzw;
MAD result.texcoord[1].zw, vertex.texcoord[0].xyxy, c[18].xyxy, c[18];
MAD result.texcoord[1].xy, vertex.texcoord[0], c[17], c[17].zwzw;
MAD result.texcoord[2].xy, vertex.texcoord[1], c[14], c[14].zwzw;
DP4 result.position.w, vertex.position, c[4];
DP4 result.position.z, vertex.position, c[3];
DP4 result.position.y, vertex.position, c[2];
DP4 result.position.x, vertex.position, c[1];
END
# 9 instructions, 0 R-regs
"
}

SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "DIRLIGHTMAP_ON" "SHADOWS_OFF" }
Bind "vertex" Vertex
Bind "texcoord" TexCoord0
Bind "texcoord1" TexCoord1
Matrix 0 [glstate_matrix_mvp]
Vector 12 [unity_LightmapST]
Vector 13 [_Control_ST]
Vector 14 [_Splat0_ST]
Vector 15 [_Splat1_ST]
Vector 16 [_Splat2_ST]
"vs_2_0
; 9 ALU
dcl_position0 v0
dcl_texcoord0 v3
dcl_texcoord1 v4
mad oT0.zw, v3.xyxy, c14.xyxy, c14
mad oT0.xy, v3, c13, c13.zwzw
mad oT1.zw, v3.xyxy, c16.xyxy, c16
mad oT1.xy, v3, c15, c15.zwzw
mad oT2.xy, v4, c12, c12.zwzw
dp4 oPos.w, v0, c3
dp4 oPos.z, v0, c2
dp4 oPos.y, v0, c1
dp4 oPos.x, v0, c0
"
}

SubProgram "gles " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "DIRLIGHTMAP_ON" "SHADOWS_OFF" }
"!!GLES
#define SHADER_API_GLES 1
#define tex2D texture2D


#ifdef VERTEX
#define gl_ModelViewProjectionMatrix glstate_matrix_mvp
uniform mat4 glstate_matrix_mvp;

varying highp vec2 xlv_TEXCOORD2;
varying highp vec4 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;
uniform highp vec4 unity_LightmapST;

uniform highp vec4 _Splat2_ST;
uniform highp vec4 _Splat1_ST;
uniform highp vec4 _Splat0_ST;
uniform highp vec4 _Control_ST;
attribute vec4 _glesMultiTexCoord1;
attribute vec4 _glesMultiTexCoord0;
attribute vec4 _glesVertex;
void main ()
{
  highp vec4 tmpvar_1;
  highp vec4 tmpvar_2;
  tmpvar_1.xy = ((_glesMultiTexCoord0.xy * _Control_ST.xy) + _Control_ST.zw);
  tmpvar_1.zw = ((_glesMultiTexCoord0.xy * _Splat0_ST.xy) + _Splat0_ST.zw);
  tmpvar_2.xy = ((_glesMultiTexCoord0.xy * _Splat1_ST.xy) + _Splat1_ST.zw);
  tmpvar_2.zw = ((_glesMultiTexCoord0.xy * _Splat2_ST.xy) + _Splat2_ST.zw);
  gl_Position = (gl_ModelViewProjectionMatrix * _glesVertex);
  xlv_TEXCOORD0 = tmpvar_1;
  xlv_TEXCOORD1 = tmpvar_2;
  xlv_TEXCOORD2 = ((_glesMultiTexCoord1.xy * unity_LightmapST.xy) + unity_LightmapST.zw);
}



#endif
#ifdef FRAGMENT

varying highp vec2 xlv_TEXCOORD2;
varying highp vec4 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;
uniform sampler2D unity_LightmapInd;
uniform sampler2D unity_Lightmap;
uniform sampler2D _Splat2;
uniform sampler2D _Splat1;
uniform sampler2D _Splat0;
uniform sampler2D _Control;
uniform sampler2D _BumpSplat2;
uniform sampler2D _BumpSplat1;
uniform sampler2D _BumpSplat0;
void main ()
{
  lowp vec4 c;
  highp vec2 tmpvar_1;
  tmpvar_1 = xlv_TEXCOORD0.xy;
  highp vec2 tmpvar_2;
  tmpvar_2 = xlv_TEXCOORD0.zw;
  highp vec2 tmpvar_3;
  tmpvar_3 = xlv_TEXCOORD1.xy;
  highp vec2 tmpvar_4;
  tmpvar_4 = xlv_TEXCOORD1.zw;
  lowp vec3 tmpvar_5;
  lowp vec4 tmpvar_6;
  tmpvar_6 = texture2D (_Control, tmpvar_1);
  tmpvar_5 = (((texture2D (_Splat0, tmpvar_2).xyz * tmpvar_6.x) + (texture2D (_Splat1, tmpvar_3).xyz * tmpvar_6.y)) + (texture2D (_Splat2, tmpvar_4).xyz * tmpvar_6.z));
  lowp vec3 tmpvar_7;
  tmpvar_7 = (((((texture2D (_BumpSplat0, tmpvar_2).xyz * 2.0) - 1.0) * tmpvar_6.x) + (((texture2D (_BumpSplat1, tmpvar_3).xyz * 2.0) - 1.0) * tmpvar_6.y)) + (((texture2D (_BumpSplat2, tmpvar_4).xyz * 2.0) - 1.0) * tmpvar_6.z));
  c = vec4(0.0, 0.0, 0.0, 0.0);
  mediump vec3 normal;
  normal = tmpvar_7;
  mediump vec3 scalePerBasisVector_i0;
  mediump vec3 lm_i0;
  lowp vec3 tmpvar_8;
  tmpvar_8 = (2.0 * texture2D (unity_Lightmap, xlv_TEXCOORD2).xyz);
  lm_i0 = tmpvar_8;
  lowp vec3 tmpvar_9;
  tmpvar_9 = (2.0 * texture2D (unity_LightmapInd, xlv_TEXCOORD2).xyz);
  scalePerBasisVector_i0 = tmpvar_9;
  lm_i0 = (lm_i0 * dot (clamp ((mat3(0.816497, -0.408248, -0.408248, 0.0, 0.707107, -0.707107, 0.57735, 0.57735, 0.57735) * normal), 0.0, 1.0), scalePerBasisVector_i0));
  mediump vec3 tmpvar_10;
  tmpvar_10 = (tmpvar_5 * lm_i0);
  c.xyz = tmpvar_10;
  c.w = 0.0;
  gl_FragData[0] = c;
}



#endif"
}

SubProgram "glesdesktop " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "DIRLIGHTMAP_ON" "SHADOWS_OFF" }
"!!GLES
#define SHADER_API_GLES 1
#define tex2D texture2D


#ifdef VERTEX
#define gl_ModelViewProjectionMatrix glstate_matrix_mvp
uniform mat4 glstate_matrix_mvp;

varying highp vec2 xlv_TEXCOORD2;
varying highp vec4 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;
uniform highp vec4 unity_LightmapST;

uniform highp vec4 _Splat2_ST;
uniform highp vec4 _Splat1_ST;
uniform highp vec4 _Splat0_ST;
uniform highp vec4 _Control_ST;
attribute vec4 _glesMultiTexCoord1;
attribute vec4 _glesMultiTexCoord0;
attribute vec4 _glesVertex;
void main ()
{
  highp vec4 tmpvar_1;
  highp vec4 tmpvar_2;
  tmpvar_1.xy = ((_glesMultiTexCoord0.xy * _Control_ST.xy) + _Control_ST.zw);
  tmpvar_1.zw = ((_glesMultiTexCoord0.xy * _Splat0_ST.xy) + _Splat0_ST.zw);
  tmpvar_2.xy = ((_glesMultiTexCoord0.xy * _Splat1_ST.xy) + _Splat1_ST.zw);
  tmpvar_2.zw = ((_glesMultiTexCoord0.xy * _Splat2_ST.xy) + _Splat2_ST.zw);
  gl_Position = (gl_ModelViewProjectionMatrix * _glesVertex);
  xlv_TEXCOORD0 = tmpvar_1;
  xlv_TEXCOORD1 = tmpvar_2;
  xlv_TEXCOORD2 = ((_glesMultiTexCoord1.xy * unity_LightmapST.xy) + unity_LightmapST.zw);
}



#endif
#ifdef FRAGMENT

varying highp vec2 xlv_TEXCOORD2;
varying highp vec4 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;
uniform sampler2D unity_LightmapInd;
uniform sampler2D unity_Lightmap;
uniform sampler2D _Splat2;
uniform sampler2D _Splat1;
uniform sampler2D _Splat0;
uniform sampler2D _Control;
uniform sampler2D _BumpSplat2;
uniform sampler2D _BumpSplat1;
uniform sampler2D _BumpSplat0;
void main ()
{
  lowp vec4 c;
  highp vec2 tmpvar_1;
  tmpvar_1 = xlv_TEXCOORD0.xy;
  highp vec2 tmpvar_2;
  tmpvar_2 = xlv_TEXCOORD0.zw;
  highp vec2 tmpvar_3;
  tmpvar_3 = xlv_TEXCOORD1.xy;
  highp vec2 tmpvar_4;
  tmpvar_4 = xlv_TEXCOORD1.zw;
  lowp vec3 tmpvar_5;
  lowp vec4 tmpvar_6;
  tmpvar_6 = texture2D (_Control, tmpvar_1);
  tmpvar_5 = (((texture2D (_Splat0, tmpvar_2).xyz * tmpvar_6.x) + (texture2D (_Splat1, tmpvar_3).xyz * tmpvar_6.y)) + (texture2D (_Splat2, tmpvar_4).xyz * tmpvar_6.z));
  lowp vec3 normal;
  normal.xy = ((texture2D (_BumpSplat0, tmpvar_2).wy * 2.0) - 1.0);
  normal.z = sqrt (((1.0 - (normal.x * normal.x)) - (normal.y * normal.y)));
  lowp vec3 normal_i0;
  normal_i0.xy = ((texture2D (_BumpSplat1, tmpvar_3).wy * 2.0) - 1.0);
  normal_i0.z = sqrt (((1.0 - (normal_i0.x * normal_i0.x)) - (normal_i0.y * normal_i0.y)));
  lowp vec3 normal_i0_i1;
  normal_i0_i1.xy = ((texture2D (_BumpSplat2, tmpvar_4).wy * 2.0) - 1.0);
  normal_i0_i1.z = sqrt (((1.0 - (normal_i0_i1.x * normal_i0_i1.x)) - (normal_i0_i1.y * normal_i0_i1.y)));
  lowp vec3 tmpvar_7;
  tmpvar_7 = (((normal * tmpvar_6.x) + (normal_i0 * tmpvar_6.y)) + (normal_i0_i1 * tmpvar_6.z));
  c = vec4(0.0, 0.0, 0.0, 0.0);
  lowp vec4 tmpvar_8;
  tmpvar_8 = texture2D (unity_Lightmap, xlv_TEXCOORD2);
  lowp vec4 tmpvar_9;
  tmpvar_9 = texture2D (unity_LightmapInd, xlv_TEXCOORD2);
  mediump vec3 normal_i0_i1_i2;
  normal_i0_i1_i2 = tmpvar_7;
  mediump vec3 scalePerBasisVector_i0;
  mediump vec3 lm_i0;
  lowp vec3 tmpvar_10;
  tmpvar_10 = ((8.0 * tmpvar_8.w) * tmpvar_8.xyz);
  lm_i0 = tmpvar_10;
  lowp vec3 tmpvar_11;
  tmpvar_11 = ((8.0 * tmpvar_9.w) * tmpvar_9.xyz);
  scalePerBasisVector_i0 = tmpvar_11;
  lm_i0 = (lm_i0 * dot (clamp ((mat3(0.816497, -0.408248, -0.408248, 0.0, 0.707107, -0.707107, 0.57735, 0.57735, 0.57735) * normal_i0_i1_i2), 0.0, 1.0), scalePerBasisVector_i0));
  mediump vec3 tmpvar_12;
  tmpvar_12 = (tmpvar_5 * lm_i0);
  c.xyz = tmpvar_12;
  c.w = 0.0;
  gl_FragData[0] = c;
}



#endif"
}

SubProgram "opengl " {
Keywords { "DIRECTIONAL" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "SHADOWS_SCREEN" }
Bind "vertex" Vertex
Bind "tangent" ATTR14
Bind "normal" Normal
Bind "texcoord" TexCoord0
Vector 13 [_ProjectionParams]
Vector 14 [unity_Scale]
Vector 15 [_WorldSpaceLightPos0]
Matrix 5 [_Object2World]
Matrix 9 [_World2Object]
Vector 16 [unity_SHAr]
Vector 17 [unity_SHAg]
Vector 18 [unity_SHAb]
Vector 19 [unity_SHBr]
Vector 20 [unity_SHBg]
Vector 21 [unity_SHBb]
Vector 22 [unity_SHC]
Vector 23 [_Control_ST]
Vector 24 [_Splat0_ST]
Vector 25 [_Splat1_ST]
Vector 26 [_Splat2_ST]
"!!ARBvp1.0
# 42 ALU
PARAM c[27] = { { 1, 0.5 },
		state.matrix.mvp,
		program.local[5..26] };
TEMP R0;
TEMP R1;
TEMP R2;
TEMP R3;
MUL R1.xyz, vertex.normal, c[14].w;
DP3 R2.w, R1, c[6];
DP3 R0.x, R1, c[5];
DP3 R0.z, R1, c[7];
MOV R0.y, R2.w;
MUL R1, R0.xyzz, R0.yzzx;
MOV R0.w, c[0].x;
DP4 R2.z, R0, c[18];
DP4 R2.y, R0, c[17];
DP4 R2.x, R0, c[16];
MUL R0.y, R2.w, R2.w;
DP4 R3.z, R1, c[21];
DP4 R3.y, R1, c[20];
DP4 R3.x, R1, c[19];
MOV R1.xyz, vertex.attrib[14];
DP4 R0.w, vertex.position, c[4];
ADD R3.xyz, R2, R3;
MAD R0.x, R0, R0, -R0.y;
MUL R2.xyz, R0.x, c[22];
MUL R0.xyz, vertex.normal.zxyw, R1.yzxw;
MAD R0.xyz, vertex.normal.yzxw, R1.zxyw, -R0;
MUL R0.xyz, R0, vertex.attrib[14].w;
MOV R1, c[15];
ADD result.texcoord[3].xyz, R3, R2;
DP4 R2.z, R1, c[11];
DP4 R2.y, R1, c[10];
DP4 R2.x, R1, c[9];
DP3 result.texcoord[2].y, R2, R0;
DP4 R0.z, vertex.position, c[3];
DP4 R0.x, vertex.position, c[1];
DP4 R0.y, vertex.position, c[2];
MUL R1.xyz, R0.xyww, c[0].y;
MUL R1.y, R1, c[13].x;
DP3 result.texcoord[2].z, vertex.normal, R2;
DP3 result.texcoord[2].x, R2, vertex.attrib[14];
ADD result.texcoord[4].xy, R1, R1.z;
MOV result.position, R0;
MOV result.texcoord[4].zw, R0;
MAD result.texcoord[0].zw, vertex.texcoord[0].xyxy, c[24].xyxy, c[24];
MAD result.texcoord[0].xy, vertex.texcoord[0], c[23], c[23].zwzw;
MAD result.texcoord[1].zw, vertex.texcoord[0].xyxy, c[26].xyxy, c[26];
MAD result.texcoord[1].xy, vertex.texcoord[0], c[25], c[25].zwzw;
END
# 42 instructions, 4 R-regs
"
}

SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "SHADOWS_SCREEN" }
Bind "vertex" Vertex
Bind "tangent" TexCoord2
Bind "normal" Normal
Bind "texcoord" TexCoord0
Matrix 0 [glstate_matrix_mvp]
Vector 12 [_ProjectionParams]
Vector 13 [_ScreenParams]
Vector 14 [unity_Scale]
Vector 15 [_WorldSpaceLightPos0]
Matrix 4 [_Object2World]
Matrix 8 [_World2Object]
Vector 16 [unity_SHAr]
Vector 17 [unity_SHAg]
Vector 18 [unity_SHAb]
Vector 19 [unity_SHBr]
Vector 20 [unity_SHBg]
Vector 21 [unity_SHBb]
Vector 22 [unity_SHC]
Vector 23 [_Control_ST]
Vector 24 [_Splat0_ST]
Vector 25 [_Splat1_ST]
Vector 26 [_Splat2_ST]
"vs_2_0
; 45 ALU
def c27, 1.00000000, 0.50000000, 0, 0
dcl_position0 v0
dcl_tangent0 v1
dcl_normal0 v2
dcl_texcoord0 v3
mul r1.xyz, v2, c14.w
dp3 r2.w, r1, c5
dp3 r0.x, r1, c4
dp3 r0.z, r1, c6
mov r0.y, r2.w
mul r1, r0.xyzz, r0.yzzx
mov r0.w, c27.x
dp4 r2.z, r0, c18
dp4 r2.y, r0, c17
dp4 r2.x, r0, c16
mul r0.y, r2.w, r2.w
dp4 r3.z, r1, c21
dp4 r3.y, r1, c20
dp4 r3.x, r1, c19
mad r0.x, r0, r0, -r0.y
mul r1.xyz, r0.x, c22
add r2.xyz, r2, r3
mov r0.xyz, v1
add oT3.xyz, r2, r1
mul r1.xyz, v2.zxyw, r0.yzxw
mov r0.xyz, v1
mad r1.xyz, v2.yzxw, r0.zxyw, -r1
mov r0, c10
dp4 r3.z, c15, r0
mov r0, c9
dp4 r3.y, c15, r0
mul r2.xyz, r1, v1.w
mov r1, c8
dp4 r3.x, c15, r1
dp4 r0.w, v0, c3
dp4 r0.z, v0, c2
dp4 r0.x, v0, c0
dp4 r0.y, v0, c1
mul r1.xyz, r0.xyww, c27.y
mul r1.y, r1, c12.x
dp3 oT2.y, r3, r2
dp3 oT2.z, v2, r3
dp3 oT2.x, r3, v1
mad oT4.xy, r1.z, c13.zwzw, r1
mov oPos, r0
mov oT4.zw, r0
mad oT0.zw, v3.xyxy, c24.xyxy, c24
mad oT0.xy, v3, c23, c23.zwzw
mad oT1.zw, v3.xyxy, c26.xyxy, c26
mad oT1.xy, v3, c25, c25.zwzw
"
}

SubProgram "gles " {
Keywords { "DIRECTIONAL" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "SHADOWS_SCREEN" }
"!!GLES
#define SHADER_API_GLES 1
#define tex2D texture2D


#ifdef VERTEX
#define gl_ModelViewProjectionMatrix glstate_matrix_mvp
uniform mat4 glstate_matrix_mvp;

varying highp vec4 xlv_TEXCOORD4;
varying lowp vec3 xlv_TEXCOORD3;
varying lowp vec3 xlv_TEXCOORD2;
varying highp vec4 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;
uniform highp vec4 unity_Scale;
uniform highp vec4 unity_SHC;
uniform highp vec4 unity_SHBr;
uniform highp vec4 unity_SHBg;
uniform highp vec4 unity_SHBb;
uniform highp vec4 unity_SHAr;
uniform highp vec4 unity_SHAg;
uniform highp vec4 unity_SHAb;

uniform lowp vec4 _WorldSpaceLightPos0;
uniform highp mat4 _World2Object;
uniform highp vec4 _Splat2_ST;
uniform highp vec4 _Splat1_ST;
uniform highp vec4 _Splat0_ST;
uniform highp vec4 _ProjectionParams;
uniform highp mat4 _Object2World;
uniform highp vec4 _Control_ST;
attribute vec4 _glesTANGENT;
attribute vec4 _glesMultiTexCoord0;
attribute vec3 _glesNormal;
attribute vec4 _glesVertex;
void main ()
{
  vec4 tmpvar_1;
  tmpvar_1.xyz = normalize (_glesTANGENT.xyz);
  tmpvar_1.w = _glesTANGENT.w;
  vec3 tmpvar_2;
  tmpvar_2 = normalize (_glesNormal);
  highp vec3 shlight;
  highp vec4 tmpvar_3;
  highp vec4 tmpvar_4;
  lowp vec3 tmpvar_5;
  lowp vec3 tmpvar_6;
  highp vec4 tmpvar_7;
  tmpvar_7 = (gl_ModelViewProjectionMatrix * _glesVertex);
  tmpvar_3.xy = ((_glesMultiTexCoord0.xy * _Control_ST.xy) + _Control_ST.zw);
  tmpvar_3.zw = ((_glesMultiTexCoord0.xy * _Splat0_ST.xy) + _Splat0_ST.zw);
  tmpvar_4.xy = ((_glesMultiTexCoord0.xy * _Splat1_ST.xy) + _Splat1_ST.zw);
  tmpvar_4.zw = ((_glesMultiTexCoord0.xy * _Splat2_ST.xy) + _Splat2_ST.zw);
  mat3 tmpvar_8;
  tmpvar_8[0] = _Object2World[0].xyz;
  tmpvar_8[1] = _Object2World[1].xyz;
  tmpvar_8[2] = _Object2World[2].xyz;
  highp mat3 tmpvar_9;
  tmpvar_9[0] = tmpvar_1.xyz;
  tmpvar_9[1] = (cross (tmpvar_2, tmpvar_1.xyz) * _glesTANGENT.w);
  tmpvar_9[2] = tmpvar_2;
  mat3 tmpvar_10;
  tmpvar_10[0].x = tmpvar_9[0].x;
  tmpvar_10[0].y = tmpvar_9[1].x;
  tmpvar_10[0].z = tmpvar_9[2].x;
  tmpvar_10[1].x = tmpvar_9[0].y;
  tmpvar_10[1].y = tmpvar_9[1].y;
  tmpvar_10[1].z = tmpvar_9[2].y;
  tmpvar_10[2].x = tmpvar_9[0].z;
  tmpvar_10[2].y = tmpvar_9[1].z;
  tmpvar_10[2].z = tmpvar_9[2].z;
  highp vec3 tmpvar_11;
  tmpvar_11 = (tmpvar_10 * (_World2Object * _WorldSpaceLightPos0).xyz);
  tmpvar_5 = tmpvar_11;
  highp vec4 tmpvar_12;
  tmpvar_12.w = 1.0;
  tmpvar_12.xyz = (tmpvar_8 * (tmpvar_2 * unity_Scale.w));
  mediump vec3 tmpvar_13;
  mediump vec4 normal;
  normal = tmpvar_12;
  mediump vec3 x3;
  highp float vC;
  mediump vec3 x2;
  mediump vec3 x1;
  highp float tmpvar_14;
  tmpvar_14 = dot (unity_SHAr, normal);
  x1.x = tmpvar_14;
  highp float tmpvar_15;
  tmpvar_15 = dot (unity_SHAg, normal);
  x1.y = tmpvar_15;
  highp float tmpvar_16;
  tmpvar_16 = dot (unity_SHAb, normal);
  x1.z = tmpvar_16;
  mediump vec4 tmpvar_17;
  tmpvar_17 = (normal.xyzz * normal.yzzx);
  highp float tmpvar_18;
  tmpvar_18 = dot (unity_SHBr, tmpvar_17);
  x2.x = tmpvar_18;
  highp float tmpvar_19;
  tmpvar_19 = dot (unity_SHBg, tmpvar_17);
  x2.y = tmpvar_19;
  highp float tmpvar_20;
  tmpvar_20 = dot (unity_SHBb, tmpvar_17);
  x2.z = tmpvar_20;
  mediump float tmpvar_21;
  tmpvar_21 = ((normal.x * normal.x) - (normal.y * normal.y));
  vC = tmpvar_21;
  highp vec3 tmpvar_22;
  tmpvar_22 = (unity_SHC.xyz * vC);
  x3 = tmpvar_22;
  tmpvar_13 = ((x1 + x2) + x3);
  shlight = tmpvar_13;
  tmpvar_6 = shlight;
  highp vec4 o_i0;
  highp vec4 tmpvar_23;
  tmpvar_23 = (tmpvar_7 * 0.5);
  o_i0 = tmpvar_23;
  highp vec2 tmpvar_24;
  tmpvar_24.x = tmpvar_23.x;
  tmpvar_24.y = (tmpvar_23.y * _ProjectionParams.x);
  o_i0.xy = (tmpvar_24 + tmpvar_23.w);
  o_i0.zw = tmpvar_7.zw;
  gl_Position = tmpvar_7;
  xlv_TEXCOORD0 = tmpvar_3;
  xlv_TEXCOORD1 = tmpvar_4;
  xlv_TEXCOORD2 = tmpvar_5;
  xlv_TEXCOORD3 = tmpvar_6;
  xlv_TEXCOORD4 = o_i0;
}



#endif
#ifdef FRAGMENT

varying highp vec4 xlv_TEXCOORD4;
varying lowp vec3 xlv_TEXCOORD3;
varying lowp vec3 xlv_TEXCOORD2;
varying highp vec4 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;
uniform sampler2D _Splat2;
uniform sampler2D _Splat1;
uniform sampler2D _Splat0;
uniform sampler2D _ShadowMapTexture;
uniform lowp vec4 _LightColor0;
uniform sampler2D _Control;
uniform sampler2D _BumpSplat2;
uniform sampler2D _BumpSplat1;
uniform sampler2D _BumpSplat0;
void main ()
{
  lowp vec4 c;
  highp vec2 tmpvar_1;
  tmpvar_1 = xlv_TEXCOORD0.xy;
  highp vec2 tmpvar_2;
  tmpvar_2 = xlv_TEXCOORD0.zw;
  highp vec2 tmpvar_3;
  tmpvar_3 = xlv_TEXCOORD1.xy;
  highp vec2 tmpvar_4;
  tmpvar_4 = xlv_TEXCOORD1.zw;
  lowp vec3 tmpvar_5;
  lowp vec4 tmpvar_6;
  tmpvar_6 = texture2D (_Control, tmpvar_1);
  tmpvar_5 = (((texture2D (_Splat0, tmpvar_2).xyz * tmpvar_6.x) + (texture2D (_Splat1, tmpvar_3).xyz * tmpvar_6.y)) + (texture2D (_Splat2, tmpvar_4).xyz * tmpvar_6.z));
  lowp vec4 c_i0;
  c_i0.xyz = ((tmpvar_5 * _LightColor0.xyz) * ((max (0.0, dot ((((((texture2D (_BumpSplat0, tmpvar_2).xyz * 2.0) - 1.0) * tmpvar_6.x) + (((texture2D (_BumpSplat1, tmpvar_3).xyz * 2.0) - 1.0) * tmpvar_6.y)) + (((texture2D (_BumpSplat2, tmpvar_4).xyz * 2.0) - 1.0) * tmpvar_6.z)), xlv_TEXCOORD2)) * texture2DProj (_ShadowMapTexture, xlv_TEXCOORD4).x) * 2.0));
  c_i0.w = 0.0;
  c = c_i0;
  c.xyz = (c_i0.xyz + (tmpvar_5 * xlv_TEXCOORD3));
  gl_FragData[0] = c;
}



#endif"
}

SubProgram "glesdesktop " {
Keywords { "DIRECTIONAL" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "SHADOWS_SCREEN" }
"!!GLES
#define SHADER_API_GLES 1
#define tex2D texture2D


#ifdef VERTEX
#define gl_ModelViewProjectionMatrix glstate_matrix_mvp
uniform mat4 glstate_matrix_mvp;

varying highp vec4 xlv_TEXCOORD4;
varying lowp vec3 xlv_TEXCOORD3;
varying lowp vec3 xlv_TEXCOORD2;
varying highp vec4 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;
uniform highp vec4 unity_Scale;
uniform highp vec4 unity_SHC;
uniform highp vec4 unity_SHBr;
uniform highp vec4 unity_SHBg;
uniform highp vec4 unity_SHBb;
uniform highp vec4 unity_SHAr;
uniform highp vec4 unity_SHAg;
uniform highp vec4 unity_SHAb;

uniform lowp vec4 _WorldSpaceLightPos0;
uniform highp mat4 _World2Object;
uniform highp vec4 _Splat2_ST;
uniform highp vec4 _Splat1_ST;
uniform highp vec4 _Splat0_ST;
uniform highp vec4 _ProjectionParams;
uniform highp mat4 _Object2World;
uniform highp vec4 _Control_ST;
attribute vec4 _glesTANGENT;
attribute vec4 _glesMultiTexCoord0;
attribute vec3 _glesNormal;
attribute vec4 _glesVertex;
void main ()
{
  vec4 tmpvar_1;
  tmpvar_1.xyz = normalize (_glesTANGENT.xyz);
  tmpvar_1.w = _glesTANGENT.w;
  vec3 tmpvar_2;
  tmpvar_2 = normalize (_glesNormal);
  highp vec3 shlight;
  highp vec4 tmpvar_3;
  highp vec4 tmpvar_4;
  lowp vec3 tmpvar_5;
  lowp vec3 tmpvar_6;
  highp vec4 tmpvar_7;
  tmpvar_7 = (gl_ModelViewProjectionMatrix * _glesVertex);
  tmpvar_3.xy = ((_glesMultiTexCoord0.xy * _Control_ST.xy) + _Control_ST.zw);
  tmpvar_3.zw = ((_glesMultiTexCoord0.xy * _Splat0_ST.xy) + _Splat0_ST.zw);
  tmpvar_4.xy = ((_glesMultiTexCoord0.xy * _Splat1_ST.xy) + _Splat1_ST.zw);
  tmpvar_4.zw = ((_glesMultiTexCoord0.xy * _Splat2_ST.xy) + _Splat2_ST.zw);
  mat3 tmpvar_8;
  tmpvar_8[0] = _Object2World[0].xyz;
  tmpvar_8[1] = _Object2World[1].xyz;
  tmpvar_8[2] = _Object2World[2].xyz;
  highp mat3 tmpvar_9;
  tmpvar_9[0] = tmpvar_1.xyz;
  tmpvar_9[1] = (cross (tmpvar_2, tmpvar_1.xyz) * _glesTANGENT.w);
  tmpvar_9[2] = tmpvar_2;
  mat3 tmpvar_10;
  tmpvar_10[0].x = tmpvar_9[0].x;
  tmpvar_10[0].y = tmpvar_9[1].x;
  tmpvar_10[0].z = tmpvar_9[2].x;
  tmpvar_10[1].x = tmpvar_9[0].y;
  tmpvar_10[1].y = tmpvar_9[1].y;
  tmpvar_10[1].z = tmpvar_9[2].y;
  tmpvar_10[2].x = tmpvar_9[0].z;
  tmpvar_10[2].y = tmpvar_9[1].z;
  tmpvar_10[2].z = tmpvar_9[2].z;
  highp vec3 tmpvar_11;
  tmpvar_11 = (tmpvar_10 * (_World2Object * _WorldSpaceLightPos0).xyz);
  tmpvar_5 = tmpvar_11;
  highp vec4 tmpvar_12;
  tmpvar_12.w = 1.0;
  tmpvar_12.xyz = (tmpvar_8 * (tmpvar_2 * unity_Scale.w));
  mediump vec3 tmpvar_13;
  mediump vec4 normal;
  normal = tmpvar_12;
  mediump vec3 x3;
  highp float vC;
  mediump vec3 x2;
  mediump vec3 x1;
  highp float tmpvar_14;
  tmpvar_14 = dot (unity_SHAr, normal);
  x1.x = tmpvar_14;
  highp float tmpvar_15;
  tmpvar_15 = dot (unity_SHAg, normal);
  x1.y = tmpvar_15;
  highp float tmpvar_16;
  tmpvar_16 = dot (unity_SHAb, normal);
  x1.z = tmpvar_16;
  mediump vec4 tmpvar_17;
  tmpvar_17 = (normal.xyzz * normal.yzzx);
  highp float tmpvar_18;
  tmpvar_18 = dot (unity_SHBr, tmpvar_17);
  x2.x = tmpvar_18;
  highp float tmpvar_19;
  tmpvar_19 = dot (unity_SHBg, tmpvar_17);
  x2.y = tmpvar_19;
  highp float tmpvar_20;
  tmpvar_20 = dot (unity_SHBb, tmpvar_17);
  x2.z = tmpvar_20;
  mediump float tmpvar_21;
  tmpvar_21 = ((normal.x * normal.x) - (normal.y * normal.y));
  vC = tmpvar_21;
  highp vec3 tmpvar_22;
  tmpvar_22 = (unity_SHC.xyz * vC);
  x3 = tmpvar_22;
  tmpvar_13 = ((x1 + x2) + x3);
  shlight = tmpvar_13;
  tmpvar_6 = shlight;
  highp vec4 o_i0;
  highp vec4 tmpvar_23;
  tmpvar_23 = (tmpvar_7 * 0.5);
  o_i0 = tmpvar_23;
  highp vec2 tmpvar_24;
  tmpvar_24.x = tmpvar_23.x;
  tmpvar_24.y = (tmpvar_23.y * _ProjectionParams.x);
  o_i0.xy = (tmpvar_24 + tmpvar_23.w);
  o_i0.zw = tmpvar_7.zw;
  gl_Position = tmpvar_7;
  xlv_TEXCOORD0 = tmpvar_3;
  xlv_TEXCOORD1 = tmpvar_4;
  xlv_TEXCOORD2 = tmpvar_5;
  xlv_TEXCOORD3 = tmpvar_6;
  xlv_TEXCOORD4 = o_i0;
}



#endif
#ifdef FRAGMENT

varying highp vec4 xlv_TEXCOORD4;
varying lowp vec3 xlv_TEXCOORD3;
varying lowp vec3 xlv_TEXCOORD2;
varying highp vec4 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;
uniform sampler2D _Splat2;
uniform sampler2D _Splat1;
uniform sampler2D _Splat0;
uniform sampler2D _ShadowMapTexture;
uniform lowp vec4 _LightColor0;
uniform sampler2D _Control;
uniform sampler2D _BumpSplat2;
uniform sampler2D _BumpSplat1;
uniform sampler2D _BumpSplat0;
void main ()
{
  lowp vec4 c;
  highp vec2 tmpvar_1;
  tmpvar_1 = xlv_TEXCOORD0.xy;
  highp vec2 tmpvar_2;
  tmpvar_2 = xlv_TEXCOORD0.zw;
  highp vec2 tmpvar_3;
  tmpvar_3 = xlv_TEXCOORD1.xy;
  highp vec2 tmpvar_4;
  tmpvar_4 = xlv_TEXCOORD1.zw;
  lowp vec3 tmpvar_5;
  lowp vec4 tmpvar_6;
  tmpvar_6 = texture2D (_Control, tmpvar_1);
  tmpvar_5 = (((texture2D (_Splat0, tmpvar_2).xyz * tmpvar_6.x) + (texture2D (_Splat1, tmpvar_3).xyz * tmpvar_6.y)) + (texture2D (_Splat2, tmpvar_4).xyz * tmpvar_6.z));
  lowp vec3 normal;
  normal.xy = ((texture2D (_BumpSplat0, tmpvar_2).wy * 2.0) - 1.0);
  normal.z = sqrt (((1.0 - (normal.x * normal.x)) - (normal.y * normal.y)));
  lowp vec3 normal_i0;
  normal_i0.xy = ((texture2D (_BumpSplat1, tmpvar_3).wy * 2.0) - 1.0);
  normal_i0.z = sqrt (((1.0 - (normal_i0.x * normal_i0.x)) - (normal_i0.y * normal_i0.y)));
  lowp vec3 normal_i0_i1;
  normal_i0_i1.xy = ((texture2D (_BumpSplat2, tmpvar_4).wy * 2.0) - 1.0);
  normal_i0_i1.z = sqrt (((1.0 - (normal_i0_i1.x * normal_i0_i1.x)) - (normal_i0_i1.y * normal_i0_i1.y)));
  lowp vec4 c_i0;
  c_i0.xyz = ((tmpvar_5 * _LightColor0.xyz) * ((max (0.0, dot ((((normal * tmpvar_6.x) + (normal_i0 * tmpvar_6.y)) + (normal_i0_i1 * tmpvar_6.z)), xlv_TEXCOORD2)) * texture2DProj (_ShadowMapTexture, xlv_TEXCOORD4).x) * 2.0));
  c_i0.w = 0.0;
  c = c_i0;
  c.xyz = (c_i0.xyz + (tmpvar_5 * xlv_TEXCOORD3));
  gl_FragData[0] = c;
}



#endif"
}

SubProgram "opengl " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "DIRLIGHTMAP_OFF" "SHADOWS_SCREEN" }
Bind "vertex" Vertex
Bind "texcoord" TexCoord0
Bind "texcoord1" TexCoord1
Vector 13 [_ProjectionParams]
Vector 15 [unity_LightmapST]
Vector 16 [_Control_ST]
Vector 17 [_Splat0_ST]
Vector 18 [_Splat1_ST]
Vector 19 [_Splat2_ST]
"!!ARBvp1.0
# 14 ALU
PARAM c[20] = { { 0.5 },
		state.matrix.mvp,
		program.local[5..19] };
TEMP R0;
TEMP R1;
DP4 R0.w, vertex.position, c[4];
DP4 R0.z, vertex.position, c[3];
DP4 R0.x, vertex.position, c[1];
DP4 R0.y, vertex.position, c[2];
MUL R1.xyz, R0.xyww, c[0].x;
MUL R1.y, R1, c[13].x;
ADD result.texcoord[3].xy, R1, R1.z;
MOV result.position, R0;
MOV result.texcoord[3].zw, R0;
MAD result.texcoord[0].zw, vertex.texcoord[0].xyxy, c[17].xyxy, c[17];
MAD result.texcoord[0].xy, vertex.texcoord[0], c[16], c[16].zwzw;
MAD result.texcoord[1].zw, vertex.texcoord[0].xyxy, c[19].xyxy, c[19];
MAD result.texcoord[1].xy, vertex.texcoord[0], c[18], c[18].zwzw;
MAD result.texcoord[2].xy, vertex.texcoord[1], c[15], c[15].zwzw;
END
# 14 instructions, 2 R-regs
"
}

SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "DIRLIGHTMAP_OFF" "SHADOWS_SCREEN" }
Bind "vertex" Vertex
Bind "texcoord" TexCoord0
Bind "texcoord1" TexCoord1
Matrix 0 [glstate_matrix_mvp]
Vector 12 [_ProjectionParams]
Vector 13 [_ScreenParams]
Vector 14 [unity_LightmapST]
Vector 15 [_Control_ST]
Vector 16 [_Splat0_ST]
Vector 17 [_Splat1_ST]
Vector 18 [_Splat2_ST]
"vs_2_0
; 14 ALU
def c19, 0.50000000, 0, 0, 0
dcl_position0 v0
dcl_texcoord0 v3
dcl_texcoord1 v4
dp4 r0.w, v0, c3
dp4 r0.z, v0, c2
dp4 r0.x, v0, c0
dp4 r0.y, v0, c1
mul r1.xyz, r0.xyww, c19.x
mul r1.y, r1, c12.x
mad oT3.xy, r1.z, c13.zwzw, r1
mov oPos, r0
mov oT3.zw, r0
mad oT0.zw, v3.xyxy, c16.xyxy, c16
mad oT0.xy, v3, c15, c15.zwzw
mad oT1.zw, v3.xyxy, c18.xyxy, c18
mad oT1.xy, v3, c17, c17.zwzw
mad oT2.xy, v4, c14, c14.zwzw
"
}

SubProgram "gles " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "DIRLIGHTMAP_OFF" "SHADOWS_SCREEN" }
"!!GLES
#define SHADER_API_GLES 1
#define tex2D texture2D


#ifdef VERTEX
#define gl_ModelViewProjectionMatrix glstate_matrix_mvp
uniform mat4 glstate_matrix_mvp;

varying highp vec4 xlv_TEXCOORD3;
varying highp vec2 xlv_TEXCOORD2;
varying highp vec4 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;
uniform highp vec4 unity_LightmapST;

uniform highp vec4 _Splat2_ST;
uniform highp vec4 _Splat1_ST;
uniform highp vec4 _Splat0_ST;
uniform highp vec4 _ProjectionParams;
uniform highp vec4 _Control_ST;
attribute vec4 _glesMultiTexCoord1;
attribute vec4 _glesMultiTexCoord0;
attribute vec4 _glesVertex;
void main ()
{
  highp vec4 tmpvar_1;
  highp vec4 tmpvar_2;
  highp vec4 tmpvar_3;
  tmpvar_3 = (gl_ModelViewProjectionMatrix * _glesVertex);
  tmpvar_1.xy = ((_glesMultiTexCoord0.xy * _Control_ST.xy) + _Control_ST.zw);
  tmpvar_1.zw = ((_glesMultiTexCoord0.xy * _Splat0_ST.xy) + _Splat0_ST.zw);
  tmpvar_2.xy = ((_glesMultiTexCoord0.xy * _Splat1_ST.xy) + _Splat1_ST.zw);
  tmpvar_2.zw = ((_glesMultiTexCoord0.xy * _Splat2_ST.xy) + _Splat2_ST.zw);
  highp vec4 o_i0;
  highp vec4 tmpvar_4;
  tmpvar_4 = (tmpvar_3 * 0.5);
  o_i0 = tmpvar_4;
  highp vec2 tmpvar_5;
  tmpvar_5.x = tmpvar_4.x;
  tmpvar_5.y = (tmpvar_4.y * _ProjectionParams.x);
  o_i0.xy = (tmpvar_5 + tmpvar_4.w);
  o_i0.zw = tmpvar_3.zw;
  gl_Position = tmpvar_3;
  xlv_TEXCOORD0 = tmpvar_1;
  xlv_TEXCOORD1 = tmpvar_2;
  xlv_TEXCOORD2 = ((_glesMultiTexCoord1.xy * unity_LightmapST.xy) + unity_LightmapST.zw);
  xlv_TEXCOORD3 = o_i0;
}



#endif
#ifdef FRAGMENT

varying highp vec4 xlv_TEXCOORD3;
varying highp vec2 xlv_TEXCOORD2;
varying highp vec4 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;
uniform sampler2D unity_Lightmap;
uniform sampler2D _Splat2;
uniform sampler2D _Splat1;
uniform sampler2D _Splat0;
uniform sampler2D _ShadowMapTexture;
uniform sampler2D _Control;
void main ()
{
  lowp vec4 c;
  highp vec2 tmpvar_1;
  tmpvar_1 = xlv_TEXCOORD0.xy;
  highp vec2 tmpvar_2;
  tmpvar_2 = xlv_TEXCOORD0.zw;
  highp vec2 tmpvar_3;
  tmpvar_3 = xlv_TEXCOORD1.xy;
  highp vec2 tmpvar_4;
  tmpvar_4 = xlv_TEXCOORD1.zw;
  lowp vec4 tmpvar_5;
  tmpvar_5 = texture2D (_Control, tmpvar_1);
  c = vec4(0.0, 0.0, 0.0, 0.0);
  c.xyz = ((((texture2D (_Splat0, tmpvar_2).xyz * tmpvar_5.x) + (texture2D (_Splat1, tmpvar_3).xyz * tmpvar_5.y)) + (texture2D (_Splat2, tmpvar_4).xyz * tmpvar_5.z)) * min ((2.0 * texture2D (unity_Lightmap, xlv_TEXCOORD2).xyz), vec3((texture2DProj (_ShadowMapTexture, xlv_TEXCOORD3).x * 2.0))));
  c.w = 0.0;
  gl_FragData[0] = c;
}



#endif"
}

SubProgram "glesdesktop " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "DIRLIGHTMAP_OFF" "SHADOWS_SCREEN" }
"!!GLES
#define SHADER_API_GLES 1
#define tex2D texture2D


#ifdef VERTEX
#define gl_ModelViewProjectionMatrix glstate_matrix_mvp
uniform mat4 glstate_matrix_mvp;

varying highp vec4 xlv_TEXCOORD3;
varying highp vec2 xlv_TEXCOORD2;
varying highp vec4 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;
uniform highp vec4 unity_LightmapST;

uniform highp vec4 _Splat2_ST;
uniform highp vec4 _Splat1_ST;
uniform highp vec4 _Splat0_ST;
uniform highp vec4 _ProjectionParams;
uniform highp vec4 _Control_ST;
attribute vec4 _glesMultiTexCoord1;
attribute vec4 _glesMultiTexCoord0;
attribute vec4 _glesVertex;
void main ()
{
  highp vec4 tmpvar_1;
  highp vec4 tmpvar_2;
  highp vec4 tmpvar_3;
  tmpvar_3 = (gl_ModelViewProjectionMatrix * _glesVertex);
  tmpvar_1.xy = ((_glesMultiTexCoord0.xy * _Control_ST.xy) + _Control_ST.zw);
  tmpvar_1.zw = ((_glesMultiTexCoord0.xy * _Splat0_ST.xy) + _Splat0_ST.zw);
  tmpvar_2.xy = ((_glesMultiTexCoord0.xy * _Splat1_ST.xy) + _Splat1_ST.zw);
  tmpvar_2.zw = ((_glesMultiTexCoord0.xy * _Splat2_ST.xy) + _Splat2_ST.zw);
  highp vec4 o_i0;
  highp vec4 tmpvar_4;
  tmpvar_4 = (tmpvar_3 * 0.5);
  o_i0 = tmpvar_4;
  highp vec2 tmpvar_5;
  tmpvar_5.x = tmpvar_4.x;
  tmpvar_5.y = (tmpvar_4.y * _ProjectionParams.x);
  o_i0.xy = (tmpvar_5 + tmpvar_4.w);
  o_i0.zw = tmpvar_3.zw;
  gl_Position = tmpvar_3;
  xlv_TEXCOORD0 = tmpvar_1;
  xlv_TEXCOORD1 = tmpvar_2;
  xlv_TEXCOORD2 = ((_glesMultiTexCoord1.xy * unity_LightmapST.xy) + unity_LightmapST.zw);
  xlv_TEXCOORD3 = o_i0;
}



#endif
#ifdef FRAGMENT

varying highp vec4 xlv_TEXCOORD3;
varying highp vec2 xlv_TEXCOORD2;
varying highp vec4 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;
uniform sampler2D unity_Lightmap;
uniform sampler2D _Splat2;
uniform sampler2D _Splat1;
uniform sampler2D _Splat0;
uniform sampler2D _ShadowMapTexture;
uniform sampler2D _Control;
uniform sampler2D _BumpSplat2;
uniform sampler2D _BumpSplat1;
uniform sampler2D _BumpSplat0;
void main ()
{
  lowp vec4 c;
  highp vec2 tmpvar_1;
  tmpvar_1 = xlv_TEXCOORD0.xy;
  highp vec2 tmpvar_2;
  tmpvar_2 = xlv_TEXCOORD0.zw;
  highp vec2 tmpvar_3;
  tmpvar_3 = xlv_TEXCOORD1.xy;
  highp vec2 tmpvar_4;
  tmpvar_4 = xlv_TEXCOORD1.zw;
  lowp vec4 tmpvar_5;
  tmpvar_5 = texture2D (_Control, tmpvar_1);
  lowp vec3 normal;
  normal.xy = ((texture2D (_BumpSplat0, tmpvar_2).wy * 2.0) - 1.0);
  normal.z = sqrt (((1.0 - (normal.x * normal.x)) - (normal.y * normal.y)));
  lowp vec3 normal_i0;
  normal_i0.xy = ((texture2D (_BumpSplat1, tmpvar_3).wy * 2.0) - 1.0);
  normal_i0.z = sqrt (((1.0 - (normal_i0.x * normal_i0.x)) - (normal_i0.y * normal_i0.y)));
  lowp vec3 normal_i0_i1;
  normal_i0_i1.xy = ((texture2D (_BumpSplat2, tmpvar_4).wy * 2.0) - 1.0);
  normal_i0_i1.z = sqrt (((1.0 - (normal_i0_i1.x * normal_i0_i1.x)) - (normal_i0_i1.y * normal_i0_i1.y)));
  lowp vec4 tmpvar_6;
  tmpvar_6 = texture2DProj (_ShadowMapTexture, xlv_TEXCOORD3);
  c = vec4(0.0, 0.0, 0.0, 0.0);
  lowp vec4 tmpvar_7;
  tmpvar_7 = texture2D (unity_Lightmap, xlv_TEXCOORD2);
  lowp vec3 tmpvar_8;
  tmpvar_8 = ((8.0 * tmpvar_7.w) * tmpvar_7.xyz);
  c.xyz = ((((texture2D (_Splat0, tmpvar_2).xyz * tmpvar_5.x) + (texture2D (_Splat1, tmpvar_3).xyz * tmpvar_5.y)) + (texture2D (_Splat2, tmpvar_4).xyz * tmpvar_5.z)) * max (min (tmpvar_8, ((tmpvar_6.x * 2.0) * tmpvar_7.xyz)), (tmpvar_8 * tmpvar_6.x)));
  c.w = 0.0;
  gl_FragData[0] = c;
}



#endif"
}

SubProgram "opengl " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "DIRLIGHTMAP_ON" "SHADOWS_SCREEN" }
Bind "vertex" Vertex
Bind "texcoord" TexCoord0
Bind "texcoord1" TexCoord1
Vector 13 [_ProjectionParams]
Vector 15 [unity_LightmapST]
Vector 16 [_Control_ST]
Vector 17 [_Splat0_ST]
Vector 18 [_Splat1_ST]
Vector 19 [_Splat2_ST]
"!!ARBvp1.0
# 14 ALU
PARAM c[20] = { { 0.5 },
		state.matrix.mvp,
		program.local[5..19] };
TEMP R0;
TEMP R1;
DP4 R0.w, vertex.position, c[4];
DP4 R0.z, vertex.position, c[3];
DP4 R0.x, vertex.position, c[1];
DP4 R0.y, vertex.position, c[2];
MUL R1.xyz, R0.xyww, c[0].x;
MUL R1.y, R1, c[13].x;
ADD result.texcoord[3].xy, R1, R1.z;
MOV result.position, R0;
MOV result.texcoord[3].zw, R0;
MAD result.texcoord[0].zw, vertex.texcoord[0].xyxy, c[17].xyxy, c[17];
MAD result.texcoord[0].xy, vertex.texcoord[0], c[16], c[16].zwzw;
MAD result.texcoord[1].zw, vertex.texcoord[0].xyxy, c[19].xyxy, c[19];
MAD result.texcoord[1].xy, vertex.texcoord[0], c[18], c[18].zwzw;
MAD result.texcoord[2].xy, vertex.texcoord[1], c[15], c[15].zwzw;
END
# 14 instructions, 2 R-regs
"
}

SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "DIRLIGHTMAP_ON" "SHADOWS_SCREEN" }
Bind "vertex" Vertex
Bind "texcoord" TexCoord0
Bind "texcoord1" TexCoord1
Matrix 0 [glstate_matrix_mvp]
Vector 12 [_ProjectionParams]
Vector 13 [_ScreenParams]
Vector 14 [unity_LightmapST]
Vector 15 [_Control_ST]
Vector 16 [_Splat0_ST]
Vector 17 [_Splat1_ST]
Vector 18 [_Splat2_ST]
"vs_2_0
; 14 ALU
def c19, 0.50000000, 0, 0, 0
dcl_position0 v0
dcl_texcoord0 v3
dcl_texcoord1 v4
dp4 r0.w, v0, c3
dp4 r0.z, v0, c2
dp4 r0.x, v0, c0
dp4 r0.y, v0, c1
mul r1.xyz, r0.xyww, c19.x
mul r1.y, r1, c12.x
mad oT3.xy, r1.z, c13.zwzw, r1
mov oPos, r0
mov oT3.zw, r0
mad oT0.zw, v3.xyxy, c16.xyxy, c16
mad oT0.xy, v3, c15, c15.zwzw
mad oT1.zw, v3.xyxy, c18.xyxy, c18
mad oT1.xy, v3, c17, c17.zwzw
mad oT2.xy, v4, c14, c14.zwzw
"
}

SubProgram "gles " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "DIRLIGHTMAP_ON" "SHADOWS_SCREEN" }
"!!GLES
#define SHADER_API_GLES 1
#define tex2D texture2D


#ifdef VERTEX
#define gl_ModelViewProjectionMatrix glstate_matrix_mvp
uniform mat4 glstate_matrix_mvp;

varying highp vec4 xlv_TEXCOORD3;
varying highp vec2 xlv_TEXCOORD2;
varying highp vec4 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;
uniform highp vec4 unity_LightmapST;

uniform highp vec4 _Splat2_ST;
uniform highp vec4 _Splat1_ST;
uniform highp vec4 _Splat0_ST;
uniform highp vec4 _ProjectionParams;
uniform highp vec4 _Control_ST;
attribute vec4 _glesMultiTexCoord1;
attribute vec4 _glesMultiTexCoord0;
attribute vec4 _glesVertex;
void main ()
{
  highp vec4 tmpvar_1;
  highp vec4 tmpvar_2;
  highp vec4 tmpvar_3;
  tmpvar_3 = (gl_ModelViewProjectionMatrix * _glesVertex);
  tmpvar_1.xy = ((_glesMultiTexCoord0.xy * _Control_ST.xy) + _Control_ST.zw);
  tmpvar_1.zw = ((_glesMultiTexCoord0.xy * _Splat0_ST.xy) + _Splat0_ST.zw);
  tmpvar_2.xy = ((_glesMultiTexCoord0.xy * _Splat1_ST.xy) + _Splat1_ST.zw);
  tmpvar_2.zw = ((_glesMultiTexCoord0.xy * _Splat2_ST.xy) + _Splat2_ST.zw);
  highp vec4 o_i0;
  highp vec4 tmpvar_4;
  tmpvar_4 = (tmpvar_3 * 0.5);
  o_i0 = tmpvar_4;
  highp vec2 tmpvar_5;
  tmpvar_5.x = tmpvar_4.x;
  tmpvar_5.y = (tmpvar_4.y * _ProjectionParams.x);
  o_i0.xy = (tmpvar_5 + tmpvar_4.w);
  o_i0.zw = tmpvar_3.zw;
  gl_Position = tmpvar_3;
  xlv_TEXCOORD0 = tmpvar_1;
  xlv_TEXCOORD1 = tmpvar_2;
  xlv_TEXCOORD2 = ((_glesMultiTexCoord1.xy * unity_LightmapST.xy) + unity_LightmapST.zw);
  xlv_TEXCOORD3 = o_i0;
}



#endif
#ifdef FRAGMENT

varying highp vec4 xlv_TEXCOORD3;
varying highp vec2 xlv_TEXCOORD2;
varying highp vec4 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;
uniform sampler2D unity_LightmapInd;
uniform sampler2D unity_Lightmap;
uniform sampler2D _Splat2;
uniform sampler2D _Splat1;
uniform sampler2D _Splat0;
uniform sampler2D _ShadowMapTexture;
uniform sampler2D _Control;
uniform sampler2D _BumpSplat2;
uniform sampler2D _BumpSplat1;
uniform sampler2D _BumpSplat0;
void main ()
{
  lowp vec4 c;
  highp vec2 tmpvar_1;
  tmpvar_1 = xlv_TEXCOORD0.xy;
  highp vec2 tmpvar_2;
  tmpvar_2 = xlv_TEXCOORD0.zw;
  highp vec2 tmpvar_3;
  tmpvar_3 = xlv_TEXCOORD1.xy;
  highp vec2 tmpvar_4;
  tmpvar_4 = xlv_TEXCOORD1.zw;
  lowp vec3 tmpvar_5;
  lowp vec4 tmpvar_6;
  tmpvar_6 = texture2D (_Control, tmpvar_1);
  tmpvar_5 = (((texture2D (_Splat0, tmpvar_2).xyz * tmpvar_6.x) + (texture2D (_Splat1, tmpvar_3).xyz * tmpvar_6.y)) + (texture2D (_Splat2, tmpvar_4).xyz * tmpvar_6.z));
  lowp vec3 tmpvar_7;
  tmpvar_7 = (((((texture2D (_BumpSplat0, tmpvar_2).xyz * 2.0) - 1.0) * tmpvar_6.x) + (((texture2D (_BumpSplat1, tmpvar_3).xyz * 2.0) - 1.0) * tmpvar_6.y)) + (((texture2D (_BumpSplat2, tmpvar_4).xyz * 2.0) - 1.0) * tmpvar_6.z));
  c = vec4(0.0, 0.0, 0.0, 0.0);
  mediump vec3 normal;
  normal = tmpvar_7;
  mediump vec3 scalePerBasisVector_i0;
  mediump vec3 lm_i0;
  lowp vec3 tmpvar_8;
  tmpvar_8 = (2.0 * texture2D (unity_Lightmap, xlv_TEXCOORD2).xyz);
  lm_i0 = tmpvar_8;
  lowp vec3 tmpvar_9;
  tmpvar_9 = (2.0 * texture2D (unity_LightmapInd, xlv_TEXCOORD2).xyz);
  scalePerBasisVector_i0 = tmpvar_9;
  lm_i0 = (lm_i0 * dot (clamp ((mat3(0.816497, -0.408248, -0.408248, 0.0, 0.707107, -0.707107, 0.57735, 0.57735, 0.57735) * normal), 0.0, 1.0), scalePerBasisVector_i0));
  mediump vec4 tmpvar_10;
  tmpvar_10.w = 0.0;
  tmpvar_10.xyz = lm_i0;
  lowp vec3 tmpvar_11;
  tmpvar_11 = vec3((texture2DProj (_ShadowMapTexture, xlv_TEXCOORD3).x * 2.0));
  mediump vec3 tmpvar_12;
  tmpvar_12 = (tmpvar_5 * min (tmpvar_10.xyz, tmpvar_11));
  c.xyz = tmpvar_12;
  c.w = 0.0;
  gl_FragData[0] = c;
}



#endif"
}

SubProgram "glesdesktop " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "DIRLIGHTMAP_ON" "SHADOWS_SCREEN" }
"!!GLES
#define SHADER_API_GLES 1
#define tex2D texture2D


#ifdef VERTEX
#define gl_ModelViewProjectionMatrix glstate_matrix_mvp
uniform mat4 glstate_matrix_mvp;

varying highp vec4 xlv_TEXCOORD3;
varying highp vec2 xlv_TEXCOORD2;
varying highp vec4 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;
uniform highp vec4 unity_LightmapST;

uniform highp vec4 _Splat2_ST;
uniform highp vec4 _Splat1_ST;
uniform highp vec4 _Splat0_ST;
uniform highp vec4 _ProjectionParams;
uniform highp vec4 _Control_ST;
attribute vec4 _glesMultiTexCoord1;
attribute vec4 _glesMultiTexCoord0;
attribute vec4 _glesVertex;
void main ()
{
  highp vec4 tmpvar_1;
  highp vec4 tmpvar_2;
  highp vec4 tmpvar_3;
  tmpvar_3 = (gl_ModelViewProjectionMatrix * _glesVertex);
  tmpvar_1.xy = ((_glesMultiTexCoord0.xy * _Control_ST.xy) + _Control_ST.zw);
  tmpvar_1.zw = ((_glesMultiTexCoord0.xy * _Splat0_ST.xy) + _Splat0_ST.zw);
  tmpvar_2.xy = ((_glesMultiTexCoord0.xy * _Splat1_ST.xy) + _Splat1_ST.zw);
  tmpvar_2.zw = ((_glesMultiTexCoord0.xy * _Splat2_ST.xy) + _Splat2_ST.zw);
  highp vec4 o_i0;
  highp vec4 tmpvar_4;
  tmpvar_4 = (tmpvar_3 * 0.5);
  o_i0 = tmpvar_4;
  highp vec2 tmpvar_5;
  tmpvar_5.x = tmpvar_4.x;
  tmpvar_5.y = (tmpvar_4.y * _ProjectionParams.x);
  o_i0.xy = (tmpvar_5 + tmpvar_4.w);
  o_i0.zw = tmpvar_3.zw;
  gl_Position = tmpvar_3;
  xlv_TEXCOORD0 = tmpvar_1;
  xlv_TEXCOORD1 = tmpvar_2;
  xlv_TEXCOORD2 = ((_glesMultiTexCoord1.xy * unity_LightmapST.xy) + unity_LightmapST.zw);
  xlv_TEXCOORD3 = o_i0;
}



#endif
#ifdef FRAGMENT

varying highp vec4 xlv_TEXCOORD3;
varying highp vec2 xlv_TEXCOORD2;
varying highp vec4 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;
uniform sampler2D unity_LightmapInd;
uniform sampler2D unity_Lightmap;
uniform sampler2D _Splat2;
uniform sampler2D _Splat1;
uniform sampler2D _Splat0;
uniform sampler2D _ShadowMapTexture;
uniform sampler2D _Control;
uniform sampler2D _BumpSplat2;
uniform sampler2D _BumpSplat1;
uniform sampler2D _BumpSplat0;
void main ()
{
  lowp vec4 c;
  highp vec2 tmpvar_1;
  tmpvar_1 = xlv_TEXCOORD0.xy;
  highp vec2 tmpvar_2;
  tmpvar_2 = xlv_TEXCOORD0.zw;
  highp vec2 tmpvar_3;
  tmpvar_3 = xlv_TEXCOORD1.xy;
  highp vec2 tmpvar_4;
  tmpvar_4 = xlv_TEXCOORD1.zw;
  lowp vec3 tmpvar_5;
  lowp vec4 tmpvar_6;
  tmpvar_6 = texture2D (_Control, tmpvar_1);
  tmpvar_5 = (((texture2D (_Splat0, tmpvar_2).xyz * tmpvar_6.x) + (texture2D (_Splat1, tmpvar_3).xyz * tmpvar_6.y)) + (texture2D (_Splat2, tmpvar_4).xyz * tmpvar_6.z));
  lowp vec3 normal;
  normal.xy = ((texture2D (_BumpSplat0, tmpvar_2).wy * 2.0) - 1.0);
  normal.z = sqrt (((1.0 - (normal.x * normal.x)) - (normal.y * normal.y)));
  lowp vec3 normal_i0;
  normal_i0.xy = ((texture2D (_BumpSplat1, tmpvar_3).wy * 2.0) - 1.0);
  normal_i0.z = sqrt (((1.0 - (normal_i0.x * normal_i0.x)) - (normal_i0.y * normal_i0.y)));
  lowp vec3 normal_i0_i1;
  normal_i0_i1.xy = ((texture2D (_BumpSplat2, tmpvar_4).wy * 2.0) - 1.0);
  normal_i0_i1.z = sqrt (((1.0 - (normal_i0_i1.x * normal_i0_i1.x)) - (normal_i0_i1.y * normal_i0_i1.y)));
  lowp vec3 tmpvar_7;
  tmpvar_7 = (((normal * tmpvar_6.x) + (normal_i0 * tmpvar_6.y)) + (normal_i0_i1 * tmpvar_6.z));
  lowp vec4 tmpvar_8;
  tmpvar_8 = texture2DProj (_ShadowMapTexture, xlv_TEXCOORD3);
  c = vec4(0.0, 0.0, 0.0, 0.0);
  lowp vec4 tmpvar_9;
  tmpvar_9 = texture2D (unity_Lightmap, xlv_TEXCOORD2);
  lowp vec4 tmpvar_10;
  tmpvar_10 = texture2D (unity_LightmapInd, xlv_TEXCOORD2);
  mediump vec3 normal_i0_i1_i2;
  normal_i0_i1_i2 = tmpvar_7;
  mediump vec3 scalePerBasisVector_i0;
  mediump vec3 lm_i0;
  lowp vec3 tmpvar_11;
  tmpvar_11 = ((8.0 * tmpvar_9.w) * tmpvar_9.xyz);
  lm_i0 = tmpvar_11;
  lowp vec3 tmpvar_12;
  tmpvar_12 = ((8.0 * tmpvar_10.w) * tmpvar_10.xyz);
  scalePerBasisVector_i0 = tmpvar_12;
  lm_i0 = (lm_i0 * dot (clamp ((mat3(0.816497, -0.408248, -0.408248, 0.0, 0.707107, -0.707107, 0.57735, 0.57735, 0.57735) * normal_i0_i1_i2), 0.0, 1.0), scalePerBasisVector_i0));
  mediump vec4 tmpvar_13;
  tmpvar_13.w = 0.0;
  tmpvar_13.xyz = lm_i0;
  mediump vec3 tmpvar_14;
  tmpvar_14 = (tmpvar_5 * max (min (tmpvar_13.xyz, ((tmpvar_8.x * 2.0) * tmpvar_9.xyz)), (lm_i0 * tmpvar_8.x)));
  c.xyz = tmpvar_14;
  c.w = 0.0;
  gl_FragData[0] = c;
}



#endif"
}

SubProgram "opengl " {
Keywords { "DIRECTIONAL" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "SHADOWS_OFF" "VERTEXLIGHT_ON" }
Bind "vertex" Vertex
Bind "tangent" ATTR14
Bind "normal" Normal
Bind "texcoord" TexCoord0
Vector 13 [unity_Scale]
Vector 14 [_WorldSpaceLightPos0]
Matrix 5 [_Object2World]
Matrix 9 [_World2Object]
Vector 15 [unity_4LightPosX0]
Vector 16 [unity_4LightPosY0]
Vector 17 [unity_4LightPosZ0]
Vector 18 [unity_4LightAtten0]
Vector 19 [unity_LightColor0]
Vector 20 [unity_LightColor1]
Vector 21 [unity_LightColor2]
Vector 22 [unity_LightColor3]
Vector 23 [unity_SHAr]
Vector 24 [unity_SHAg]
Vector 25 [unity_SHAb]
Vector 26 [unity_SHBr]
Vector 27 [unity_SHBg]
Vector 28 [unity_SHBb]
Vector 29 [unity_SHC]
Vector 30 [_Control_ST]
Vector 31 [_Splat0_ST]
Vector 32 [_Splat1_ST]
Vector 33 [_Splat2_ST]
"!!ARBvp1.0
# 68 ALU
PARAM c[34] = { { 1, 0 },
		state.matrix.mvp,
		program.local[5..33] };
TEMP R0;
TEMP R1;
TEMP R2;
TEMP R3;
TEMP R4;
MUL R3.xyz, vertex.normal, c[13].w;
DP4 R0.x, vertex.position, c[6];
ADD R1, -R0.x, c[16];
DP3 R3.w, R3, c[6];
DP3 R4.x, R3, c[5];
DP3 R3.x, R3, c[7];
MUL R2, R3.w, R1;
DP4 R0.x, vertex.position, c[5];
ADD R0, -R0.x, c[15];
MUL R1, R1, R1;
MOV R4.z, R3.x;
MAD R2, R4.x, R0, R2;
MOV R4.w, c[0].x;
DP4 R4.y, vertex.position, c[7];
MAD R1, R0, R0, R1;
ADD R0, -R4.y, c[17];
MAD R1, R0, R0, R1;
MAD R0, R3.x, R0, R2;
MUL R2, R1, c[18];
MOV R4.y, R3.w;
RSQ R1.x, R1.x;
RSQ R1.y, R1.y;
RSQ R1.w, R1.w;
RSQ R1.z, R1.z;
MUL R0, R0, R1;
ADD R1, R2, c[0].x;
RCP R1.x, R1.x;
RCP R1.y, R1.y;
RCP R1.w, R1.w;
RCP R1.z, R1.z;
MAX R0, R0, c[0].y;
MUL R0, R0, R1;
MUL R1.xyz, R0.y, c[20];
MAD R1.xyz, R0.x, c[19], R1;
MAD R0.xyz, R0.z, c[21], R1;
MAD R1.xyz, R0.w, c[22], R0;
MUL R0, R4.xyzz, R4.yzzx;
DP4 R3.z, R0, c[28];
DP4 R3.y, R0, c[27];
DP4 R3.x, R0, c[26];
MUL R1.w, R3, R3;
MAD R0.x, R4, R4, -R1.w;
DP4 R2.z, R4, c[25];
DP4 R2.y, R4, c[24];
DP4 R2.x, R4, c[23];
ADD R2.xyz, R2, R3;
MUL R3.xyz, R0.x, c[29];
ADD R3.xyz, R2, R3;
MOV R0.xyz, vertex.attrib[14];
MUL R2.xyz, vertex.normal.zxyw, R0.yzxw;
ADD result.texcoord[3].xyz, R3, R1;
MAD R1.xyz, vertex.normal.yzxw, R0.zxyw, -R2;
MOV R0, c[14];
DP4 R2.z, R0, c[11];
DP4 R2.y, R0, c[10];
DP4 R2.x, R0, c[9];
MUL R1.xyz, R1, vertex.attrib[14].w;
DP3 result.texcoord[2].y, R2, R1;
DP3 result.texcoord[2].z, vertex.normal, R2;
DP3 result.texcoord[2].x, R2, vertex.attrib[14];
MAD result.texcoord[0].zw, vertex.texcoord[0].xyxy, c[31].xyxy, c[31];
MAD result.texcoord[0].xy, vertex.texcoord[0], c[30], c[30].zwzw;
MAD result.texcoord[1].zw, vertex.texcoord[0].xyxy, c[33].xyxy, c[33];
MAD result.texcoord[1].xy, vertex.texcoord[0], c[32], c[32].zwzw;
DP4 result.position.w, vertex.position, c[4];
DP4 result.position.z, vertex.position, c[3];
DP4 result.position.y, vertex.position, c[2];
DP4 result.position.x, vertex.position, c[1];
END
# 68 instructions, 5 R-regs
"
}

SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "SHADOWS_OFF" "VERTEXLIGHT_ON" }
Bind "vertex" Vertex
Bind "tangent" TexCoord2
Bind "normal" Normal
Bind "texcoord" TexCoord0
Matrix 0 [glstate_matrix_mvp]
Vector 12 [unity_Scale]
Vector 13 [_WorldSpaceLightPos0]
Matrix 4 [_Object2World]
Matrix 8 [_World2Object]
Vector 14 [unity_4LightPosX0]
Vector 15 [unity_4LightPosY0]
Vector 16 [unity_4LightPosZ0]
Vector 17 [unity_4LightAtten0]
Vector 18 [unity_LightColor0]
Vector 19 [unity_LightColor1]
Vector 20 [unity_LightColor2]
Vector 21 [unity_LightColor3]
Vector 22 [unity_SHAr]
Vector 23 [unity_SHAg]
Vector 24 [unity_SHAb]
Vector 25 [unity_SHBr]
Vector 26 [unity_SHBg]
Vector 27 [unity_SHBb]
Vector 28 [unity_SHC]
Vector 29 [_Control_ST]
Vector 30 [_Splat0_ST]
Vector 31 [_Splat1_ST]
Vector 32 [_Splat2_ST]
"vs_2_0
; 71 ALU
def c33, 1.00000000, 0.00000000, 0, 0
dcl_position0 v0
dcl_tangent0 v1
dcl_normal0 v2
dcl_texcoord0 v3
mul r3.xyz, v2, c12.w
dp4 r0.x, v0, c5
add r1, -r0.x, c15
dp3 r3.w, r3, c5
dp3 r4.x, r3, c4
dp3 r3.x, r3, c6
mul r2, r3.w, r1
dp4 r0.x, v0, c4
add r0, -r0.x, c14
mul r1, r1, r1
mov r4.z, r3.x
mad r2, r4.x, r0, r2
mov r4.w, c33.x
dp4 r4.y, v0, c6
mad r1, r0, r0, r1
add r0, -r4.y, c16
mad r1, r0, r0, r1
mad r0, r3.x, r0, r2
mul r2, r1, c17
mov r4.y, r3.w
rsq r1.x, r1.x
rsq r1.y, r1.y
rsq r1.w, r1.w
rsq r1.z, r1.z
mul r0, r0, r1
add r1, r2, c33.x
rcp r1.x, r1.x
rcp r1.y, r1.y
rcp r1.w, r1.w
rcp r1.z, r1.z
max r0, r0, c33.y
mul r0, r0, r1
mul r1.xyz, r0.y, c19
mad r1.xyz, r0.x, c18, r1
mad r0.xyz, r0.z, c20, r1
mad r1.xyz, r0.w, c21, r0
mul r0, r4.xyzz, r4.yzzx
mul r1.w, r3, r3
dp4 r3.z, r0, c27
dp4 r3.y, r0, c26
dp4 r3.x, r0, c25
mad r1.w, r4.x, r4.x, -r1
mul r0.xyz, r1.w, c28
dp4 r2.z, r4, c24
dp4 r2.y, r4, c23
dp4 r2.x, r4, c22
add r2.xyz, r2, r3
add r2.xyz, r2, r0
mov r0.xyz, v1
add oT3.xyz, r2, r1
mul r1.xyz, v2.zxyw, r0.yzxw
mov r0.xyz, v1
mad r1.xyz, v2.yzxw, r0.zxyw, -r1
mov r0, c10
dp4 r3.z, c13, r0
mul r2.xyz, r1, v1.w
mov r1, c9
mov r0, c8
dp4 r3.y, c13, r1
dp4 r3.x, c13, r0
dp3 oT2.y, r3, r2
dp3 oT2.z, v2, r3
dp3 oT2.x, r3, v1
mad oT0.zw, v3.xyxy, c30.xyxy, c30
mad oT0.xy, v3, c29, c29.zwzw
mad oT1.zw, v3.xyxy, c32.xyxy, c32
mad oT1.xy, v3, c31, c31.zwzw
dp4 oPos.w, v0, c3
dp4 oPos.z, v0, c2
dp4 oPos.y, v0, c1
dp4 oPos.x, v0, c0
"
}

SubProgram "gles " {
Keywords { "DIRECTIONAL" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "SHADOWS_OFF" "VERTEXLIGHT_ON" }
"!!GLES
#define SHADER_API_GLES 1
#define tex2D texture2D


#ifdef VERTEX
#define gl_ModelViewProjectionMatrix glstate_matrix_mvp
uniform mat4 glstate_matrix_mvp;

varying lowp vec3 xlv_TEXCOORD3;
varying lowp vec3 xlv_TEXCOORD2;
varying highp vec4 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;
uniform highp vec4 unity_Scale;
uniform highp vec4 unity_SHC;
uniform highp vec4 unity_SHBr;
uniform highp vec4 unity_SHBg;
uniform highp vec4 unity_SHBb;
uniform highp vec4 unity_SHAr;
uniform highp vec4 unity_SHAg;
uniform highp vec4 unity_SHAb;
uniform highp vec4 unity_LightColor[4];
uniform highp vec4 unity_4LightPosZ0;
uniform highp vec4 unity_4LightPosY0;
uniform highp vec4 unity_4LightPosX0;
uniform highp vec4 unity_4LightAtten0;

uniform lowp vec4 _WorldSpaceLightPos0;
uniform highp mat4 _World2Object;
uniform highp vec4 _Splat2_ST;
uniform highp vec4 _Splat1_ST;
uniform highp vec4 _Splat0_ST;
uniform highp mat4 _Object2World;
uniform highp vec4 _Control_ST;
attribute vec4 _glesTANGENT;
attribute vec4 _glesMultiTexCoord0;
attribute vec3 _glesNormal;
attribute vec4 _glesVertex;
void main ()
{
  vec4 tmpvar_1;
  tmpvar_1.xyz = normalize (_glesTANGENT.xyz);
  tmpvar_1.w = _glesTANGENT.w;
  vec3 tmpvar_2;
  tmpvar_2 = normalize (_glesNormal);
  highp vec3 shlight;
  highp vec4 tmpvar_3;
  highp vec4 tmpvar_4;
  lowp vec3 tmpvar_5;
  lowp vec3 tmpvar_6;
  tmpvar_3.xy = ((_glesMultiTexCoord0.xy * _Control_ST.xy) + _Control_ST.zw);
  tmpvar_3.zw = ((_glesMultiTexCoord0.xy * _Splat0_ST.xy) + _Splat0_ST.zw);
  tmpvar_4.xy = ((_glesMultiTexCoord0.xy * _Splat1_ST.xy) + _Splat1_ST.zw);
  tmpvar_4.zw = ((_glesMultiTexCoord0.xy * _Splat2_ST.xy) + _Splat2_ST.zw);
  mat3 tmpvar_7;
  tmpvar_7[0] = _Object2World[0].xyz;
  tmpvar_7[1] = _Object2World[1].xyz;
  tmpvar_7[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_8;
  tmpvar_8 = (tmpvar_7 * (tmpvar_2 * unity_Scale.w));
  highp mat3 tmpvar_9;
  tmpvar_9[0] = tmpvar_1.xyz;
  tmpvar_9[1] = (cross (tmpvar_2, tmpvar_1.xyz) * _glesTANGENT.w);
  tmpvar_9[2] = tmpvar_2;
  mat3 tmpvar_10;
  tmpvar_10[0].x = tmpvar_9[0].x;
  tmpvar_10[0].y = tmpvar_9[1].x;
  tmpvar_10[0].z = tmpvar_9[2].x;
  tmpvar_10[1].x = tmpvar_9[0].y;
  tmpvar_10[1].y = tmpvar_9[1].y;
  tmpvar_10[1].z = tmpvar_9[2].y;
  tmpvar_10[2].x = tmpvar_9[0].z;
  tmpvar_10[2].y = tmpvar_9[1].z;
  tmpvar_10[2].z = tmpvar_9[2].z;
  highp vec3 tmpvar_11;
  tmpvar_11 = (tmpvar_10 * (_World2Object * _WorldSpaceLightPos0).xyz);
  tmpvar_5 = tmpvar_11;
  highp vec4 tmpvar_12;
  tmpvar_12.w = 1.0;
  tmpvar_12.xyz = tmpvar_8;
  mediump vec3 tmpvar_13;
  mediump vec4 normal;
  normal = tmpvar_12;
  mediump vec3 x3;
  highp float vC;
  mediump vec3 x2;
  mediump vec3 x1;
  highp float tmpvar_14;
  tmpvar_14 = dot (unity_SHAr, normal);
  x1.x = tmpvar_14;
  highp float tmpvar_15;
  tmpvar_15 = dot (unity_SHAg, normal);
  x1.y = tmpvar_15;
  highp float tmpvar_16;
  tmpvar_16 = dot (unity_SHAb, normal);
  x1.z = tmpvar_16;
  mediump vec4 tmpvar_17;
  tmpvar_17 = (normal.xyzz * normal.yzzx);
  highp float tmpvar_18;
  tmpvar_18 = dot (unity_SHBr, tmpvar_17);
  x2.x = tmpvar_18;
  highp float tmpvar_19;
  tmpvar_19 = dot (unity_SHBg, tmpvar_17);
  x2.y = tmpvar_19;
  highp float tmpvar_20;
  tmpvar_20 = dot (unity_SHBb, tmpvar_17);
  x2.z = tmpvar_20;
  mediump float tmpvar_21;
  tmpvar_21 = ((normal.x * normal.x) - (normal.y * normal.y));
  vC = tmpvar_21;
  highp vec3 tmpvar_22;
  tmpvar_22 = (unity_SHC.xyz * vC);
  x3 = tmpvar_22;
  tmpvar_13 = ((x1 + x2) + x3);
  shlight = tmpvar_13;
  tmpvar_6 = shlight;
  highp vec3 tmpvar_23;
  tmpvar_23 = (_Object2World * _glesVertex).xyz;
  highp vec4 tmpvar_24;
  tmpvar_24 = (unity_4LightPosX0 - tmpvar_23.x);
  highp vec4 tmpvar_25;
  tmpvar_25 = (unity_4LightPosY0 - tmpvar_23.y);
  highp vec4 tmpvar_26;
  tmpvar_26 = (unity_4LightPosZ0 - tmpvar_23.z);
  highp vec4 tmpvar_27;
  tmpvar_27 = (((tmpvar_24 * tmpvar_24) + (tmpvar_25 * tmpvar_25)) + (tmpvar_26 * tmpvar_26));
  highp vec4 tmpvar_28;
  tmpvar_28 = (max (vec4(0.0, 0.0, 0.0, 0.0), ((((tmpvar_24 * tmpvar_8.x) + (tmpvar_25 * tmpvar_8.y)) + (tmpvar_26 * tmpvar_8.z)) * inversesqrt (tmpvar_27))) * (1.0/((1.0 + (tmpvar_27 * unity_4LightAtten0)))));
  highp vec3 tmpvar_29;
  tmpvar_29 = (tmpvar_6 + ((((unity_LightColor[0].xyz * tmpvar_28.x) + (unity_LightColor[1].xyz * tmpvar_28.y)) + (unity_LightColor[2].xyz * tmpvar_28.z)) + (unity_LightColor[3].xyz * tmpvar_28.w)));
  tmpvar_6 = tmpvar_29;
  gl_Position = (gl_ModelViewProjectionMatrix * _glesVertex);
  xlv_TEXCOORD0 = tmpvar_3;
  xlv_TEXCOORD1 = tmpvar_4;
  xlv_TEXCOORD2 = tmpvar_5;
  xlv_TEXCOORD3 = tmpvar_6;
}



#endif
#ifdef FRAGMENT

varying lowp vec3 xlv_TEXCOORD3;
varying lowp vec3 xlv_TEXCOORD2;
varying highp vec4 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;
uniform sampler2D _Splat2;
uniform sampler2D _Splat1;
uniform sampler2D _Splat0;
uniform lowp vec4 _LightColor0;
uniform sampler2D _Control;
uniform sampler2D _BumpSplat2;
uniform sampler2D _BumpSplat1;
uniform sampler2D _BumpSplat0;
void main ()
{
  lowp vec4 c;
  highp vec2 tmpvar_1;
  tmpvar_1 = xlv_TEXCOORD0.xy;
  highp vec2 tmpvar_2;
  tmpvar_2 = xlv_TEXCOORD0.zw;
  highp vec2 tmpvar_3;
  tmpvar_3 = xlv_TEXCOORD1.xy;
  highp vec2 tmpvar_4;
  tmpvar_4 = xlv_TEXCOORD1.zw;
  lowp vec3 tmpvar_5;
  lowp vec4 tmpvar_6;
  tmpvar_6 = texture2D (_Control, tmpvar_1);
  tmpvar_5 = (((texture2D (_Splat0, tmpvar_2).xyz * tmpvar_6.x) + (texture2D (_Splat1, tmpvar_3).xyz * tmpvar_6.y)) + (texture2D (_Splat2, tmpvar_4).xyz * tmpvar_6.z));
  lowp vec4 c_i0;
  c_i0.xyz = ((tmpvar_5 * _LightColor0.xyz) * (max (0.0, dot ((((((texture2D (_BumpSplat0, tmpvar_2).xyz * 2.0) - 1.0) * tmpvar_6.x) + (((texture2D (_BumpSplat1, tmpvar_3).xyz * 2.0) - 1.0) * tmpvar_6.y)) + (((texture2D (_BumpSplat2, tmpvar_4).xyz * 2.0) - 1.0) * tmpvar_6.z)), xlv_TEXCOORD2)) * 2.0));
  c_i0.w = 0.0;
  c = c_i0;
  c.xyz = (c_i0.xyz + (tmpvar_5 * xlv_TEXCOORD3));
  gl_FragData[0] = c;
}



#endif"
}

SubProgram "glesdesktop " {
Keywords { "DIRECTIONAL" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "SHADOWS_OFF" "VERTEXLIGHT_ON" }
"!!GLES
#define SHADER_API_GLES 1
#define tex2D texture2D


#ifdef VERTEX
#define gl_ModelViewProjectionMatrix glstate_matrix_mvp
uniform mat4 glstate_matrix_mvp;

varying lowp vec3 xlv_TEXCOORD3;
varying lowp vec3 xlv_TEXCOORD2;
varying highp vec4 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;
uniform highp vec4 unity_Scale;
uniform highp vec4 unity_SHC;
uniform highp vec4 unity_SHBr;
uniform highp vec4 unity_SHBg;
uniform highp vec4 unity_SHBb;
uniform highp vec4 unity_SHAr;
uniform highp vec4 unity_SHAg;
uniform highp vec4 unity_SHAb;
uniform highp vec4 unity_LightColor[4];
uniform highp vec4 unity_4LightPosZ0;
uniform highp vec4 unity_4LightPosY0;
uniform highp vec4 unity_4LightPosX0;
uniform highp vec4 unity_4LightAtten0;

uniform lowp vec4 _WorldSpaceLightPos0;
uniform highp mat4 _World2Object;
uniform highp vec4 _Splat2_ST;
uniform highp vec4 _Splat1_ST;
uniform highp vec4 _Splat0_ST;
uniform highp mat4 _Object2World;
uniform highp vec4 _Control_ST;
attribute vec4 _glesTANGENT;
attribute vec4 _glesMultiTexCoord0;
attribute vec3 _glesNormal;
attribute vec4 _glesVertex;
void main ()
{
  vec4 tmpvar_1;
  tmpvar_1.xyz = normalize (_glesTANGENT.xyz);
  tmpvar_1.w = _glesTANGENT.w;
  vec3 tmpvar_2;
  tmpvar_2 = normalize (_glesNormal);
  highp vec3 shlight;
  highp vec4 tmpvar_3;
  highp vec4 tmpvar_4;
  lowp vec3 tmpvar_5;
  lowp vec3 tmpvar_6;
  tmpvar_3.xy = ((_glesMultiTexCoord0.xy * _Control_ST.xy) + _Control_ST.zw);
  tmpvar_3.zw = ((_glesMultiTexCoord0.xy * _Splat0_ST.xy) + _Splat0_ST.zw);
  tmpvar_4.xy = ((_glesMultiTexCoord0.xy * _Splat1_ST.xy) + _Splat1_ST.zw);
  tmpvar_4.zw = ((_glesMultiTexCoord0.xy * _Splat2_ST.xy) + _Splat2_ST.zw);
  mat3 tmpvar_7;
  tmpvar_7[0] = _Object2World[0].xyz;
  tmpvar_7[1] = _Object2World[1].xyz;
  tmpvar_7[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_8;
  tmpvar_8 = (tmpvar_7 * (tmpvar_2 * unity_Scale.w));
  highp mat3 tmpvar_9;
  tmpvar_9[0] = tmpvar_1.xyz;
  tmpvar_9[1] = (cross (tmpvar_2, tmpvar_1.xyz) * _glesTANGENT.w);
  tmpvar_9[2] = tmpvar_2;
  mat3 tmpvar_10;
  tmpvar_10[0].x = tmpvar_9[0].x;
  tmpvar_10[0].y = tmpvar_9[1].x;
  tmpvar_10[0].z = tmpvar_9[2].x;
  tmpvar_10[1].x = tmpvar_9[0].y;
  tmpvar_10[1].y = tmpvar_9[1].y;
  tmpvar_10[1].z = tmpvar_9[2].y;
  tmpvar_10[2].x = tmpvar_9[0].z;
  tmpvar_10[2].y = tmpvar_9[1].z;
  tmpvar_10[2].z = tmpvar_9[2].z;
  highp vec3 tmpvar_11;
  tmpvar_11 = (tmpvar_10 * (_World2Object * _WorldSpaceLightPos0).xyz);
  tmpvar_5 = tmpvar_11;
  highp vec4 tmpvar_12;
  tmpvar_12.w = 1.0;
  tmpvar_12.xyz = tmpvar_8;
  mediump vec3 tmpvar_13;
  mediump vec4 normal;
  normal = tmpvar_12;
  mediump vec3 x3;
  highp float vC;
  mediump vec3 x2;
  mediump vec3 x1;
  highp float tmpvar_14;
  tmpvar_14 = dot (unity_SHAr, normal);
  x1.x = tmpvar_14;
  highp float tmpvar_15;
  tmpvar_15 = dot (unity_SHAg, normal);
  x1.y = tmpvar_15;
  highp float tmpvar_16;
  tmpvar_16 = dot (unity_SHAb, normal);
  x1.z = tmpvar_16;
  mediump vec4 tmpvar_17;
  tmpvar_17 = (normal.xyzz * normal.yzzx);
  highp float tmpvar_18;
  tmpvar_18 = dot (unity_SHBr, tmpvar_17);
  x2.x = tmpvar_18;
  highp float tmpvar_19;
  tmpvar_19 = dot (unity_SHBg, tmpvar_17);
  x2.y = tmpvar_19;
  highp float tmpvar_20;
  tmpvar_20 = dot (unity_SHBb, tmpvar_17);
  x2.z = tmpvar_20;
  mediump float tmpvar_21;
  tmpvar_21 = ((normal.x * normal.x) - (normal.y * normal.y));
  vC = tmpvar_21;
  highp vec3 tmpvar_22;
  tmpvar_22 = (unity_SHC.xyz * vC);
  x3 = tmpvar_22;
  tmpvar_13 = ((x1 + x2) + x3);
  shlight = tmpvar_13;
  tmpvar_6 = shlight;
  highp vec3 tmpvar_23;
  tmpvar_23 = (_Object2World * _glesVertex).xyz;
  highp vec4 tmpvar_24;
  tmpvar_24 = (unity_4LightPosX0 - tmpvar_23.x);
  highp vec4 tmpvar_25;
  tmpvar_25 = (unity_4LightPosY0 - tmpvar_23.y);
  highp vec4 tmpvar_26;
  tmpvar_26 = (unity_4LightPosZ0 - tmpvar_23.z);
  highp vec4 tmpvar_27;
  tmpvar_27 = (((tmpvar_24 * tmpvar_24) + (tmpvar_25 * tmpvar_25)) + (tmpvar_26 * tmpvar_26));
  highp vec4 tmpvar_28;
  tmpvar_28 = (max (vec4(0.0, 0.0, 0.0, 0.0), ((((tmpvar_24 * tmpvar_8.x) + (tmpvar_25 * tmpvar_8.y)) + (tmpvar_26 * tmpvar_8.z)) * inversesqrt (tmpvar_27))) * (1.0/((1.0 + (tmpvar_27 * unity_4LightAtten0)))));
  highp vec3 tmpvar_29;
  tmpvar_29 = (tmpvar_6 + ((((unity_LightColor[0].xyz * tmpvar_28.x) + (unity_LightColor[1].xyz * tmpvar_28.y)) + (unity_LightColor[2].xyz * tmpvar_28.z)) + (unity_LightColor[3].xyz * tmpvar_28.w)));
  tmpvar_6 = tmpvar_29;
  gl_Position = (gl_ModelViewProjectionMatrix * _glesVertex);
  xlv_TEXCOORD0 = tmpvar_3;
  xlv_TEXCOORD1 = tmpvar_4;
  xlv_TEXCOORD2 = tmpvar_5;
  xlv_TEXCOORD3 = tmpvar_6;
}



#endif
#ifdef FRAGMENT

varying lowp vec3 xlv_TEXCOORD3;
varying lowp vec3 xlv_TEXCOORD2;
varying highp vec4 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;
uniform sampler2D _Splat2;
uniform sampler2D _Splat1;
uniform sampler2D _Splat0;
uniform lowp vec4 _LightColor0;
uniform sampler2D _Control;
uniform sampler2D _BumpSplat2;
uniform sampler2D _BumpSplat1;
uniform sampler2D _BumpSplat0;
void main ()
{
  lowp vec4 c;
  highp vec2 tmpvar_1;
  tmpvar_1 = xlv_TEXCOORD0.xy;
  highp vec2 tmpvar_2;
  tmpvar_2 = xlv_TEXCOORD0.zw;
  highp vec2 tmpvar_3;
  tmpvar_3 = xlv_TEXCOORD1.xy;
  highp vec2 tmpvar_4;
  tmpvar_4 = xlv_TEXCOORD1.zw;
  lowp vec3 tmpvar_5;
  lowp vec4 tmpvar_6;
  tmpvar_6 = texture2D (_Control, tmpvar_1);
  tmpvar_5 = (((texture2D (_Splat0, tmpvar_2).xyz * tmpvar_6.x) + (texture2D (_Splat1, tmpvar_3).xyz * tmpvar_6.y)) + (texture2D (_Splat2, tmpvar_4).xyz * tmpvar_6.z));
  lowp vec3 normal;
  normal.xy = ((texture2D (_BumpSplat0, tmpvar_2).wy * 2.0) - 1.0);
  normal.z = sqrt (((1.0 - (normal.x * normal.x)) - (normal.y * normal.y)));
  lowp vec3 normal_i0;
  normal_i0.xy = ((texture2D (_BumpSplat1, tmpvar_3).wy * 2.0) - 1.0);
  normal_i0.z = sqrt (((1.0 - (normal_i0.x * normal_i0.x)) - (normal_i0.y * normal_i0.y)));
  lowp vec3 normal_i0_i1;
  normal_i0_i1.xy = ((texture2D (_BumpSplat2, tmpvar_4).wy * 2.0) - 1.0);
  normal_i0_i1.z = sqrt (((1.0 - (normal_i0_i1.x * normal_i0_i1.x)) - (normal_i0_i1.y * normal_i0_i1.y)));
  lowp vec4 c_i0;
  c_i0.xyz = ((tmpvar_5 * _LightColor0.xyz) * (max (0.0, dot ((((normal * tmpvar_6.x) + (normal_i0 * tmpvar_6.y)) + (normal_i0_i1 * tmpvar_6.z)), xlv_TEXCOORD2)) * 2.0));
  c_i0.w = 0.0;
  c = c_i0;
  c.xyz = (c_i0.xyz + (tmpvar_5 * xlv_TEXCOORD3));
  gl_FragData[0] = c;
}



#endif"
}

SubProgram "opengl " {
Keywords { "DIRECTIONAL" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "SHADOWS_SCREEN" "VERTEXLIGHT_ON" }
Bind "vertex" Vertex
Bind "tangent" ATTR14
Bind "normal" Normal
Bind "texcoord" TexCoord0
Vector 13 [_ProjectionParams]
Vector 14 [unity_Scale]
Vector 15 [_WorldSpaceLightPos0]
Matrix 5 [_Object2World]
Matrix 9 [_World2Object]
Vector 16 [unity_4LightPosX0]
Vector 17 [unity_4LightPosY0]
Vector 18 [unity_4LightPosZ0]
Vector 19 [unity_4LightAtten0]
Vector 20 [unity_LightColor0]
Vector 21 [unity_LightColor1]
Vector 22 [unity_LightColor2]
Vector 23 [unity_LightColor3]
Vector 24 [unity_SHAr]
Vector 25 [unity_SHAg]
Vector 26 [unity_SHAb]
Vector 27 [unity_SHBr]
Vector 28 [unity_SHBg]
Vector 29 [unity_SHBb]
Vector 30 [unity_SHC]
Vector 31 [_Control_ST]
Vector 32 [_Splat0_ST]
Vector 33 [_Splat1_ST]
Vector 34 [_Splat2_ST]
"!!ARBvp1.0
# 73 ALU
PARAM c[35] = { { 1, 0, 0.5 },
		state.matrix.mvp,
		program.local[5..34] };
TEMP R0;
TEMP R1;
TEMP R2;
TEMP R3;
TEMP R4;
MUL R3.xyz, vertex.normal, c[14].w;
DP4 R0.x, vertex.position, c[6];
ADD R1, -R0.x, c[17];
DP3 R3.w, R3, c[6];
DP3 R4.x, R3, c[5];
DP3 R3.x, R3, c[7];
MUL R2, R3.w, R1;
DP4 R0.x, vertex.position, c[5];
ADD R0, -R0.x, c[16];
MUL R1, R1, R1;
MOV R4.z, R3.x;
MAD R2, R4.x, R0, R2;
MOV R4.w, c[0].x;
DP4 R4.y, vertex.position, c[7];
MAD R1, R0, R0, R1;
ADD R0, -R4.y, c[18];
MAD R1, R0, R0, R1;
MAD R0, R3.x, R0, R2;
MUL R2, R1, c[19];
MOV R4.y, R3.w;
RSQ R1.x, R1.x;
RSQ R1.y, R1.y;
RSQ R1.w, R1.w;
RSQ R1.z, R1.z;
MUL R0, R0, R1;
ADD R1, R2, c[0].x;
RCP R1.x, R1.x;
RCP R1.y, R1.y;
RCP R1.w, R1.w;
RCP R1.z, R1.z;
MAX R0, R0, c[0].y;
MUL R0, R0, R1;
MUL R1.xyz, R0.y, c[21];
MAD R1.xyz, R0.x, c[20], R1;
MAD R0.xyz, R0.z, c[22], R1;
MAD R1.xyz, R0.w, c[23], R0;
MUL R0, R4.xyzz, R4.yzzx;
DP4 R3.z, R0, c[29];
DP4 R3.y, R0, c[28];
DP4 R3.x, R0, c[27];
MUL R1.w, R3, R3;
MAD R0.x, R4, R4, -R1.w;
DP4 R2.z, R4, c[26];
DP4 R2.y, R4, c[25];
DP4 R2.x, R4, c[24];
ADD R2.xyz, R2, R3;
MUL R3.xyz, R0.x, c[30];
ADD R3.xyz, R2, R3;
MOV R0.xyz, vertex.attrib[14];
MUL R2.xyz, vertex.normal.zxyw, R0.yzxw;
ADD result.texcoord[3].xyz, R3, R1;
MAD R1.xyz, vertex.normal.yzxw, R0.zxyw, -R2;
MOV R0, c[15];
DP4 R2.z, R0, c[11];
DP4 R2.y, R0, c[10];
DP4 R2.x, R0, c[9];
MUL R1.xyz, R1, vertex.attrib[14].w;
DP4 R0.w, vertex.position, c[4];
DP4 R0.z, vertex.position, c[3];
DP4 R0.x, vertex.position, c[1];
DP4 R0.y, vertex.position, c[2];
DP3 result.texcoord[2].y, R2, R1;
MUL R1.xyz, R0.xyww, c[0].z;
MUL R1.y, R1, c[13].x;
DP3 result.texcoord[2].z, vertex.normal, R2;
DP3 result.texcoord[2].x, R2, vertex.attrib[14];
ADD result.texcoord[4].xy, R1, R1.z;
MOV result.position, R0;
MOV result.texcoord[4].zw, R0;
MAD result.texcoord[0].zw, vertex.texcoord[0].xyxy, c[32].xyxy, c[32];
MAD result.texcoord[0].xy, vertex.texcoord[0], c[31], c[31].zwzw;
MAD result.texcoord[1].zw, vertex.texcoord[0].xyxy, c[34].xyxy, c[34];
MAD result.texcoord[1].xy, vertex.texcoord[0], c[33], c[33].zwzw;
END
# 73 instructions, 5 R-regs
"
}

SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "SHADOWS_SCREEN" "VERTEXLIGHT_ON" }
Bind "vertex" Vertex
Bind "tangent" TexCoord2
Bind "normal" Normal
Bind "texcoord" TexCoord0
Matrix 0 [glstate_matrix_mvp]
Vector 12 [_ProjectionParams]
Vector 13 [_ScreenParams]
Vector 14 [unity_Scale]
Vector 15 [_WorldSpaceLightPos0]
Matrix 4 [_Object2World]
Matrix 8 [_World2Object]
Vector 16 [unity_4LightPosX0]
Vector 17 [unity_4LightPosY0]
Vector 18 [unity_4LightPosZ0]
Vector 19 [unity_4LightAtten0]
Vector 20 [unity_LightColor0]
Vector 21 [unity_LightColor1]
Vector 22 [unity_LightColor2]
Vector 23 [unity_LightColor3]
Vector 24 [unity_SHAr]
Vector 25 [unity_SHAg]
Vector 26 [unity_SHAb]
Vector 27 [unity_SHBr]
Vector 28 [unity_SHBg]
Vector 29 [unity_SHBb]
Vector 30 [unity_SHC]
Vector 31 [_Control_ST]
Vector 32 [_Splat0_ST]
Vector 33 [_Splat1_ST]
Vector 34 [_Splat2_ST]
"vs_2_0
; 76 ALU
def c35, 1.00000000, 0.00000000, 0.50000000, 0
dcl_position0 v0
dcl_tangent0 v1
dcl_normal0 v2
dcl_texcoord0 v3
mul r3.xyz, v2, c14.w
dp4 r0.x, v0, c5
add r1, -r0.x, c17
dp3 r3.w, r3, c5
dp3 r4.x, r3, c4
dp3 r3.x, r3, c6
mul r2, r3.w, r1
dp4 r0.x, v0, c4
add r0, -r0.x, c16
mul r1, r1, r1
mov r4.z, r3.x
mad r2, r4.x, r0, r2
mov r4.w, c35.x
dp4 r4.y, v0, c6
mad r1, r0, r0, r1
add r0, -r4.y, c18
mad r1, r0, r0, r1
mad r0, r3.x, r0, r2
mul r2, r1, c19
mov r4.y, r3.w
rsq r1.x, r1.x
rsq r1.y, r1.y
rsq r1.w, r1.w
rsq r1.z, r1.z
mul r0, r0, r1
add r1, r2, c35.x
rcp r1.x, r1.x
rcp r1.y, r1.y
rcp r1.w, r1.w
rcp r1.z, r1.z
max r0, r0, c35.y
mul r0, r0, r1
mul r1.xyz, r0.y, c21
mad r1.xyz, r0.x, c20, r1
mad r0.xyz, r0.z, c22, r1
mad r1.xyz, r0.w, c23, r0
mul r0, r4.xyzz, r4.yzzx
mul r1.w, r3, r3
dp4 r3.z, r0, c29
dp4 r3.y, r0, c28
dp4 r3.x, r0, c27
mad r1.w, r4.x, r4.x, -r1
mul r0.xyz, r1.w, c30
dp4 r2.z, r4, c26
dp4 r2.y, r4, c25
dp4 r2.x, r4, c24
add r2.xyz, r2, r3
add r2.xyz, r2, r0
mov r0.xyz, v1
add oT3.xyz, r2, r1
mul r1.xyz, v2.zxyw, r0.yzxw
mov r0.xyz, v1
mad r1.xyz, v2.yzxw, r0.zxyw, -r1
mov r0, c10
dp4 r3.z, c15, r0
mov r0, c8
dp4 r3.x, c15, r0
mul r2.xyz, r1, v1.w
mov r1, c9
dp4 r3.y, c15, r1
dp4 r0.w, v0, c3
dp4 r0.z, v0, c2
dp4 r0.x, v0, c0
dp4 r0.y, v0, c1
mul r1.xyz, r0.xyww, c35.z
mul r1.y, r1, c12.x
dp3 oT2.y, r3, r2
dp3 oT2.z, v2, r3
dp3 oT2.x, r3, v1
mad oT4.xy, r1.z, c13.zwzw, r1
mov oPos, r0
mov oT4.zw, r0
mad oT0.zw, v3.xyxy, c32.xyxy, c32
mad oT0.xy, v3, c31, c31.zwzw
mad oT1.zw, v3.xyxy, c34.xyxy, c34
mad oT1.xy, v3, c33, c33.zwzw
"
}

SubProgram "gles " {
Keywords { "DIRECTIONAL" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "SHADOWS_SCREEN" "VERTEXLIGHT_ON" }
"!!GLES
#define SHADER_API_GLES 1
#define tex2D texture2D


#ifdef VERTEX
#define gl_ModelViewProjectionMatrix glstate_matrix_mvp
uniform mat4 glstate_matrix_mvp;

varying highp vec4 xlv_TEXCOORD4;
varying lowp vec3 xlv_TEXCOORD3;
varying lowp vec3 xlv_TEXCOORD2;
varying highp vec4 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;
uniform highp vec4 unity_Scale;
uniform highp vec4 unity_SHC;
uniform highp vec4 unity_SHBr;
uniform highp vec4 unity_SHBg;
uniform highp vec4 unity_SHBb;
uniform highp vec4 unity_SHAr;
uniform highp vec4 unity_SHAg;
uniform highp vec4 unity_SHAb;
uniform highp vec4 unity_LightColor[4];
uniform highp vec4 unity_4LightPosZ0;
uniform highp vec4 unity_4LightPosY0;
uniform highp vec4 unity_4LightPosX0;
uniform highp vec4 unity_4LightAtten0;

uniform lowp vec4 _WorldSpaceLightPos0;
uniform highp mat4 _World2Object;
uniform highp vec4 _Splat2_ST;
uniform highp vec4 _Splat1_ST;
uniform highp vec4 _Splat0_ST;
uniform highp vec4 _ProjectionParams;
uniform highp mat4 _Object2World;
uniform highp vec4 _Control_ST;
attribute vec4 _glesTANGENT;
attribute vec4 _glesMultiTexCoord0;
attribute vec3 _glesNormal;
attribute vec4 _glesVertex;
void main ()
{
  vec4 tmpvar_1;
  tmpvar_1.xyz = normalize (_glesTANGENT.xyz);
  tmpvar_1.w = _glesTANGENT.w;
  vec3 tmpvar_2;
  tmpvar_2 = normalize (_glesNormal);
  highp vec3 shlight;
  highp vec4 tmpvar_3;
  highp vec4 tmpvar_4;
  lowp vec3 tmpvar_5;
  lowp vec3 tmpvar_6;
  highp vec4 tmpvar_7;
  tmpvar_7 = (gl_ModelViewProjectionMatrix * _glesVertex);
  tmpvar_3.xy = ((_glesMultiTexCoord0.xy * _Control_ST.xy) + _Control_ST.zw);
  tmpvar_3.zw = ((_glesMultiTexCoord0.xy * _Splat0_ST.xy) + _Splat0_ST.zw);
  tmpvar_4.xy = ((_glesMultiTexCoord0.xy * _Splat1_ST.xy) + _Splat1_ST.zw);
  tmpvar_4.zw = ((_glesMultiTexCoord0.xy * _Splat2_ST.xy) + _Splat2_ST.zw);
  mat3 tmpvar_8;
  tmpvar_8[0] = _Object2World[0].xyz;
  tmpvar_8[1] = _Object2World[1].xyz;
  tmpvar_8[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_9;
  tmpvar_9 = (tmpvar_8 * (tmpvar_2 * unity_Scale.w));
  highp mat3 tmpvar_10;
  tmpvar_10[0] = tmpvar_1.xyz;
  tmpvar_10[1] = (cross (tmpvar_2, tmpvar_1.xyz) * _glesTANGENT.w);
  tmpvar_10[2] = tmpvar_2;
  mat3 tmpvar_11;
  tmpvar_11[0].x = tmpvar_10[0].x;
  tmpvar_11[0].y = tmpvar_10[1].x;
  tmpvar_11[0].z = tmpvar_10[2].x;
  tmpvar_11[1].x = tmpvar_10[0].y;
  tmpvar_11[1].y = tmpvar_10[1].y;
  tmpvar_11[1].z = tmpvar_10[2].y;
  tmpvar_11[2].x = tmpvar_10[0].z;
  tmpvar_11[2].y = tmpvar_10[1].z;
  tmpvar_11[2].z = tmpvar_10[2].z;
  highp vec3 tmpvar_12;
  tmpvar_12 = (tmpvar_11 * (_World2Object * _WorldSpaceLightPos0).xyz);
  tmpvar_5 = tmpvar_12;
  highp vec4 tmpvar_13;
  tmpvar_13.w = 1.0;
  tmpvar_13.xyz = tmpvar_9;
  mediump vec3 tmpvar_14;
  mediump vec4 normal;
  normal = tmpvar_13;
  mediump vec3 x3;
  highp float vC;
  mediump vec3 x2;
  mediump vec3 x1;
  highp float tmpvar_15;
  tmpvar_15 = dot (unity_SHAr, normal);
  x1.x = tmpvar_15;
  highp float tmpvar_16;
  tmpvar_16 = dot (unity_SHAg, normal);
  x1.y = tmpvar_16;
  highp float tmpvar_17;
  tmpvar_17 = dot (unity_SHAb, normal);
  x1.z = tmpvar_17;
  mediump vec4 tmpvar_18;
  tmpvar_18 = (normal.xyzz * normal.yzzx);
  highp float tmpvar_19;
  tmpvar_19 = dot (unity_SHBr, tmpvar_18);
  x2.x = tmpvar_19;
  highp float tmpvar_20;
  tmpvar_20 = dot (unity_SHBg, tmpvar_18);
  x2.y = tmpvar_20;
  highp float tmpvar_21;
  tmpvar_21 = dot (unity_SHBb, tmpvar_18);
  x2.z = tmpvar_21;
  mediump float tmpvar_22;
  tmpvar_22 = ((normal.x * normal.x) - (normal.y * normal.y));
  vC = tmpvar_22;
  highp vec3 tmpvar_23;
  tmpvar_23 = (unity_SHC.xyz * vC);
  x3 = tmpvar_23;
  tmpvar_14 = ((x1 + x2) + x3);
  shlight = tmpvar_14;
  tmpvar_6 = shlight;
  highp vec3 tmpvar_24;
  tmpvar_24 = (_Object2World * _glesVertex).xyz;
  highp vec4 tmpvar_25;
  tmpvar_25 = (unity_4LightPosX0 - tmpvar_24.x);
  highp vec4 tmpvar_26;
  tmpvar_26 = (unity_4LightPosY0 - tmpvar_24.y);
  highp vec4 tmpvar_27;
  tmpvar_27 = (unity_4LightPosZ0 - tmpvar_24.z);
  highp vec4 tmpvar_28;
  tmpvar_28 = (((tmpvar_25 * tmpvar_25) + (tmpvar_26 * tmpvar_26)) + (tmpvar_27 * tmpvar_27));
  highp vec4 tmpvar_29;
  tmpvar_29 = (max (vec4(0.0, 0.0, 0.0, 0.0), ((((tmpvar_25 * tmpvar_9.x) + (tmpvar_26 * tmpvar_9.y)) + (tmpvar_27 * tmpvar_9.z)) * inversesqrt (tmpvar_28))) * (1.0/((1.0 + (tmpvar_28 * unity_4LightAtten0)))));
  highp vec3 tmpvar_30;
  tmpvar_30 = (tmpvar_6 + ((((unity_LightColor[0].xyz * tmpvar_29.x) + (unity_LightColor[1].xyz * tmpvar_29.y)) + (unity_LightColor[2].xyz * tmpvar_29.z)) + (unity_LightColor[3].xyz * tmpvar_29.w)));
  tmpvar_6 = tmpvar_30;
  highp vec4 o_i0;
  highp vec4 tmpvar_31;
  tmpvar_31 = (tmpvar_7 * 0.5);
  o_i0 = tmpvar_31;
  highp vec2 tmpvar_32;
  tmpvar_32.x = tmpvar_31.x;
  tmpvar_32.y = (tmpvar_31.y * _ProjectionParams.x);
  o_i0.xy = (tmpvar_32 + tmpvar_31.w);
  o_i0.zw = tmpvar_7.zw;
  gl_Position = tmpvar_7;
  xlv_TEXCOORD0 = tmpvar_3;
  xlv_TEXCOORD1 = tmpvar_4;
  xlv_TEXCOORD2 = tmpvar_5;
  xlv_TEXCOORD3 = tmpvar_6;
  xlv_TEXCOORD4 = o_i0;
}



#endif
#ifdef FRAGMENT

varying highp vec4 xlv_TEXCOORD4;
varying lowp vec3 xlv_TEXCOORD3;
varying lowp vec3 xlv_TEXCOORD2;
varying highp vec4 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;
uniform sampler2D _Splat2;
uniform sampler2D _Splat1;
uniform sampler2D _Splat0;
uniform sampler2D _ShadowMapTexture;
uniform lowp vec4 _LightColor0;
uniform sampler2D _Control;
uniform sampler2D _BumpSplat2;
uniform sampler2D _BumpSplat1;
uniform sampler2D _BumpSplat0;
void main ()
{
  lowp vec4 c;
  highp vec2 tmpvar_1;
  tmpvar_1 = xlv_TEXCOORD0.xy;
  highp vec2 tmpvar_2;
  tmpvar_2 = xlv_TEXCOORD0.zw;
  highp vec2 tmpvar_3;
  tmpvar_3 = xlv_TEXCOORD1.xy;
  highp vec2 tmpvar_4;
  tmpvar_4 = xlv_TEXCOORD1.zw;
  lowp vec3 tmpvar_5;
  lowp vec4 tmpvar_6;
  tmpvar_6 = texture2D (_Control, tmpvar_1);
  tmpvar_5 = (((texture2D (_Splat0, tmpvar_2).xyz * tmpvar_6.x) + (texture2D (_Splat1, tmpvar_3).xyz * tmpvar_6.y)) + (texture2D (_Splat2, tmpvar_4).xyz * tmpvar_6.z));
  lowp vec4 c_i0;
  c_i0.xyz = ((tmpvar_5 * _LightColor0.xyz) * ((max (0.0, dot ((((((texture2D (_BumpSplat0, tmpvar_2).xyz * 2.0) - 1.0) * tmpvar_6.x) + (((texture2D (_BumpSplat1, tmpvar_3).xyz * 2.0) - 1.0) * tmpvar_6.y)) + (((texture2D (_BumpSplat2, tmpvar_4).xyz * 2.0) - 1.0) * tmpvar_6.z)), xlv_TEXCOORD2)) * texture2DProj (_ShadowMapTexture, xlv_TEXCOORD4).x) * 2.0));
  c_i0.w = 0.0;
  c = c_i0;
  c.xyz = (c_i0.xyz + (tmpvar_5 * xlv_TEXCOORD3));
  gl_FragData[0] = c;
}



#endif"
}

SubProgram "glesdesktop " {
Keywords { "DIRECTIONAL" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "SHADOWS_SCREEN" "VERTEXLIGHT_ON" }
"!!GLES
#define SHADER_API_GLES 1
#define tex2D texture2D


#ifdef VERTEX
#define gl_ModelViewProjectionMatrix glstate_matrix_mvp
uniform mat4 glstate_matrix_mvp;

varying highp vec4 xlv_TEXCOORD4;
varying lowp vec3 xlv_TEXCOORD3;
varying lowp vec3 xlv_TEXCOORD2;
varying highp vec4 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;
uniform highp vec4 unity_Scale;
uniform highp vec4 unity_SHC;
uniform highp vec4 unity_SHBr;
uniform highp vec4 unity_SHBg;
uniform highp vec4 unity_SHBb;
uniform highp vec4 unity_SHAr;
uniform highp vec4 unity_SHAg;
uniform highp vec4 unity_SHAb;
uniform highp vec4 unity_LightColor[4];
uniform highp vec4 unity_4LightPosZ0;
uniform highp vec4 unity_4LightPosY0;
uniform highp vec4 unity_4LightPosX0;
uniform highp vec4 unity_4LightAtten0;

uniform lowp vec4 _WorldSpaceLightPos0;
uniform highp mat4 _World2Object;
uniform highp vec4 _Splat2_ST;
uniform highp vec4 _Splat1_ST;
uniform highp vec4 _Splat0_ST;
uniform highp vec4 _ProjectionParams;
uniform highp mat4 _Object2World;
uniform highp vec4 _Control_ST;
attribute vec4 _glesTANGENT;
attribute vec4 _glesMultiTexCoord0;
attribute vec3 _glesNormal;
attribute vec4 _glesVertex;
void main ()
{
  vec4 tmpvar_1;
  tmpvar_1.xyz = normalize (_glesTANGENT.xyz);
  tmpvar_1.w = _glesTANGENT.w;
  vec3 tmpvar_2;
  tmpvar_2 = normalize (_glesNormal);
  highp vec3 shlight;
  highp vec4 tmpvar_3;
  highp vec4 tmpvar_4;
  lowp vec3 tmpvar_5;
  lowp vec3 tmpvar_6;
  highp vec4 tmpvar_7;
  tmpvar_7 = (gl_ModelViewProjectionMatrix * _glesVertex);
  tmpvar_3.xy = ((_glesMultiTexCoord0.xy * _Control_ST.xy) + _Control_ST.zw);
  tmpvar_3.zw = ((_glesMultiTexCoord0.xy * _Splat0_ST.xy) + _Splat0_ST.zw);
  tmpvar_4.xy = ((_glesMultiTexCoord0.xy * _Splat1_ST.xy) + _Splat1_ST.zw);
  tmpvar_4.zw = ((_glesMultiTexCoord0.xy * _Splat2_ST.xy) + _Splat2_ST.zw);
  mat3 tmpvar_8;
  tmpvar_8[0] = _Object2World[0].xyz;
  tmpvar_8[1] = _Object2World[1].xyz;
  tmpvar_8[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_9;
  tmpvar_9 = (tmpvar_8 * (tmpvar_2 * unity_Scale.w));
  highp mat3 tmpvar_10;
  tmpvar_10[0] = tmpvar_1.xyz;
  tmpvar_10[1] = (cross (tmpvar_2, tmpvar_1.xyz) * _glesTANGENT.w);
  tmpvar_10[2] = tmpvar_2;
  mat3 tmpvar_11;
  tmpvar_11[0].x = tmpvar_10[0].x;
  tmpvar_11[0].y = tmpvar_10[1].x;
  tmpvar_11[0].z = tmpvar_10[2].x;
  tmpvar_11[1].x = tmpvar_10[0].y;
  tmpvar_11[1].y = tmpvar_10[1].y;
  tmpvar_11[1].z = tmpvar_10[2].y;
  tmpvar_11[2].x = tmpvar_10[0].z;
  tmpvar_11[2].y = tmpvar_10[1].z;
  tmpvar_11[2].z = tmpvar_10[2].z;
  highp vec3 tmpvar_12;
  tmpvar_12 = (tmpvar_11 * (_World2Object * _WorldSpaceLightPos0).xyz);
  tmpvar_5 = tmpvar_12;
  highp vec4 tmpvar_13;
  tmpvar_13.w = 1.0;
  tmpvar_13.xyz = tmpvar_9;
  mediump vec3 tmpvar_14;
  mediump vec4 normal;
  normal = tmpvar_13;
  mediump vec3 x3;
  highp float vC;
  mediump vec3 x2;
  mediump vec3 x1;
  highp float tmpvar_15;
  tmpvar_15 = dot (unity_SHAr, normal);
  x1.x = tmpvar_15;
  highp float tmpvar_16;
  tmpvar_16 = dot (unity_SHAg, normal);
  x1.y = tmpvar_16;
  highp float tmpvar_17;
  tmpvar_17 = dot (unity_SHAb, normal);
  x1.z = tmpvar_17;
  mediump vec4 tmpvar_18;
  tmpvar_18 = (normal.xyzz * normal.yzzx);
  highp float tmpvar_19;
  tmpvar_19 = dot (unity_SHBr, tmpvar_18);
  x2.x = tmpvar_19;
  highp float tmpvar_20;
  tmpvar_20 = dot (unity_SHBg, tmpvar_18);
  x2.y = tmpvar_20;
  highp float tmpvar_21;
  tmpvar_21 = dot (unity_SHBb, tmpvar_18);
  x2.z = tmpvar_21;
  mediump float tmpvar_22;
  tmpvar_22 = ((normal.x * normal.x) - (normal.y * normal.y));
  vC = tmpvar_22;
  highp vec3 tmpvar_23;
  tmpvar_23 = (unity_SHC.xyz * vC);
  x3 = tmpvar_23;
  tmpvar_14 = ((x1 + x2) + x3);
  shlight = tmpvar_14;
  tmpvar_6 = shlight;
  highp vec3 tmpvar_24;
  tmpvar_24 = (_Object2World * _glesVertex).xyz;
  highp vec4 tmpvar_25;
  tmpvar_25 = (unity_4LightPosX0 - tmpvar_24.x);
  highp vec4 tmpvar_26;
  tmpvar_26 = (unity_4LightPosY0 - tmpvar_24.y);
  highp vec4 tmpvar_27;
  tmpvar_27 = (unity_4LightPosZ0 - tmpvar_24.z);
  highp vec4 tmpvar_28;
  tmpvar_28 = (((tmpvar_25 * tmpvar_25) + (tmpvar_26 * tmpvar_26)) + (tmpvar_27 * tmpvar_27));
  highp vec4 tmpvar_29;
  tmpvar_29 = (max (vec4(0.0, 0.0, 0.0, 0.0), ((((tmpvar_25 * tmpvar_9.x) + (tmpvar_26 * tmpvar_9.y)) + (tmpvar_27 * tmpvar_9.z)) * inversesqrt (tmpvar_28))) * (1.0/((1.0 + (tmpvar_28 * unity_4LightAtten0)))));
  highp vec3 tmpvar_30;
  tmpvar_30 = (tmpvar_6 + ((((unity_LightColor[0].xyz * tmpvar_29.x) + (unity_LightColor[1].xyz * tmpvar_29.y)) + (unity_LightColor[2].xyz * tmpvar_29.z)) + (unity_LightColor[3].xyz * tmpvar_29.w)));
  tmpvar_6 = tmpvar_30;
  highp vec4 o_i0;
  highp vec4 tmpvar_31;
  tmpvar_31 = (tmpvar_7 * 0.5);
  o_i0 = tmpvar_31;
  highp vec2 tmpvar_32;
  tmpvar_32.x = tmpvar_31.x;
  tmpvar_32.y = (tmpvar_31.y * _ProjectionParams.x);
  o_i0.xy = (tmpvar_32 + tmpvar_31.w);
  o_i0.zw = tmpvar_7.zw;
  gl_Position = tmpvar_7;
  xlv_TEXCOORD0 = tmpvar_3;
  xlv_TEXCOORD1 = tmpvar_4;
  xlv_TEXCOORD2 = tmpvar_5;
  xlv_TEXCOORD3 = tmpvar_6;
  xlv_TEXCOORD4 = o_i0;
}



#endif
#ifdef FRAGMENT

varying highp vec4 xlv_TEXCOORD4;
varying lowp vec3 xlv_TEXCOORD3;
varying lowp vec3 xlv_TEXCOORD2;
varying highp vec4 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;
uniform sampler2D _Splat2;
uniform sampler2D _Splat1;
uniform sampler2D _Splat0;
uniform sampler2D _ShadowMapTexture;
uniform lowp vec4 _LightColor0;
uniform sampler2D _Control;
uniform sampler2D _BumpSplat2;
uniform sampler2D _BumpSplat1;
uniform sampler2D _BumpSplat0;
void main ()
{
  lowp vec4 c;
  highp vec2 tmpvar_1;
  tmpvar_1 = xlv_TEXCOORD0.xy;
  highp vec2 tmpvar_2;
  tmpvar_2 = xlv_TEXCOORD0.zw;
  highp vec2 tmpvar_3;
  tmpvar_3 = xlv_TEXCOORD1.xy;
  highp vec2 tmpvar_4;
  tmpvar_4 = xlv_TEXCOORD1.zw;
  lowp vec3 tmpvar_5;
  lowp vec4 tmpvar_6;
  tmpvar_6 = texture2D (_Control, tmpvar_1);
  tmpvar_5 = (((texture2D (_Splat0, tmpvar_2).xyz * tmpvar_6.x) + (texture2D (_Splat1, tmpvar_3).xyz * tmpvar_6.y)) + (texture2D (_Splat2, tmpvar_4).xyz * tmpvar_6.z));
  lowp vec3 normal;
  normal.xy = ((texture2D (_BumpSplat0, tmpvar_2).wy * 2.0) - 1.0);
  normal.z = sqrt (((1.0 - (normal.x * normal.x)) - (normal.y * normal.y)));
  lowp vec3 normal_i0;
  normal_i0.xy = ((texture2D (_BumpSplat1, tmpvar_3).wy * 2.0) - 1.0);
  normal_i0.z = sqrt (((1.0 - (normal_i0.x * normal_i0.x)) - (normal_i0.y * normal_i0.y)));
  lowp vec3 normal_i0_i1;
  normal_i0_i1.xy = ((texture2D (_BumpSplat2, tmpvar_4).wy * 2.0) - 1.0);
  normal_i0_i1.z = sqrt (((1.0 - (normal_i0_i1.x * normal_i0_i1.x)) - (normal_i0_i1.y * normal_i0_i1.y)));
  lowp vec4 c_i0;
  c_i0.xyz = ((tmpvar_5 * _LightColor0.xyz) * ((max (0.0, dot ((((normal * tmpvar_6.x) + (normal_i0 * tmpvar_6.y)) + (normal_i0_i1 * tmpvar_6.z)), xlv_TEXCOORD2)) * texture2DProj (_ShadowMapTexture, xlv_TEXCOORD4).x) * 2.0));
  c_i0.w = 0.0;
  c = c_i0;
  c.xyz = (c_i0.xyz + (tmpvar_5 * xlv_TEXCOORD3));
  gl_FragData[0] = c;
}



#endif"
}

}
Program "fp" {
// Fragment combos: 6
//   opengl - ALU: 12 to 50, TEX: 5 to 10
//   d3d9 - ALU: 12 to 54, TEX: 5 to 10
SubProgram "opengl " {
Keywords { "DIRECTIONAL" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "SHADOWS_OFF" }
Vector 0 [_LightColor0]
SetTexture 0 [_Control] 2D
SetTexture 1 [_Splat0] 2D
SetTexture 2 [_Splat1] 2D
SetTexture 3 [_Splat2] 2D
SetTexture 4 [_BumpSplat0] 2D
SetTexture 5 [_BumpSplat1] 2D
SetTexture 6 [_BumpSplat2] 2D
"!!ARBfp1.0
OPTION ARB_precision_hint_fastest;
# 38 ALU, 7 TEX
PARAM c[2] = { program.local[0],
		{ 2, 1, 0 } };
TEMP R0;
TEMP R1;
TEMP R2;
TEMP R3;
TEMP R4;
TEMP R5;
TEMP R6;
TEX R4.yw, fragment.texcoord[1], texture[5], 2D;
TEX R0.xyz, fragment.texcoord[0], texture[0], 2D;
TEX R3.xyz, fragment.texcoord[1], texture[2], 2D;
TEX R1.xyz, fragment.texcoord[1].zwzw, texture[3], 2D;
TEX R2.xyz, fragment.texcoord[0].zwzw, texture[1], 2D;
TEX R5.yw, fragment.texcoord[1].zwzw, texture[6], 2D;
TEX R6.yw, fragment.texcoord[0].zwzw, texture[4], 2D;
MUL R3.xyz, R0.y, R3;
MAD R2.xyz, R0.x, R2, R3;
MAD R4.xy, R4.wyzw, c[1].x, -c[1].y;
MUL R0.w, R4.y, R4.y;
MAD R0.w, -R4.x, R4.x, -R0;
ADD R0.w, R0, c[1].y;
RSQ R0.w, R0.w;
MAD R1.xyz, R0.z, R1, R2;
RCP R4.z, R0.w;
MUL R2.xyz, R0.y, R4;
MAD R3.xy, R6.wyzw, c[1].x, -c[1].y;
MAD R4.xy, R5.wyzw, c[1].x, -c[1].y;
MUL R0.w, R3.y, R3.y;
MUL R0.y, R4, R4;
MAD R0.w, -R3.x, R3.x, -R0;
MAD R0.y, -R4.x, R4.x, -R0;
ADD R0.w, R0, c[1].y;
RSQ R0.w, R0.w;
RCP R3.z, R0.w;
ADD R0.y, R0, c[1];
RSQ R0.y, R0.y;
MAD R2.xyz, R0.x, R3, R2;
RCP R4.z, R0.y;
MAD R2.xyz, R4, R0.z, R2;
MUL R0.xyz, R1, fragment.texcoord[3];
DP3 R0.w, R2, fragment.texcoord[2];
MUL R1.xyz, R1, c[0];
MAX R0.w, R0, c[1].z;
MUL R1.xyz, R0.w, R1;
MAD result.color.xyz, R1, c[1].x, R0;
MOV result.color.w, c[1].z;
END
# 38 instructions, 7 R-regs
"
}

SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "SHADOWS_OFF" }
Vector 0 [_LightColor0]
SetTexture 0 [_Control] 2D
SetTexture 1 [_Splat0] 2D
SetTexture 2 [_Splat1] 2D
SetTexture 3 [_Splat2] 2D
SetTexture 4 [_BumpSplat0] 2D
SetTexture 5 [_BumpSplat1] 2D
SetTexture 6 [_BumpSplat2] 2D
"ps_2_0
; 45 ALU, 7 TEX
dcl_2d s0
dcl_2d s1
dcl_2d s2
dcl_2d s3
dcl_2d s4
dcl_2d s5
dcl_2d s6
def c1, 2.00000000, -1.00000000, 1.00000000, 0.00000000
dcl t0
dcl t1
dcl t2.xyz
dcl t3.xyz
texld r6, t1, s5
texld r5, t0, s0
mov r6.x, r6.w
mad_pp r6.xy, r6, c1.x, c1.y
mov r2.y, t1.w
mov r2.x, t1.z
mov r3.xy, r2
mov r2.y, t0.w
mov r2.x, t0.z
mov r0.y, t0.w
mov r0.x, t0.z
mov r1.y, t1.w
mov r1.x, t1.z
texld r4, r3, s3
texld r3, r2, s1
texld r1, r1, s6
texld r0, r0, s4
texld r2, t1, s2
mul_pp r0.x, r6.y, r6.y
mad_pp r0.x, -r6, r6, -r0
add_pp r0.x, r0, c1.z
rsq_pp r0.x, r0.x
rcp_pp r6.z, r0.x
mov r0.x, r0.w
mad_pp r7.xy, r0, c1.x, c1.y
mul_pp r1.x, r7.y, r7.y
mad_pp r1.x, -r7, r7, -r1
add_pp r1.x, r1, c1.z
rsq_pp r1.x, r1.x
mov r0.y, r1
mov r0.x, r1.w
mad_pp r8.xy, r0, c1.x, c1.y
mul_pp r0.x, r8.y, r8.y
mad_pp r0.x, -r8, r8, -r0
add_pp r0.x, r0, c1.z
rsq_pp r0.x, r0.x
mul_pp r6.xyz, r5.y, r6
rcp_pp r7.z, r1.x
mad_pp r1.xyz, r5.x, r7, r6
rcp_pp r8.z, r0.x
mad_pp r0.xyz, r8, r5.z, r1
dp3_pp r0.x, r0, t2
mul_pp r1.xyz, r5.y, r2
mad_pp r1.xyz, r5.x, r3, r1
mad_pp r1.xyz, r5.z, r4, r1
mul_pp r2.xyz, r1, c0
max_pp r0.x, r0, c1.w
mul_pp r0.xyz, r0.x, r2
mul_pp r1.xyz, r1, t3
mov_pp r0.w, c1
mad_pp r0.xyz, r0, c1.x, r1
mov_pp oC0, r0
"
}

SubProgram "gles " {
Keywords { "DIRECTIONAL" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "SHADOWS_OFF" }
"!!GLES"
}

SubProgram "glesdesktop " {
Keywords { "DIRECTIONAL" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "SHADOWS_OFF" }
"!!GLES"
}

SubProgram "opengl " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "DIRLIGHTMAP_OFF" "SHADOWS_OFF" }
SetTexture 0 [_Control] 2D
SetTexture 1 [_Splat0] 2D
SetTexture 2 [_Splat1] 2D
SetTexture 3 [_Splat2] 2D
SetTexture 7 [unity_Lightmap] 2D
"!!ARBfp1.0
OPTION ARB_precision_hint_fastest;
# 12 ALU, 5 TEX
PARAM c[1] = { { 0, 8 } };
TEMP R0;
TEMP R1;
TEMP R2;
TEMP R3;
TEMP R4;
TEX R0, fragment.texcoord[2], texture[7], 2D;
TEX R2.xyz, fragment.texcoord[0], texture[0], 2D;
TEX R3.xyz, fragment.texcoord[1], texture[2], 2D;
TEX R4.xyz, fragment.texcoord[1].zwzw, texture[3], 2D;
TEX R1.xyz, fragment.texcoord[0].zwzw, texture[1], 2D;
MUL R3.xyz, R2.y, R3;
MAD R1.xyz, R2.x, R1, R3;
MAD R1.xyz, R4, R2.z, R1;
MUL R0.xyz, R0.w, R0;
MUL R0.xyz, R0, R1;
MUL result.color.xyz, R0, c[0].y;
MOV result.color.w, c[0].x;
END
# 12 instructions, 5 R-regs
"
}

SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "DIRLIGHTMAP_OFF" "SHADOWS_OFF" }
SetTexture 0 [_Control] 2D
SetTexture 1 [_Splat0] 2D
SetTexture 2 [_Splat1] 2D
SetTexture 3 [_Splat2] 2D
SetTexture 7 [unity_Lightmap] 2D
"ps_2_0
; 12 ALU, 5 TEX
dcl_2d s0
dcl_2d s1
dcl_2d s2
dcl_2d s3
dcl_2d s7
def c0, 8.00000000, 0.00000000, 0, 0
dcl t0
dcl t1
dcl t2.xy
texld r3, t0, s0
mov r0.y, t1.w
mov r0.x, t1.z
mov r1.y, t0.w
mov r1.x, t0.z
texld r4, r0, s3
texld r2, r1, s1
texld r0, t2, s7
texld r1, t1, s2
mul_pp r0.xyz, r0.w, r0
mul_pp r1.xyz, r3.y, r1
mad_pp r1.xyz, r3.x, r2, r1
mad_pp r1.xyz, r4, r3.z, r1
mul_pp r0.xyz, r0, r1
mul_pp r0.xyz, r0, c0.x
mov_pp r0.w, c0.y
mov_pp oC0, r0
"
}

SubProgram "gles " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "DIRLIGHTMAP_OFF" "SHADOWS_OFF" }
"!!GLES"
}

SubProgram "glesdesktop " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "DIRLIGHTMAP_OFF" "SHADOWS_OFF" }
"!!GLES"
}

SubProgram "opengl " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "DIRLIGHTMAP_ON" "SHADOWS_OFF" }
SetTexture 0 [_Control] 2D
SetTexture 1 [_Splat0] 2D
SetTexture 2 [_Splat1] 2D
SetTexture 3 [_Splat2] 2D
SetTexture 4 [_BumpSplat0] 2D
SetTexture 5 [_BumpSplat1] 2D
SetTexture 6 [_BumpSplat2] 2D
SetTexture 7 [unity_Lightmap] 2D
SetTexture 8 [unity_LightmapInd] 2D
"!!ARBfp1.0
OPTION ARB_precision_hint_fastest;
# 44 ALU, 9 TEX
PARAM c[4] = { { 0, 2, 1, 8 },
		{ -0.40824828, -0.70710677, 0.57735026 },
		{ -0.40824831, 0.70710677, 0.57735026 },
		{ 0.81649655, 0, 0.57735026 } };
TEMP R0;
TEMP R1;
TEMP R2;
TEMP R3;
TEMP R4;
TEMP R5;
TEMP R6;
TEMP R7;
TEMP R8;
TEX R1, fragment.texcoord[2], texture[8], 2D;
TEX R0, fragment.texcoord[2], texture[7], 2D;
TEX R7.yw, fragment.texcoord[1].zwzw, texture[6], 2D;
TEX R6.yw, fragment.texcoord[1], texture[5], 2D;
TEX R8.yw, fragment.texcoord[0].zwzw, texture[4], 2D;
TEX R4.xyz, fragment.texcoord[0], texture[0], 2D;
TEX R5.xyz, fragment.texcoord[1], texture[2], 2D;
TEX R2.xyz, fragment.texcoord[1].zwzw, texture[3], 2D;
TEX R3.xyz, fragment.texcoord[0].zwzw, texture[1], 2D;
MAD R6.xy, R6.wyzw, c[0].y, -c[0].z;
MAD R7.xy, R7.wyzw, c[0].y, -c[0].z;
MUL R2.w, R6.y, R6.y;
MAD R2.w, -R6.x, R6.x, -R2;
MUL R3.w, R7.y, R7.y;
MAD R3.w, -R7.x, R7.x, -R3;
ADD R2.w, R2, c[0].z;
RSQ R2.w, R2.w;
RCP R6.z, R2.w;
MAD R8.xy, R8.wyzw, c[0].y, -c[0].z;
MUL R2.w, R8.y, R8.y;
MAD R2.w, -R8.x, R8.x, -R2;
MUL R5.xyz, R4.y, R5;
ADD R2.w, R2, c[0].z;
ADD R4.w, R3, c[0].z;
RSQ R3.w, R2.w;
RSQ R2.w, R4.w;
MUL R1.xyz, R1.w, R1;
MUL R6.xyz, R4.y, R6;
RCP R8.z, R3.w;
MAD R6.xyz, R4.x, R8, R6;
RCP R7.z, R2.w;
MAD R7.xyz, R7, R4.z, R6;
DP3_SAT R6.z, R7, c[1];
DP3_SAT R6.y, R7, c[2];
DP3_SAT R6.x, R7, c[3];
MUL R1.xyz, R1, R6;
DP3 R1.w, R1, c[0].w;
MAD R3.xyz, R4.x, R3, R5;
MUL R0.xyz, R0.w, R0;
MAD R1.xyz, R4.z, R2, R3;
MUL R0.xyz, R0, R1.w;
MUL R0.xyz, R0, R1;
MUL result.color.xyz, R0, c[0].w;
MOV result.color.w, c[0].x;
END
# 44 instructions, 9 R-regs
"
}

SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "DIRLIGHTMAP_ON" "SHADOWS_OFF" }
SetTexture 0 [_Control] 2D
SetTexture 1 [_Splat0] 2D
SetTexture 2 [_Splat1] 2D
SetTexture 3 [_Splat2] 2D
SetTexture 4 [_BumpSplat0] 2D
SetTexture 5 [_BumpSplat1] 2D
SetTexture 6 [_BumpSplat2] 2D
SetTexture 7 [unity_Lightmap] 2D
SetTexture 8 [unity_LightmapInd] 2D
"ps_2_0
; 48 ALU, 9 TEX
dcl_2d s0
dcl_2d s1
dcl_2d s2
dcl_2d s3
dcl_2d s4
dcl_2d s5
dcl_2d s6
dcl_2d s7
dcl_2d s8
def c0, 2.00000000, -1.00000000, 1.00000000, 8.00000000
def c1, -0.40824828, -0.70710677, 0.57735026, 0.00000000
def c2, -0.40824831, 0.70710677, 0.57735026, 0
def c3, 0.81649655, 0.00000000, 0.57735026, 0
dcl t0
dcl t1
dcl t2.xy
texld r8, t1, s5
texld r5, t1, s2
texld r4, t0, s0
mov r8.x, r8.w
mad_pp r8.xy, r8, c0.x, c0.y
mov r2.y, t1.w
mov r2.x, t1.z
mov r3.y, t0.w
mov r3.x, t0.z
mov r0.y, t0.w
mov r0.x, t0.z
mov r1.y, t1.w
mov r1.x, t1.z
texld r7, r2, s3
texld r6, r3, s1
texld r1, r1, s6
texld r0, r0, s4
texld r2, t2, s7
texld r3, t2, s8
mul_pp r0.x, r8.y, r8.y
mad_pp r0.x, -r8, r8, -r0
add_pp r0.x, r0, c0.z
rsq_pp r0.x, r0.x
rcp_pp r8.z, r0.x
mov r0.x, r0.w
mad_pp r9.xy, r0, c0.x, c0.y
mul_pp r1.x, r9.y, r9.y
mad_pp r1.x, -r9, r9, -r1
add_pp r1.x, r1, c0.z
rsq_pp r1.x, r1.x
mov r0.y, r1
mov r0.x, r1.w
mad_pp r10.xy, r0, c0.x, c0.y
mul_pp r0.x, r10.y, r10.y
mad_pp r0.x, -r10, r10, -r0
add_pp r0.x, r0, c0.z
rsq_pp r0.x, r0.x
rcp_pp r10.z, r0.x
mul_pp r8.xyz, r4.y, r8
rcp_pp r9.z, r1.x
mad_pp r1.xyz, r4.x, r9, r8
mad_pp r1.xyz, r10, r4.z, r1
dp3_pp_sat r0.z, r1, c1
dp3_pp_sat r0.y, r1, c2
dp3_pp_sat r0.x, r1, c3
mul_pp r1.xyz, r3.w, r3
mul_pp r0.xyz, r1, r0
dp3_pp r0.x, r0, c0.w
mul_pp r2.xyz, r2.w, r2
mul_pp r1.xyz, r4.y, r5
mad_pp r1.xyz, r4.x, r6, r1
mad_pp r1.xyz, r4.z, r7, r1
mul_pp r0.xyz, r2, r0.x
mul_pp r0.xyz, r0, r1
mul_pp r0.xyz, r0, c0.w
mov_pp r0.w, c1
mov_pp oC0, r0
"
}

SubProgram "gles " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "DIRLIGHTMAP_ON" "SHADOWS_OFF" }
"!!GLES"
}

SubProgram "glesdesktop " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "DIRLIGHTMAP_ON" "SHADOWS_OFF" }
"!!GLES"
}

SubProgram "opengl " {
Keywords { "DIRECTIONAL" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "SHADOWS_SCREEN" }
Vector 0 [_LightColor0]
SetTexture 0 [_Control] 2D
SetTexture 1 [_Splat0] 2D
SetTexture 2 [_Splat1] 2D
SetTexture 3 [_Splat2] 2D
SetTexture 4 [_BumpSplat0] 2D
SetTexture 5 [_BumpSplat1] 2D
SetTexture 6 [_BumpSplat2] 2D
SetTexture 7 [_ShadowMapTexture] 2D
"!!ARBfp1.0
OPTION ARB_precision_hint_fastest;
# 40 ALU, 8 TEX
PARAM c[2] = { program.local[0],
		{ 2, 1, 0 } };
TEMP R0;
TEMP R1;
TEMP R2;
TEMP R3;
TEMP R4;
TEMP R5;
TEMP R6;
TEX R4.yw, fragment.texcoord[1], texture[5], 2D;
TEX R0.xyz, fragment.texcoord[0], texture[0], 2D;
TEX R3.xyz, fragment.texcoord[1], texture[2], 2D;
TEX R1.xyz, fragment.texcoord[1].zwzw, texture[3], 2D;
TEX R2.xyz, fragment.texcoord[0].zwzw, texture[1], 2D;
TEX R5.yw, fragment.texcoord[1].zwzw, texture[6], 2D;
TEX R6.yw, fragment.texcoord[0].zwzw, texture[4], 2D;
TXP R5.x, fragment.texcoord[4], texture[7], 2D;
MUL R3.xyz, R0.y, R3;
MAD R2.xyz, R0.x, R2, R3;
MAD R4.xy, R4.wyzw, c[1].x, -c[1].y;
MUL R0.w, R4.y, R4.y;
MAD R0.w, -R4.x, R4.x, -R0;
ADD R0.w, R0, c[1].y;
RSQ R0.w, R0.w;
MAD R1.xyz, R0.z, R1, R2;
RCP R4.z, R0.w;
MUL R2.xyz, R0.y, R4;
MAD R3.xy, R6.wyzw, c[1].x, -c[1].y;
MAD R4.xy, R5.wyzw, c[1].x, -c[1].y;
MUL R0.w, R3.y, R3.y;
MUL R0.y, R4, R4;
MAD R0.w, -R3.x, R3.x, -R0;
MAD R0.y, -R4.x, R4.x, -R0;
ADD R0.w, R0, c[1].y;
RSQ R0.w, R0.w;
RCP R3.z, R0.w;
ADD R0.y, R0, c[1];
RSQ R0.y, R0.y;
MAD R2.xyz, R0.x, R3, R2;
RCP R4.z, R0.y;
MAD R0.xyz, R4, R0.z, R2;
DP3 R0.w, R0, fragment.texcoord[2];
MAX R0.w, R0, c[1].z;
MUL R2.xyz, R1, fragment.texcoord[3];
MUL R0.xyz, R1, c[0];
MUL R0.w, R0, R5.x;
MUL R0.xyz, R0.w, R0;
MAD result.color.xyz, R0, c[1].x, R2;
MOV result.color.w, c[1].z;
END
# 40 instructions, 7 R-regs
"
}

SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "SHADOWS_SCREEN" }
Vector 0 [_LightColor0]
SetTexture 0 [_Control] 2D
SetTexture 1 [_Splat0] 2D
SetTexture 2 [_Splat1] 2D
SetTexture 3 [_Splat2] 2D
SetTexture 4 [_BumpSplat0] 2D
SetTexture 5 [_BumpSplat1] 2D
SetTexture 6 [_BumpSplat2] 2D
SetTexture 7 [_ShadowMapTexture] 2D
"ps_2_0
; 45 ALU, 8 TEX
dcl_2d s0
dcl_2d s1
dcl_2d s2
dcl_2d s3
dcl_2d s4
dcl_2d s5
dcl_2d s6
dcl_2d s7
def c1, 2.00000000, -1.00000000, 1.00000000, 0.00000000
dcl t0
dcl t1
dcl t2.xyz
dcl t3.xyz
dcl t4
texld r6, t1, s5
texldp r9, t4, s7
texld r5, t0, s0
mov r6.x, r6.w
mad_pp r6.xy, r6, c1.x, c1.y
mov r2.y, t0.w
mov r2.x, t0.z
mov r3.y, t1.w
mov r3.x, t1.z
mov r0.y, t0.w
mov r0.x, t0.z
mov r1.y, t1.w
mov r1.x, t1.z
texld r4, r3, s3
texld r3, r2, s1
texld r1, r1, s6
texld r0, r0, s4
texld r2, t1, s2
mul_pp r0.x, r6.y, r6.y
mad_pp r0.x, -r6, r6, -r0
add_pp r0.x, r0, c1.z
rsq_pp r0.x, r0.x
rcp_pp r6.z, r0.x
mov r0.x, r0.w
mad_pp r7.xy, r0, c1.x, c1.y
mul_pp r1.x, r7.y, r7.y
mad_pp r1.x, -r7, r7, -r1
add_pp r1.x, r1, c1.z
rsq_pp r1.x, r1.x
mov r0.y, r1
mov r0.x, r1.w
mad_pp r8.xy, r0, c1.x, c1.y
mul_pp r0.x, r8.y, r8.y
mad_pp r0.x, -r8, r8, -r0
add_pp r0.x, r0, c1.z
rsq_pp r0.x, r0.x
mul_pp r6.xyz, r5.y, r6
rcp_pp r7.z, r1.x
mad_pp r1.xyz, r5.x, r7, r6
rcp_pp r8.z, r0.x
mad_pp r0.xyz, r8, r5.z, r1
dp3_pp r0.x, r0, t2
max_pp r0.x, r0, c1.w
mul_pp r1.xyz, r5.y, r2
mad_pp r1.xyz, r5.x, r3, r1
mad_pp r1.xyz, r5.z, r4, r1
mul_pp r2.xyz, r1, c0
mul_pp r0.x, r0, r9
mul_pp r0.xyz, r0.x, r2
mul_pp r1.xyz, r1, t3
mov_pp r0.w, c1
mad_pp r0.xyz, r0, c1.x, r1
mov_pp oC0, r0
"
}

SubProgram "gles " {
Keywords { "DIRECTIONAL" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "SHADOWS_SCREEN" }
"!!GLES"
}

SubProgram "glesdesktop " {
Keywords { "DIRECTIONAL" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "SHADOWS_SCREEN" }
"!!GLES"
}

SubProgram "opengl " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "DIRLIGHTMAP_OFF" "SHADOWS_SCREEN" }
SetTexture 0 [_Control] 2D
SetTexture 1 [_Splat0] 2D
SetTexture 2 [_Splat1] 2D
SetTexture 3 [_Splat2] 2D
SetTexture 7 [_ShadowMapTexture] 2D
SetTexture 8 [unity_Lightmap] 2D
"!!ARBfp1.0
OPTION ARB_precision_hint_fastest;
# 18 ALU, 6 TEX
PARAM c[1] = { { 0, 8, 2 } };
TEMP R0;
TEMP R1;
TEMP R2;
TEMP R3;
TEMP R4;
TEMP R5;
TEX R2.xyz, fragment.texcoord[0], texture[0], 2D;
TEX R3.xyz, fragment.texcoord[1], texture[2], 2D;
TEX R0, fragment.texcoord[2], texture[8], 2D;
TXP R5.x, fragment.texcoord[3], texture[7], 2D;
TEX R4.xyz, fragment.texcoord[1].zwzw, texture[3], 2D;
TEX R1.xyz, fragment.texcoord[0].zwzw, texture[1], 2D;
MUL R3.xyz, R2.y, R3;
MAD R1.xyz, R2.x, R1, R3;
MUL R3.xyz, R0.w, R0;
MUL R3.xyz, R3, c[0].y;
MUL R0.xyz, R0, R5.x;
MUL R0.xyz, R0, c[0].z;
MUL R2.xyw, R3.xyzz, R5.x;
MIN R0.xyz, R3, R0;
MAX R0.xyz, R0, R2.xyww;
MAD R1.xyz, R4, R2.z, R1;
MUL result.color.xyz, R1, R0;
MOV result.color.w, c[0].x;
END
# 18 instructions, 6 R-regs
"
}

SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "DIRLIGHTMAP_OFF" "SHADOWS_SCREEN" }
SetTexture 0 [_Control] 2D
SetTexture 1 [_Splat0] 2D
SetTexture 2 [_Splat1] 2D
SetTexture 3 [_Splat2] 2D
SetTexture 7 [_ShadowMapTexture] 2D
SetTexture 8 [unity_Lightmap] 2D
"ps_2_0
; 18 ALU, 6 TEX
dcl_2d s0
dcl_2d s1
dcl_2d s2
dcl_2d s3
dcl_2d s7
dcl_2d s8
def c0, 8.00000000, 2.00000000, 0.00000000, 0
dcl t0
dcl t1
dcl t2.xy
dcl t3
texld r5, t0, s0
texld r3, t1, s2
mov r0.y, t1.w
mov r0.x, t1.z
mov r1.xy, r0
mov r0.y, t0.w
mov r0.x, t0.z
texld r2, r1, s3
texld r4, r0, s1
texldp r1, t3, s7
texld r0, t2, s8
mul_pp r6.xyz, r0.w, r0
mul_pp r0.xyz, r0, r1.x
mul_pp r6.xyz, r6, c0.x
mul_pp r0.xyz, r0, c0.y
mul_pp r1.xyz, r6, r1.x
min_pp r0.xyz, r6, r0
max_pp r0.xyz, r0, r1
mul_pp r1.xyz, r5.y, r3
mad_pp r1.xyz, r5.x, r4, r1
mad_pp r1.xyz, r2, r5.z, r1
mul_pp r0.xyz, r1, r0
mov_pp r0.w, c0.z
mov_pp oC0, r0
"
}

SubProgram "gles " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "DIRLIGHTMAP_OFF" "SHADOWS_SCREEN" }
"!!GLES"
}

SubProgram "glesdesktop " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "DIRLIGHTMAP_OFF" "SHADOWS_SCREEN" }
"!!GLES"
}

SubProgram "opengl " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "DIRLIGHTMAP_ON" "SHADOWS_SCREEN" }
SetTexture 0 [_Control] 2D
SetTexture 1 [_Splat0] 2D
SetTexture 2 [_Splat1] 2D
SetTexture 3 [_Splat2] 2D
SetTexture 4 [_BumpSplat0] 2D
SetTexture 5 [_BumpSplat1] 2D
SetTexture 6 [_BumpSplat2] 2D
SetTexture 7 [_ShadowMapTexture] 2D
SetTexture 8 [unity_Lightmap] 2D
SetTexture 9 [unity_LightmapInd] 2D
"!!ARBfp1.0
OPTION ARB_precision_hint_fastest;
# 50 ALU, 10 TEX
PARAM c[4] = { { 0, 2, 1, 8 },
		{ -0.40824828, -0.70710677, 0.57735026 },
		{ -0.40824831, 0.70710677, 0.57735026 },
		{ 0.81649655, 0, 0.57735026 } };
TEMP R0;
TEMP R1;
TEMP R2;
TEMP R3;
TEMP R4;
TEMP R5;
TEMP R6;
TEMP R7;
TEMP R8;
TEX R0, fragment.texcoord[2], texture[9], 2D;
TEX R6.yw, fragment.texcoord[1], texture[5], 2D;
TEX R8.yw, fragment.texcoord[1].zwzw, texture[6], 2D;
TEX R7.yw, fragment.texcoord[0].zwzw, texture[4], 2D;
TEX R3.xyz, fragment.texcoord[0], texture[0], 2D;
TEX R2.xyz, fragment.texcoord[1], texture[2], 2D;
TEX R1, fragment.texcoord[2], texture[8], 2D;
TXP R6.x, fragment.texcoord[3], texture[7], 2D;
TEX R5.xyz, fragment.texcoord[1].zwzw, texture[3], 2D;
TEX R4.xyz, fragment.texcoord[0].zwzw, texture[1], 2D;
MAD R6.yz, R6.xwyw, c[0].y, -c[0].z;
MAD R7.xy, R7.wyzw, c[0].y, -c[0].z;
MUL R2.w, R6.z, R6.z;
MAD R2.w, -R6.y, R6.y, -R2;
MUL R3.w, R7.y, R7.y;
MAD R3.w, -R7.x, R7.x, -R3;
ADD R2.w, R2, c[0].z;
RSQ R2.w, R2.w;
RCP R6.w, R2.w;
MAD R8.xy, R8.wyzw, c[0].y, -c[0].z;
MUL R2.w, R8.y, R8.y;
MAD R2.w, -R8.x, R8.x, -R2;
ADD R3.w, R3, c[0].z;
ADD R2.w, R2, c[0].z;
RSQ R3.w, R3.w;
RSQ R2.w, R2.w;
MUL R2.xyz, R3.y, R2;
RCP R8.z, R2.w;
MUL R0.xyz, R0.w, R0;
MUL R6.yzw, R3.y, R6;
RCP R7.z, R3.w;
MAD R7.xyz, R3.x, R7, R6.yzww;
MAD R7.xyz, R3.z, R8, R7;
DP3_SAT R8.z, R7, c[1];
DP3_SAT R8.y, R7, c[2];
DP3_SAT R8.x, R7, c[3];
MUL R0.xyz, R0, R8;
DP3 R0.w, R0, c[0].w;
MUL R0.xyz, R1, R6.x;
MUL R1.xyz, R1.w, R1;
MUL R1.xyz, R1, R0.w;
MUL R1.xyz, R1, c[0].w;
MUL R0.xyz, R0, c[0].y;
MIN R0.xyz, R1, R0;
MUL R1.xyz, R1, R6.x;
MAX R1.xyz, R0, R1;
MAD R2.xyz, R3.x, R4, R2;
MAD R0.xyz, R5, R3.z, R2;
MUL result.color.xyz, R0, R1;
MOV result.color.w, c[0].x;
END
# 50 instructions, 9 R-regs
"
}

SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "DIRLIGHTMAP_ON" "SHADOWS_SCREEN" }
SetTexture 0 [_Control] 2D
SetTexture 1 [_Splat0] 2D
SetTexture 2 [_Splat1] 2D
SetTexture 3 [_Splat2] 2D
SetTexture 4 [_BumpSplat0] 2D
SetTexture 5 [_BumpSplat1] 2D
SetTexture 6 [_BumpSplat2] 2D
SetTexture 7 [_ShadowMapTexture] 2D
SetTexture 8 [unity_Lightmap] 2D
SetTexture 9 [unity_LightmapInd] 2D
"ps_2_0
; 54 ALU, 10 TEX
dcl_2d s0
dcl_2d s1
dcl_2d s2
dcl_2d s3
dcl_2d s4
dcl_2d s5
dcl_2d s6
dcl_2d s7
dcl_2d s8
dcl_2d s9
def c0, 2.00000000, -1.00000000, 1.00000000, 8.00000000
def c1, -0.40824828, -0.70710677, 0.57735026, 0.00000000
def c2, -0.40824831, 0.70710677, 0.57735026, 0
def c3, 0.81649655, 0.00000000, 0.57735026, 0
dcl t0
dcl t1
dcl t2.xy
dcl t3
texld r8, t1, s5
texld r5, t1, s2
texldp r10, t3, s7
texld r4, t0, s0
mov r8.x, r8.w
mad_pp r8.xy, r8, c0.x, c0.y
mov r2.y, t1.w
mov r2.x, t1.z
mov r3.xy, r2
mov r2.y, t0.w
mov r2.x, t0.z
mov r0.y, t0.w
mov r0.x, t0.z
mov r1.y, t1.w
mov r1.x, t1.z
texld r7, r3, s3
texld r6, r2, s1
texld r1, r1, s6
texld r0, r0, s4
texld r3, t2, s8
texld r2, t2, s9
mul_pp r0.x, r8.y, r8.y
mad_pp r0.x, -r8, r8, -r0
add_pp r0.x, r0, c0.z
rsq_pp r0.x, r0.x
rcp_pp r8.z, r0.x
mov r0.x, r0.w
mad_pp r9.xy, r0, c0.x, c0.y
mul_pp r1.x, r9.y, r9.y
mad_pp r1.x, -r9, r9, -r1
add_pp r1.x, r1, c0.z
rsq_pp r1.x, r1.x
mov r0.y, r1
mov r0.x, r1.w
mad_pp r11.xy, r0, c0.x, c0.y
mul_pp r0.x, r11.y, r11.y
mad_pp r0.x, -r11, r11, -r0
add_pp r0.x, r0, c0.z
rsq_pp r0.x, r0.x
rcp_pp r11.z, r0.x
mul_pp r8.xyz, r4.y, r8
rcp_pp r9.z, r1.x
mad_pp r1.xyz, r4.x, r9, r8
mad_pp r1.xyz, r4.z, r11, r1
dp3_pp_sat r0.z, r1, c1
dp3_pp_sat r0.y, r1, c2
dp3_pp_sat r0.x, r1, c3
mul_pp r1.xyz, r2.w, r2
mul_pp r0.xyz, r1, r0
mul_pp r1.xyz, r3, r10.x
dp3_pp r0.x, r0, c0.w
mul_pp r2.xyz, r3.w, r3
mul_pp r0.xyz, r2, r0.x
mul_pp r0.xyz, r0, c0.w
mul_pp r1.xyz, r1, c0.x
min_pp r1.xyz, r0, r1
mul_pp r0.xyz, r0, r10.x
max_pp r0.xyz, r1, r0
mul_pp r1.xyz, r4.y, r5
mad_pp r1.xyz, r4.x, r6, r1
mad_pp r1.xyz, r7, r4.z, r1
mul_pp r0.xyz, r1, r0
mov_pp r0.w, c1
mov_pp oC0, r0
"
}

SubProgram "gles " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "DIRLIGHTMAP_ON" "SHADOWS_SCREEN" }
"!!GLES"
}

SubProgram "glesdesktop " {
Keywords { "DIRECTIONAL" "LIGHTMAP_ON" "DIRLIGHTMAP_ON" "SHADOWS_SCREEN" }
"!!GLES"
}

}
	}
	Pass {
		Name "FORWARD"
		Tags { "LightMode" = "ForwardAdd" }
		ZWrite Off Blend One One Fog { Color (0,0,0,0) }
Program "vp" {
// Vertex combos: 5
//   opengl - ALU: 19 to 28
//   d3d9 - ALU: 22 to 31
SubProgram "opengl " {
Keywords { "POINT" }
Bind "vertex" Vertex
Bind "tangent" ATTR14
Bind "normal" Normal
Bind "texcoord" TexCoord0
Vector 17 [unity_Scale]
Vector 18 [_WorldSpaceLightPos0]
Matrix 5 [_Object2World]
Matrix 9 [_World2Object]
Matrix 13 [_LightMatrix0]
Vector 19 [_Control_ST]
Vector 20 [_Splat0_ST]
Vector 21 [_Splat1_ST]
Vector 22 [_Splat2_ST]
"!!ARBvp1.0
# 27 ALU
PARAM c[23] = { program.local[0],
		state.matrix.mvp,
		program.local[5..22] };
TEMP R0;
TEMP R1;
TEMP R2;
MOV R0.xyz, vertex.attrib[14];
MUL R1.xyz, vertex.normal.zxyw, R0.yzxw;
MAD R1.xyz, vertex.normal.yzxw, R0.zxyw, -R1;
MOV R0, c[18];
DP4 R2.z, R0, c[11];
DP4 R2.x, R0, c[9];
DP4 R2.y, R0, c[10];
MAD R0.xyz, R2, c[17].w, -vertex.position;
MUL R1.xyz, R1, vertex.attrib[14].w;
DP3 result.texcoord[2].y, R0, R1;
DP3 result.texcoord[2].z, vertex.normal, R0;
DP3 result.texcoord[2].x, R0, vertex.attrib[14];
DP4 R0.w, vertex.position, c[8];
DP4 R0.z, vertex.position, c[7];
DP4 R0.x, vertex.position, c[5];
DP4 R0.y, vertex.position, c[6];
DP4 result.texcoord[3].z, R0, c[15];
DP4 result.texcoord[3].y, R0, c[14];
DP4 result.texcoord[3].x, R0, c[13];
MAD result.texcoord[0].zw, vertex.texcoord[0].xyxy, c[20].xyxy, c[20];
MAD result.texcoord[0].xy, vertex.texcoord[0], c[19], c[19].zwzw;
MAD result.texcoord[1].zw, vertex.texcoord[0].xyxy, c[22].xyxy, c[22];
MAD result.texcoord[1].xy, vertex.texcoord[0], c[21], c[21].zwzw;
DP4 result.position.w, vertex.position, c[4];
DP4 result.position.z, vertex.position, c[3];
DP4 result.position.y, vertex.position, c[2];
DP4 result.position.x, vertex.position, c[1];
END
# 27 instructions, 3 R-regs
"
}

SubProgram "d3d9 " {
Keywords { "POINT" }
Bind "vertex" Vertex
Bind "tangent" TexCoord2
Bind "normal" Normal
Bind "texcoord" TexCoord0
Matrix 0 [glstate_matrix_mvp]
Vector 16 [unity_Scale]
Vector 17 [_WorldSpaceLightPos0]
Matrix 4 [_Object2World]
Matrix 8 [_World2Object]
Matrix 12 [_LightMatrix0]
Vector 18 [_Control_ST]
Vector 19 [_Splat0_ST]
Vector 20 [_Splat1_ST]
Vector 21 [_Splat2_ST]
"vs_2_0
; 30 ALU
dcl_position0 v0
dcl_tangent0 v1
dcl_normal0 v2
dcl_texcoord0 v3
mov r0.xyz, v1
mul r1.xyz, v2.zxyw, r0.yzxw
mov r0.xyz, v1
mad r1.xyz, v2.yzxw, r0.zxyw, -r1
mov r0, c10
mul r2.xyz, r1, v1.w
dp4 r3.z, c17, r0
mov r0, c9
dp4 r3.y, c17, r0
mov r1, c8
dp4 r3.x, c17, r1
mad r0.xyz, r3, c16.w, -v0
dp3 oT2.y, r0, r2
dp3 oT2.z, v2, r0
dp3 oT2.x, r0, v1
dp4 r0.w, v0, c7
dp4 r0.z, v0, c6
dp4 r0.x, v0, c4
dp4 r0.y, v0, c5
dp4 oT3.z, r0, c14
dp4 oT3.y, r0, c13
dp4 oT3.x, r0, c12
mad oT0.zw, v3.xyxy, c19.xyxy, c19
mad oT0.xy, v3, c18, c18.zwzw
mad oT1.zw, v3.xyxy, c21.xyxy, c21
mad oT1.xy, v3, c20, c20.zwzw
dp4 oPos.w, v0, c3
dp4 oPos.z, v0, c2
dp4 oPos.y, v0, c1
dp4 oPos.x, v0, c0
"
}

SubProgram "gles " {
Keywords { "POINT" }
"!!GLES
#define SHADER_API_GLES 1
#define tex2D texture2D


#ifdef VERTEX
#define gl_ModelViewProjectionMatrix glstate_matrix_mvp
uniform mat4 glstate_matrix_mvp;

varying highp vec3 xlv_TEXCOORD3;
varying mediump vec3 xlv_TEXCOORD2;
varying highp vec4 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;
uniform highp vec4 unity_Scale;

uniform highp vec4 _WorldSpaceLightPos0;
uniform highp mat4 _World2Object;
uniform highp vec4 _Splat2_ST;
uniform highp vec4 _Splat1_ST;
uniform highp vec4 _Splat0_ST;
uniform highp mat4 _Object2World;
uniform highp mat4 _LightMatrix0;
uniform highp vec4 _Control_ST;
attribute vec4 _glesTANGENT;
attribute vec4 _glesMultiTexCoord0;
attribute vec3 _glesNormal;
attribute vec4 _glesVertex;
void main ()
{
  vec4 tmpvar_1;
  tmpvar_1.xyz = normalize (_glesTANGENT.xyz);
  tmpvar_1.w = _glesTANGENT.w;
  vec3 tmpvar_2;
  tmpvar_2 = normalize (_glesNormal);
  highp vec4 tmpvar_3;
  highp vec4 tmpvar_4;
  mediump vec3 tmpvar_5;
  tmpvar_3.xy = ((_glesMultiTexCoord0.xy * _Control_ST.xy) + _Control_ST.zw);
  tmpvar_3.zw = ((_glesMultiTexCoord0.xy * _Splat0_ST.xy) + _Splat0_ST.zw);
  tmpvar_4.xy = ((_glesMultiTexCoord0.xy * _Splat1_ST.xy) + _Splat1_ST.zw);
  tmpvar_4.zw = ((_glesMultiTexCoord0.xy * _Splat2_ST.xy) + _Splat2_ST.zw);
  highp mat3 tmpvar_6;
  tmpvar_6[0] = tmpvar_1.xyz;
  tmpvar_6[1] = (cross (tmpvar_2, tmpvar_1.xyz) * _glesTANGENT.w);
  tmpvar_6[2] = tmpvar_2;
  mat3 tmpvar_7;
  tmpvar_7[0].x = tmpvar_6[0].x;
  tmpvar_7[0].y = tmpvar_6[1].x;
  tmpvar_7[0].z = tmpvar_6[2].x;
  tmpvar_7[1].x = tmpvar_6[0].y;
  tmpvar_7[1].y = tmpvar_6[1].y;
  tmpvar_7[1].z = tmpvar_6[2].y;
  tmpvar_7[2].x = tmpvar_6[0].z;
  tmpvar_7[2].y = tmpvar_6[1].z;
  tmpvar_7[2].z = tmpvar_6[2].z;
  highp vec3 tmpvar_8;
  tmpvar_8 = (tmpvar_7 * (((_World2Object * _WorldSpaceLightPos0).xyz * unity_Scale.w) - _glesVertex.xyz));
  tmpvar_5 = tmpvar_8;
  gl_Position = (gl_ModelViewProjectionMatrix * _glesVertex);
  xlv_TEXCOORD0 = tmpvar_3;
  xlv_TEXCOORD1 = tmpvar_4;
  xlv_TEXCOORD2 = tmpvar_5;
  xlv_TEXCOORD3 = (_LightMatrix0 * (_Object2World * _glesVertex)).xyz;
}



#endif
#ifdef FRAGMENT

varying highp vec3 xlv_TEXCOORD3;
varying mediump vec3 xlv_TEXCOORD2;
varying highp vec4 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;
uniform sampler2D _Splat2;
uniform sampler2D _Splat1;
uniform sampler2D _Splat0;
uniform sampler2D _LightTexture0;
uniform lowp vec4 _LightColor0;
uniform sampler2D _Control;
uniform sampler2D _BumpSplat2;
uniform sampler2D _BumpSplat1;
uniform sampler2D _BumpSplat0;
void main ()
{
  lowp vec4 c;
  lowp vec3 lightDir;
  highp vec2 tmpvar_1;
  tmpvar_1 = xlv_TEXCOORD0.xy;
  highp vec2 tmpvar_2;
  tmpvar_2 = xlv_TEXCOORD0.zw;
  highp vec2 tmpvar_3;
  tmpvar_3 = xlv_TEXCOORD1.xy;
  highp vec2 tmpvar_4;
  tmpvar_4 = xlv_TEXCOORD1.zw;
  lowp vec4 tmpvar_5;
  tmpvar_5 = texture2D (_Control, tmpvar_1);
  mediump vec3 tmpvar_6;
  tmpvar_6 = normalize (xlv_TEXCOORD2);
  lightDir = tmpvar_6;
  highp vec2 tmpvar_7;
  tmpvar_7 = vec2(dot (xlv_TEXCOORD3, xlv_TEXCOORD3));
  lowp vec4 c_i0;
  c_i0.xyz = (((((texture2D (_Splat0, tmpvar_2).xyz * tmpvar_5.x) + (texture2D (_Splat1, tmpvar_3).xyz * tmpvar_5.y)) + (texture2D (_Splat2, tmpvar_4).xyz * tmpvar_5.z)) * _LightColor0.xyz) * ((max (0.0, dot ((((((texture2D (_BumpSplat0, tmpvar_2).xyz * 2.0) - 1.0) * tmpvar_5.x) + (((texture2D (_BumpSplat1, tmpvar_3).xyz * 2.0) - 1.0) * tmpvar_5.y)) + (((texture2D (_BumpSplat2, tmpvar_4).xyz * 2.0) - 1.0) * tmpvar_5.z)), lightDir)) * texture2D (_LightTexture0, tmpvar_7).w) * 2.0));
  c_i0.w = 0.0;
  c = c_i0;
  c.w = 0.0;
  gl_FragData[0] = c;
}



#endif"
}

SubProgram "glesdesktop " {
Keywords { "POINT" }
"!!GLES
#define SHADER_API_GLES 1
#define tex2D texture2D


#ifdef VERTEX
#define gl_ModelViewProjectionMatrix glstate_matrix_mvp
uniform mat4 glstate_matrix_mvp;

varying highp vec3 xlv_TEXCOORD3;
varying mediump vec3 xlv_TEXCOORD2;
varying highp vec4 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;
uniform highp vec4 unity_Scale;

uniform highp vec4 _WorldSpaceLightPos0;
uniform highp mat4 _World2Object;
uniform highp vec4 _Splat2_ST;
uniform highp vec4 _Splat1_ST;
uniform highp vec4 _Splat0_ST;
uniform highp mat4 _Object2World;
uniform highp mat4 _LightMatrix0;
uniform highp vec4 _Control_ST;
attribute vec4 _glesTANGENT;
attribute vec4 _glesMultiTexCoord0;
attribute vec3 _glesNormal;
attribute vec4 _glesVertex;
void main ()
{
  vec4 tmpvar_1;
  tmpvar_1.xyz = normalize (_glesTANGENT.xyz);
  tmpvar_1.w = _glesTANGENT.w;
  vec3 tmpvar_2;
  tmpvar_2 = normalize (_glesNormal);
  highp vec4 tmpvar_3;
  highp vec4 tmpvar_4;
  mediump vec3 tmpvar_5;
  tmpvar_3.xy = ((_glesMultiTexCoord0.xy * _Control_ST.xy) + _Control_ST.zw);
  tmpvar_3.zw = ((_glesMultiTexCoord0.xy * _Splat0_ST.xy) + _Splat0_ST.zw);
  tmpvar_4.xy = ((_glesMultiTexCoord0.xy * _Splat1_ST.xy) + _Splat1_ST.zw);
  tmpvar_4.zw = ((_glesMultiTexCoord0.xy * _Splat2_ST.xy) + _Splat2_ST.zw);
  highp mat3 tmpvar_6;
  tmpvar_6[0] = tmpvar_1.xyz;
  tmpvar_6[1] = (cross (tmpvar_2, tmpvar_1.xyz) * _glesTANGENT.w);
  tmpvar_6[2] = tmpvar_2;
  mat3 tmpvar_7;
  tmpvar_7[0].x = tmpvar_6[0].x;
  tmpvar_7[0].y = tmpvar_6[1].x;
  tmpvar_7[0].z = tmpvar_6[2].x;
  tmpvar_7[1].x = tmpvar_6[0].y;
  tmpvar_7[1].y = tmpvar_6[1].y;
  tmpvar_7[1].z = tmpvar_6[2].y;
  tmpvar_7[2].x = tmpvar_6[0].z;
  tmpvar_7[2].y = tmpvar_6[1].z;
  tmpvar_7[2].z = tmpvar_6[2].z;
  highp vec3 tmpvar_8;
  tmpvar_8 = (tmpvar_7 * (((_World2Object * _WorldSpaceLightPos0).xyz * unity_Scale.w) - _glesVertex.xyz));
  tmpvar_5 = tmpvar_8;
  gl_Position = (gl_ModelViewProjectionMatrix * _glesVertex);
  xlv_TEXCOORD0 = tmpvar_3;
  xlv_TEXCOORD1 = tmpvar_4;
  xlv_TEXCOORD2 = tmpvar_5;
  xlv_TEXCOORD3 = (_LightMatrix0 * (_Object2World * _glesVertex)).xyz;
}



#endif
#ifdef FRAGMENT

varying highp vec3 xlv_TEXCOORD3;
varying mediump vec3 xlv_TEXCOORD2;
varying highp vec4 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;
uniform sampler2D _Splat2;
uniform sampler2D _Splat1;
uniform sampler2D _Splat0;
uniform sampler2D _LightTexture0;
uniform lowp vec4 _LightColor0;
uniform sampler2D _Control;
uniform sampler2D _BumpSplat2;
uniform sampler2D _BumpSplat1;
uniform sampler2D _BumpSplat0;
void main ()
{
  lowp vec4 c;
  lowp vec3 lightDir;
  highp vec2 tmpvar_1;
  tmpvar_1 = xlv_TEXCOORD0.xy;
  highp vec2 tmpvar_2;
  tmpvar_2 = xlv_TEXCOORD0.zw;
  highp vec2 tmpvar_3;
  tmpvar_3 = xlv_TEXCOORD1.xy;
  highp vec2 tmpvar_4;
  tmpvar_4 = xlv_TEXCOORD1.zw;
  lowp vec4 tmpvar_5;
  tmpvar_5 = texture2D (_Control, tmpvar_1);
  lowp vec3 normal;
  normal.xy = ((texture2D (_BumpSplat0, tmpvar_2).wy * 2.0) - 1.0);
  normal.z = sqrt (((1.0 - (normal.x * normal.x)) - (normal.y * normal.y)));
  lowp vec3 normal_i0;
  normal_i0.xy = ((texture2D (_BumpSplat1, tmpvar_3).wy * 2.0) - 1.0);
  normal_i0.z = sqrt (((1.0 - (normal_i0.x * normal_i0.x)) - (normal_i0.y * normal_i0.y)));
  lowp vec3 normal_i0_i1;
  normal_i0_i1.xy = ((texture2D (_BumpSplat2, tmpvar_4).wy * 2.0) - 1.0);
  normal_i0_i1.z = sqrt (((1.0 - (normal_i0_i1.x * normal_i0_i1.x)) - (normal_i0_i1.y * normal_i0_i1.y)));
  mediump vec3 tmpvar_6;
  tmpvar_6 = normalize (xlv_TEXCOORD2);
  lightDir = tmpvar_6;
  highp vec2 tmpvar_7;
  tmpvar_7 = vec2(dot (xlv_TEXCOORD3, xlv_TEXCOORD3));
  lowp vec4 c_i0;
  c_i0.xyz = (((((texture2D (_Splat0, tmpvar_2).xyz * tmpvar_5.x) + (texture2D (_Splat1, tmpvar_3).xyz * tmpvar_5.y)) + (texture2D (_Splat2, tmpvar_4).xyz * tmpvar_5.z)) * _LightColor0.xyz) * ((max (0.0, dot ((((normal * tmpvar_5.x) + (normal_i0 * tmpvar_5.y)) + (normal_i0_i1 * tmpvar_5.z)), lightDir)) * texture2D (_LightTexture0, tmpvar_7).w) * 2.0));
  c_i0.w = 0.0;
  c = c_i0;
  c.w = 0.0;
  gl_FragData[0] = c;
}



#endif"
}

SubProgram "opengl " {
Keywords { "DIRECTIONAL" }
Bind "vertex" Vertex
Bind "tangent" ATTR14
Bind "normal" Normal
Bind "texcoord" TexCoord0
Vector 9 [_WorldSpaceLightPos0]
Matrix 5 [_World2Object]
Vector 10 [_Control_ST]
Vector 11 [_Splat0_ST]
Vector 12 [_Splat1_ST]
Vector 13 [_Splat2_ST]
"!!ARBvp1.0
# 19 ALU
PARAM c[14] = { program.local[0],
		state.matrix.mvp,
		program.local[5..13] };
TEMP R0;
TEMP R1;
TEMP R2;
MOV R0.xyz, vertex.attrib[14];
MUL R1.xyz, vertex.normal.zxyw, R0.yzxw;
MAD R0.xyz, vertex.normal.yzxw, R0.zxyw, -R1;
MOV R1, c[9];
DP4 R2.z, R1, c[7];
DP4 R2.y, R1, c[6];
DP4 R2.x, R1, c[5];
MUL R0.xyz, R0, vertex.attrib[14].w;
DP3 result.texcoord[2].y, R2, R0;
DP3 result.texcoord[2].z, vertex.normal, R2;
DP3 result.texcoord[2].x, R2, vertex.attrib[14];
MAD result.texcoord[0].zw, vertex.texcoord[0].xyxy, c[11].xyxy, c[11];
MAD result.texcoord[0].xy, vertex.texcoord[0], c[10], c[10].zwzw;
MAD result.texcoord[1].zw, vertex.texcoord[0].xyxy, c[13].xyxy, c[13];
MAD result.texcoord[1].xy, vertex.texcoord[0], c[12], c[12].zwzw;
DP4 result.position.w, vertex.position, c[4];
DP4 result.position.z, vertex.position, c[3];
DP4 result.position.y, vertex.position, c[2];
DP4 result.position.x, vertex.position, c[1];
END
# 19 instructions, 3 R-regs
"
}

SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" }
Bind "vertex" Vertex
Bind "tangent" TexCoord2
Bind "normal" Normal
Bind "texcoord" TexCoord0
Matrix 0 [glstate_matrix_mvp]
Vector 8 [_WorldSpaceLightPos0]
Matrix 4 [_World2Object]
Vector 9 [_Control_ST]
Vector 10 [_Splat0_ST]
Vector 11 [_Splat1_ST]
Vector 12 [_Splat2_ST]
"vs_2_0
; 22 ALU
dcl_position0 v0
dcl_tangent0 v1
dcl_normal0 v2
dcl_texcoord0 v3
mov r0.xyz, v1
mul r1.xyz, v2.zxyw, r0.yzxw
mov r0.xyz, v1
mad r1.xyz, v2.yzxw, r0.zxyw, -r1
mov r0, c6
dp4 r3.z, c8, r0
mul r2.xyz, r1, v1.w
mov r0, c5
mov r1, c4
dp4 r3.y, c8, r0
dp4 r3.x, c8, r1
dp3 oT2.y, r3, r2
dp3 oT2.z, v2, r3
dp3 oT2.x, r3, v1
mad oT0.zw, v3.xyxy, c10.xyxy, c10
mad oT0.xy, v3, c9, c9.zwzw
mad oT1.zw, v3.xyxy, c12.xyxy, c12
mad oT1.xy, v3, c11, c11.zwzw
dp4 oPos.w, v0, c3
dp4 oPos.z, v0, c2
dp4 oPos.y, v0, c1
dp4 oPos.x, v0, c0
"
}

SubProgram "gles " {
Keywords { "DIRECTIONAL" }
"!!GLES
#define SHADER_API_GLES 1
#define tex2D texture2D


#ifdef VERTEX
#define gl_ModelViewProjectionMatrix glstate_matrix_mvp
uniform mat4 glstate_matrix_mvp;

varying mediump vec3 xlv_TEXCOORD2;
varying highp vec4 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;

uniform lowp vec4 _WorldSpaceLightPos0;
uniform highp mat4 _World2Object;
uniform highp vec4 _Splat2_ST;
uniform highp vec4 _Splat1_ST;
uniform highp vec4 _Splat0_ST;
uniform highp vec4 _Control_ST;
attribute vec4 _glesTANGENT;
attribute vec4 _glesMultiTexCoord0;
attribute vec3 _glesNormal;
attribute vec4 _glesVertex;
void main ()
{
  vec4 tmpvar_1;
  tmpvar_1.xyz = normalize (_glesTANGENT.xyz);
  tmpvar_1.w = _glesTANGENT.w;
  vec3 tmpvar_2;
  tmpvar_2 = normalize (_glesNormal);
  highp vec4 tmpvar_3;
  highp vec4 tmpvar_4;
  mediump vec3 tmpvar_5;
  tmpvar_3.xy = ((_glesMultiTexCoord0.xy * _Control_ST.xy) + _Control_ST.zw);
  tmpvar_3.zw = ((_glesMultiTexCoord0.xy * _Splat0_ST.xy) + _Splat0_ST.zw);
  tmpvar_4.xy = ((_glesMultiTexCoord0.xy * _Splat1_ST.xy) + _Splat1_ST.zw);
  tmpvar_4.zw = ((_glesMultiTexCoord0.xy * _Splat2_ST.xy) + _Splat2_ST.zw);
  highp mat3 tmpvar_6;
  tmpvar_6[0] = tmpvar_1.xyz;
  tmpvar_6[1] = (cross (tmpvar_2, tmpvar_1.xyz) * _glesTANGENT.w);
  tmpvar_6[2] = tmpvar_2;
  mat3 tmpvar_7;
  tmpvar_7[0].x = tmpvar_6[0].x;
  tmpvar_7[0].y = tmpvar_6[1].x;
  tmpvar_7[0].z = tmpvar_6[2].x;
  tmpvar_7[1].x = tmpvar_6[0].y;
  tmpvar_7[1].y = tmpvar_6[1].y;
  tmpvar_7[1].z = tmpvar_6[2].y;
  tmpvar_7[2].x = tmpvar_6[0].z;
  tmpvar_7[2].y = tmpvar_6[1].z;
  tmpvar_7[2].z = tmpvar_6[2].z;
  highp vec3 tmpvar_8;
  tmpvar_8 = (tmpvar_7 * (_World2Object * _WorldSpaceLightPos0).xyz);
  tmpvar_5 = tmpvar_8;
  gl_Position = (gl_ModelViewProjectionMatrix * _glesVertex);
  xlv_TEXCOORD0 = tmpvar_3;
  xlv_TEXCOORD1 = tmpvar_4;
  xlv_TEXCOORD2 = tmpvar_5;
}



#endif
#ifdef FRAGMENT

varying mediump vec3 xlv_TEXCOORD2;
varying highp vec4 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;
uniform sampler2D _Splat2;
uniform sampler2D _Splat1;
uniform sampler2D _Splat0;
uniform lowp vec4 _LightColor0;
uniform sampler2D _Control;
uniform sampler2D _BumpSplat2;
uniform sampler2D _BumpSplat1;
uniform sampler2D _BumpSplat0;
void main ()
{
  lowp vec4 c;
  lowp vec3 lightDir;
  highp vec2 tmpvar_1;
  tmpvar_1 = xlv_TEXCOORD0.xy;
  highp vec2 tmpvar_2;
  tmpvar_2 = xlv_TEXCOORD0.zw;
  highp vec2 tmpvar_3;
  tmpvar_3 = xlv_TEXCOORD1.xy;
  highp vec2 tmpvar_4;
  tmpvar_4 = xlv_TEXCOORD1.zw;
  lowp vec4 tmpvar_5;
  tmpvar_5 = texture2D (_Control, tmpvar_1);
  lightDir = xlv_TEXCOORD2;
  lowp vec4 c_i0;
  c_i0.xyz = (((((texture2D (_Splat0, tmpvar_2).xyz * tmpvar_5.x) + (texture2D (_Splat1, tmpvar_3).xyz * tmpvar_5.y)) + (texture2D (_Splat2, tmpvar_4).xyz * tmpvar_5.z)) * _LightColor0.xyz) * (max (0.0, dot ((((((texture2D (_BumpSplat0, tmpvar_2).xyz * 2.0) - 1.0) * tmpvar_5.x) + (((texture2D (_BumpSplat1, tmpvar_3).xyz * 2.0) - 1.0) * tmpvar_5.y)) + (((texture2D (_BumpSplat2, tmpvar_4).xyz * 2.0) - 1.0) * tmpvar_5.z)), lightDir)) * 2.0));
  c_i0.w = 0.0;
  c = c_i0;
  c.w = 0.0;
  gl_FragData[0] = c;
}



#endif"
}

SubProgram "glesdesktop " {
Keywords { "DIRECTIONAL" }
"!!GLES
#define SHADER_API_GLES 1
#define tex2D texture2D


#ifdef VERTEX
#define gl_ModelViewProjectionMatrix glstate_matrix_mvp
uniform mat4 glstate_matrix_mvp;

varying mediump vec3 xlv_TEXCOORD2;
varying highp vec4 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;

uniform lowp vec4 _WorldSpaceLightPos0;
uniform highp mat4 _World2Object;
uniform highp vec4 _Splat2_ST;
uniform highp vec4 _Splat1_ST;
uniform highp vec4 _Splat0_ST;
uniform highp vec4 _Control_ST;
attribute vec4 _glesTANGENT;
attribute vec4 _glesMultiTexCoord0;
attribute vec3 _glesNormal;
attribute vec4 _glesVertex;
void main ()
{
  vec4 tmpvar_1;
  tmpvar_1.xyz = normalize (_glesTANGENT.xyz);
  tmpvar_1.w = _glesTANGENT.w;
  vec3 tmpvar_2;
  tmpvar_2 = normalize (_glesNormal);
  highp vec4 tmpvar_3;
  highp vec4 tmpvar_4;
  mediump vec3 tmpvar_5;
  tmpvar_3.xy = ((_glesMultiTexCoord0.xy * _Control_ST.xy) + _Control_ST.zw);
  tmpvar_3.zw = ((_glesMultiTexCoord0.xy * _Splat0_ST.xy) + _Splat0_ST.zw);
  tmpvar_4.xy = ((_glesMultiTexCoord0.xy * _Splat1_ST.xy) + _Splat1_ST.zw);
  tmpvar_4.zw = ((_glesMultiTexCoord0.xy * _Splat2_ST.xy) + _Splat2_ST.zw);
  highp mat3 tmpvar_6;
  tmpvar_6[0] = tmpvar_1.xyz;
  tmpvar_6[1] = (cross (tmpvar_2, tmpvar_1.xyz) * _glesTANGENT.w);
  tmpvar_6[2] = tmpvar_2;
  mat3 tmpvar_7;
  tmpvar_7[0].x = tmpvar_6[0].x;
  tmpvar_7[0].y = tmpvar_6[1].x;
  tmpvar_7[0].z = tmpvar_6[2].x;
  tmpvar_7[1].x = tmpvar_6[0].y;
  tmpvar_7[1].y = tmpvar_6[1].y;
  tmpvar_7[1].z = tmpvar_6[2].y;
  tmpvar_7[2].x = tmpvar_6[0].z;
  tmpvar_7[2].y = tmpvar_6[1].z;
  tmpvar_7[2].z = tmpvar_6[2].z;
  highp vec3 tmpvar_8;
  tmpvar_8 = (tmpvar_7 * (_World2Object * _WorldSpaceLightPos0).xyz);
  tmpvar_5 = tmpvar_8;
  gl_Position = (gl_ModelViewProjectionMatrix * _glesVertex);
  xlv_TEXCOORD0 = tmpvar_3;
  xlv_TEXCOORD1 = tmpvar_4;
  xlv_TEXCOORD2 = tmpvar_5;
}



#endif
#ifdef FRAGMENT

varying mediump vec3 xlv_TEXCOORD2;
varying highp vec4 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;
uniform sampler2D _Splat2;
uniform sampler2D _Splat1;
uniform sampler2D _Splat0;
uniform lowp vec4 _LightColor0;
uniform sampler2D _Control;
uniform sampler2D _BumpSplat2;
uniform sampler2D _BumpSplat1;
uniform sampler2D _BumpSplat0;
void main ()
{
  lowp vec4 c;
  lowp vec3 lightDir;
  highp vec2 tmpvar_1;
  tmpvar_1 = xlv_TEXCOORD0.xy;
  highp vec2 tmpvar_2;
  tmpvar_2 = xlv_TEXCOORD0.zw;
  highp vec2 tmpvar_3;
  tmpvar_3 = xlv_TEXCOORD1.xy;
  highp vec2 tmpvar_4;
  tmpvar_4 = xlv_TEXCOORD1.zw;
  lowp vec4 tmpvar_5;
  tmpvar_5 = texture2D (_Control, tmpvar_1);
  lowp vec3 normal;
  normal.xy = ((texture2D (_BumpSplat0, tmpvar_2).wy * 2.0) - 1.0);
  normal.z = sqrt (((1.0 - (normal.x * normal.x)) - (normal.y * normal.y)));
  lowp vec3 normal_i0;
  normal_i0.xy = ((texture2D (_BumpSplat1, tmpvar_3).wy * 2.0) - 1.0);
  normal_i0.z = sqrt (((1.0 - (normal_i0.x * normal_i0.x)) - (normal_i0.y * normal_i0.y)));
  lowp vec3 normal_i0_i1;
  normal_i0_i1.xy = ((texture2D (_BumpSplat2, tmpvar_4).wy * 2.0) - 1.0);
  normal_i0_i1.z = sqrt (((1.0 - (normal_i0_i1.x * normal_i0_i1.x)) - (normal_i0_i1.y * normal_i0_i1.y)));
  lightDir = xlv_TEXCOORD2;
  lowp vec4 c_i0;
  c_i0.xyz = (((((texture2D (_Splat0, tmpvar_2).xyz * tmpvar_5.x) + (texture2D (_Splat1, tmpvar_3).xyz * tmpvar_5.y)) + (texture2D (_Splat2, tmpvar_4).xyz * tmpvar_5.z)) * _LightColor0.xyz) * (max (0.0, dot ((((normal * tmpvar_5.x) + (normal_i0 * tmpvar_5.y)) + (normal_i0_i1 * tmpvar_5.z)), lightDir)) * 2.0));
  c_i0.w = 0.0;
  c = c_i0;
  c.w = 0.0;
  gl_FragData[0] = c;
}



#endif"
}

SubProgram "opengl " {
Keywords { "SPOT" }
Bind "vertex" Vertex
Bind "tangent" ATTR14
Bind "normal" Normal
Bind "texcoord" TexCoord0
Vector 17 [unity_Scale]
Vector 18 [_WorldSpaceLightPos0]
Matrix 5 [_Object2World]
Matrix 9 [_World2Object]
Matrix 13 [_LightMatrix0]
Vector 19 [_Control_ST]
Vector 20 [_Splat0_ST]
Vector 21 [_Splat1_ST]
Vector 22 [_Splat2_ST]
"!!ARBvp1.0
# 28 ALU
PARAM c[23] = { program.local[0],
		state.matrix.mvp,
		program.local[5..22] };
TEMP R0;
TEMP R1;
TEMP R2;
MOV R0.xyz, vertex.attrib[14];
MUL R1.xyz, vertex.normal.zxyw, R0.yzxw;
MAD R1.xyz, vertex.normal.yzxw, R0.zxyw, -R1;
MOV R0, c[18];
DP4 R2.z, R0, c[11];
DP4 R2.x, R0, c[9];
DP4 R2.y, R0, c[10];
MAD R0.xyz, R2, c[17].w, -vertex.position;
MUL R1.xyz, R1, vertex.attrib[14].w;
DP4 R0.w, vertex.position, c[8];
DP3 result.texcoord[2].y, R0, R1;
DP3 result.texcoord[2].z, vertex.normal, R0;
DP3 result.texcoord[2].x, R0, vertex.attrib[14];
DP4 R0.z, vertex.position, c[7];
DP4 R0.x, vertex.position, c[5];
DP4 R0.y, vertex.position, c[6];
DP4 result.texcoord[3].w, R0, c[16];
DP4 result.texcoord[3].z, R0, c[15];
DP4 result.texcoord[3].y, R0, c[14];
DP4 result.texcoord[3].x, R0, c[13];
MAD result.texcoord[0].zw, vertex.texcoord[0].xyxy, c[20].xyxy, c[20];
MAD result.texcoord[0].xy, vertex.texcoord[0], c[19], c[19].zwzw;
MAD result.texcoord[1].zw, vertex.texcoord[0].xyxy, c[22].xyxy, c[22];
MAD result.texcoord[1].xy, vertex.texcoord[0], c[21], c[21].zwzw;
DP4 result.position.w, vertex.position, c[4];
DP4 result.position.z, vertex.position, c[3];
DP4 result.position.y, vertex.position, c[2];
DP4 result.position.x, vertex.position, c[1];
END
# 28 instructions, 3 R-regs
"
}

SubProgram "d3d9 " {
Keywords { "SPOT" }
Bind "vertex" Vertex
Bind "tangent" TexCoord2
Bind "normal" Normal
Bind "texcoord" TexCoord0
Matrix 0 [glstate_matrix_mvp]
Vector 16 [unity_Scale]
Vector 17 [_WorldSpaceLightPos0]
Matrix 4 [_Object2World]
Matrix 8 [_World2Object]
Matrix 12 [_LightMatrix0]
Vector 18 [_Control_ST]
Vector 19 [_Splat0_ST]
Vector 20 [_Splat1_ST]
Vector 21 [_Splat2_ST]
"vs_2_0
; 31 ALU
dcl_position0 v0
dcl_tangent0 v1
dcl_normal0 v2
dcl_texcoord0 v3
mov r0.xyz, v1
mul r1.xyz, v2.zxyw, r0.yzxw
mov r0.xyz, v1
mad r1.xyz, v2.yzxw, r0.zxyw, -r1
mov r0, c10
mul r2.xyz, r1, v1.w
dp4 r3.z, c17, r0
mov r0, c9
dp4 r3.y, c17, r0
mov r1, c8
dp4 r3.x, c17, r1
mad r0.xyz, r3, c16.w, -v0
dp4 r0.w, v0, c7
dp3 oT2.y, r0, r2
dp3 oT2.z, v2, r0
dp3 oT2.x, r0, v1
dp4 r0.z, v0, c6
dp4 r0.x, v0, c4
dp4 r0.y, v0, c5
dp4 oT3.w, r0, c15
dp4 oT3.z, r0, c14
dp4 oT3.y, r0, c13
dp4 oT3.x, r0, c12
mad oT0.zw, v3.xyxy, c19.xyxy, c19
mad oT0.xy, v3, c18, c18.zwzw
mad oT1.zw, v3.xyxy, c21.xyxy, c21
mad oT1.xy, v3, c20, c20.zwzw
dp4 oPos.w, v0, c3
dp4 oPos.z, v0, c2
dp4 oPos.y, v0, c1
dp4 oPos.x, v0, c0
"
}

SubProgram "gles " {
Keywords { "SPOT" }
"!!GLES
#define SHADER_API_GLES 1
#define tex2D texture2D


#ifdef VERTEX
#define gl_ModelViewProjectionMatrix glstate_matrix_mvp
uniform mat4 glstate_matrix_mvp;

varying highp vec4 xlv_TEXCOORD3;
varying mediump vec3 xlv_TEXCOORD2;
varying highp vec4 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;
uniform highp vec4 unity_Scale;

uniform highp vec4 _WorldSpaceLightPos0;
uniform highp mat4 _World2Object;
uniform highp vec4 _Splat2_ST;
uniform highp vec4 _Splat1_ST;
uniform highp vec4 _Splat0_ST;
uniform highp mat4 _Object2World;
uniform highp mat4 _LightMatrix0;
uniform highp vec4 _Control_ST;
attribute vec4 _glesTANGENT;
attribute vec4 _glesMultiTexCoord0;
attribute vec3 _glesNormal;
attribute vec4 _glesVertex;
void main ()
{
  vec4 tmpvar_1;
  tmpvar_1.xyz = normalize (_glesTANGENT.xyz);
  tmpvar_1.w = _glesTANGENT.w;
  vec3 tmpvar_2;
  tmpvar_2 = normalize (_glesNormal);
  highp vec4 tmpvar_3;
  highp vec4 tmpvar_4;
  mediump vec3 tmpvar_5;
  tmpvar_3.xy = ((_glesMultiTexCoord0.xy * _Control_ST.xy) + _Control_ST.zw);
  tmpvar_3.zw = ((_glesMultiTexCoord0.xy * _Splat0_ST.xy) + _Splat0_ST.zw);
  tmpvar_4.xy = ((_glesMultiTexCoord0.xy * _Splat1_ST.xy) + _Splat1_ST.zw);
  tmpvar_4.zw = ((_glesMultiTexCoord0.xy * _Splat2_ST.xy) + _Splat2_ST.zw);
  highp mat3 tmpvar_6;
  tmpvar_6[0] = tmpvar_1.xyz;
  tmpvar_6[1] = (cross (tmpvar_2, tmpvar_1.xyz) * _glesTANGENT.w);
  tmpvar_6[2] = tmpvar_2;
  mat3 tmpvar_7;
  tmpvar_7[0].x = tmpvar_6[0].x;
  tmpvar_7[0].y = tmpvar_6[1].x;
  tmpvar_7[0].z = tmpvar_6[2].x;
  tmpvar_7[1].x = tmpvar_6[0].y;
  tmpvar_7[1].y = tmpvar_6[1].y;
  tmpvar_7[1].z = tmpvar_6[2].y;
  tmpvar_7[2].x = tmpvar_6[0].z;
  tmpvar_7[2].y = tmpvar_6[1].z;
  tmpvar_7[2].z = tmpvar_6[2].z;
  highp vec3 tmpvar_8;
  tmpvar_8 = (tmpvar_7 * (((_World2Object * _WorldSpaceLightPos0).xyz * unity_Scale.w) - _glesVertex.xyz));
  tmpvar_5 = tmpvar_8;
  gl_Position = (gl_ModelViewProjectionMatrix * _glesVertex);
  xlv_TEXCOORD0 = tmpvar_3;
  xlv_TEXCOORD1 = tmpvar_4;
  xlv_TEXCOORD2 = tmpvar_5;
  xlv_TEXCOORD3 = (_LightMatrix0 * (_Object2World * _glesVertex));
}



#endif
#ifdef FRAGMENT

varying highp vec4 xlv_TEXCOORD3;
varying mediump vec3 xlv_TEXCOORD2;
varying highp vec4 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;
uniform sampler2D _Splat2;
uniform sampler2D _Splat1;
uniform sampler2D _Splat0;
uniform sampler2D _LightTextureB0;
uniform sampler2D _LightTexture0;
uniform lowp vec4 _LightColor0;
uniform sampler2D _Control;
uniform sampler2D _BumpSplat2;
uniform sampler2D _BumpSplat1;
uniform sampler2D _BumpSplat0;
void main ()
{
  lowp vec4 c;
  lowp vec3 lightDir;
  highp vec2 tmpvar_1;
  tmpvar_1 = xlv_TEXCOORD0.xy;
  highp vec2 tmpvar_2;
  tmpvar_2 = xlv_TEXCOORD0.zw;
  highp vec2 tmpvar_3;
  tmpvar_3 = xlv_TEXCOORD1.xy;
  highp vec2 tmpvar_4;
  tmpvar_4 = xlv_TEXCOORD1.zw;
  lowp vec4 tmpvar_5;
  tmpvar_5 = texture2D (_Control, tmpvar_1);
  mediump vec3 tmpvar_6;
  tmpvar_6 = normalize (xlv_TEXCOORD2);
  lightDir = tmpvar_6;
  highp vec3 LightCoord_i0;
  LightCoord_i0 = xlv_TEXCOORD3.xyz;
  highp vec2 tmpvar_7;
  tmpvar_7 = vec2(dot (LightCoord_i0, LightCoord_i0));
  lowp float atten;
  atten = ((float((xlv_TEXCOORD3.z > 0.0)) * texture2D (_LightTexture0, ((xlv_TEXCOORD3.xy / xlv_TEXCOORD3.w) + 0.5)).w) * texture2D (_LightTextureB0, tmpvar_7).w);
  lowp vec4 c_i0;
  c_i0.xyz = (((((texture2D (_Splat0, tmpvar_2).xyz * tmpvar_5.x) + (texture2D (_Splat1, tmpvar_3).xyz * tmpvar_5.y)) + (texture2D (_Splat2, tmpvar_4).xyz * tmpvar_5.z)) * _LightColor0.xyz) * ((max (0.0, dot ((((((texture2D (_BumpSplat0, tmpvar_2).xyz * 2.0) - 1.0) * tmpvar_5.x) + (((texture2D (_BumpSplat1, tmpvar_3).xyz * 2.0) - 1.0) * tmpvar_5.y)) + (((texture2D (_BumpSplat2, tmpvar_4).xyz * 2.0) - 1.0) * tmpvar_5.z)), lightDir)) * atten) * 2.0));
  c_i0.w = 0.0;
  c = c_i0;
  c.w = 0.0;
  gl_FragData[0] = c;
}



#endif"
}

SubProgram "glesdesktop " {
Keywords { "SPOT" }
"!!GLES
#define SHADER_API_GLES 1
#define tex2D texture2D


#ifdef VERTEX
#define gl_ModelViewProjectionMatrix glstate_matrix_mvp
uniform mat4 glstate_matrix_mvp;

varying highp vec4 xlv_TEXCOORD3;
varying mediump vec3 xlv_TEXCOORD2;
varying highp vec4 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;
uniform highp vec4 unity_Scale;

uniform highp vec4 _WorldSpaceLightPos0;
uniform highp mat4 _World2Object;
uniform highp vec4 _Splat2_ST;
uniform highp vec4 _Splat1_ST;
uniform highp vec4 _Splat0_ST;
uniform highp mat4 _Object2World;
uniform highp mat4 _LightMatrix0;
uniform highp vec4 _Control_ST;
attribute vec4 _glesTANGENT;
attribute vec4 _glesMultiTexCoord0;
attribute vec3 _glesNormal;
attribute vec4 _glesVertex;
void main ()
{
  vec4 tmpvar_1;
  tmpvar_1.xyz = normalize (_glesTANGENT.xyz);
  tmpvar_1.w = _glesTANGENT.w;
  vec3 tmpvar_2;
  tmpvar_2 = normalize (_glesNormal);
  highp vec4 tmpvar_3;
  highp vec4 tmpvar_4;
  mediump vec3 tmpvar_5;
  tmpvar_3.xy = ((_glesMultiTexCoord0.xy * _Control_ST.xy) + _Control_ST.zw);
  tmpvar_3.zw = ((_glesMultiTexCoord0.xy * _Splat0_ST.xy) + _Splat0_ST.zw);
  tmpvar_4.xy = ((_glesMultiTexCoord0.xy * _Splat1_ST.xy) + _Splat1_ST.zw);
  tmpvar_4.zw = ((_glesMultiTexCoord0.xy * _Splat2_ST.xy) + _Splat2_ST.zw);
  highp mat3 tmpvar_6;
  tmpvar_6[0] = tmpvar_1.xyz;
  tmpvar_6[1] = (cross (tmpvar_2, tmpvar_1.xyz) * _glesTANGENT.w);
  tmpvar_6[2] = tmpvar_2;
  mat3 tmpvar_7;
  tmpvar_7[0].x = tmpvar_6[0].x;
  tmpvar_7[0].y = tmpvar_6[1].x;
  tmpvar_7[0].z = tmpvar_6[2].x;
  tmpvar_7[1].x = tmpvar_6[0].y;
  tmpvar_7[1].y = tmpvar_6[1].y;
  tmpvar_7[1].z = tmpvar_6[2].y;
  tmpvar_7[2].x = tmpvar_6[0].z;
  tmpvar_7[2].y = tmpvar_6[1].z;
  tmpvar_7[2].z = tmpvar_6[2].z;
  highp vec3 tmpvar_8;
  tmpvar_8 = (tmpvar_7 * (((_World2Object * _WorldSpaceLightPos0).xyz * unity_Scale.w) - _glesVertex.xyz));
  tmpvar_5 = tmpvar_8;
  gl_Position = (gl_ModelViewProjectionMatrix * _glesVertex);
  xlv_TEXCOORD0 = tmpvar_3;
  xlv_TEXCOORD1 = tmpvar_4;
  xlv_TEXCOORD2 = tmpvar_5;
  xlv_TEXCOORD3 = (_LightMatrix0 * (_Object2World * _glesVertex));
}



#endif
#ifdef FRAGMENT

varying highp vec4 xlv_TEXCOORD3;
varying mediump vec3 xlv_TEXCOORD2;
varying highp vec4 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;
uniform sampler2D _Splat2;
uniform sampler2D _Splat1;
uniform sampler2D _Splat0;
uniform sampler2D _LightTextureB0;
uniform sampler2D _LightTexture0;
uniform lowp vec4 _LightColor0;
uniform sampler2D _Control;
uniform sampler2D _BumpSplat2;
uniform sampler2D _BumpSplat1;
uniform sampler2D _BumpSplat0;
void main ()
{
  lowp vec4 c;
  lowp vec3 lightDir;
  highp vec2 tmpvar_1;
  tmpvar_1 = xlv_TEXCOORD0.xy;
  highp vec2 tmpvar_2;
  tmpvar_2 = xlv_TEXCOORD0.zw;
  highp vec2 tmpvar_3;
  tmpvar_3 = xlv_TEXCOORD1.xy;
  highp vec2 tmpvar_4;
  tmpvar_4 = xlv_TEXCOORD1.zw;
  lowp vec4 tmpvar_5;
  tmpvar_5 = texture2D (_Control, tmpvar_1);
  lowp vec3 normal;
  normal.xy = ((texture2D (_BumpSplat0, tmpvar_2).wy * 2.0) - 1.0);
  normal.z = sqrt (((1.0 - (normal.x * normal.x)) - (normal.y * normal.y)));
  lowp vec3 normal_i0;
  normal_i0.xy = ((texture2D (_BumpSplat1, tmpvar_3).wy * 2.0) - 1.0);
  normal_i0.z = sqrt (((1.0 - (normal_i0.x * normal_i0.x)) - (normal_i0.y * normal_i0.y)));
  lowp vec3 normal_i0_i1;
  normal_i0_i1.xy = ((texture2D (_BumpSplat2, tmpvar_4).wy * 2.0) - 1.0);
  normal_i0_i1.z = sqrt (((1.0 - (normal_i0_i1.x * normal_i0_i1.x)) - (normal_i0_i1.y * normal_i0_i1.y)));
  mediump vec3 tmpvar_6;
  tmpvar_6 = normalize (xlv_TEXCOORD2);
  lightDir = tmpvar_6;
  highp vec3 LightCoord_i0;
  LightCoord_i0 = xlv_TEXCOORD3.xyz;
  highp vec2 tmpvar_7;
  tmpvar_7 = vec2(dot (LightCoord_i0, LightCoord_i0));
  lowp float atten;
  atten = ((float((xlv_TEXCOORD3.z > 0.0)) * texture2D (_LightTexture0, ((xlv_TEXCOORD3.xy / xlv_TEXCOORD3.w) + 0.5)).w) * texture2D (_LightTextureB0, tmpvar_7).w);
  lowp vec4 c_i0;
  c_i0.xyz = (((((texture2D (_Splat0, tmpvar_2).xyz * tmpvar_5.x) + (texture2D (_Splat1, tmpvar_3).xyz * tmpvar_5.y)) + (texture2D (_Splat2, tmpvar_4).xyz * tmpvar_5.z)) * _LightColor0.xyz) * ((max (0.0, dot ((((normal * tmpvar_5.x) + (normal_i0 * tmpvar_5.y)) + (normal_i0_i1 * tmpvar_5.z)), lightDir)) * atten) * 2.0));
  c_i0.w = 0.0;
  c = c_i0;
  c.w = 0.0;
  gl_FragData[0] = c;
}



#endif"
}

SubProgram "opengl " {
Keywords { "POINT_COOKIE" }
Bind "vertex" Vertex
Bind "tangent" ATTR14
Bind "normal" Normal
Bind "texcoord" TexCoord0
Vector 17 [unity_Scale]
Vector 18 [_WorldSpaceLightPos0]
Matrix 5 [_Object2World]
Matrix 9 [_World2Object]
Matrix 13 [_LightMatrix0]
Vector 19 [_Control_ST]
Vector 20 [_Splat0_ST]
Vector 21 [_Splat1_ST]
Vector 22 [_Splat2_ST]
"!!ARBvp1.0
# 27 ALU
PARAM c[23] = { program.local[0],
		state.matrix.mvp,
		program.local[5..22] };
TEMP R0;
TEMP R1;
TEMP R2;
MOV R0.xyz, vertex.attrib[14];
MUL R1.xyz, vertex.normal.zxyw, R0.yzxw;
MAD R1.xyz, vertex.normal.yzxw, R0.zxyw, -R1;
MOV R0, c[18];
DP4 R2.z, R0, c[11];
DP4 R2.x, R0, c[9];
DP4 R2.y, R0, c[10];
MAD R0.xyz, R2, c[17].w, -vertex.position;
MUL R1.xyz, R1, vertex.attrib[14].w;
DP3 result.texcoord[2].y, R0, R1;
DP3 result.texcoord[2].z, vertex.normal, R0;
DP3 result.texcoord[2].x, R0, vertex.attrib[14];
DP4 R0.w, vertex.position, c[8];
DP4 R0.z, vertex.position, c[7];
DP4 R0.x, vertex.position, c[5];
DP4 R0.y, vertex.position, c[6];
DP4 result.texcoord[3].z, R0, c[15];
DP4 result.texcoord[3].y, R0, c[14];
DP4 result.texcoord[3].x, R0, c[13];
MAD result.texcoord[0].zw, vertex.texcoord[0].xyxy, c[20].xyxy, c[20];
MAD result.texcoord[0].xy, vertex.texcoord[0], c[19], c[19].zwzw;
MAD result.texcoord[1].zw, vertex.texcoord[0].xyxy, c[22].xyxy, c[22];
MAD result.texcoord[1].xy, vertex.texcoord[0], c[21], c[21].zwzw;
DP4 result.position.w, vertex.position, c[4];
DP4 result.position.z, vertex.position, c[3];
DP4 result.position.y, vertex.position, c[2];
DP4 result.position.x, vertex.position, c[1];
END
# 27 instructions, 3 R-regs
"
}

SubProgram "d3d9 " {
Keywords { "POINT_COOKIE" }
Bind "vertex" Vertex
Bind "tangent" TexCoord2
Bind "normal" Normal
Bind "texcoord" TexCoord0
Matrix 0 [glstate_matrix_mvp]
Vector 16 [unity_Scale]
Vector 17 [_WorldSpaceLightPos0]
Matrix 4 [_Object2World]
Matrix 8 [_World2Object]
Matrix 12 [_LightMatrix0]
Vector 18 [_Control_ST]
Vector 19 [_Splat0_ST]
Vector 20 [_Splat1_ST]
Vector 21 [_Splat2_ST]
"vs_2_0
; 30 ALU
dcl_position0 v0
dcl_tangent0 v1
dcl_normal0 v2
dcl_texcoord0 v3
mov r0.xyz, v1
mul r1.xyz, v2.zxyw, r0.yzxw
mov r0.xyz, v1
mad r1.xyz, v2.yzxw, r0.zxyw, -r1
mov r0, c10
mul r2.xyz, r1, v1.w
dp4 r3.z, c17, r0
mov r0, c9
dp4 r3.y, c17, r0
mov r1, c8
dp4 r3.x, c17, r1
mad r0.xyz, r3, c16.w, -v0
dp3 oT2.y, r0, r2
dp3 oT2.z, v2, r0
dp3 oT2.x, r0, v1
dp4 r0.w, v0, c7
dp4 r0.z, v0, c6
dp4 r0.x, v0, c4
dp4 r0.y, v0, c5
dp4 oT3.z, r0, c14
dp4 oT3.y, r0, c13
dp4 oT3.x, r0, c12
mad oT0.zw, v3.xyxy, c19.xyxy, c19
mad oT0.xy, v3, c18, c18.zwzw
mad oT1.zw, v3.xyxy, c21.xyxy, c21
mad oT1.xy, v3, c20, c20.zwzw
dp4 oPos.w, v0, c3
dp4 oPos.z, v0, c2
dp4 oPos.y, v0, c1
dp4 oPos.x, v0, c0
"
}

SubProgram "gles " {
Keywords { "POINT_COOKIE" }
"!!GLES
#define SHADER_API_GLES 1
#define tex2D texture2D


#ifdef VERTEX
#define gl_ModelViewProjectionMatrix glstate_matrix_mvp
uniform mat4 glstate_matrix_mvp;

varying highp vec3 xlv_TEXCOORD3;
varying mediump vec3 xlv_TEXCOORD2;
varying highp vec4 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;
uniform highp vec4 unity_Scale;

uniform highp vec4 _WorldSpaceLightPos0;
uniform highp mat4 _World2Object;
uniform highp vec4 _Splat2_ST;
uniform highp vec4 _Splat1_ST;
uniform highp vec4 _Splat0_ST;
uniform highp mat4 _Object2World;
uniform highp mat4 _LightMatrix0;
uniform highp vec4 _Control_ST;
attribute vec4 _glesTANGENT;
attribute vec4 _glesMultiTexCoord0;
attribute vec3 _glesNormal;
attribute vec4 _glesVertex;
void main ()
{
  vec4 tmpvar_1;
  tmpvar_1.xyz = normalize (_glesTANGENT.xyz);
  tmpvar_1.w = _glesTANGENT.w;
  vec3 tmpvar_2;
  tmpvar_2 = normalize (_glesNormal);
  highp vec4 tmpvar_3;
  highp vec4 tmpvar_4;
  mediump vec3 tmpvar_5;
  tmpvar_3.xy = ((_glesMultiTexCoord0.xy * _Control_ST.xy) + _Control_ST.zw);
  tmpvar_3.zw = ((_glesMultiTexCoord0.xy * _Splat0_ST.xy) + _Splat0_ST.zw);
  tmpvar_4.xy = ((_glesMultiTexCoord0.xy * _Splat1_ST.xy) + _Splat1_ST.zw);
  tmpvar_4.zw = ((_glesMultiTexCoord0.xy * _Splat2_ST.xy) + _Splat2_ST.zw);
  highp mat3 tmpvar_6;
  tmpvar_6[0] = tmpvar_1.xyz;
  tmpvar_6[1] = (cross (tmpvar_2, tmpvar_1.xyz) * _glesTANGENT.w);
  tmpvar_6[2] = tmpvar_2;
  mat3 tmpvar_7;
  tmpvar_7[0].x = tmpvar_6[0].x;
  tmpvar_7[0].y = tmpvar_6[1].x;
  tmpvar_7[0].z = tmpvar_6[2].x;
  tmpvar_7[1].x = tmpvar_6[0].y;
  tmpvar_7[1].y = tmpvar_6[1].y;
  tmpvar_7[1].z = tmpvar_6[2].y;
  tmpvar_7[2].x = tmpvar_6[0].z;
  tmpvar_7[2].y = tmpvar_6[1].z;
  tmpvar_7[2].z = tmpvar_6[2].z;
  highp vec3 tmpvar_8;
  tmpvar_8 = (tmpvar_7 * (((_World2Object * _WorldSpaceLightPos0).xyz * unity_Scale.w) - _glesVertex.xyz));
  tmpvar_5 = tmpvar_8;
  gl_Position = (gl_ModelViewProjectionMatrix * _glesVertex);
  xlv_TEXCOORD0 = tmpvar_3;
  xlv_TEXCOORD1 = tmpvar_4;
  xlv_TEXCOORD2 = tmpvar_5;
  xlv_TEXCOORD3 = (_LightMatrix0 * (_Object2World * _glesVertex)).xyz;
}



#endif
#ifdef FRAGMENT

varying highp vec3 xlv_TEXCOORD3;
varying mediump vec3 xlv_TEXCOORD2;
varying highp vec4 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;
uniform sampler2D _Splat2;
uniform sampler2D _Splat1;
uniform sampler2D _Splat0;
uniform sampler2D _LightTextureB0;
uniform samplerCube _LightTexture0;
uniform lowp vec4 _LightColor0;
uniform sampler2D _Control;
uniform sampler2D _BumpSplat2;
uniform sampler2D _BumpSplat1;
uniform sampler2D _BumpSplat0;
void main ()
{
  lowp vec4 c;
  lowp vec3 lightDir;
  highp vec2 tmpvar_1;
  tmpvar_1 = xlv_TEXCOORD0.xy;
  highp vec2 tmpvar_2;
  tmpvar_2 = xlv_TEXCOORD0.zw;
  highp vec2 tmpvar_3;
  tmpvar_3 = xlv_TEXCOORD1.xy;
  highp vec2 tmpvar_4;
  tmpvar_4 = xlv_TEXCOORD1.zw;
  lowp vec4 tmpvar_5;
  tmpvar_5 = texture2D (_Control, tmpvar_1);
  mediump vec3 tmpvar_6;
  tmpvar_6 = normalize (xlv_TEXCOORD2);
  lightDir = tmpvar_6;
  highp vec2 tmpvar_7;
  tmpvar_7 = vec2(dot (xlv_TEXCOORD3, xlv_TEXCOORD3));
  lowp vec4 c_i0;
  c_i0.xyz = (((((texture2D (_Splat0, tmpvar_2).xyz * tmpvar_5.x) + (texture2D (_Splat1, tmpvar_3).xyz * tmpvar_5.y)) + (texture2D (_Splat2, tmpvar_4).xyz * tmpvar_5.z)) * _LightColor0.xyz) * ((max (0.0, dot ((((((texture2D (_BumpSplat0, tmpvar_2).xyz * 2.0) - 1.0) * tmpvar_5.x) + (((texture2D (_BumpSplat1, tmpvar_3).xyz * 2.0) - 1.0) * tmpvar_5.y)) + (((texture2D (_BumpSplat2, tmpvar_4).xyz * 2.0) - 1.0) * tmpvar_5.z)), lightDir)) * (texture2D (_LightTextureB0, tmpvar_7).w * textureCube (_LightTexture0, xlv_TEXCOORD3).w)) * 2.0));
  c_i0.w = 0.0;
  c = c_i0;
  c.w = 0.0;
  gl_FragData[0] = c;
}



#endif"
}

SubProgram "glesdesktop " {
Keywords { "POINT_COOKIE" }
"!!GLES
#define SHADER_API_GLES 1
#define tex2D texture2D


#ifdef VERTEX
#define gl_ModelViewProjectionMatrix glstate_matrix_mvp
uniform mat4 glstate_matrix_mvp;

varying highp vec3 xlv_TEXCOORD3;
varying mediump vec3 xlv_TEXCOORD2;
varying highp vec4 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;
uniform highp vec4 unity_Scale;

uniform highp vec4 _WorldSpaceLightPos0;
uniform highp mat4 _World2Object;
uniform highp vec4 _Splat2_ST;
uniform highp vec4 _Splat1_ST;
uniform highp vec4 _Splat0_ST;
uniform highp mat4 _Object2World;
uniform highp mat4 _LightMatrix0;
uniform highp vec4 _Control_ST;
attribute vec4 _glesTANGENT;
attribute vec4 _glesMultiTexCoord0;
attribute vec3 _glesNormal;
attribute vec4 _glesVertex;
void main ()
{
  vec4 tmpvar_1;
  tmpvar_1.xyz = normalize (_glesTANGENT.xyz);
  tmpvar_1.w = _glesTANGENT.w;
  vec3 tmpvar_2;
  tmpvar_2 = normalize (_glesNormal);
  highp vec4 tmpvar_3;
  highp vec4 tmpvar_4;
  mediump vec3 tmpvar_5;
  tmpvar_3.xy = ((_glesMultiTexCoord0.xy * _Control_ST.xy) + _Control_ST.zw);
  tmpvar_3.zw = ((_glesMultiTexCoord0.xy * _Splat0_ST.xy) + _Splat0_ST.zw);
  tmpvar_4.xy = ((_glesMultiTexCoord0.xy * _Splat1_ST.xy) + _Splat1_ST.zw);
  tmpvar_4.zw = ((_glesMultiTexCoord0.xy * _Splat2_ST.xy) + _Splat2_ST.zw);
  highp mat3 tmpvar_6;
  tmpvar_6[0] = tmpvar_1.xyz;
  tmpvar_6[1] = (cross (tmpvar_2, tmpvar_1.xyz) * _glesTANGENT.w);
  tmpvar_6[2] = tmpvar_2;
  mat3 tmpvar_7;
  tmpvar_7[0].x = tmpvar_6[0].x;
  tmpvar_7[0].y = tmpvar_6[1].x;
  tmpvar_7[0].z = tmpvar_6[2].x;
  tmpvar_7[1].x = tmpvar_6[0].y;
  tmpvar_7[1].y = tmpvar_6[1].y;
  tmpvar_7[1].z = tmpvar_6[2].y;
  tmpvar_7[2].x = tmpvar_6[0].z;
  tmpvar_7[2].y = tmpvar_6[1].z;
  tmpvar_7[2].z = tmpvar_6[2].z;
  highp vec3 tmpvar_8;
  tmpvar_8 = (tmpvar_7 * (((_World2Object * _WorldSpaceLightPos0).xyz * unity_Scale.w) - _glesVertex.xyz));
  tmpvar_5 = tmpvar_8;
  gl_Position = (gl_ModelViewProjectionMatrix * _glesVertex);
  xlv_TEXCOORD0 = tmpvar_3;
  xlv_TEXCOORD1 = tmpvar_4;
  xlv_TEXCOORD2 = tmpvar_5;
  xlv_TEXCOORD3 = (_LightMatrix0 * (_Object2World * _glesVertex)).xyz;
}



#endif
#ifdef FRAGMENT

varying highp vec3 xlv_TEXCOORD3;
varying mediump vec3 xlv_TEXCOORD2;
varying highp vec4 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;
uniform sampler2D _Splat2;
uniform sampler2D _Splat1;
uniform sampler2D _Splat0;
uniform sampler2D _LightTextureB0;
uniform samplerCube _LightTexture0;
uniform lowp vec4 _LightColor0;
uniform sampler2D _Control;
uniform sampler2D _BumpSplat2;
uniform sampler2D _BumpSplat1;
uniform sampler2D _BumpSplat0;
void main ()
{
  lowp vec4 c;
  lowp vec3 lightDir;
  highp vec2 tmpvar_1;
  tmpvar_1 = xlv_TEXCOORD0.xy;
  highp vec2 tmpvar_2;
  tmpvar_2 = xlv_TEXCOORD0.zw;
  highp vec2 tmpvar_3;
  tmpvar_3 = xlv_TEXCOORD1.xy;
  highp vec2 tmpvar_4;
  tmpvar_4 = xlv_TEXCOORD1.zw;
  lowp vec4 tmpvar_5;
  tmpvar_5 = texture2D (_Control, tmpvar_1);
  lowp vec3 normal;
  normal.xy = ((texture2D (_BumpSplat0, tmpvar_2).wy * 2.0) - 1.0);
  normal.z = sqrt (((1.0 - (normal.x * normal.x)) - (normal.y * normal.y)));
  lowp vec3 normal_i0;
  normal_i0.xy = ((texture2D (_BumpSplat1, tmpvar_3).wy * 2.0) - 1.0);
  normal_i0.z = sqrt (((1.0 - (normal_i0.x * normal_i0.x)) - (normal_i0.y * normal_i0.y)));
  lowp vec3 normal_i0_i1;
  normal_i0_i1.xy = ((texture2D (_BumpSplat2, tmpvar_4).wy * 2.0) - 1.0);
  normal_i0_i1.z = sqrt (((1.0 - (normal_i0_i1.x * normal_i0_i1.x)) - (normal_i0_i1.y * normal_i0_i1.y)));
  mediump vec3 tmpvar_6;
  tmpvar_6 = normalize (xlv_TEXCOORD2);
  lightDir = tmpvar_6;
  highp vec2 tmpvar_7;
  tmpvar_7 = vec2(dot (xlv_TEXCOORD3, xlv_TEXCOORD3));
  lowp vec4 c_i0;
  c_i0.xyz = (((((texture2D (_Splat0, tmpvar_2).xyz * tmpvar_5.x) + (texture2D (_Splat1, tmpvar_3).xyz * tmpvar_5.y)) + (texture2D (_Splat2, tmpvar_4).xyz * tmpvar_5.z)) * _LightColor0.xyz) * ((max (0.0, dot ((((normal * tmpvar_5.x) + (normal_i0 * tmpvar_5.y)) + (normal_i0_i1 * tmpvar_5.z)), lightDir)) * (texture2D (_LightTextureB0, tmpvar_7).w * textureCube (_LightTexture0, xlv_TEXCOORD3).w)) * 2.0));
  c_i0.w = 0.0;
  c = c_i0;
  c.w = 0.0;
  gl_FragData[0] = c;
}



#endif"
}

SubProgram "opengl " {
Keywords { "DIRECTIONAL_COOKIE" }
Bind "vertex" Vertex
Bind "tangent" ATTR14
Bind "normal" Normal
Bind "texcoord" TexCoord0
Vector 17 [_WorldSpaceLightPos0]
Matrix 5 [_Object2World]
Matrix 9 [_World2Object]
Matrix 13 [_LightMatrix0]
Vector 18 [_Control_ST]
Vector 19 [_Splat0_ST]
Vector 20 [_Splat1_ST]
Vector 21 [_Splat2_ST]
"!!ARBvp1.0
# 25 ALU
PARAM c[22] = { program.local[0],
		state.matrix.mvp,
		program.local[5..21] };
TEMP R0;
TEMP R1;
TEMP R2;
MOV R0.xyz, vertex.attrib[14];
MUL R1.xyz, vertex.normal.zxyw, R0.yzxw;
MAD R0.xyz, vertex.normal.yzxw, R0.zxyw, -R1;
MOV R1, c[17];
MUL R0.xyz, R0, vertex.attrib[14].w;
DP4 R2.z, R1, c[11];
DP4 R2.y, R1, c[10];
DP4 R2.x, R1, c[9];
DP3 result.texcoord[2].y, R2, R0;
DP4 R0.w, vertex.position, c[8];
DP4 R0.z, vertex.position, c[7];
DP4 R0.x, vertex.position, c[5];
DP4 R0.y, vertex.position, c[6];
DP3 result.texcoord[2].z, vertex.normal, R2;
DP3 result.texcoord[2].x, R2, vertex.attrib[14];
DP4 result.texcoord[3].y, R0, c[14];
DP4 result.texcoord[3].x, R0, c[13];
MAD result.texcoord[0].zw, vertex.texcoord[0].xyxy, c[19].xyxy, c[19];
MAD result.texcoord[0].xy, vertex.texcoord[0], c[18], c[18].zwzw;
MAD result.texcoord[1].zw, vertex.texcoord[0].xyxy, c[21].xyxy, c[21];
MAD result.texcoord[1].xy, vertex.texcoord[0], c[20], c[20].zwzw;
DP4 result.position.w, vertex.position, c[4];
DP4 result.position.z, vertex.position, c[3];
DP4 result.position.y, vertex.position, c[2];
DP4 result.position.x, vertex.position, c[1];
END
# 25 instructions, 3 R-regs
"
}

SubProgram "d3d9 " {
Keywords { "DIRECTIONAL_COOKIE" }
Bind "vertex" Vertex
Bind "tangent" TexCoord2
Bind "normal" Normal
Bind "texcoord" TexCoord0
Matrix 0 [glstate_matrix_mvp]
Vector 16 [_WorldSpaceLightPos0]
Matrix 4 [_Object2World]
Matrix 8 [_World2Object]
Matrix 12 [_LightMatrix0]
Vector 17 [_Control_ST]
Vector 18 [_Splat0_ST]
Vector 19 [_Splat1_ST]
Vector 20 [_Splat2_ST]
"vs_2_0
; 28 ALU
dcl_position0 v0
dcl_tangent0 v1
dcl_normal0 v2
dcl_texcoord0 v3
mov r0.xyz, v1
mul r1.xyz, v2.zxyw, r0.yzxw
mov r0.xyz, v1
mad r1.xyz, v2.yzxw, r0.zxyw, -r1
mov r0, c10
dp4 r3.z, c16, r0
mov r0, c9
dp4 r3.y, c16, r0
mul r2.xyz, r1, v1.w
mov r1, c8
dp4 r3.x, c16, r1
dp4 r0.w, v0, c7
dp4 r0.z, v0, c6
dp4 r0.x, v0, c4
dp4 r0.y, v0, c5
dp3 oT2.y, r3, r2
dp3 oT2.z, v2, r3
dp3 oT2.x, r3, v1
dp4 oT3.y, r0, c13
dp4 oT3.x, r0, c12
mad oT0.zw, v3.xyxy, c18.xyxy, c18
mad oT0.xy, v3, c17, c17.zwzw
mad oT1.zw, v3.xyxy, c20.xyxy, c20
mad oT1.xy, v3, c19, c19.zwzw
dp4 oPos.w, v0, c3
dp4 oPos.z, v0, c2
dp4 oPos.y, v0, c1
dp4 oPos.x, v0, c0
"
}

SubProgram "gles " {
Keywords { "DIRECTIONAL_COOKIE" }
"!!GLES
#define SHADER_API_GLES 1
#define tex2D texture2D


#ifdef VERTEX
#define gl_ModelViewProjectionMatrix glstate_matrix_mvp
uniform mat4 glstate_matrix_mvp;

varying highp vec2 xlv_TEXCOORD3;
varying mediump vec3 xlv_TEXCOORD2;
varying highp vec4 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;

uniform lowp vec4 _WorldSpaceLightPos0;
uniform highp mat4 _World2Object;
uniform highp vec4 _Splat2_ST;
uniform highp vec4 _Splat1_ST;
uniform highp vec4 _Splat0_ST;
uniform highp mat4 _Object2World;
uniform highp mat4 _LightMatrix0;
uniform highp vec4 _Control_ST;
attribute vec4 _glesTANGENT;
attribute vec4 _glesMultiTexCoord0;
attribute vec3 _glesNormal;
attribute vec4 _glesVertex;
void main ()
{
  vec4 tmpvar_1;
  tmpvar_1.xyz = normalize (_glesTANGENT.xyz);
  tmpvar_1.w = _glesTANGENT.w;
  vec3 tmpvar_2;
  tmpvar_2 = normalize (_glesNormal);
  highp vec4 tmpvar_3;
  highp vec4 tmpvar_4;
  mediump vec3 tmpvar_5;
  tmpvar_3.xy = ((_glesMultiTexCoord0.xy * _Control_ST.xy) + _Control_ST.zw);
  tmpvar_3.zw = ((_glesMultiTexCoord0.xy * _Splat0_ST.xy) + _Splat0_ST.zw);
  tmpvar_4.xy = ((_glesMultiTexCoord0.xy * _Splat1_ST.xy) + _Splat1_ST.zw);
  tmpvar_4.zw = ((_glesMultiTexCoord0.xy * _Splat2_ST.xy) + _Splat2_ST.zw);
  highp mat3 tmpvar_6;
  tmpvar_6[0] = tmpvar_1.xyz;
  tmpvar_6[1] = (cross (tmpvar_2, tmpvar_1.xyz) * _glesTANGENT.w);
  tmpvar_6[2] = tmpvar_2;
  mat3 tmpvar_7;
  tmpvar_7[0].x = tmpvar_6[0].x;
  tmpvar_7[0].y = tmpvar_6[1].x;
  tmpvar_7[0].z = tmpvar_6[2].x;
  tmpvar_7[1].x = tmpvar_6[0].y;
  tmpvar_7[1].y = tmpvar_6[1].y;
  tmpvar_7[1].z = tmpvar_6[2].y;
  tmpvar_7[2].x = tmpvar_6[0].z;
  tmpvar_7[2].y = tmpvar_6[1].z;
  tmpvar_7[2].z = tmpvar_6[2].z;
  highp vec3 tmpvar_8;
  tmpvar_8 = (tmpvar_7 * (_World2Object * _WorldSpaceLightPos0).xyz);
  tmpvar_5 = tmpvar_8;
  gl_Position = (gl_ModelViewProjectionMatrix * _glesVertex);
  xlv_TEXCOORD0 = tmpvar_3;
  xlv_TEXCOORD1 = tmpvar_4;
  xlv_TEXCOORD2 = tmpvar_5;
  xlv_TEXCOORD3 = (_LightMatrix0 * (_Object2World * _glesVertex)).xy;
}



#endif
#ifdef FRAGMENT

varying highp vec2 xlv_TEXCOORD3;
varying mediump vec3 xlv_TEXCOORD2;
varying highp vec4 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;
uniform sampler2D _Splat2;
uniform sampler2D _Splat1;
uniform sampler2D _Splat0;
uniform sampler2D _LightTexture0;
uniform lowp vec4 _LightColor0;
uniform sampler2D _Control;
uniform sampler2D _BumpSplat2;
uniform sampler2D _BumpSplat1;
uniform sampler2D _BumpSplat0;
void main ()
{
  lowp vec4 c;
  lowp vec3 lightDir;
  highp vec2 tmpvar_1;
  tmpvar_1 = xlv_TEXCOORD0.xy;
  highp vec2 tmpvar_2;
  tmpvar_2 = xlv_TEXCOORD0.zw;
  highp vec2 tmpvar_3;
  tmpvar_3 = xlv_TEXCOORD1.xy;
  highp vec2 tmpvar_4;
  tmpvar_4 = xlv_TEXCOORD1.zw;
  lowp vec4 tmpvar_5;
  tmpvar_5 = texture2D (_Control, tmpvar_1);
  lightDir = xlv_TEXCOORD2;
  lowp vec4 c_i0;
  c_i0.xyz = (((((texture2D (_Splat0, tmpvar_2).xyz * tmpvar_5.x) + (texture2D (_Splat1, tmpvar_3).xyz * tmpvar_5.y)) + (texture2D (_Splat2, tmpvar_4).xyz * tmpvar_5.z)) * _LightColor0.xyz) * ((max (0.0, dot ((((((texture2D (_BumpSplat0, tmpvar_2).xyz * 2.0) - 1.0) * tmpvar_5.x) + (((texture2D (_BumpSplat1, tmpvar_3).xyz * 2.0) - 1.0) * tmpvar_5.y)) + (((texture2D (_BumpSplat2, tmpvar_4).xyz * 2.0) - 1.0) * tmpvar_5.z)), lightDir)) * texture2D (_LightTexture0, xlv_TEXCOORD3).w) * 2.0));
  c_i0.w = 0.0;
  c = c_i0;
  c.w = 0.0;
  gl_FragData[0] = c;
}



#endif"
}

SubProgram "glesdesktop " {
Keywords { "DIRECTIONAL_COOKIE" }
"!!GLES
#define SHADER_API_GLES 1
#define tex2D texture2D


#ifdef VERTEX
#define gl_ModelViewProjectionMatrix glstate_matrix_mvp
uniform mat4 glstate_matrix_mvp;

varying highp vec2 xlv_TEXCOORD3;
varying mediump vec3 xlv_TEXCOORD2;
varying highp vec4 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;

uniform lowp vec4 _WorldSpaceLightPos0;
uniform highp mat4 _World2Object;
uniform highp vec4 _Splat2_ST;
uniform highp vec4 _Splat1_ST;
uniform highp vec4 _Splat0_ST;
uniform highp mat4 _Object2World;
uniform highp mat4 _LightMatrix0;
uniform highp vec4 _Control_ST;
attribute vec4 _glesTANGENT;
attribute vec4 _glesMultiTexCoord0;
attribute vec3 _glesNormal;
attribute vec4 _glesVertex;
void main ()
{
  vec4 tmpvar_1;
  tmpvar_1.xyz = normalize (_glesTANGENT.xyz);
  tmpvar_1.w = _glesTANGENT.w;
  vec3 tmpvar_2;
  tmpvar_2 = normalize (_glesNormal);
  highp vec4 tmpvar_3;
  highp vec4 tmpvar_4;
  mediump vec3 tmpvar_5;
  tmpvar_3.xy = ((_glesMultiTexCoord0.xy * _Control_ST.xy) + _Control_ST.zw);
  tmpvar_3.zw = ((_glesMultiTexCoord0.xy * _Splat0_ST.xy) + _Splat0_ST.zw);
  tmpvar_4.xy = ((_glesMultiTexCoord0.xy * _Splat1_ST.xy) + _Splat1_ST.zw);
  tmpvar_4.zw = ((_glesMultiTexCoord0.xy * _Splat2_ST.xy) + _Splat2_ST.zw);
  highp mat3 tmpvar_6;
  tmpvar_6[0] = tmpvar_1.xyz;
  tmpvar_6[1] = (cross (tmpvar_2, tmpvar_1.xyz) * _glesTANGENT.w);
  tmpvar_6[2] = tmpvar_2;
  mat3 tmpvar_7;
  tmpvar_7[0].x = tmpvar_6[0].x;
  tmpvar_7[0].y = tmpvar_6[1].x;
  tmpvar_7[0].z = tmpvar_6[2].x;
  tmpvar_7[1].x = tmpvar_6[0].y;
  tmpvar_7[1].y = tmpvar_6[1].y;
  tmpvar_7[1].z = tmpvar_6[2].y;
  tmpvar_7[2].x = tmpvar_6[0].z;
  tmpvar_7[2].y = tmpvar_6[1].z;
  tmpvar_7[2].z = tmpvar_6[2].z;
  highp vec3 tmpvar_8;
  tmpvar_8 = (tmpvar_7 * (_World2Object * _WorldSpaceLightPos0).xyz);
  tmpvar_5 = tmpvar_8;
  gl_Position = (gl_ModelViewProjectionMatrix * _glesVertex);
  xlv_TEXCOORD0 = tmpvar_3;
  xlv_TEXCOORD1 = tmpvar_4;
  xlv_TEXCOORD2 = tmpvar_5;
  xlv_TEXCOORD3 = (_LightMatrix0 * (_Object2World * _glesVertex)).xy;
}



#endif
#ifdef FRAGMENT

varying highp vec2 xlv_TEXCOORD3;
varying mediump vec3 xlv_TEXCOORD2;
varying highp vec4 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;
uniform sampler2D _Splat2;
uniform sampler2D _Splat1;
uniform sampler2D _Splat0;
uniform sampler2D _LightTexture0;
uniform lowp vec4 _LightColor0;
uniform sampler2D _Control;
uniform sampler2D _BumpSplat2;
uniform sampler2D _BumpSplat1;
uniform sampler2D _BumpSplat0;
void main ()
{
  lowp vec4 c;
  lowp vec3 lightDir;
  highp vec2 tmpvar_1;
  tmpvar_1 = xlv_TEXCOORD0.xy;
  highp vec2 tmpvar_2;
  tmpvar_2 = xlv_TEXCOORD0.zw;
  highp vec2 tmpvar_3;
  tmpvar_3 = xlv_TEXCOORD1.xy;
  highp vec2 tmpvar_4;
  tmpvar_4 = xlv_TEXCOORD1.zw;
  lowp vec4 tmpvar_5;
  tmpvar_5 = texture2D (_Control, tmpvar_1);
  lowp vec3 normal;
  normal.xy = ((texture2D (_BumpSplat0, tmpvar_2).wy * 2.0) - 1.0);
  normal.z = sqrt (((1.0 - (normal.x * normal.x)) - (normal.y * normal.y)));
  lowp vec3 normal_i0;
  normal_i0.xy = ((texture2D (_BumpSplat1, tmpvar_3).wy * 2.0) - 1.0);
  normal_i0.z = sqrt (((1.0 - (normal_i0.x * normal_i0.x)) - (normal_i0.y * normal_i0.y)));
  lowp vec3 normal_i0_i1;
  normal_i0_i1.xy = ((texture2D (_BumpSplat2, tmpvar_4).wy * 2.0) - 1.0);
  normal_i0_i1.z = sqrt (((1.0 - (normal_i0_i1.x * normal_i0_i1.x)) - (normal_i0_i1.y * normal_i0_i1.y)));
  lightDir = xlv_TEXCOORD2;
  lowp vec4 c_i0;
  c_i0.xyz = (((((texture2D (_Splat0, tmpvar_2).xyz * tmpvar_5.x) + (texture2D (_Splat1, tmpvar_3).xyz * tmpvar_5.y)) + (texture2D (_Splat2, tmpvar_4).xyz * tmpvar_5.z)) * _LightColor0.xyz) * ((max (0.0, dot ((((normal * tmpvar_5.x) + (normal_i0 * tmpvar_5.y)) + (normal_i0_i1 * tmpvar_5.z)), lightDir)) * texture2D (_LightTexture0, xlv_TEXCOORD3).w) * 2.0));
  c_i0.w = 0.0;
  c = c_i0;
  c.w = 0.0;
  gl_FragData[0] = c;
}



#endif"
}

}
Program "fp" {
// Fragment combos: 5
//   opengl - ALU: 37 to 49, TEX: 7 to 9
//   d3d9 - ALU: 44 to 57, TEX: 7 to 9
SubProgram "opengl " {
Keywords { "POINT" }
Vector 0 [_LightColor0]
SetTexture 0 [_Control] 2D
SetTexture 1 [_Splat0] 2D
SetTexture 2 [_Splat1] 2D
SetTexture 3 [_Splat2] 2D
SetTexture 4 [_BumpSplat0] 2D
SetTexture 5 [_BumpSplat1] 2D
SetTexture 6 [_BumpSplat2] 2D
SetTexture 7 [_LightTexture0] 2D
"!!ARBfp1.0
OPTION ARB_precision_hint_fastest;
# 43 ALU, 8 TEX
PARAM c[2] = { program.local[0],
		{ 0, 2, 1 } };
TEMP R0;
TEMP R1;
TEMP R2;
TEMP R3;
TEMP R4;
TEMP R5;
TEMP R6;
TEX R4.yw, fragment.texcoord[1].zwzw, texture[6], 2D;
TEX R6.yw, fragment.texcoord[1], texture[5], 2D;
TEX R0.xyz, fragment.texcoord[0], texture[0], 2D;
TEX R3.xyz, fragment.texcoord[1], texture[2], 2D;
TEX R1.xyz, fragment.texcoord[1].zwzw, texture[3], 2D;
TEX R2.xyz, fragment.texcoord[0].zwzw, texture[1], 2D;
TEX R5.yw, fragment.texcoord[0].zwzw, texture[4], 2D;
MUL R3.xyz, R0.y, R3;
MAD R2.xyz, R0.x, R2, R3;
MAD R1.xyz, R0.z, R1, R2;
MAD R6.xy, R6.wyzw, c[1].y, -c[1].z;
DP3 R0.w, fragment.texcoord[3], fragment.texcoord[3];
MUL R1.w, R6.y, R6.y;
MAD R1.w, -R6.x, R6.x, -R1;
ADD R2.z, R1.w, c[1];
MAD R2.xy, R5.wyzw, c[1].y, -c[1].z;
MUL R1.w, R2.y, R2.y;
RSQ R2.z, R2.z;
MAD R1.w, -R2.x, R2.x, -R1;
RCP R6.z, R2.z;
MUL R3.xyz, R0.y, R6;
ADD R1.w, R1, c[1].z;
RSQ R0.y, R1.w;
RCP R2.z, R0.y;
MAD R4.xy, R4.wyzw, c[1].y, -c[1].z;
MAD R2.xyz, R0.x, R2, R3;
MUL R0.y, R4, R4;
MAD R0.x, -R4, R4, -R0.y;
DP3 R0.y, fragment.texcoord[2], fragment.texcoord[2];
RSQ R0.y, R0.y;
ADD R0.x, R0, c[1].z;
RSQ R0.x, R0.x;
MUL R3.xyz, R0.y, fragment.texcoord[2];
RCP R4.z, R0.x;
MAD R0.xyz, R4, R0.z, R2;
DP3 R0.x, R0, R3;
MUL R1.xyz, R1, c[0];
MAX R0.x, R0, c[1];
MOV result.color.w, c[1].x;
TEX R0.w, R0.w, texture[7], 2D;
MUL R0.x, R0, R0.w;
MUL R0.xyz, R0.x, R1;
MUL result.color.xyz, R0, c[1].y;
END
# 43 instructions, 7 R-regs
"
}

SubProgram "d3d9 " {
Keywords { "POINT" }
Vector 0 [_LightColor0]
SetTexture 0 [_Control] 2D
SetTexture 1 [_Splat0] 2D
SetTexture 2 [_Splat1] 2D
SetTexture 3 [_Splat2] 2D
SetTexture 4 [_BumpSplat0] 2D
SetTexture 5 [_BumpSplat1] 2D
SetTexture 6 [_BumpSplat2] 2D
SetTexture 7 [_LightTexture0] 2D
"ps_2_0
; 50 ALU, 8 TEX
dcl_2d s0
dcl_2d s1
dcl_2d s2
dcl_2d s3
dcl_2d s4
dcl_2d s5
dcl_2d s6
dcl_2d s7
def c1, 2.00000000, -1.00000000, 1.00000000, 0.00000000
dcl t0
dcl t1
dcl t2.xyz
dcl t3.xyz
texld r6, t1, s5
texld r5, t0, s0
dp3 r2.x, t3, t3
mov r4.xy, r2.x
mov r6.x, r6.w
mad_pp r6.xy, r6, c1.x, c1.y
mov r0.y, t0.w
mov r0.x, t0.z
mov r1.xy, r0
mov r2.y, t1.w
mov r2.x, t1.z
mov r0.y, t1.w
mov r0.x, t1.z
mov r3.y, t0.w
mov r3.x, t0.z
texld r9, r4, s7
texld r4, r2, s3
texld r0, r0, s6
texld r1, r1, s4
texld r3, r3, s1
texld r2, t1, s2
mul_pp r0.x, r6.y, r6.y
mad_pp r0.x, -r6, r6, -r0
mov r1.x, r1.w
add_pp r0.x, r0, c1.z
rsq_pp r0.x, r0.x
rcp_pp r6.z, r0.x
mad_pp r8.xy, r1, c1.x, c1.y
mul_pp r0.x, r8.y, r8.y
mov r1.x, r0.w
mul_pp r7.xyz, r5.y, r6
mov r1.y, r0
mad_pp r6.xy, r1, c1.x, c1.y
mad_pp r0.x, -r8, r8, -r0
add_pp r1.x, r0, c1.z
rsq_pp r1.x, r1.x
rcp_pp r8.z, r1.x
mul_pp r0.x, r6.y, r6.y
mad_pp r0.x, -r6, r6, -r0
add_pp r0.x, r0, c1.z
rsq_pp r0.x, r0.x
dp3_pp r1.x, t2, t2
rsq_pp r1.x, r1.x
rcp_pp r6.z, r0.x
mul_pp r0.xyz, r1.x, t2
mad_pp r7.xyz, r5.x, r8, r7
mad_pp r1.xyz, r6, r5.z, r7
dp3_pp r0.x, r1, r0
max_pp r0.x, r0, c1.w
mul_pp r1.xyz, r5.y, r2
mad_pp r1.xyz, r5.x, r3, r1
mad_pp r1.xyz, r5.z, r4, r1
mul_pp r1.xyz, r1, c0
mul_pp r0.x, r0, r9
mul_pp r0.xyz, r0.x, r1
mul_pp r0.xyz, r0, c1.x
mov_pp r0.w, c1
mov_pp oC0, r0
"
}

SubProgram "gles " {
Keywords { "POINT" }
"!!GLES"
}

SubProgram "glesdesktop " {
Keywords { "POINT" }
"!!GLES"
}

SubProgram "opengl " {
Keywords { "DIRECTIONAL" }
Vector 0 [_LightColor0]
SetTexture 0 [_Control] 2D
SetTexture 1 [_Splat0] 2D
SetTexture 2 [_Splat1] 2D
SetTexture 3 [_Splat2] 2D
SetTexture 4 [_BumpSplat0] 2D
SetTexture 5 [_BumpSplat1] 2D
SetTexture 6 [_BumpSplat2] 2D
"!!ARBfp1.0
OPTION ARB_precision_hint_fastest;
# 37 ALU, 7 TEX
PARAM c[2] = { program.local[0],
		{ 0, 2, 1 } };
TEMP R0;
TEMP R1;
TEMP R2;
TEMP R3;
TEMP R4;
TEMP R5;
TEMP R6;
TEX R5.yw, fragment.texcoord[1].zwzw, texture[6], 2D;
TEX R4.yw, fragment.texcoord[1], texture[5], 2D;
TEX R0.xyz, fragment.texcoord[0], texture[0], 2D;
TEX R3.xyz, fragment.texcoord[1], texture[2], 2D;
TEX R1.xyz, fragment.texcoord[1].zwzw, texture[3], 2D;
TEX R2.xyz, fragment.texcoord[0].zwzw, texture[1], 2D;
TEX R6.yw, fragment.texcoord[0].zwzw, texture[4], 2D;
MAD R4.xy, R4.wyzw, c[1].y, -c[1].z;
MUL R3.xyz, R0.y, R3;
MAD R2.xyz, R0.x, R2, R3;
MUL R0.w, R4.y, R4.y;
MAD R0.w, -R4.x, R4.x, -R0;
MAD R1.xyz, R0.z, R1, R2;
ADD R0.w, R0, c[1].z;
RSQ R0.w, R0.w;
RCP R4.z, R0.w;
MUL R3.xyz, R0.y, R4;
MAD R4.xy, R6.wyzw, c[1].y, -c[1].z;
MAD R5.xy, R5.wyzw, c[1].y, -c[1].z;
MUL R0.w, R4.y, R4.y;
MUL R0.y, R5, R5;
MAD R0.w, -R4.x, R4.x, -R0;
MAD R0.y, -R5.x, R5.x, -R0;
ADD R0.w, R0, c[1].z;
ADD R0.y, R0, c[1].z;
RSQ R0.y, R0.y;
RSQ R0.w, R0.w;
RCP R4.z, R0.w;
MAD R3.xyz, R0.x, R4, R3;
RCP R5.z, R0.y;
MAD R3.xyz, R5, R0.z, R3;
DP3 R0.x, R3, fragment.texcoord[2];
MUL R1.xyz, R1, c[0];
MAX R0.x, R0, c[1];
MUL R0.xyz, R0.x, R1;
MUL result.color.xyz, R0, c[1].y;
MOV result.color.w, c[1].x;
END
# 37 instructions, 7 R-regs
"
}

SubProgram "d3d9 " {
Keywords { "DIRECTIONAL" }
Vector 0 [_LightColor0]
SetTexture 0 [_Control] 2D
SetTexture 1 [_Splat0] 2D
SetTexture 2 [_Splat1] 2D
SetTexture 3 [_Splat2] 2D
SetTexture 4 [_BumpSplat0] 2D
SetTexture 5 [_BumpSplat1] 2D
SetTexture 6 [_BumpSplat2] 2D
"ps_2_0
; 44 ALU, 7 TEX
dcl_2d s0
dcl_2d s1
dcl_2d s2
dcl_2d s3
dcl_2d s4
dcl_2d s5
dcl_2d s6
def c1, 2.00000000, -1.00000000, 1.00000000, 0.00000000
dcl t0
dcl t1
dcl t2.xyz
texld r6, t1, s5
texld r5, t0, s0
mov r6.x, r6.w
mad_pp r6.xy, r6, c1.x, c1.y
mov r2.y, t1.w
mov r2.x, t1.z
mov r3.xy, r2
mov r2.y, t0.w
mov r2.x, t0.z
mov r0.y, t0.w
mov r0.x, t0.z
mov r1.y, t1.w
mov r1.x, t1.z
texld r4, r3, s3
texld r3, r2, s1
texld r1, r1, s6
texld r0, r0, s4
texld r2, t1, s2
mul_pp r0.x, r6.y, r6.y
mad_pp r0.x, -r6, r6, -r0
add_pp r0.x, r0, c1.z
rsq_pp r0.x, r0.x
rcp_pp r6.z, r0.x
mov r0.x, r0.w
mad_pp r7.xy, r0, c1.x, c1.y
mul_pp r1.x, r7.y, r7.y
mad_pp r1.x, -r7, r7, -r1
add_pp r1.x, r1, c1.z
rsq_pp r1.x, r1.x
mov r0.y, r1
mov r0.x, r1.w
mad_pp r8.xy, r0, c1.x, c1.y
mul_pp r0.x, r8.y, r8.y
mad_pp r0.x, -r8, r8, -r0
add_pp r0.x, r0, c1.z
rsq_pp r0.x, r0.x
mul_pp r6.xyz, r5.y, r6
rcp_pp r7.z, r1.x
mad_pp r1.xyz, r5.x, r7, r6
rcp_pp r8.z, r0.x
mad_pp r0.xyz, r8, r5.z, r1
dp3_pp r0.x, r0, t2
mul_pp r1.xyz, r5.y, r2
mad_pp r1.xyz, r5.x, r3, r1
mad_pp r1.xyz, r5.z, r4, r1
mul_pp r1.xyz, r1, c0
max_pp r0.x, r0, c1.w
mul_pp r0.xyz, r0.x, r1
mul_pp r0.xyz, r0, c1.x
mov_pp r0.w, c1
mov_pp oC0, r0
"
}

SubProgram "gles " {
Keywords { "DIRECTIONAL" }
"!!GLES"
}

SubProgram "glesdesktop " {
Keywords { "DIRECTIONAL" }
"!!GLES"
}

SubProgram "opengl " {
Keywords { "SPOT" }
Vector 0 [_LightColor0]
SetTexture 0 [_Control] 2D
SetTexture 1 [_Splat0] 2D
SetTexture 2 [_Splat1] 2D
SetTexture 3 [_Splat2] 2D
SetTexture 4 [_BumpSplat0] 2D
SetTexture 5 [_BumpSplat1] 2D
SetTexture 6 [_BumpSplat2] 2D
SetTexture 7 [_LightTexture0] 2D
SetTexture 8 [_LightTextureB0] 2D
"!!ARBfp1.0
OPTION ARB_precision_hint_fastest;
# 49 ALU, 9 TEX
PARAM c[2] = { program.local[0],
		{ 0, 2, 1, 0.5 } };
TEMP R0;
TEMP R1;
TEMP R2;
TEMP R3;
TEMP R4;
TEMP R5;
TEMP R6;
TEX R1.xyz, fragment.texcoord[1].zwzw, texture[3], 2D;
TEX R2.xyz, fragment.texcoord[0].zwzw, texture[1], 2D;
TEX R3.xyz, fragment.texcoord[1], texture[2], 2D;
TEX R5.yw, fragment.texcoord[0].zwzw, texture[4], 2D;
TEX R6.yw, fragment.texcoord[1], texture[5], 2D;
RCP R0.x, fragment.texcoord[3].w;
MAD R4.xy, fragment.texcoord[3], R0.x, c[1].w;
DP3 R1.w, fragment.texcoord[3], fragment.texcoord[3];
MOV result.color.w, c[1].x;
TEX R0.xyz, fragment.texcoord[0], texture[0], 2D;
TEX R0.w, R4, texture[7], 2D;
TEX R4.yw, fragment.texcoord[1].zwzw, texture[6], 2D;
TEX R1.w, R1.w, texture[8], 2D;
MUL R3.xyz, R0.y, R3;
MAD R2.xyz, R0.x, R2, R3;
MAD R1.xyz, R0.z, R1, R2;
MAD R3.xy, R6.wyzw, c[1].y, -c[1].z;
MUL R2.x, R3.y, R3.y;
MAD R2.z, -R3.x, R3.x, -R2.x;
ADD R2.w, R2.z, c[1].z;
MAD R2.xy, R5.wyzw, c[1].y, -c[1].z;
MUL R2.z, R2.y, R2.y;
MAD R2.z, -R2.x, R2.x, -R2;
RSQ R2.w, R2.w;
RCP R3.z, R2.w;
MUL R3.xyz, R0.y, R3;
ADD R2.z, R2, c[1];
RSQ R0.y, R2.z;
RCP R2.z, R0.y;
MAD R4.xy, R4.wyzw, c[1].y, -c[1].z;
MAD R2.xyz, R0.x, R2, R3;
MUL R0.y, R4, R4;
MAD R0.x, -R4, R4, -R0.y;
DP3 R0.y, fragment.texcoord[2], fragment.texcoord[2];
RSQ R0.y, R0.y;
ADD R0.x, R0, c[1].z;
RSQ R0.x, R0.x;
MUL R3.xyz, R0.y, fragment.texcoord[2];
RCP R4.z, R0.x;
MAD R0.xyz, R4, R0.z, R2;
DP3 R0.x, R0, R3;
SLT R0.y, c[1].x, fragment.texcoord[3].z;
MUL R0.y, R0, R0.w;
MUL R0.y, R0, R1.w;
MAX R0.x, R0, c[1];
MUL R1.xyz, R1, c[0];
MUL R0.x, R0, R0.y;
MUL R0.xyz, R0.x, R1;
MUL result.color.xyz, R0, c[1].y;
END
# 49 instructions, 7 R-regs
"
}

SubProgram "d3d9 " {
Keywords { "SPOT" }
Vector 0 [_LightColor0]
SetTexture 0 [_Control] 2D
SetTexture 1 [_Splat0] 2D
SetTexture 2 [_Splat1] 2D
SetTexture 3 [_Splat2] 2D
SetTexture 4 [_BumpSplat0] 2D
SetTexture 5 [_BumpSplat1] 2D
SetTexture 6 [_BumpSplat2] 2D
SetTexture 7 [_LightTexture0] 2D
SetTexture 8 [_LightTextureB0] 2D
"ps_2_0
; 57 ALU, 9 TEX
dcl_2d s0
dcl_2d s1
dcl_2d s2
dcl_2d s3
dcl_2d s4
dcl_2d s5
dcl_2d s6
dcl_2d s7
dcl_2d s8
def c1, 2.00000000, -1.00000000, 1.00000000, 0.00000000
def c2, 0.50000000, 0, 0, 0
dcl t0
dcl t1
dcl t2.xyz
dcl t3
texld r6, t1, s5
rcp r4.x, t3.w
mad r4.xy, t3, r4.x, c2.x
mov r6.x, r6.w
mad_pp r6.xy, r6, c1.x, c1.y
mov r0.y, t0.w
mov r0.x, t0.z
mov r1.xy, r0
mov r2.y, t0.w
mov r2.x, t0.z
mov r3.xy, r2
mov r2.y, t1.w
mov r2.x, t1.z
mov r5.xy, r2
dp3 r2.x, t3, t3
mov r2.xy, r2.x
mov r0.y, t1.w
mov r0.x, t1.z
texld r9, r2, s8
texld r0, r0, s6
texld r1, r1, s4
texld r7, r4, s7
texld r4, r5, s3
texld r3, r3, s1
texld r2, t1, s2
texld r5, t0, s0
mul_pp r0.x, r6.y, r6.y
mad_pp r0.x, -r6, r6, -r0
mov r1.x, r1.w
add_pp r0.x, r0, c1.z
rsq_pp r0.x, r0.x
rcp_pp r6.z, r0.x
mad_pp r8.xy, r1, c1.x, c1.y
mul_pp r0.x, r8.y, r8.y
mov r1.x, r0.w
mul_pp r7.xyz, r5.y, r6
mov r1.y, r0
mad_pp r6.xy, r1, c1.x, c1.y
mad_pp r0.x, -r8, r8, -r0
add_pp r1.x, r0, c1.z
rsq_pp r1.x, r1.x
rcp_pp r8.z, r1.x
mad_pp r7.xyz, r5.x, r8, r7
mul_pp r0.x, r6.y, r6.y
mad_pp r0.x, -r6, r6, -r0
add_pp r0.x, r0, c1.z
rsq_pp r0.x, r0.x
dp3_pp r1.x, t2, t2
rsq_pp r1.x, r1.x
rcp_pp r6.z, r0.x
mul_pp r0.xyz, r1.x, t2
mad_pp r1.xyz, r6, r5.z, r7
dp3_pp r0.x, r1, r0
mul_pp r1.xyz, r5.y, r2
mad_pp r1.xyz, r5.x, r3, r1
mad_pp r2.xyz, r5.z, r4, r1
max_pp r0.x, r0, c1.w
cmp r1.x, -t3.z, c1.w, c1.z
mul_pp r1.x, r1, r7.w
mul_pp r1.x, r1, r9
mul_pp r2.xyz, r2, c0
mul_pp r0.x, r0, r1
mul_pp r0.xyz, r0.x, r2
mul_pp r0.xyz, r0, c1.x
mov_pp r0.w, c1
mov_pp oC0, r0
"
}

SubProgram "gles " {
Keywords { "SPOT" }
"!!GLES"
}

SubProgram "glesdesktop " {
Keywords { "SPOT" }
"!!GLES"
}

SubProgram "opengl " {
Keywords { "POINT_COOKIE" }
Vector 0 [_LightColor0]
SetTexture 0 [_Control] 2D
SetTexture 1 [_Splat0] 2D
SetTexture 2 [_Splat1] 2D
SetTexture 3 [_Splat2] 2D
SetTexture 4 [_BumpSplat0] 2D
SetTexture 5 [_BumpSplat1] 2D
SetTexture 6 [_BumpSplat2] 2D
SetTexture 7 [_LightTextureB0] 2D
SetTexture 8 [_LightTexture0] CUBE
"!!ARBfp1.0
OPTION ARB_precision_hint_fastest;
# 45 ALU, 9 TEX
PARAM c[2] = { program.local[0],
		{ 0, 2, 1 } };
TEMP R0;
TEMP R1;
TEMP R2;
TEMP R3;
TEMP R4;
TEMP R5;
TEMP R6;
TEX R4.yw, fragment.texcoord[1].zwzw, texture[6], 2D;
TEX R0.xyz, fragment.texcoord[0], texture[0], 2D;
TEX R3.xyz, fragment.texcoord[1], texture[2], 2D;
TEX R1.xyz, fragment.texcoord[1].zwzw, texture[3], 2D;
TEX R2.xyz, fragment.texcoord[0].zwzw, texture[1], 2D;
TEX R5.yw, fragment.texcoord[0].zwzw, texture[4], 2D;
TEX R6.yw, fragment.texcoord[1], texture[5], 2D;
TEX R1.w, fragment.texcoord[3], texture[8], CUBE;
MUL R3.xyz, R0.y, R3;
MAD R2.xyz, R0.x, R2, R3;
MAD R1.xyz, R0.z, R1, R2;
MAD R3.xy, R6.wyzw, c[1].y, -c[1].z;
MUL R2.x, R3.y, R3.y;
MAD R2.z, -R3.x, R3.x, -R2.x;
ADD R2.w, R2.z, c[1].z;
MAD R2.xy, R5.wyzw, c[1].y, -c[1].z;
MUL R2.z, R2.y, R2.y;
MAD R2.z, -R2.x, R2.x, -R2;
DP3 R0.w, fragment.texcoord[3], fragment.texcoord[3];
RSQ R2.w, R2.w;
RCP R3.z, R2.w;
MUL R3.xyz, R0.y, R3;
ADD R2.z, R2, c[1];
RSQ R0.y, R2.z;
RCP R2.z, R0.y;
MAD R4.xy, R4.wyzw, c[1].y, -c[1].z;
MAD R2.xyz, R0.x, R2, R3;
MUL R0.y, R4, R4;
MAD R0.x, -R4, R4, -R0.y;
DP3 R0.y, fragment.texcoord[2], fragment.texcoord[2];
RSQ R0.y, R0.y;
ADD R0.x, R0, c[1].z;
RSQ R0.x, R0.x;
MUL R3.xyz, R0.y, fragment.texcoord[2];
RCP R4.z, R0.x;
MAD R0.xyz, R4, R0.z, R2;
DP3 R0.x, R0, R3;
MUL R1.xyz, R1, c[0];
MAX R0.x, R0, c[1];
MOV result.color.w, c[1].x;
TEX R0.w, R0.w, texture[7], 2D;
MUL R0.y, R0.w, R1.w;
MUL R0.x, R0, R0.y;
MUL R0.xyz, R0.x, R1;
MUL result.color.xyz, R0, c[1].y;
END
# 45 instructions, 7 R-regs
"
}

SubProgram "d3d9 " {
Keywords { "POINT_COOKIE" }
Vector 0 [_LightColor0]
SetTexture 0 [_Control] 2D
SetTexture 1 [_Splat0] 2D
SetTexture 2 [_Splat1] 2D
SetTexture 3 [_Splat2] 2D
SetTexture 4 [_BumpSplat0] 2D
SetTexture 5 [_BumpSplat1] 2D
SetTexture 6 [_BumpSplat2] 2D
SetTexture 7 [_LightTextureB0] 2D
SetTexture 8 [_LightTexture0] CUBE
"ps_2_0
; 51 ALU, 9 TEX
dcl_2d s0
dcl_2d s1
dcl_2d s2
dcl_2d s3
dcl_2d s4
dcl_2d s5
dcl_2d s6
dcl_2d s7
dcl_cube s8
def c1, 2.00000000, -1.00000000, 1.00000000, 0.00000000
dcl t0
dcl t1
dcl t2.xyz
dcl t3.xyz
texld r6, t1, s5
texld r5, t0, s0
texld r7, t3, s8
dp3 r3.x, t3, t3
mov r4.xy, r3.x
mov r6.x, r6.w
mad_pp r6.xy, r6, c1.x, c1.y
mov r0.y, t0.w
mov r0.x, t0.z
mov r1.xy, r0
mov r2.y, t0.w
mov r2.x, t0.z
mov r3.y, t1.w
mov r3.x, t1.z
mov r0.y, t1.w
mov r0.x, t1.z
texld r9, r4, s7
texld r4, r3, s3
texld r3, r2, s1
texld r0, r0, s6
texld r1, r1, s4
texld r2, t1, s2
mul_pp r0.x, r6.y, r6.y
mad_pp r0.x, -r6, r6, -r0
mov r1.x, r1.w
add_pp r0.x, r0, c1.z
rsq_pp r0.x, r0.x
rcp_pp r6.z, r0.x
mad_pp r8.xy, r1, c1.x, c1.y
mul_pp r0.x, r8.y, r8.y
mov r1.x, r0.w
mul_pp r7.xyz, r5.y, r6
mov r1.y, r0
mad_pp r6.xy, r1, c1.x, c1.y
mad_pp r0.x, -r8, r8, -r0
add_pp r1.x, r0, c1.z
rsq_pp r1.x, r1.x
rcp_pp r8.z, r1.x
mad_pp r7.xyz, r5.x, r8, r7
mul_pp r0.x, r6.y, r6.y
mad_pp r0.x, -r6, r6, -r0
add_pp r0.x, r0, c1.z
rsq_pp r0.x, r0.x
dp3_pp r1.x, t2, t2
rsq_pp r1.x, r1.x
rcp_pp r6.z, r0.x
mul_pp r0.xyz, r1.x, t2
mad_pp r1.xyz, r6, r5.z, r7
dp3_pp r0.x, r1, r0
mul_pp r1.xyz, r5.y, r2
mad_pp r1.xyz, r5.x, r3, r1
mad_pp r2.xyz, r5.z, r4, r1
max_pp r0.x, r0, c1.w
mul r1.x, r9, r7.w
mul_pp r2.xyz, r2, c0
mul_pp r0.x, r0, r1
mul_pp r0.xyz, r0.x, r2
mul_pp r0.xyz, r0, c1.x
mov_pp r0.w, c1
mov_pp oC0, r0
"
}

SubProgram "gles " {
Keywords { "POINT_COOKIE" }
"!!GLES"
}

SubProgram "glesdesktop " {
Keywords { "POINT_COOKIE" }
"!!GLES"
}

SubProgram "opengl " {
Keywords { "DIRECTIONAL_COOKIE" }
Vector 0 [_LightColor0]
SetTexture 0 [_Control] 2D
SetTexture 1 [_Splat0] 2D
SetTexture 2 [_Splat1] 2D
SetTexture 3 [_Splat2] 2D
SetTexture 4 [_BumpSplat0] 2D
SetTexture 5 [_BumpSplat1] 2D
SetTexture 6 [_BumpSplat2] 2D
SetTexture 7 [_LightTexture0] 2D
"!!ARBfp1.0
OPTION ARB_precision_hint_fastest;
# 39 ALU, 8 TEX
PARAM c[2] = { program.local[0],
		{ 0, 2, 1 } };
TEMP R0;
TEMP R1;
TEMP R2;
TEMP R3;
TEMP R4;
TEMP R5;
TEMP R6;
TEX R5.yw, fragment.texcoord[1].zwzw, texture[6], 2D;
TEX R4.yw, fragment.texcoord[1], texture[5], 2D;
TEX R0.xyz, fragment.texcoord[0], texture[0], 2D;
TEX R3.xyz, fragment.texcoord[1], texture[2], 2D;
TEX R1.xyz, fragment.texcoord[1].zwzw, texture[3], 2D;
TEX R2.xyz, fragment.texcoord[0].zwzw, texture[1], 2D;
TEX R6.yw, fragment.texcoord[0].zwzw, texture[4], 2D;
TEX R0.w, fragment.texcoord[3], texture[7], 2D;
MAD R4.xy, R4.wyzw, c[1].y, -c[1].z;
MUL R3.xyz, R0.y, R3;
MAD R2.xyz, R0.x, R2, R3;
MUL R1.w, R4.y, R4.y;
MAD R1.w, -R4.x, R4.x, -R1;
MAD R1.xyz, R0.z, R1, R2;
ADD R1.w, R1, c[1].z;
RSQ R1.w, R1.w;
RCP R4.z, R1.w;
MUL R3.xyz, R0.y, R4;
MAD R4.xy, R6.wyzw, c[1].y, -c[1].z;
MAD R5.xy, R5.wyzw, c[1].y, -c[1].z;
MUL R1.w, R4.y, R4.y;
MUL R0.y, R5, R5;
MAD R1.w, -R4.x, R4.x, -R1;
MAD R0.y, -R5.x, R5.x, -R0;
ADD R1.w, R1, c[1].z;
ADD R0.y, R0, c[1].z;
RSQ R0.y, R0.y;
RSQ R1.w, R1.w;
RCP R4.z, R1.w;
MAD R3.xyz, R0.x, R4, R3;
RCP R5.z, R0.y;
MAD R3.xyz, R5, R0.z, R3;
DP3 R0.x, R3, fragment.texcoord[2];
MAX R0.x, R0, c[1];
MUL R1.xyz, R1, c[0];
MUL R0.x, R0, R0.w;
MUL R0.xyz, R0.x, R1;
MUL result.color.xyz, R0, c[1].y;
MOV result.color.w, c[1].x;
END
# 39 instructions, 7 R-regs
"
}

SubProgram "d3d9 " {
Keywords { "DIRECTIONAL_COOKIE" }
Vector 0 [_LightColor0]
SetTexture 0 [_Control] 2D
SetTexture 1 [_Splat0] 2D
SetTexture 2 [_Splat1] 2D
SetTexture 3 [_Splat2] 2D
SetTexture 4 [_BumpSplat0] 2D
SetTexture 5 [_BumpSplat1] 2D
SetTexture 6 [_BumpSplat2] 2D
SetTexture 7 [_LightTexture0] 2D
"ps_2_0
; 44 ALU, 8 TEX
dcl_2d s0
dcl_2d s1
dcl_2d s2
dcl_2d s3
dcl_2d s4
dcl_2d s5
dcl_2d s6
dcl_2d s7
def c1, 2.00000000, -1.00000000, 1.00000000, 0.00000000
dcl t0
dcl t1
dcl t2.xyz
dcl t3.xy
texld r6, t1, s5
texld r5, t0, s0
texld r7, t3, s7
mov r6.x, r6.w
mad_pp r6.xy, r6, c1.x, c1.y
mov r2.y, t0.w
mov r2.x, t0.z
mov r3.y, t1.w
mov r3.x, t1.z
mov r0.y, t0.w
mov r0.x, t0.z
mov r1.y, t1.w
mov r1.x, t1.z
texld r4, r3, s3
texld r3, r2, s1
texld r1, r1, s6
texld r0, r0, s4
texld r2, t1, s2
mul_pp r0.x, r6.y, r6.y
mad_pp r0.x, -r6, r6, -r0
add_pp r0.x, r0, c1.z
rsq_pp r0.x, r0.x
rcp_pp r6.z, r0.x
mov r0.x, r0.w
mad_pp r7.xy, r0, c1.x, c1.y
mul_pp r1.x, r7.y, r7.y
mad_pp r1.x, -r7, r7, -r1
add_pp r1.x, r1, c1.z
rsq_pp r1.x, r1.x
rcp_pp r7.z, r1.x
mov r0.y, r1
mov r0.x, r1.w
mad_pp r8.xy, r0, c1.x, c1.y
mul_pp r6.xyz, r5.y, r6
mul_pp r0.x, r8.y, r8.y
mad_pp r0.x, -r8, r8, -r0
add_pp r0.x, r0, c1.z
rsq_pp r0.x, r0.x
mad_pp r1.xyz, r5.x, r7, r6
rcp_pp r8.z, r0.x
mad_pp r0.xyz, r8, r5.z, r1
dp3_pp r0.x, r0, t2
max_pp r0.x, r0, c1.w
mul_pp r1.xyz, r5.y, r2
mad_pp r1.xyz, r5.x, r3, r1
mad_pp r1.xyz, r5.z, r4, r1
mul_pp r1.xyz, r1, c0
mul_pp r0.x, r0, r7.w
mul_pp r0.xyz, r0.x, r1
mul_pp r0.xyz, r0, c1.x
mov_pp r0.w, c1
mov_pp oC0, r0
"
}

SubProgram "gles " {
Keywords { "DIRECTIONAL_COOKIE" }
"!!GLES"
}

SubProgram "glesdesktop " {
Keywords { "DIRECTIONAL_COOKIE" }
"!!GLES"
}

}
	}

#LINE 47
  
}
// Fallback to Diffuse
Fallback "Diffuse"
}
