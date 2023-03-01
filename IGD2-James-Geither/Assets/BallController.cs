using UnityEngine;
using System.Collections;
public class BallController : MonoBehaviour
{
    private Rigidbody rb;
    private new ParticleSystem particleSystem;
    public GameObject particles;
    public float duration;

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
        if (Input.GetKeyDown(KeyCode.R))
        {
            // Reset the object's position to (0, 0, 0)
            transform.position = new Vector3(0f, 10f, 0f);

            // Reset the object's velocity to zero
            rb.velocity = Vector3.zero;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        ActivateParticles();
    }
    public void ActivateParticles()
    {
        particleSystem.Play();
    }

    public void ParticlesFalse()
    {
        particleSystem.Stop();
    }
}