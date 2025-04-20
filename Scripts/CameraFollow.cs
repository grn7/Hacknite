using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;  // Player target
    public float smoothSpeed = 0.125f;

    void Start()
    {
        // Make sure the camera follows the player at the start of the game
        if (target == null)
        {
            // Find and set the player as the camera target at the beginning
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    void LateUpdate()
    {
        if (target != null)
        {
            Vector3 desiredPosition = new Vector3(target.position.x, transform.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        }
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
