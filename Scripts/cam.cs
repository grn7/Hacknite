using UnityEngine;

public class cam : MonoBehaviour
{
    public Transform player; // The player object
    public float smoothing = 5f; // How smoothly the camera follows the player

    private Vector3 offset; // The offset between the camera and the player
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Set the initial offset
        offset = transform.position - player.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void LateUpdate()
    {
        // Move the camera based on the player's position and the offset
        Vector3 targetPosition = player.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing * Time.deltaTime);
    }
}
