using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WinTrigger : MonoBehaviour
{
    public TextMeshPro winMessageText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger is the player
        if (other.CompareTag("Player"))
        {
            // Display the "You won!" message
            winMessageText.text = "You won!";
            ResultHolder.playerWon = true;
            ResultHolder.returnedFromFuture = true;

            // Optional: Disable further interaction if needed, e.g., stop player movement
            // other.GetComponent<PlayerMovement>().enabled = false;
        }
    }
}
