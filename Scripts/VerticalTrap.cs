using UnityEngine;
using UnityEngine.SceneManagement;

public class VerticalTrap : MonoBehaviour
{
    [Header("Trap Movement")]
    [SerializeField] private float heightChange = 2f;
    [SerializeField] private float speed = 2f;

    [Header("Shard Effect")]
    [SerializeField] private GameObject shardPrefab;
    [SerializeField] private int numberOfShards = 8;
    [SerializeField] private float explosionForce = 5f;

    private Vector3 startPos;
    private bool movingUp = true;

    private float camTopY;
    private float camBottomY;
    public GameObject gameOverPanel;
    private bool gameOver = false;

    private void Start()
    {
        startPos = transform.position;

        // Calculate camera bounds
        Camera cam = Camera.main;
        float camHeight = 2f * cam.orthographicSize;
        float camCenterY = cam.transform.position.y;

        camTopY = camCenterY + (camHeight / 2f);
        camBottomY = camCenterY - (camHeight / 2f);

        float maxAllowedHeight = Mathf.Min(heightChange, camTopY - startPos.y);
        heightChange = Mathf.Max(0f, maxAllowedHeight);
    }

    private void Update()
    {
        float direction = movingUp ? 1 : -1;
        transform.Translate(Vector2.up * direction * speed * Time.deltaTime);

        if (movingUp && transform.position.y >= startPos.y + heightChange)
        {
            transform.position = new Vector3(transform.position.x, startPos.y + heightChange, transform.position.z);
            movingUp = false;
        }
        else if (!movingUp && transform.position.y <= startPos.y)
        {
            transform.position = new Vector3(transform.position.x, startPos.y, transform.position.z);
            movingUp = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SpriteRenderer sr = other.GetComponent<SpriteRenderer>();
            Sprite playerSprite = sr != null ? sr.sprite : null;
            Color playerColor = sr != null ? sr.color : Color.white;

            ShatterPlayer(other.transform.position, playerColor, playerSprite);
            Destroy(other.gameObject);

            TriggerGameOver();
        }
    }

    private void ShatterPlayer(Vector3 position, Color color, Sprite sprite)
    {
        for (int i = 0; i < numberOfShards; i++)
        {
            GameObject shard = Instantiate(shardPrefab, position, Quaternion.identity);
            Rigidbody2D rb = shard.GetComponent<Rigidbody2D>();
            SpriteRenderer sr = shard.GetComponent<SpriteRenderer>();

            if (sr != null)
            {
                sr.color = color;
                sr.sprite = sprite;
            }

            if (rb != null)
            {
                Vector2 forceDir = Random.insideUnitCircle.normalized;
                rb.AddForce(forceDir * explosionForce, ForceMode2D.Impulse);
                rb.AddTorque(Random.Range(-200f, 200f));
            }

            Destroy(shard, 2f);
        }
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
