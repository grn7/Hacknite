using UnityEngine;

public class MovingGround : MonoBehaviour
{
    public Transform player;   // Reference to the player object
    public float speed = 5f;   // Speed at which the ground moves

    void Update()
    {
        // Make the ground move based on the player's position
        transform.position = new Vector3(player.position.x * speed, transform.position.y, transform.position.z);
    }
}
