using System.Collections;
using UnityEngine;

public class SpawnScript : MonoBehaviour
{
    public GameObject glassTilePrefab;   // Assign your prefab here
    public Transform spawnPoint;         // Set an empty GameObject at the right of the screen
    public int maxTiles = 100;            // How many tiles to spawn
    public float spawnDelay = 1f;        // Time between each tile spawn
    public float moveSpeed = 2f;         // Speed at which tiles move left
    public float verticalRange = 2f;     // How much up/down variation is allowed

    private int tileCount = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(SpawnTilesOverTime());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator SpawnTilesOverTime()
    {
        while (tileCount < maxTiles)
        {
            // Random Y position around spawnPoint.y
            float randomY = Random.Range(-verticalRange, verticalRange);
            Vector3 spawnPos = new Vector3(spawnPoint.position.x, spawnPoint.position.y + randomY, 0f);

            GameObject tile = Instantiate(glassTilePrefab, spawnPos, Quaternion.identity);

            // Dynamically add the movement script
            GlassTileMover mover = tile.GetComponent<GlassTileMover>();
            if (mover == null)
            {
                mover = tile.AddComponent<GlassTileMover>();
            }
            mover.moveSpeed = moveSpeed;

            tileCount++;
            yield return new WaitForSeconds(spawnDelay);
        }
    }
}
