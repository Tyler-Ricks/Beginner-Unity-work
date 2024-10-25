using UnityEngine;
using UnityEngine.UIElements;

public class Graphing : MonoBehaviour
{
    [SerializeField]
    Transform pointPreFab;

    [SerializeField, Range(1,100)] //how many cubes are generated. one extra cube is added so it looks nicer
    int resolution = 10;

    [SerializeField, Range(-50,50)] //bounds by which to generate the function
    int lowerBound = -1, upperBound = 1;
    float horizontalShift = 0, verticalShift = 0, horizontalStretch = 1, verticalStretch = 1;

    Transform[] points;
    void Awake()
    {
        Transform point;    
        float scale = ((upperBound - lowerBound) / ((float) resolution));
        var position = Vector3.zero;
        points = new Transform[resolution + 1];

        for (int i = 0; i <= resolution; i++) { //loop creates n instances of point, each scaled according to the bounds
            point = Instantiate(pointPreFab); //creates the cube object
            points[i] = point;

            position.x = lowerBound + (i * scale); //x values of our function
            //position.y = position.x * position.x * position.x; //y values of our function
            position.z = i * scale;
            point.localPosition = position; //shifts the created box to its proper place

            point.localScale = Vector3.one * scale; //scales the cubes to prevent overlap
        }

        /*point.localPosition = Vector3.right;

        point = Instantiate(pointPreFab);
        point.localPosition = Vector3.right * 2f;*/
    }

    private void Update()
    {
        float time = Time.time;
        for(int i = 0; i < points.Length; i++)
        {
            Transform point = points[i];
            Vector3 position = point.localPosition;
            position.y = Mathf.Sin(Mathf.PI * (position.x + time));
            point.localPosition = position;

            
        }
    }
}
