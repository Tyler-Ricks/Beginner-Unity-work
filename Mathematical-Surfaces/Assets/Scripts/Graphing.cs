//Script that animates a given function using a number of boxes based on a given resolution


using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;


public class Graphing : MonoBehaviour
{
    [SerializeField]
    Transform pointPreFab;

    [SerializeField, Range(1,100)] //how many cubes are generated. one extra cube is added so it looks nicer
    int resolution = 10;

    /*[SerializeField, Range(-50,50)] //bounds by which to generate the function
    int lowerBound = -1, upperBound = 1;
    float horizontalShift = 0, verticalShift = 0, horizontalStretch = 1, verticalStretch = 1;*/

    [SerializeField]
    FunctionLibrary.FunctionName function;

    Transform[] points;
    void Awake()
    { 
        float step = 2f / resolution;
        var scale = Vector3.one * step;
        //var position = Vector3.zero;
        points = new Transform[resolution * resolution];

        for (int i = 0; i < points.Length; i++) { //loop creates n instances of point, each scaled according to the bounds
            Transform point = points[i] = Instantiate(pointPreFab); //creates the cube object
            point.localScale = scale; //scales the cubes to prevent overlap
            point.SetParent(transform, false);
        }

    }

    private void Update()
    {
        FunctionLibrary.Function func = FunctionLibrary.getFunction(function);
        //Debug.Log("points: " + points.Length);

        float time = Time.time;
        float step = 2f / resolution;

        float v = 0.5f * step - 1f;
        for (int i = 0, x = 0, z = 0; i < points.Length; i++, x++)
        {
            if( x == resolution)
            {
                x = 0;
                z += 1;
                v = (z + 0.5f) * step - 1f; //v only needs to be recalculated when we start a new layer 
            }
            float u = (x + 0.5f) * step - 1f;
            //float v = (z + 0.5f) * step - 1f;
            points[i].localPosition = func(u, v, time);

            /*Transform point = points[i];
            var position = point.localPosition;
            //position.y = FunctionLibrary.wave(position.x, time); // calls a function from the FunctionLibrary script for the y value

            position.y = func(position.x, position.z, time);

            point.localPosition = position; */ //old stuff that got replaced whwen we went from a function on the xz plane to just individually plotting
        }
    }
}
