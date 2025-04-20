using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalScript : MonoBehaviour
{
    public GameObject winnerPanel;
    public GameObject gameLostPanel;
    public HeartLife heartLifeScript; // Reference to HeartLife to access heart count
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            int currentHearts = ResultHolder.hearts;

            if (currentHearts >= 5)
            {
                winnerPanel.SetActive(true);
            }
            else
            {
                gameLostPanel.SetActive(true);
            }

            Time.timeScale = 0f; // Pause game when panel shows
        }
    }
    public void PlayAgain()
    {
        Time.timeScale = 1f;
        ResultHolder.lives = 1;    // or whatever your default is
        ResultHolder.hearts = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
