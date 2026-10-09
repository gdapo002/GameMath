using UnityEditor;
using UnityEngine;

public class BezierPath : MonoBehaviour
{

    // Array of BezierPoints
    public BezierPoint[] Points;

    // Should we make a loop?
    public bool Loop = true;
    
    [Range(0f, 1f), Tooltip("T-value for the whole path")]
    public float TValue = 0.0f;

    
    
    private void OnDrawGizmos()
    {
        // This is the number of bezier segments in the whole path
        int segments = Points.Length - 1;

        if (segments <= 0) {
            return;
        }

        if (TValue < 0f || TValue > 1f) {
            return;
        }

        // Draw the bezier path using handles
        for (int i=0;i<segments; i++)
        {
            Handles.DrawBezier(Points[i].GetAnchor(), Points[i + 1].GetAnchor(),
                               Points[i].GetControlEnd(), Points[i + 1].GetControlStart(),
                               Color.magenta, null, 5f);
        }

        if (Loop) {
            Handles.DrawBezier(Points[segments].GetAnchor(), Points[0].GetAnchor(),
                               Points[segments].GetControlEnd(), Points[0].GetControlStart(),
                               Color.magenta, null, 5f);
        }


        // Compute the start index by taking the TValue and multiplying by number of segments
        // then getting the integer part
        int startIndex = (int)( TValue * segments);
        if (Loop)
            startIndex = (int)( TValue * (segments+1));
        
        //Debug.Log("startIndex:" + startIndex);

        /*
        float t_per_segment = 1.0f / segments;
        int startIndex = (int) (TValue / t_per_segment);
        Debug.Log("Start index: " + startIndex);
        */

        // Compute the t for current segment by first multiplying TValue (t of the whole path)
        // by the number of segments and then subtracting the startIndex 
        float t = TValue * segments - startIndex;
        if (Loop)
            t = TValue * (segments + 1) - startIndex;
        
        //Debug.Log("t: " + t);

        Vector3 point = new Vector3();
        
        // If we are looping and at the last segment (startIndex equal or larger than segments)
        if (startIndex >= segments && Loop)
        {
            if (TValue < 1f) {
                point = CalculateBezier(Points[startIndex].GetAnchor(),
                    Points[startIndex].GetControlEnd(),
                    Points[0].GetControlStart(),
                    Points[0].GetAnchor(), t);
            }
            else {
                // TValue is 1, so just return the last anchor point
                point = Points[0].GetAnchor();
            }
        }
        else {
            if (TValue < 1f) {
                // Calculate Bezier point from Points[startIndex] -- Points[startIndex+1]
                point = CalculateBezier(Points[startIndex].GetAnchor(),
                    Points[startIndex].GetControlEnd(),
                    Points[startIndex + 1].GetControlStart(),
                    Points[startIndex + 1].GetAnchor(), t);
            }
            else {
                // TValue is 1, so just return the last anchor point
                point = Points[segments].GetAnchor();
            }
        }


        Gizmos.color = Color.white;
        Gizmos.DrawSphere(point, 0.1f);

    }

    private Vector3 CalculateBezier(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        // Lerp
        Vector3 x = Interpolate(p0, p1, t);
        Vector3 y = Interpolate(p1, p2, t);
        Vector3 z = Interpolate(p2, p3, t);

        Vector3 r = Interpolate(x, y, t);
        Vector3 s = Interpolate(y, z, t);

        return Interpolate(r, s, t);
    }

    private Vector3 Interpolate(Vector3 a, Vector3 b, float t)
    {
        return a + (b - a) * t;  // Or: (1-t)*a + t*b
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
