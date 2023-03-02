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

    private void Start()
    {
        // Get the Rigidbody component of the object
        rb = GetComponent<Rigidbody>();
        particleSystem = particles.GetComponent<ParticleSystem>();
        ParticlesFalse();
    }

    // Update is called once per frame
    private void Update()
    {
        // Check if the 'r' key is pressed
        if (Input.GetKeyDown(KeyCode.K))
        {
            // Reset the object's position to (0, 0, 0)
            transform.position = new Vector3(0f, 10f, 0f);

            // Reset the object's velocity to zero
            rb.velocity = Vector3.zero;
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("Moving");
            StartCoroutine(MoveTo(stairVectors, speed, false));
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("Moving");
            StartCoroutine(MoveTo(jumpVectors, speed, true));
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Water"))
        {
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
        }
    }

    public void ParticlesFalse()
    {
        particleSystem.Stop();
    }
}