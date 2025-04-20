using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D myRigidBody;
    public float jumpstrength = 7f;
    public float strongjumpstrength = 15f;
    private bool canMove = false;
    public float moveSpeed = 5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (GameManagerRealWorld.Instance != null && GameManagerRealWorld.Instance.hasSavedPosition)
        {
            Vector3 offset = new Vector3(2f, 1f, 0f);
            transform.position = GameManagerRealWorld.Instance.lastPlayerPosition+offset;
            GameManagerRealWorld.Instance.hasSavedPosition = false;
            p0[] parallaxLayers = Object.FindObjectsByType<p0>(FindObjectsSortMode.None);
            foreach (p0 layer in parallaxLayers)
            {
                layer.SkipNextFrameParallax();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveDirection = Vector2.zero;

        //if (Input.GetKey(KeyCode.LeftArrow))
        //{
        //    moveDirection = Vector2.left;
        //}

        if (Input.GetKey(KeyCode.RightArrow))
        {
            moveDirection = Vector2.right;
        }

        myRigidBody.linearVelocity = new Vector2(moveDirection.x * moveSpeed, myRigidBody.linearVelocity.y);

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (canMove)
                myRigidBody.linearVelocity = Vector2.up * strongjumpstrength;
            else
                myRigidBody.linearVelocity = Vector2.up * jumpstrength;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            myRigidBody.linearVelocity = Vector2.down * jumpstrength;
        }
    }
}
