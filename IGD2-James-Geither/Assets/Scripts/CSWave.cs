using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSWave : MonoBehaviour
{
    // Use this for initialization
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        Mesh mesh = this.GetComponent<MeshFilter>().mesh;
        Vector3[] verts = mesh.vertices;
        for (var v = 0; v < verts.Length; v++)
        {
            verts[v].y = Random.Range(0, 10);
        }
        mesh.vertices = verts;
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
    }
}