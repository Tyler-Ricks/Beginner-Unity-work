using UnityEngine;
using static UnityEngine.Mathf;

public static class FunctionLibrary
{

    public delegate Vector3 Function(float u, float v, float t);

    public enum FunctionName {wave, multiWave, ripple, sphere, torus}

    static Function[] functions = { wave, multiWave, ripple, sphere, torus}; //array of functions so we can reference each without using nested conditionals

    public static Function getFunction(FunctionName name)
    {
        return (functions[(int)name]);
    }


    public static Vector3 wave(float u, float v, float t)
    {
        Vector3 p;
        p.x = u;
        p.y = Sin(PI * (u + v + t));
        p.z = v;

        return (p);
    }

    public static Vector3 multiWave(float u, float v, float t)
    {
        Vector3 p;

        p.x = u;
        p.y = Sin(PI * (u + 0.5f * t));
        p.y += 0.5f * Sin(2f * PI * (v + t));
        p.y += Sin(PI * (u + v + 0.25f * t));
        p.y *= 1f / 2.5f;
        p.z = v;

        return (p);

        /*float y = Sin(PI * (u + 0.5f * t));
        y += 0.5f * Sin(2f * PI * (v + t));
        y += Sin(PI * (u + v + 0.25f * t));
        return (y * (1f / 2.5f));*/
    }

    public static Vector3 ripple(float u, float v, float t)
    {
        Vector3 p;

        p.x = u;
        float d = Sqrt(u * u + v * v);
        p.y = Sin(PI * (4f * d - t));
        p.y /= (1f + 10f * d);
        p.z = v;

        return (p);
        /*float d = Sqrt(u * u + v * v);
        float y = Sin(PI * (4f * d - t));
        return (y / (1f + 10f * d));*/
    }

    public static Vector3 sphere(float u, float v, float t)
    {
        //float r = Cos(PI * 0.5f * v); //For every circle on the xz plane we generate, the radius of the circle depends on the y value
        //float r = 0.5f + 0.5f * Sin(PI * t); //uniform expanding/shrinking radius
        //float r = 0.9f + 0.1f * Sin(8f * PI * u); //vertical bands; radius changes as a result of x values
        float r = 0.9f + 0.1f * Sin(PI * (6f * u + 4f * v + t)); //swirly bands, radius changes for x and z values.
        float s = r * Cos(0.5f * PI * v);
        Vector3 p;
        p.x = s * Sin(PI * u);
        p.y = r * Sin(PI * 0.5f * v); //y starts at 0 for our initial input of -PI, so Sin
        p.z = s * Cos(PI * u);

        return (p);
    }

    public static Vector3 torus(float u, float v, float t)
    {
        //float r = 1f;
        float rmajor = 0.7f + 0.1f * Sin(PI * (6f * u + 0.5f * t)); //actual radius of the torus
        float rminor = 0.15f + 0.05f * Sin(PI * (8f * u + 4f * v + 2f * t)); //thickness of the torus
        float s = rmajor + rminor * Cos(PI * v);
        Vector3 p;
        p.x = s * Sin(PI * u);
        p.y = rminor * Sin(PI * v);
        p.z = s * Cos(PI * u);
        return p;
    }
}
