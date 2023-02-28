using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;  // The game object to follow
    public float smoothing = 5f;  // The amount of smoothing to apply to the camera movement

    private Vector3 _offset;  // The initial offset between the camera and the target

    private void Start()
    {
        _offset = transform.position - target.position;  // Calculate the initial offset between the camera and the target
    }

    private void FixedUpdate()
    {
        Vector3 targetCamPos = target.position + _offset;  // Calculate the target position of the camera
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);  // Move the camera towards the target position
    }
}