using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidMovement : MonoBehaviour
{
    public float speed = 5f;
    public float rotationSpeed = 5f;
    public float neighbourDistance = 5f;
    public float avoidanceDistance = 2f;
    public float separationWeight = 1f;
    public float alignmentWeight = 1f;
    public float cohesionWeight = 1f;
    public float waterAvoidanceDistance = 5f;
    public float subgroupValue = 0f;
    public float directionValue = 0f;
    public Transform target;
    public Transform obstacle;

    public Vector3 RBvelocity;

    private void Start()
    {
        RBvelocity = Random.insideUnitSphere * speed;
    }

    private void Update()
    {
        Vector3 targetPosition = target.position;
        if (obstacle != null && target.position.y < obstacle.position.y)
        {
            targetPosition = new Vector3(target.position.x + subgroupValue, obstacle.position.y + waterAvoidanceDistance + directionValue, target.position.z);
        }

        List<Transform> neighbours = new List<Transform>();
        Collider[] colliders = Physics.OverlapSphere(transform.position, neighbourDistance);
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject != gameObject)
            {
                neighbours.Add(collider.transform);
            }
        }

        Vector3 separation = Vector3.zero;
        Vector3 alignment = Vector3.zero;
        Vector3 cohesion = Vector3.zero;
        int neighbourCount = neighbours.Count;

        foreach (Transform neighbour in neighbours)
        {
            Vector3 difference = transform.position - neighbour.position;
            float distance = difference.magnitude;
            if (distance < avoidanceDistance)
            {
                separation += difference.normalized / distance;
            }
            alignment += neighbour.forward;
            cohesion += neighbour.position;
        }

        if (neighbourCount > 0)
        {
            separation /= neighbourCount;
            alignment /= neighbourCount;
            cohesion /= neighbourCount;
            separation = separation.normalized * separationWeight;
            alignment = alignment.normalized * alignmentWeight;
            cohesion = (cohesion - transform.position).normalized * cohesionWeight;
        }

        Vector3 targetVelocity = separation + alignment + cohesion;
        targetVelocity += (targetPosition - transform.position).normalized;
        RBvelocity = Vector3.RotateTowards(RBvelocity, targetVelocity, rotationSpeed * Time.deltaTime, 0f);
        transform.position += RBvelocity * Time.deltaTime;
        transform.forward = RBvelocity.normalized;
    }
}