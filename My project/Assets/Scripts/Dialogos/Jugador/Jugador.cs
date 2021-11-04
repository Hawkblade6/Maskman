using UnityEngine;

public class Jugador : MonoBehaviour
{
    private const float MoveSpeed = 10f;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        rb.MovePosition(rb.position + input.normalized * (MoveSpeed * Time.fixedDeltaTime));
    }
}
