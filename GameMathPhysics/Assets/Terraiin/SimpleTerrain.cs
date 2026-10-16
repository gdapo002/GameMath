using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class SimpleTerrain : MonoBehaviour
{
    [Range(1, 100)]
    public int Segments = 10;

    [Range(1f, 1000f)]
    public float Size = 10f;

    
    public float NoiseAmplitude = 10f;

    [Range(0.01f, 2.0f)]
    public float NoiseFrequency = 1.0f;

    [Range(0f, 20f)]
    public float NoiseOffsetX = 10f;

    [Range(0f, 20f)]
    public float NoiseOffsetY = 10f;

    public Mesh mymesh;

    private Mesh GenerateMesh()
    {  
        Mesh mesh = new Mesh();
        mesh.name = "SimplePlane";

        List<Vector3> verts = new List<Vector3>();
        List<Vector2> uvs = new List<Vector2>();
        List<int> tri_indexes = new List<int>();

        float delta = Size /(float) Segments;

        Vector3 point = Vector3.zero;
        for (int i = 0; i <= Segments; i++)
        {
            for (int j = 0; j <= Segments; j++)
            {
                point.x = j * delta;
                point.z = i * delta;
                point.y = Mathf.PerlinNoise(NoiseFrequency * point.x + NoiseOffsetX, NoiseFrequency * point.z + NoiseOffsetY ) * NoiseAmplitude;
                verts.Add(point);
                uvs.Add(new Vector2(j / (float) Segments, i / (float) Segments));
            }
        }

        for (int i = 0; i < Segments; i++)
        {
            for (int j = 0; j < Segments; j++)
            {
                int  lower_left = i * (Segments +1) + j;
                int lower_right = lower_left + 1;

                int upper_left = (i + 1) * (Segments + 1) + j;
                int upper_right = upper_left + 1;

                tri_indexes.Add(lower_left);
                tri_indexes.Add(upper_right);
                tri_indexes.Add(lower_right);

                tri_indexes.Add(lower_left);
                tri_indexes.Add(upper_left);
                tri_indexes.Add(upper_right);


            }
        }
        mesh.SetVertices(verts); // Assign vertexes to the mesh
        mesh.SetTriangles(tri_indexes, 0);
        mesh.SetUVs(0, uvs);
        mesh.RecalculateNormals();
        return mesh;
    }

    private void OnDrawGizmos()
    { 
     mymesh = GenerateMesh();
    GetComponent<MeshFilter>().sharedMesh = mymesh;
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
