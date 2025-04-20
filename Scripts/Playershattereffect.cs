using UnityEngine;

public class PlayerShatterEffect : MonoBehaviour
{
    public GameObject shardPrefab;
    public int shardCount = 10;
    public float explosionForce = 5f;
    public float lifespan = 2f;

    public void Explode(Vector3 origin, Color playerColor)
    {
        for (int i = 0; i < shardCount; i++)
        {
            GameObject shard = Instantiate(shardPrefab, origin, Quaternion.identity);

            // Set shard color
            SpriteRenderer sr = shard.GetComponent<SpriteRenderer>();
            if (sr != null)
                sr.color = playerColor;

            // Apply random force
            Rigidbody2D rb = shard.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 dir = Random.insideUnitCircle.normalized;
                rb.AddForce(dir * explosionForce, ForceMode2D.Impulse);
            }

            Destroy(shard, lifespan); // auto-clean
        }

        Destroy(gameObject); // destroy explosion spawner
    }
}
