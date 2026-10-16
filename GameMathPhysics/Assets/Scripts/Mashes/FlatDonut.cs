using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class FlatDonut : MonoBehaviour
{
    [Range(0.1f, 20f)]
    public float InnerRadius = 1.0f;

    [Range(0.1f, 20f)]
    public float OuterRadius = 3.0f;

    [Range(3, 264)]
    public int Segments = 12;

    private void GenerateMesh()
    {
        Mesh donutmesh = new Mesh();

        List<Vector3> verts = new List<Vector3>();
        List<Vector2> uvs = new List<Vector2>();
        List<int> trl_indices = new List<int>();

        float delta_angle = 360f / Segments;
        Vector3 point = Vector3.zero;

        for (int i = 0; i < Segments; i++)
        {
            point.x = Mathf.Cos(Mathf.Deg2Rad * delta_angle * i);
            point.y = Mathf.Sin(Mathf.Deg2Rad * delta_angle * i);
            
            verts.Add(point*InnerRadius);
            Vector2 uvcoord = new Vector2(point.x, point.y);
            uvs.Add(0.5f * uvcoord *(InnerRadius/OuterRadius) + new Vector2(0.5f, 0.5f));


            verts.Add(point*OuterRadius);
            uvs.Add(0.5f * uvcoord + new Vector2(0.5f, 0.5f));
        }

        for (int i = 0; i <= Segments - 2; i++)
        {
            int innerCurrent = 2 * i;
            int outerCurrent = 2 * i + 1;
            int innerNext = 2 * (i + 1);
            int outerNext = 2 * (i + 1) + 1;

            trl_indices.Add(innerCurrent);
            trl_indices.Add(outerNext);
            trl_indices.Add(outerCurrent);

            trl_indices.Add(innerCurrent);
            trl_indices.Add(innerNext);
            trl_indices.Add(outerNext);
        }

        int lastInner = 2 * (Segments - 1);
        int lastOuter = 2 * (Segments - 1) + 1;

        trl_indices.Add(lastInner);
        trl_indices.Add(1);
        trl_indices.Add(lastOuter);

        trl_indices.Add(lastInner);
        trl_indices.Add(0);
        trl_indices.Add(1);

        donutmesh.SetVertices(verts);
        donutmesh.SetTriangles(trl_indices, 0);
        donutmesh.SetUVs(0,uvs);
        donutmesh.RecalculateNormals();

        GetComponent<MeshFilter>().sharedMesh = donutmesh;
    }

    private void OnDrawGizmos()
    {
        GenerateMesh();
    }
}