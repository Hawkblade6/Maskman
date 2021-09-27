using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskmanMovement : MonoBehaviour
{

    public float speed = 5f;
    public float jumpForce = 2f;
    public float rayLength = 0.6f;

    private Rigidbody2D Rigidbody2D;
    private float horizontal;
    private bool grounded;

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        horizontal = Input.GetAxisRaw("Horizontal");

        Debug.DrawRay(transform.position, Vector3.down * rayLength, Color.red);
        if (Physics2D.Raycast(transform.position, Vector3.down, rayLength))
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }

        if (Input.GetKeyDown(KeyCode.Space) && grounded) {

            Jump();
        }

    }

    private void FixedUpdate() // Update que no depende de los fps
    {
        Rigidbody2D.velocity = new Vector2(horizontal * speed, Rigidbody2D.velocity.y);
    }

    private void Jump() {

        Rigidbody2D.AddForce(Vector2.up * jumpForce);
    }
}
