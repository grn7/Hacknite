using UnityEngine;

public class PanelManager : MonoBehaviour
{
    [SerializeField] private GameObject startPanel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Time.timeScale = 0f; // pause game
        startPanel.SetActive(true);
    }

    // Update is called once per frame
    public void OnStartButtonClicked()
    {
        startPanel.SetActive(false);
        Time.timeScale = 1f; // resume game
    }
}
