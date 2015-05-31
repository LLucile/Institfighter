Shader "Custom/GraphViewer" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_FnAColor ("FnAColor", Color) = (1,0,0,1)
		_FnBColor ("FnAColor", Color) = (0,0,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
		
		_FuncA ("FuncA", Range(0, 17)) = 0
		_FuncB ("FuncB", Range(0, 17)) = 0
		_Operator ("Operator", Range(0, 3)) = 0
		_ValA ("ValA", Float) = 1.0
		_ValB ("ValB", Float) = 1.0
		_Score ("Score", Float) = 0.0		
	}
	SubShader {
		Tags { "RenderType" = "Transparent" }
		Tags { "Queue" = "Transparent" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard decal:add fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
			float4 screenPos;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;
		fixed4 _FnAColor;
		fixed4 _FnBColor;
		float _FuncA;
		float _FuncB;
		float _Operator;
		float _ValA;
		float _ValB;
		float _Score;
		
		float Hash (float2 p)
		{
		    float3 p2 = float3(p.xy, 1.0);
		    return frac(sin(dot(p2, float3(37.1, 61.7, 12.4))) * 3758.5453123);
		}

		float noise (float2 p)
		{
			p *= 200.;
		    float2 i = floor(p);
		    float2 f = frac(p);
		    f *= f * (3.0 - 2.0 * f);

		    return lerp(lerp(Hash(i + float2(0., 0.)), Hash(i + float2(1., 0.)), f.x),
		                lerp(Hash(i + float2(0., 1.)), Hash(i + float2(1., 1.)), f.x),
		                f.y);
		}

		float root (float val)
		{
		    return pow(val, 1. / _ValA);
		}

		float sinh (float val)
		{
		    return (exp(val) - exp(-val)) / 2.;
		}

		float cosh (float val)
		{
		    return (exp(val) + exp(-val)) / 2.;
		}

		float tanh (float val)
		{
		    return (exp(2. * val) - 1.) / (exp(2. * val) + 1.);
		}

		float asinh (float val)
		{
		    return log(val + sqrt(val * val + 1.));
		}

		float acosh (float val)
		{
		    return log(val + sqrt(val + 1.) * sqrt(val - 1.));
		}

		float atanh (float val)
		{
		    return 0.5 * log(1. + val) - 0.5 * log(1. - val);
		}

		float computeValue (int func, float val)
		{		
			const int fn_exp = 0;
			const int fn_ln      = 1;
			const int fn_ax      = 2;
			const int fn_b       = 3;
			const int fn_sqrt    = 4;
			const int fn_pow     = 5;
			const int fn_sin     = 6;
			const int fn_arcsin  = 7;
			const int fn_sinh    = 8;
			const int fn_arcsinh = 9;
			const int fn_cos     = 10;
			const int fn_arccos  = 11;
			const int fn_cosh    = 12;
			const int fn_arccosh = 13;
			const int fn_tan     = 14;
			const int fn_arctan  = 15;
			const int fn_tanh    = 16;
			const int fn_arctanh = 17;
		
		    if (func == fn_exp)     return exp(val);		    
		    if (func == fn_ln)      return log(val);		    
		    if (func == fn_ax)      return _ValA * val;		    
		    if (func == fn_b)       return _ValB;		    
		    if (func == fn_sqrt)    return root(val);		    
		    if (func == fn_pow)     return pow(val, _ValA);		    
		    if (func == fn_sin)     return sin(val);		    
		    if (func == fn_arcsin ) return asin(val);
		    if (func == fn_sinh   ) return sinh(val);
		    if (func == fn_arcsinh) return asinh(val);
		    if (func == fn_cos    ) return cos(val);
		    if (func == fn_arccos ) return acos(val);
		    if (func == fn_cosh   ) return cosh(val);
		    if (func == fn_arccosh) return acosh(val);
		    if (func == fn_tan    ) return tan(val);
		    if (func == fn_arctan ) return atan(val);
		    if (func == fn_tanh   ) return tanh(val);
		    if (func == fn_arctanh) return atanh(val);
		    
		    return 0. / 0.;
		}

		float computeOperator (int op, float a, float b)
		{
			const int op_plus  = 0;
			const int op_minus = 1;
			const int op_mult  = 2;
			const int op_div   = 3;
			
			if (isnan(a))
				return a;
				
			if (isnan(b))
				return b;
		
		    if (op == op_plus ) return a + b;
		    if (op == op_minus) return a - b;
		    if (op == op_mult ) return a * b;
		    if (op == op_div  ) return a / b;

		    return 0. / 0.;
		}

		float4 drawMark (float2 nuv, float2 screenPos)
		{
			const float markEdge = 0.0125;
			
		    float4 c = float4(0., 0., 0., 0.);

		    if ((nuv.x >= -markEdge) && (nuv.x <= markEdge))
		    {
		    	float v = noise(screenPos) + 1.;
		        c += float4(v, v, v, v);
		    }

		    if (nuv.y > -markEdge && nuv.y < markEdge)
		    {
		    	float v = noise(screenPos) + 1.;
		        c += float4(v, v, v, v);
		    }

		    return c;
		}

		float4 plot (float2 uv, float2 screenPos, float y, float absScoreResult)
		{
			const float edge = 0.025 * absScoreResult;
			
			if (isnan(y))
				return float4(0., 0., 0., 0.);
		
		    float dist = abs(uv.y - y);
			
			if (isnan(dist))
				return float4(0., 0., 0., 0.);

		    if (dist < edge)
		    {
		    	float v = noise(screenPos) + 1.;
		        return float4(v, v, v, v);
		    }

		    return float4(0., 0., 0., 0.);
		}

		void surf (Input IN, inout SurfaceOutputStandard o)
		{
			float vsA = computeValue(int(_FuncA), _Score);
			float vsB = computeValue(int(_FuncB), _Score);
			float absScoreResult = max(abs(computeOperator(_Operator, vsA, vsB)), 1.);
			float absScore = max(abs(_Score), 1.);

			float2 nuv = IN.uv_MainTex.xy * 2. - 1.;
			float2 uv = nuv;
			nuv.x *= 2.25; // Board object ratio
			uv.x *= absScore;
			uv.y *= absScoreResult;

			float vA = computeValue(int(_FuncA), uv.x);
			float vB = computeValue(int(_FuncB), uv.x);
			float vR = computeOperator(_Operator, vA, vB);

			// Albedo comes from a texture tinted by color
			fixed4 c = drawMark(nuv, IN.screenPos.xy) * _Color;
			c += plot(uv, IN.screenPos.xy, vA, absScoreResult) * _FnAColor;
			c += plot(uv, IN.screenPos.xy, vB, absScoreResult) * _FnBColor;
			c += plot(uv, IN.screenPos.xy, vR, absScoreResult);
			
			o.Albedo = c.rgb;
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
