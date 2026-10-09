using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class QuadMesh : MonoBehaviour
{
    public Mesh quadmesh;

    private Vector3 v0 = Vector3.zero;
    private Vector3 v1 = Vector3.right;
    private Vector3 v2 = Vector3.up;
    private Vector3 v3 = new Vector3(1, 1, 0);

    private void OnDrawGizmos()
    {
        GenerateMesh();
        GetComponent<MeshFilter>().sharedMesh = quadmesh;
        Gizmos.DrawSphere(transform.position + v0, 0.1f);
        Gizmos.DrawSphere(transform.position + v1, 0.1f);
        Gizmos.DrawSphere(transform.position + v2, 0.1f);
        Gizmos.DrawSphere(transform.position + v3, 0.1f);
    }

    private void GenerateMesh()
    {
    if (quadmesh == null)
        {
        quadmesh = new Mesh();
        }
    else
        {
            quadmesh.Clear();
        }


    List<Vector3> verts = new List<Vector3>();
    verts.Add(v0);
    verts.Add(v1);
    verts.Add(v2);
    verts.Add(v3);

    int[] trl_indices = new int[6];
    trl_indices[0] = 0;
    trl_indices[1] = 2;
    trl_indices[2] = 3;

    trl_indices[3] = 0;
    trl_indices[4] = 3;
    trl_indices[5] = 1;

    quadmesh.SetVertices(verts);
    quadmesh.SetTriangles(trl_indices, 0);
    quadmesh.RecalculateNormals();
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
