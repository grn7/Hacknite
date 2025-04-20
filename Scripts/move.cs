using UnityEngine;

public class move : MonoBehaviour
{
    public float scrollSpeed = 0.1f;  // Speed of scrolling
    private Renderer rend;
    private Vector2 offset;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //offset.x += scrollSpeed * Time.deltaTime;
        //rend.material.mainTextureOffset = offset;
        offset.x = Camera.main.transform.position.x * scrollSpeed;
        rend.material.mainTextureOffset = offset;
    }
}
