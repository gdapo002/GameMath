using System.Collections.Generic;
using UnityEngine;

public class DiscMesh : MonoBehaviour
{
    [Range(0.1f, 20f)]
    public float Radius = 5.0f;

    [Range(3, 264)]
    public int Segments = 10;

    private void Start()
    {
        GenerateMesh();
    }

    private void GenerateMesh()
    {
        Mesh discmesh = new Mesh();

        List<Vector3> verts = new List<Vector3>();
        List<Vector2> uvs = new List<Vector2>();
        uvs.Add(new Vector2(0.5f, 0.5f));
        List<int> trl_indices = new List<int>();

        
        verts.Add(Vector3.zero);

        
        float delta_angle = 2f * Mathf.PI / Segments;

        
        for (int i = 0; i < Segments; i++)
        {
            float angle = delta_angle * i;
            float x = Radius * Mathf.Cos(angle);
            float z = Radius * Mathf.Sin(angle);
            verts.Add(new Vector3(x, 0f, z));
        }

        
        for (int i = 0; i < Segments; i++)
        {
            int current = i + 1;
            int next = ((i + 1) % Segments) + 1;

            trl_indices.Add(0);        
            trl_indices.Add(next);     
            trl_indices.Add(current);   
        }

        
        discmesh.SetVertices(verts);
        discmesh.SetTriangles(trl_indices, 0);
        discmesh.RecalculateNormals();

        
        GetComponent<MeshFilter>().sharedMesh = discmesh;
    }

    private void OnDrawGizmos()
    {
        
        float delta_angle = 2f * Mathf.PI / Segments;
        for (int i = 0; i < Segments; i++)
        {
            float angle = delta_angle * i;
            Vector3 point = new Vector3(
                Radius * Mathf.Cos(angle),
                0f,
                Radius * Mathf.Sin(angle)
            );
            Gizmos.color = Color.cyan;
            Gizmos.DrawSphere(point, 0.1f);
        }

        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(Vector3.zero, 0.15f);
    }
}