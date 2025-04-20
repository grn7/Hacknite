using UnityEngine;

public class move3 : MonoBehaviour
{
    public float scrollSpeed = 0.5f;  // Speed of scrolling
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
        offset.x += scrollSpeed * Time.deltaTime;
        rend.material.mainTextureOffset = offset;
    }
}
