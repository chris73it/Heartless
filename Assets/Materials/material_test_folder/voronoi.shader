Shader "Unlit/voronoi" {
    Properties {
        _MainTex ("Texture", 2D) = "white" {}
        _CellSize ("Cell Size", range(1.0,64.0)) = 1.0
        [KeywordEnum(Normal, NSphereRadius, DistanceToEdge)] _VoronoiType("Voronoi Type", float) = 0
    }
    SubShader {
        Tags { "RenderType" = "Opaque" }
        LOD 100

        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fog
            #pragma multi_compile _OVERLAY_NORMAL _OVERLAY_NSPHERERADIUS _OVERLAY_DISTANCETOEDGE

            #include "UnityCG.cginc"

            struct appdata {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
                float3 worldPos : TEXCOORD1;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _CellSize;
            int _VoronoiType;

            v2f vert (appdata v) {
                v2f o;
                o.worldPos = mul(unity_ObjectToWorld, v.vertex);
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            float rand2dTo1d(float2 value, float2 dotDir = float2(12.9898, 78.233)) {
                float2 smallValue = sin(value);
                float random = dot(smallValue, dotDir);
                random = frac(sin(random) * 143758.5453);
                return random;
            }

            float2 rand2dTo2d(float2 value) {
                return float2(
                    rand2dTo1d(value, float2(12.989, 78.233)),
                    rand2dTo1d(value, float2(39.346, 11.135))
                    );
            }

            float rand3dTo1d(float3 value, float3 dotDir = float3(12.9898, 78.233, 37.719)) {
                float3 smallValue = sin(value);
                float random = dot(smallValue, dotDir);
                random = frac(sin(random)*143758.5453);
                return random;
            }

            float3 rand3dTo3d(float3 value) {
                return float3(
                    rand3dTo1d(value, float3(12.989, 78.233, 37.719)),
                    rand3dTo1d(value, float3(39.346, 11.135, 83.155)),
                    rand3dTo1d(value, float3(73.156, 52.235, 09.151))
                    );
            }

            float3 voronoiNoise(float2 value, int type) {
                float2 baseCell = floor(value);

                float minDistToCell = 10;
                float2 toClosestCell;
                float2 closestCell;
                [unroll]
                for (int x = -1; x <= 1; x++) {
                    [unroll]
                    for (int y = -1; y <= 1; y++) {
                        float2 cell = baseCell + float2(x, y);
                        float2 cellPosition = cell + rand2dTo2d(cell);
                        float2 toCell = cellPosition - value;
                        float distToCell = length(toCell);
                        if (distToCell < minDistToCell) {
                            minDistToCell = distToCell;
                            closestCell = cell;
                            toClosestCell = toCell;
                        }
                    }
                }

                float minEdgeDistance = 10;
                [unroll]
                for(int x2=-1; x2<=1; x2++){
                    [unroll]
                    for(int y2=-1; y2<=1; y2++){
                        float2 cell = baseCell + float2(x2, y2);
                        float2 cellPosition = cell + rand2dTo2d(cell);
                        float2 toCell = cellPosition - value;

                        float2 diffToClosestCell = abs(closestCell - cell);
                        bool isClosestCell = diffToClosestCell.x + diffToClosestCell.y < 0.1;
                        if(!isClosestCell){
                            float2 toCenter = (toClosestCell + toCell) * 0.5;
                            float2 cellDifference = normalize(toCell - toClosestCell);
                            float edgeDistance = dot(toCenter, cellDifference);
                            minEdgeDistance = min(minEdgeDistance, edgeDistance);
                        }
                    }
                }

                float random = rand2dTo1d(closestCell);

                /*f (type == 1) { return random; }
                if (type == 2)  { return minEdgeDistance; }
                else            { return minDistToCell; }
                */
                return float3(minDistToCell, random, minEdgeDistance);
            }

            fixed4 frag(v2f i) : SV_Target{
                float2 value = i.uv.xy * _CellSize;
                float3 noise = voronoiNoise(value, _VoronoiType);
                fixed4 col = 0.0;
                col.rgb = noise.z;
                col.a = 1.0;
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
}
