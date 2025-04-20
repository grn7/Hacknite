using UnityEngine;

public class UFOShooter : MonoBehaviour
{
    public GameObject laserPrefab;
    public float fireInterval = 3f;
    public float moveSpeed = 2f;
    public float moveRange = 2f; // NEW: Configurable range
    public float pauseDuration = 0.5f;
    public float laserDuration = 0.5f;
    public float beamHeight = 10f;

    private float moveTimer;
    private float moveTime = 0f; // Custom time tracker for movement
    private Vector3 startPos;
    private bool isPaused = false;
    private float pauseTimer = 0f;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        moveTimer += Time.deltaTime;

        if (isPaused)
        {
            pauseTimer += Time.deltaTime;
            if (pauseTimer >= pauseDuration)
            {
                isPaused = false;
                pauseTimer = 0f;
            }
            return;
        }

        // ⏱ MoveTime advances only when not paused
        moveTime += Time.deltaTime;

        // Movement using custom time
        transform.position = startPos + new Vector3(Mathf.Sin(moveTime * moveSpeed) * moveRange, 0, 0);

        if (moveTimer >= fireInterval)
        {
            moveTimer = 0f;
            isPaused = true;
            FireLaser();
        }
    }

    void FireLaser()
    {
        float width = 1.8f;
        float yOffset = -2.5f;
        float beamHeight = 1.5f;

        Vector3 laserPos = new Vector3(transform.position.x, transform.position.y - (beamHeight / 2f) + yOffset, 0);
        GameObject laser = Instantiate(laserPrefab, laserPos, Quaternion.identity);
        laser.transform.localScale = new Vector3(width, beamHeight, 1f);
        Destroy(laser, laserDuration);
    }
}
