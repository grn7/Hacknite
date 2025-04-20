using UnityEngine;
using UnityEngine.SceneManagement;

public class GOBACK : MonoBehaviour
{
    [SerializeField] private GameObject startPanel; // Drag your UI panel here
    [SerializeField] private string sceneToLoad;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Save position before switching
            if (!GameManagerRealWorld.Instance.hasSavedPosition)
            {
                GameManagerRealWorld.Instance.lastPlayerPosition = other.transform.position;
                GameManagerRealWorld.Instance.hasSavedPosition = true;
            }

            Time.timeScale = 0f; // pause game
            startPanel.SetActive(true);
        }
    }

    public void OnStartButtonClicked()
    {
        ResultHolder.returnedFromFuture = true;
        ResultHolder.playerWon = true;
        Time.timeScale = 1f; // Resume game if you paused it
        SceneManager.LoadScene(sceneToLoad);
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
