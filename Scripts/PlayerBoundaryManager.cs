using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBoundaryManager : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float afterLastTrapX = 50f; // Set this in inspector to your final trap�s position
    private Vector3 playerSpawnPoint;
    
    private float leftBound;
    private float topBound;
    public float flag;
    public GameObject gameOverPanel;
    private bool gameOver = false;
    [SerializeField] private GameObject startPanel; // Drag your UI panel here


    private void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        // Save original spawn position
        playerSpawnPoint = player.position;

        // Calculate the original camera boundaries
        Vector3 bottomLeft = mainCamera.ViewportToWorldPoint(new Vector3(0, 1, mainCamera.nearClipPlane)); // top-left of view
        leftBound = bottomLeft.x;
        topBound = bottomLeft.y;
    }

    void Update()
    {
        if (player.position.x < leftBound ||
            player.position.y > topBound)
        {
            TriggerGameOver();
        }
        //if (player.position.x > afterLastTrapX)
        //{
        //    GameManagerRealWorld.Instance.lastPlayerPosition = other.transform.position;
        //    GameManagerRealWorld.Instance.hasSavedPosition = true;

        //    Time.timeScale = 0f; // pause game
        //    startPanel.SetActive(true);
        //}

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

    //  private void RespawnPlayer()
    //  {
    //      Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
    //      if (rb != null)
    //      {
    //          rb.linearVelocity = Vector2.zero;
    //          rb.angularVelocity = 0f;
    //      }

    //      player.position = playerSpawnPoint;

    //      // Face right (assuming scale.x positive = right)
    //      player.localScale = new Vector3(
    //          Mathf.Abs(player.localScale.x),
    //          player.localScale.y,
    //          player.localScale.z
    //);
    //  }
}
