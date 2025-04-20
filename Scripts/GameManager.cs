using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public GameObject playerPrefab;
    public Transform spawnPoint;
    public float respawnDelay = 1f;

    public void RespawnPlayer()
    {
        StartCoroutine(RespawnRoutine());
    }

    private IEnumerator RespawnRoutine()
    {
        yield return new WaitForSeconds(respawnDelay);

        Vector3 spawnPos;

        // Use saved position if available
        if (GameManagerRealWorld.Instance != null && GameManagerRealWorld.Instance.hasSavedPosition)
        {
            spawnPos = GameManagerRealWorld.Instance.lastPlayerPosition;
            GameManagerRealWorld.Instance.hasSavedPosition = false; // Reset flag
        }
        else
        {
            spawnPos = spawnPoint.position;
        }

        GameObject newPlayer = Instantiate(playerPrefab, spawnPos, Quaternion.identity);

        // Update camera to follow new player
        Camera.main.GetComponent<CameraFollow>().SetTarget(newPlayer.transform);
    }
}
