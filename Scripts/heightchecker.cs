using UnityEngine;
using UnityEngine.SceneManagement;

public class heightchecker : MonoBehaviour
{
    public Transform player; // Drag your player object here
    public float upperYLimit = 6.9f; // Set your Y-axis threshold
    public GameObject gameOverPanel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player.position.y > upperYLimit)
        {
            TriggerGameOver();
        }
    }
    void TriggerGameOver()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }
    public void PlayAgain()
    {
        Time.timeScale = 1f;
        ResultHolder.lives = 1;    // or whatever your default is
        ResultHolder.hearts = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
