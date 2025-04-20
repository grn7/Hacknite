using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainPlayer : MonoBehaviour
{
    public Rigidbody2D myRigidBody;
    public float jumpstrength = 7f;
    public float strongjumpstrength = 15f;
    private bool canMove = false;
    public float moveSpeed = 5f;
    public float fallThresholdY = -5.39f;
    public GameObject gameOverPanel;
    private bool gameOver = false;

    [Header("Sound Settings")]
    public AudioClip fallSound;
    public AudioClip jumpSound;
    private AudioSource audioSource;
    private bool hasPlayedFallSound = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            Debug.LogWarning("Missing AudioSource component!");
        }

        if (fallSound == null)
        {
            Debug.LogWarning("Fall sound not assigned!");
        }

        if (jumpSound == null)
        {
            Debug.LogWarning("Jump sound not assigned!");
        }
    }

    void Update()
    {
        if (gameOver) return;

        Vector2 moveDirection = Vector2.zero;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            moveDirection = Vector2.left;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            moveDirection = Vector2.right;
        }

        myRigidBody.linearVelocity = new Vector2(moveDirection.x * moveSpeed, myRigidBody.linearVelocity.y);

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (canMove)
                myRigidBody.linearVelocity = Vector2.up * strongjumpstrength;
            else
                myRigidBody.linearVelocity = Vector2.up * jumpstrength;

            // 🔊 Play jump sound
            if (jumpSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(jumpSound);
            }
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            myRigidBody.linearVelocity = Vector2.down * jumpstrength;
        }
    }

    void FixedUpdate()
    {
        if (!hasPlayedFallSound && transform.position.y < fallThresholdY)
        {
            hasPlayedFallSound = true;
            Debug.Log("Fall detected! Playing sound.");

            // 🔊 Play fall sound immediately
            if (fallSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(fallSound);
            }

            StartCoroutine(HandleFall());
        }
    }

    System.Collections.IEnumerator HandleFall()
    {
        yield return new WaitForSeconds(0.05f);
        myRigidBody.linearVelocity = Vector2.zero;
        myRigidBody.simulated = false;
        TriggerGameOver();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("GlassTile"))
        {
            canMove = true;
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("GlassTile"))
        {
            canMove = false;
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
