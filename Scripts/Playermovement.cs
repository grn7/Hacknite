using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 8f;

    private Rigidbody2D body;
    private bool grounded;
    private Vector3 originalScale;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        originalScale = transform.localScale;
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        // Move the Rigidbody
        body.linearVelocity = new Vector2(horizontalInput * speed, body.linearVelocity.y);

        // Flip the sprite
        if (horizontalInput > 0.01f)
            transform.localScale = new Vector3(originalScale.x, originalScale.y, originalScale.z);
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-originalScale.x, originalScale.y, originalScale.z);

        // Jump input
        if (Input.GetKeyDown(KeyCode.UpArrow) && grounded)
        {
            Jump();
        }
    }

    private void Jump()
    {
        body.linearVelocity = new Vector2(body.linearVelocity.x, jumpForce);
        grounded = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            grounded = true;
        }
    }
}
