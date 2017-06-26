using UnityEngine;
using System.Collections;


public class LowPolyWater : MonoBehaviour
{
    //public Vector2 range = new Vector2(0.1f, 0.3f);
    public float speed = 1;
    double[] randomTimes;
    Mesh mesh;
    //MeshRenderer mr;
    //Material material;
    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        Vector3[] oldVerts = mesh.vertices;
        Vector4[] oldTangents = mesh.tangents;
        Vector2[] oldUVs = mesh.uv;
        int[] triangles = mesh.triangles;
        Vector3[] newVerts = new Vector3[triangles.Length];
        Vector4[] newTangents = new Vector4[triangles.Length];
        Vector2[] newUVs = new Vector2[triangles.Length];
        for (int i = 0; i < triangles.Length; i++)
        {
            newVerts[i] = oldVerts[triangles[i]];
            newTangents[i] = oldTangents[triangles[i]];
            newUVs[i] = oldUVs[triangles[i]];
            triangles[i] = i;
        }
        mesh.vertices = newVerts;
        mesh.tangents = newTangents;
        mesh.triangles = triangles;
        mesh.uv = newUVs;
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
        int j = 0;
        randomTimes = new double[mesh.vertices.Length];
        while ( j < mesh.vertices.Length)
        {
            randomTimes[j] = Mathf.PerlinNoise((mesh.vertices[j].x/5)*0.5f + 0.5f,(mesh.vertices[j].z/5)*0.5f + 0.5f);
            j++;
        }

    }

    void Update()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        Vector3[] vertices = mesh.vertices;
        int[] triangles = mesh.triangles;
        int j = 0;
        while (j < vertices.Length)
        {
            vertices[j].y = 1 * Mathf.PingPong(Time.time * speed, (float)randomTimes[j]);
            j++;
        }
        
        // Debug.Log(vertices[2]);

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
        
    }
}
