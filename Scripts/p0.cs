using UnityEngine;

public class p0 : MonoBehaviour
{
    public Transform player; // Reference to the player object
    public float parallaxSpeed = 0.5f; // Speed at which the background moves
    public float mapLength = 50f; // Total distance before the map is extended

    private Vector3 lastPlayerPosition;
    private float length;
    private float startPosition;
    private bool skipParallax = false;
    void Start()
    {
        // Store the initial position of the background
        lastPlayerPosition = player.position;
        startPosition = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x; // Get the width of the background sprite
    }

    // Update is called once per frame
    void Update()
    {
        if (skipParallax)
        {
            // Reset lastPlayerPosition without moving background
            lastPlayerPosition = player.position;
            skipParallax = false;
            return;
        }
        // Calculate the distance the player has moved horizontally
        float playerMovement = player.position.x - lastPlayerPosition.x;

        // Move the background based on the player's movement
        transform.position = new Vector3(transform.position.x + playerMovement * parallaxSpeed, transform.position.y, transform.position.z);

        // If the background has moved past the specified scroll distance (50 units), reset its position
        if (transform.position.x > startPosition + mapLength)
        {
            transform.position = new Vector3(startPosition, transform.position.y, transform.position.z);
        }

        lastPlayerPosition = player.position;
    }
    public void SkipNextFrameParallax()
    {
        skipParallax = true;
    }
}
