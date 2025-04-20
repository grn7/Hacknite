using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HeartLife : MonoBehaviour
{
    public GameObject gameWonPanel;
    public GameObject gameLostPanel;

    private bool panelShown = false;

    public GameObject useLifePanel;
    public GameObject useHeartPanel;
    public Image[] heartImages;     // Assign your 5 heart images here in the Inspector
    public Sprite fullHeart;        // Red heart sprite
    public Sprite emptyHeart;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("Start() called. returnedFromFuture: " + ResultHolder.returnedFromFuture);

        if (ResultHolder.returnedFromFuture)
        {
            ResultHolder.returnedFromFuture = false; // Reset flag

            if (ResultHolder.playerWon)
            {
                if (ResultHolder.hearts < 3)
                    ResultHolder.hearts++;
                Debug.Log("Won in future: +1 heart");
            }
            else
            {
                HandleLoss();
            }
        }

        if (ResultHolder.hearts >= 3)
        {
            GameWon();
            return;
        }

        UpdateHeartUI();
    }

    void HandleLoss()
    {
        if (panelShown) return; // Prevent duplicate panels

        if (ResultHolder.lives > 0)
        {
            useLifePanel.SetActive(true);
        }
        else if (ResultHolder.hearts > 0)
        {
            useHeartPanel.SetActive(true);
        }
        else
        {
            GameLost();
        }

        panelShown = true; // Mark that we've shown a panel
    }

    public void UseLife()
    {
        ResultHolder.lives--;
        useLifePanel.SetActive(false);
        panelShown = false; // Allow next panel if needed
        Debug.Log("Used a life.");
    }

    public void UseHeart()
    {
        if (ResultHolder.hearts > 0)
        {
            ResultHolder.hearts--;
            //ResultHolder.lives = 1;
            useHeartPanel.SetActive(false);
            panelShown = false; // Allow future life/heart loss panel again
            UpdateHeartUI();
        }
    }
    void UpdateHeartUI()
    {
        for (int i = 0; i < heartImages.Length; i++)
        {
            heartImages[i].sprite = i < ResultHolder.hearts ? fullHeart : emptyHeart;
        }
    }
    public void PlayAgain()
    {
        Time.timeScale = 1f;
        ResultHolder.lives = 1;    // or whatever your default is
        ResultHolder.hearts = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    void GameWon()
    {
        Debug.Log("Game Won!");
        if (gameWonPanel != null) gameWonPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    void GameLost()
    {
        Debug.Log("Game Lost!");
        if (gameLostPanel != null) gameLostPanel.SetActive(true);
        Time.timeScale = 0f;
    }
    public void EndGame()
    {
        Debug.Log("Game Over!");
        useLifePanel.SetActive(false);
        GameLost();
        // Load Game Over screen or restart
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
