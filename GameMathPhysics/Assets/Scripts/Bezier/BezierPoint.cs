using UnityEditor;
using UnityEngine;

public class BezierPoint : MonoBehaviour
{

    public GameObject ControlStart;
    public GameObject ControlEnd;

    private bool UpdateNeeded = false;

    public bool DrawGizmos = true; 

    public Vector3 GetAnchor()
    {
        return transform.position;
    }

    public Vector3 GetControlStart()
    {
        return ControlStart.transform.position;
    }

    public Vector3 GetControlEnd()
    {
        return ControlEnd.transform.position;
    }

    void UpdateControlPoints() {
        if (ControlStart.transform.hasChanged) {
            Vector3 v = ControlStart.transform.position - transform.position;
            Vector3 w = ControlEnd.transform.position - transform.position;

            Vector3 p = Vector3.Project(w, v);
            ControlEnd.transform.position = transform.position + p;

        }
        else if (ControlEnd.transform.hasChanged) {
            Vector3 v = ControlStart.transform.position - transform.position;
            Vector3 w = ControlEnd.transform.position - transform.position;

            Vector3 p = Vector3.Project(v, w);
            ControlStart.transform.position = transform.position + p;
        }
        ControlEnd.transform.hasChanged = false;
        ControlStart.transform.hasChanged = false;
        transform.hasChanged = false;
        UpdateNeeded = false;
    }
    
    

    private void OnDrawGizmos()
    {
        if (transform.hasChanged || ControlStart.transform.hasChanged || ControlEnd.transform.hasChanged) {
            UpdateNeeded = true;
        }

        if (UpdateNeeded) {
            UpdateControlPoints();
        }

        if (DrawGizmos) {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position, 0.1f);

            Gizmos.color = Color.green;
            Gizmos.DrawSphere(ControlStart.transform.position, 0.1f);
            Gizmos.color = Color.darkGreen;
            Gizmos.DrawSphere(ControlEnd.transform.position, 0.1f);

            Handles.color = Color.hotPink;
            Handles.DrawLine(transform.position, ControlStart.transform.position, 1f);
            Handles.DrawLine(transform.position, ControlEnd.transform.position, 1f);
        }
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
