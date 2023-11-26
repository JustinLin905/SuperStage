using UnityEngine;
using UnityEditor;

public class FlipNormals : MonoBehaviour
{
    void Start()
    {
        MeshFilter meshFilter = GetComponent<MeshFilter>();

        if (meshFilter != null)
        {
            // Get the mesh
            Mesh mesh = meshFilter.mesh;

            // Flip the normals
            Vector3[] normals = mesh.normals;
            for (int i = 0; i < normals.Length; i++)
            {
                normals[i] = -normals[i];
            }

            // Assign the flipped normals back to the mesh
            mesh.normals = normals;

            // Invert triangle winding order to ensure correct rendering
            int[] triangles = mesh.triangles;
            for (int i = 0; i < triangles.Length; i += 3)
            {
                int temp = triangles[i];
                triangles[i] = triangles[i + 2];
                triangles[i + 2] = temp;
            }

            // Assign the modified triangles back to the mesh
            mesh.triangles = triangles;


            string path = "Assets/NewMesh.asset";
            AssetDatabase.CreateAsset(mesh, path);
            AssetDatabase.SaveAssets();

        }
    }
}