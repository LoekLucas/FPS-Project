using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshCollider))]
[RequireComponent(typeof(MeshRenderer))]


public class MeshGenerator : MonoBehaviour
{
    Mesh mesh; // Sets a mesh variable
    Vector3[] vertices; // Sets a vector3 
    int[] triangles; // ?

    void Start()
    {
        mesh = new Mesh(); // Makes a new mesh and sets the mesh variable to that new mesh
        GetComponent<MeshFilter>().mesh = mesh; // Get meshfilter component

        CreateShape();
        UpdateMesh();

        GetComponent<MeshCollider>().sharedMesh = mesh; // ?
        GetComponent<MeshCollider>().convex = true; // ?

    }

    void CreateShape()
    {
        vertices = new Vector3[]
        {
            // These are all coordinates for bounderied for the mesh
            new Vector3 (0f, 0f, 0f), // 0
            new Vector3 (1f, 0f, 0f), // 1
            new Vector3 (0f, 0f, 1f), // 2
            new Vector3 (1f, 0f, 1f), // 3
            new Vector3 (0.5f, 1f, 0.5f), // 4
            new Vector3 (0.5f, -1f, 0.5f) // 5
        };

        triangles = new int[]
        {
            // The coordinates get applied here
            4, 1, 0,
            4, 0, 2,
            4, 2, 3,
            4, 3, 1,
            0, 1, 5,
            2, 0, 5,
            3, 2, 5,
            1, 3, 5,
        };
    }

    void UpdateMesh()
    {
        mesh.Clear(); // Resets the mesh

        mesh.vertices = vertices; // ?
        mesh.triangles = triangles; // Makes the mesh generate triangles
    }
}
