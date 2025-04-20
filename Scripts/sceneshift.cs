using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
public class sceneshift : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;
    private bool canTrigger = true;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canTrigger = false;
            ResultHolder.lastRealWorldPosition = other.transform.position;
            SceneManager.LoadScene(sceneToLoad);
            StartCoroutine(ResetTriggerCooldown());
        }
    }
    private IEnumerator ResetTriggerCooldown()
    {
        yield return new WaitForSeconds(1f); // Adjust delay as needed
        canTrigger = true; // Re-enable trigger after cooldown
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
