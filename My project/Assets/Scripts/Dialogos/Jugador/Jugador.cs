using UnityEngine;

public class Jugador : MonoBehaviour
{
    [SerializeField] private Dialogue dialogue;

    private const float MoveSpeed = 10f;

    public Dialogue Dialogue => dialogue;

    //Able to be set and read from the outside
    public Interaccion Interaccion { get; set; }

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        rb.MovePosition(rb.position + input.normalized * (MoveSpeed * Time.fixedDeltaTime));

        if (Input.GetKeyDown(KeyCode.E)) 
        {
            if (Interaccion != null)
            {
                Interaccion.Interact(this);
            }
        }
    }
}
