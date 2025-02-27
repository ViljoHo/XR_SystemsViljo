using UnityEngine;
using System.Collections.Generic;

public class IceHoleCreator : MonoBehaviour
{
    public float holeRadius = 0.5f; // Reiän koko
    private MeshFilter meshFilter;
    private MeshCollider meshCollider;
    private Mesh mesh;

    void Start()
    {
        meshFilter = GetComponent<MeshFilter>();
        meshCollider = GetComponent<MeshCollider>();
        mesh = meshFilter.mesh;
    }

    public void CreateHole(Vector3 hitPoint)
    {
        Vector3[] vertices = mesh.vertices;
        List<int> verticesToRemove = new List<int>();

        for (int i = 0; i < vertices.Length; i++)
        {
            Vector3 worldPos = transform.TransformPoint(vertices[i]);
            float distance = Vector3.Distance(worldPos, hitPoint);

            if (distance < holeRadius)
            {
                verticesToRemove.Add(i);
            }
        }

        RemoveVertices(verticesToRemove);
    }

    private void RemoveVertices(List<int> verticesToRemove)
    {
        // Tee uudet mesh-verteksit, joista poistetaan osumakohdan verteksit
        List<Vector3> newVertices = new List<Vector3>(mesh.vertices);
        List<int> newTriangles = new List<int>();

        for (int i = 0; i < mesh.triangles.Length; i += 3)
        {
            if (!verticesToRemove.Contains(mesh.triangles[i]) &&
                !verticesToRemove.Contains(mesh.triangles[i + 1]) &&
                !verticesToRemove.Contains(mesh.triangles[i + 2]))
            {
                newTriangles.Add(mesh.triangles[i]);
                newTriangles.Add(mesh.triangles[i + 1]);
                newTriangles.Add(mesh.triangles[i + 2]);
            }
        }

        mesh.vertices = newVertices.ToArray();
        mesh.triangles = newTriangles.ToArray();
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        meshFilter.mesh = mesh;
        meshCollider.sharedMesh = null;
        meshCollider.sharedMesh = mesh; // Päivittää colliderin
    }
}
