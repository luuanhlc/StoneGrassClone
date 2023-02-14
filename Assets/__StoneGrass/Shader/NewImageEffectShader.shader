Shader "Hidden/NewImageEffectShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;

            fixed4 frag (v2f i) : SV_Target
            {
                float2 uv = fragCoord.xy / iResolution.xy * 2. - 1.;
	            uv.x *= iResolution.x / iResolution.y;
	
            	float3 pos = float3(uv, 0.);
            	pos += float3(sin(.3*sin(iTime*.18)), 0., sin(.2*cos(iTime*.18)))*20.;
	            pos += float3(30., 5.+height(pos.xz), 10.);

	            ray_t ray = lookAtDir(normalize(float3(uv, 3.)), pos, float3(0.));
	
	            float3 color = float3(0.);

	            xs_t xs = empty_xs(c_maxdist);
	            xs_t terr = trace_terrain(ray, xs.l);
	            if (terr.l < xs.l) {
		        xs = trace_grass(ray, terr.l, terr.l+c_grassmaxdist);
	            }
	
            	if (xs.l < c_maxdist) {
		        color = shade_grass(xs);
		        color = lerp(color, c_skycolor, smoothstep(c_maxdist*.35, c_maxdist, xs.l));
	            } else {
		        color = c_skycolor;
	        }
	
	// gamma correction is for those who understand it
	//fragColor = float4(pow(color, float3(2.2)), 1.);
	fragColor = float4(color, 1.);
            }
            ENDCG
        }
    }
}
