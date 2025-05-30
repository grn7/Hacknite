using UnityEngine;

public class ParallaxLayer : MonoBehaviour
{
    public float parallaxSpeed = 0.5f;
    private Transform cam;
    private Vector3 previousCamPos;

    void Start()
    {
        cam = Camera.main.transform;
        previousCamPos = cam.position;
    }

    void LateUpdate()
    {
        Vector3 delta = cam.position - previousCamPos;
        transform.position += new Vector3(delta.x * parallaxSpeed, 0, 0);
        previousCamPos = cam.position;
    }
}
