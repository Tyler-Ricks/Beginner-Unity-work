Shader "Graph/Point Surface" {

    Properties {
        _Smoothness ("Smoothness", Range(0,1)) = 0.5 // set smoothness from the engine
    }

    SubShader{


        CGPROGRAM

        #pragma surface ConfigureSurface Standard fullforwardshadows
        #pragma target 3.0

        // Declare the variable that Unity sets via the Inspector
        float _Smoothness;

        struct Input {
            float3 worldPos; // World position of the fragment
        };

        // Surface function
        void ConfigureSurface (Input input, inout SurfaceOutputStandard surface) {
            // Use the world position for Albedo (converted to a color)
            surface.Albedo = input.worldPos.xyz * 0.5 + 0.5; // Normalize worldPos to range [0,1]
            //surface.Albedo = saturate(input.worldPos * 0.5 + 0.5);
            surface.Smoothness = _Smoothness; // Set smoothness from the property
        }

        ENDCG
    }

    Fallback "Diffuse"
}
