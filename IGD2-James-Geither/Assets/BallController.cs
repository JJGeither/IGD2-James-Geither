using System.Collections;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private Rigidbody rb;
    private new ParticleSystem particleSystem;
    public GameObject particles;
    public float duration;
    public float speed;
    public Vector3[] stairVectors;
    public Vector3[] jumpVectors;
    public int score;
    public bool moving;

    private void Start()
    {
        // Get the Rigidbody component of the object
        rb = GetComponent<Rigidbody>();
        particleSystem = particles.GetComponent<ParticleSystem>();
        ParticlesFalse();
        score = 0;
        moving = false;
    }

    // Update is called once per frame
    private void Update()
    {
        // Check if the 'r' key is pressed
        if (Input.GetKeyDown(KeyCode.K) && !moving)
        {
            EnablePhysics();
            transform.rotation = Quaternion.Euler(-90f, 0f, 0f);
            transform.position = new Vector3(0f, 15f, 5f);

            rb.angularVelocity = Vector3.zero;
            rb.velocity = Vector3.zero;
        }

        if (Input.GetKeyDown(KeyCode.C) && !moving)
        {
            Debug.Log("Moving");
            transform.rotation = Quaternion.Euler(-90f, 0f, 0f);
            transform.position = stairVectors[0];

            // Reset the object's velocity to zero
            rb.velocity = Vector3.zero;
            StartCoroutine(MoveTo(stairVectors, speed, false));
        }

        if (Input.GetKeyDown(KeyCode.R) && !moving)
        {
            transform.rotation = Quaternion.Euler(-90f, 90f, 0f);
            Debug.Log("Moving");
            transform.position = jumpVectors[0];
            StartCoroutine(MoveTo(jumpVectors, speed, true));
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Water"))
        {
            score++;
            ActivateParticles();
        }
    }

    public void ActivateParticles()
    {
        particleSystem.Play();
    }

    public void EnablePhysics()
    {
        Collider collider = GetComponent<Collider>();
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        if (collider != null) collider.enabled = true;
        if (rigidbody != null) rigidbody.isKinematic = false;
    }

    public void DisablePhysics()
    {
        Collider collider = GetComponent<Collider>();
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        if (collider != null) collider.enabled = false;
        if (rigidbody != null) rigidbody.isKinematic = true;
    }

    private IEnumerator MoveTo(Vector3[] pointB, float speed, bool isDive)
    {
        DisablePhysics();
        moving = true;
        foreach (var i in pointB)
        {
            float distanceToB = Vector3.Distance(this.transform.position, i);

            while (distanceToB > 0.1f)
            {
                this.transform.position = Vector3.MoveTowards(this.transform.position, i, speed * Time.deltaTime);
                distanceToB = Vector3.Distance(this.transform.position, i);
                yield return null;
            }
        }
        if (isDive)
        {
            EnablePhysics();
            Rigidbody rigidbody = GetComponent<Rigidbody>();
            rigidbody.AddForce(new Vector3(200, 400, 0));
            rigidbody.angularVelocity = new Vector3(0, 0, -20);
        }
        moving = false;
    }

    public void ParticlesFalse()
    {
        particleSystem.Stop();
    }
}