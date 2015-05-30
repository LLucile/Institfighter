#version 100

precision mediump float;

uniform int u_int_funcA;
uniform int u_int_funcB;
uniform int u_int_operator;
uniform float u_float_valueToCompute;
uniform float u_float_a;
uniform float u_float_b;
uniform vec2 u_vec2_resolution;

const float edge = 0.1;
const float markEdge = 0.1;

const int fn_exp     = 0;
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

float root (float val)
{
    return pow(val, 1. / u_float_a);
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
    if (func == fn_exp)     return exp(val);
    if (func == fn_ln)      return log(val);
    if (func == fn_ax)      return u_float_a * val;
    if (func == fn_b)       return u_float_b;
    if (func == fn_sqrt)    return root(val);
    if (func == fn_pow)     return pow(val, u_float_a);
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
    return -1.;
}

const int op_plus  = 0;
const int op_minus = 1;
const int op_mult  = 2;
const int op_div   = 3;

float computeOperator (int op, float a, float b)
{
    if (op == op_plus ) return a + b;
    if (op == op_minus) return a - b;
    if (op == op_mult ) return a * b;
    if (op == op_div  ) return a / b;

    return 0.;
}

float computeValue (int func, float val);
vec4 drawMark (vec2 uv);
vec4 plot (vec2 uv, float y);

void main (void)
{
    vec2 uv = (gl_FragCoord.xy / u_vec2_resolution.xy) * 2.0 - 1.0;
    uv *= abs(u_float_valueToCompute) + 10.;
    //uv.x *= u_vec2_resolution.x / u_vec2_resolution.y;
    //uv.xy = uv.yx;

    vec4 finalColor = drawMark(uv) * .5;
    float vA = computeValue(u_int_funcA, uv.x);
    float vB = computeValue(u_int_funcB, uv.x);
    float vR = computeOperator(u_int_operator, vA, vB);
    finalColor += plot(uv, vA) * vec4(1., 0., 0., 1.);
    finalColor += plot(uv, vB) * vec4(0., 0., 1., 1.);
    finalColor += plot(uv, vR) * vec4(1., 1., 1., 1.);

    gl_FragColor = finalColor;
}

vec4 plot (vec2 uv, float y)
{
    float dist = abs(uv.y - y);

    if (dist < edge)
        return vec4(smoothstep(1., 0., dist / edge));

    return vec4(0.);
}

vec4 drawMark (vec2 uv)
{
    vec4 c = vec4(0.);

    if (uv.x >= -markEdge && uv.x <= markEdge)
    {
        if (uv.x < 0.)
            c += vec4(smoothstep(-markEdge, 0., uv.x));
        else
            c += vec4(smoothstep(markEdge, 0., uv.x));
    }

    if (uv.y > -markEdge && uv.y < markEdge)
    {
        if (uv.y < 0.)
            c += vec4(smoothstep(-markEdge, 0., uv.y));
        else
            c += vec4(smoothstep(markEdge, 0., uv.y));
    }

    return c;
}