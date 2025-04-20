using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    public GameObject gameOverPanel;
    private bool gameOver = false;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Laser") || other.CompareTag("Spike"))
        {

            //FindObjectOfType<GameManager>().RespawnPlayer();
            //Destroy(gameObject);
            TriggerGameOver();
        }
    }
    void TriggerGameOver()
    {
        Debug.Log("Hi");
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