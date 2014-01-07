// Unlit alpha-blended shader.
// - no lighting
// - no lightmap support
// - no per-material color

Shader "Unlit/TransparentGrey" {
Properties {
	_Grey ("Grey", Range (0, 1)) = 0
	_MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
}

SubShader {
	Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
	LOD 100
	
	ZWrite Off
	Blend SrcAlpha OneMinusSrcAlpha 
	
	Pass {  
		// shader program with errors was here
Program "err" { SubProgram { "!!error" } }

#LINE 65

	}
}

}
