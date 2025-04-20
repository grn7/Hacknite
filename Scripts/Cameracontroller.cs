using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float speed;
    private float currentPosX;
    private Vector3 velocity = Vector3.zero;

    [SerializeField] private Transform player;
    [SerializeField] private float aheadDistance;
    [SerializeField] private float cameraSpeed;
    private float lookAhead;

    private float minCameraX;
    private bool followEnabled = true;

    public void EnableFollow(bool enabled)
    {
        followEnabled = enabled;
    }

    public void ResetLookAhead()
    {
        lookAhead = 0f;
    }

    private void Start()
    {
        float halfWidth = Camera.main.orthographicSize * Camera.main.aspect;
        minCameraX = transform.position.x - halfWidth;
    }

    private void Update()
    {
        if (!followEnabled) return;

        float targetX = player.position.x + lookAhead;
        lookAhead = Mathf.Lerp(lookAhead, (aheadDistance * player.localScale.x), Time.deltaTime * cameraSpeed);

        float halfWidth = Camera.main.orthographicSize * Camera.main.aspect;
        float minX = minCameraX + halfWidth;
        float clampedX = Mathf.Max(targetX, minX);

        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
    }

    public void MoveToNewRoom(Transform _newRoom)
    {
        currentPosX = _newRoom.position.x;
    }
}
