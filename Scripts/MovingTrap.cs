using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MovingTrap : MonoBehaviour
{
    [Header("Trap Movement")]
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float moveDistance = 3f;

    [Header("Shock Effect")]
    [SerializeField] private GameObject shockEffectPrefab;

    [Header("Screen Flash")]
    [SerializeField] private ScreenFlash screenFlash; // Drag your ScreenFlash panel here

    private Vector3 startPos;
    private bool movingRight = true;
    private bool isFrozen = false;

    private Transform player;
    private Rigidbody2D playerRb;
    private PlayerMovement playerMovementScript;
    private Vector3 playerSpawnPoint;
    public GameObject gameOverPanel;
    private bool gameOver = false;

    private void Start()
    {
        startPos = transform.position;

        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerRb = player.GetComponent<Rigidbody2D>();
        playerMovementScript = player.GetComponent<PlayerMovement>();
        playerSpawnPoint = player.position;
    }

    private void Update()
    {
        if (isFrozen) return;

        float direction = movingRight ? 1f : -1f;
        transform.Translate(Vector2.right * direction * moveSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, startPos) >= moveDistance)
        {
            movingRight = !movingRight;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isFrozen)
        {
            StartCoroutine(HandleShock());
        }
    }

    private IEnumerator HandleShock()
    {
        isFrozen = true;

        // Freeze camera follow
        CameraController cam = Camera.main.GetComponent<CameraController>();
        if (cam != null)
        {
            cam.EnableFollow(false);
            cam.ResetLookAhead();
        }

        // Freeze player
        if (playerRb != null)
        {
            playerRb.linearVelocity = Vector2.zero;
            playerRb.isKinematic = true;
        }

        if (playerMovementScript != null)
            playerMovementScript.enabled = false;

        // Create shock animation
        GameObject shock = Instantiate(shockEffectPrefab, player.position, Quaternion.identity);
        Animator anim = shock.GetComponent<Animator>();

        float clipLength = 0.5f;
        if (anim != null)
        {
            yield return null;
            clipLength = anim.GetCurrentAnimatorStateInfo(0).length;
        }

        // Trigger screen shake DURING animation
        CameraShake shaker = Camera.main.transform.parent.GetComponent<CameraShake>();
        if (shaker != null)
        {
            shaker.StartShake(clipLength, 0.1f);
        }

        // Wait for animation to finish BEFORE flashing
        yield return new WaitForSeconds(clipLength);
        Destroy(shock);

        // ?? NOW flash the screen
        if (screenFlash != null)
        {
            screenFlash.Flash(0.5f,0.5f);
            yield return new WaitForSeconds(1.2f); // Wait for flash to finish
        }
        TriggerGameOver();
        //// Respawn player
        //player.position = playerSpawnPoint;
        //player.localScale = new Vector3(
        //    Mathf.Abs(player.localScale.x),
        //    player.localScale.y,
        //    player.localScale.z
        //);

        //// Unfreeze player
        //if (playerRb != null)
        //    playerRb.isKinematic = false;

        //if (playerMovementScript != null)
        //    playerMovementScript.enabled = true;

        //// Re-enable camera follow
        //if (cam != null)
        //    cam.EnableFollow(true);

        //isFrozen = false;
    }
    void TriggerGameOver()
    {
        GameManagerRealWorld.Instance.lastPlayerPosition = ResultHolder.lastRealWorldPosition;
        GameManagerRealWorld.Instance.hasSavedPosition = true;
        ResultHolder.playerWon = false;
        ResultHolder.returnedFromFuture = true;
        gameOver = true;

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        Invoke("LoadGameOverScene", 2f);
    }

    void LoadGameOverScene()
    {
        SceneManager.LoadScene("Real World");
    }
}
