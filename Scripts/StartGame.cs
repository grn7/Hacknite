using UnityEngine;
using static Trackerscript;

public class StartGame : MonoBehaviour
{
    [SerializeField] private GameObject startPanel;

    void Start()
    {
        if (!GameSessionTracker.hasGameStarted)
        {
            Time.timeScale = 0f;
            startPanel.SetActive(true);
        }
        else
        {
            startPanel.SetActive(false);
        }
    }

    public void OnStartButtonClicked()
    {
        startPanel.SetActive(false);
        Time.timeScale = 1f;

        GameSessionTracker.hasGameStarted = true; // Mark game as started
    }
}
