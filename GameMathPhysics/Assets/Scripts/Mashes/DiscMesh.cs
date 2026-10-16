using JetBrains.Annotations;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DiscMesh : MonoBehaviour
{

    [SerializeField] public GameObject CenterPoint;
    [Range(1,20)]
    [SerializeField] public float Radius;
    [Range(10,1000)]
    [SerializeField] public int Segments;

    public Mesh discmesh;

    private void GenerateMesh()
    {
        if (discmesh == null)
        {
            discmesh = new Mesh();
        }
        else
        {
            discmesh.Clear();
        }

        List<Vector3> verts = new List<Vector3>();
        verts.Add(Vector3.zero);

        List<Vector2> uvs = new List<Vector2>();
        uvs.Add(new Vector2(0.5f, 0.5f));


        float delta_angle = 360f / Segments;
        Vector3 point = Vector3.zero;

        for (int i = 0; i < Segments; i++)
        {
            point.x = Mathf.Cos(Mathf.Deg2Rad * (delta_angle * i));
            point.y = Mathf.Sin(Mathf.Deg2Rad * (delta_angle * i));
            verts.Add(point*Radius);
            uvs.Add(new Vector2(point.x, point.y)*0.5f + new Vector2(0.5f, 0.5f));
        }

        

        List<int> tri_indexes = new List<int>();

        for (int i = 0; i < Segments-1; i++)
        {
            tri_indexes.Add(0);
            tri_indexes.Add(i + 2);
            tri_indexes.Add(i + 1);
        }

        // special case

        tri_indexes.Add(0);
        tri_indexes.Add(1);
        tri_indexes.Add(verts.Count-1);

        discmesh.SetVertices(verts); // Assign vertexes to the mesh
        discmesh.SetTriangles(tri_indexes, 0);
        discmesh.SetUVs(0, uvs);
        discmesh.RecalculateNormals();

    }

    private void OnDrawGizmos()
    {

        GenerateMesh();
        GetComponent<MeshFilter>().sharedMesh = discmesh;


        float delta_angle = 360f / Segments;



        Vector3 point = Vector3.zero;

        for (int i=0; i < Segments; i++)
        {

            point.x = Radius * Mathf.Cos(Mathf.Deg2Rad * (delta_angle * i));
            point.y = Radius * Mathf.Sin(Mathf.Deg2Rad * (delta_angle * i));
            Gizmos.DrawSphere(point, 0.1f);
        }
    }




    /*
    // The math is in this method
    void CalculateDiskMesh()
    {

        if (mesh == null)
        {
            mesh = new Mesh();
        }
        else
        {
            mesh.Clear();
        }

        // First we need to calculate the angle
        float angle = 360f / Segments;

        // now we need to calculate the coordinates of the vertices
        // The first one should be to the right of the center and have a radius
        Vector3 center = CenterPoint.transform.position;
        Vector3 right = center + new Vector3(Radius, 0, 0);

        for (int i = 0; i < Segments; i++)
        {
            // Calculate the angle in radians
            float rad = Mathf.Deg2Rad * angle * i;
            // Calculate the x and y coordinates
            float x = center.x + Radius * Mathf.Cos(rad);
            float y = center.y + Radius * Mathf.Sin(rad);
            // Create a new vertex
            Vector3 vertex = new Vector3(x, y, center.z);
            // Draw the vertex as a sphere
            Gizmos.DrawSphere(vertex, 0.1f);
            // Draw a line from the center to the vertex
            Gizmos.DrawLine(center, vertex);
            // Draw a line from the vertex to the next vertex
            if (i < Segments - 1)
            {
                float nextRad = Mathf.Deg2Rad * angle * (i + 1);
                float nextX = center.x + Radius * Mathf.Cos(nextRad);
                float nextY = center.y + Radius * Mathf.Sin(nextRad);
                Vector3 nextVertex = new Vector3(nextX, nextY, center.z);
                Gizmos.DrawLine(vertex, nextVertex);
            }
            else
            {
                // If it's the last vertex, connect it to the first vertex
                Gizmos.DrawLine(vertex, right);
            }

            // Now we need to make the actual mesh for that triangle
            List<Vector3> verts = new List<Vector3>();
            verts.Add(center);
            verts.Add(vertex);



        }

    }

    private void GenerateDiskMesh()
    {
        if (mesh == null)
        {
            mesh = new Mesh();
        }
        else
        {
            mesh.Clear();
        }


    }

    */



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
