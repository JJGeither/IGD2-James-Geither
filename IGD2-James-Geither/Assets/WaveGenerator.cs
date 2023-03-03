using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveGenerator : MonoBehaviour
{
    public float Amplitude = 2;
    public float Velocity = 5;
    public float WaveLength = 2;
    public float speedOfDecay = .5f;
    private float poolDimension;
    private float r;
    private bool startWave;

    private float distance;

    private float x0;
    private float z0;
    private float t0;

    private Mesh mesh;

    // Use this for initialization
    private void Start()
    {
        mesh = this.GetComponent<MeshFilter>().mesh;
        Bounds bounds = mesh.bounds;

        Vector3 topLeftCorner = this.transform.TransformPoint(bounds.min);
        Vector3 bottomRightCorner = this.transform.TransformPoint(bounds.max);

        float widthInPixels = Mathf.Abs(bottomRightCorner.x - topLeftCorner.x);

        Debug.Log("Width of plane in pixels: " + widthInPixels);
        poolDimension = Mathf.Sqrt(widthInPixels * widthInPixels + widthInPixels * widthInPixels);
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K) || Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown(KeyCode.C))
        {
            GetComponent<Collider>().enabled = true;
        }

        if (Input.anyKeyDown)
        {
            string inputString = Input.inputString;
            if (!string.IsNullOrEmpty(inputString))
            {
                char inputChar = inputString[0];
                if (inputChar == 'A' || inputChar == 'a') // Only work for 'A' and 'a'
                {
                    if (char.IsUpper(inputChar))
                    {
                        Amplitude += 0.1f; // Increase the amplitude for uppercase input
                    }
                    else if (char.IsLower(inputChar))
                    {
                        Amplitude -= 0.1f; // Decrease the amplitude for lowercase input
                    }
                }

                if (inputChar == 'L' || inputChar == 'l') // Only work for 'A' and 'a'
                {
                    if (char.IsUpper(inputChar))
                    {
                        WaveLength += 0.1f; // Increase the amplitude for uppercase input
                    }
                    else if (char.IsLower(inputChar))
                    {
                        WaveLength -= 0.1f; // Decrease the amplitude for lowercase input
                    }
                }

                if (inputChar == 'V' || inputChar == 'v') // Only work for 'A' and 'a'
                {
                    if (char.IsUpper(inputChar))
                    {
                        Velocity += 0.1f; // Increase the amplitude for uppercase input
                    }
                    else if (char.IsLower(inputChar))
                    {
                        Velocity -= 0.1f; // Decrease the amplitude for lowercase input
                    }
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.L))
        {

        }

        if (Input.GetKeyDown(KeyCode.V))
        {

        }


        if (startWave)
        {
            Vector3[] verts = mesh.vertices;
            for (var v = 0; v < verts.Length; v++)
            {
                Vector3 vertex = verts[v];
                float x = transform.TransformPoint(vertex).x;
                float z = transform.TransformPoint(vertex).z;
                r = Mathf.Sqrt((x - x0) * (x - x0) + (z - z0) * (z - z0));
                distance = r / poolDimension;
                float time = Time.time - t0;
                vertex.y = Amplitude * Mathf.Exp(-distance - (speedOfDecay * time)) * Mathf.Cos(2 * Mathf.PI * (r - Velocity * time) / WaveLength);
                verts[v] = vertex;
            }

            mesh.vertices = verts;
            mesh.RecalculateBounds();
            mesh.RecalculateNormals();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("HIT");

        t0 = Time.time;
        GetComponent<Collider>().enabled = false;
        // Get the collision point
        Vector3 collisionPoint = collision.contacts[0].point;

        // Draw a line from the origin of the wave to the collision point
        Debug.DrawLine(transform.position, collisionPoint, Color.red, 1.0f);

        // Set the wave origin to the collision point
        x0 = collisionPoint.x;
        z0 = collisionPoint.z;

        startWave = true;
    }
}

/*
y(r,t) = A e-r-at cos(2π (r-Vt) /λ);
r = sqrt ((x-x0)*(x-x0)+ (z-z0)*(z-z0))
P0(x0, z0) : center of the wave
A: amplitude of the wave
V: velocity of the wave
λ: wave length of the wave
a: speed of decaying
t = current time – time of impact (t0)
 */