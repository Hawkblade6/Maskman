using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskmanMovement : MonoBehaviour
{
    [Header("Variables")]
    public float speed;
    public float jumpForce = 2f;
    public float rayLengthB = 0.1f;
    public float rayLengthT = 0.05f;

    [Header("Pivotes del personaje")]
    public GameObject pivBI;    //Pivotes Bottom
    public GameObject pivBD;
    public GameObject pivTI;    //Pivotes Top
    public GameObject pivTD;

    private Rigidbody2D Rigidbody2D;
    private float horizontal;
    private float defaultSpeed = 5f;
    private bool grounded;  //Controla si el personaje esta en el suelo

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        /*
        Debug.DrawRay(pivBD.transform.position, Vector3.down * rayLengthB, Color.red); 
        Debug.DrawRay(pivBI.transform.position, Vector3.down * rayLengthB, Color.red);
        Debug.DrawRay(pivTI.transform.position, Vector3.left * rayLengthT, Color.red);
        Debug.DrawRay(pivTD.transform.position, Vector3.right * rayLengthT, Color.red);
        */

        if (Physics2D.Raycast(pivBD.transform.position, Vector3.down, rayLengthB)|| (Physics2D.Raycast(pivBI.transform.position, Vector3.down, rayLengthB) )) //Si colisionan los rayos
        {
            grounded = true;
            SetDefaultSpeed();
        }
        else
        {
            grounded = false;
        }

        if (Input.GetKeyDown(KeyCode.Space) && grounded){

            Jump();
        }

        CheckWallGrip();
    }

    private void FixedUpdate() //Update que no depende de los fps
    {
        Rigidbody2D.velocity = new Vector2(horizontal * speed, Rigidbody2D.velocity.y);
    }

    private void Jump()
    {
        Rigidbody2D.AddForce(Vector2.up * jumpForce);
    }

    private void CheckWallGrip() //Comprueba si el jugador colisiona con la pared
    { 

        if (!grounded) {
            //Si el jugador esta presionando el boton 
            if ((Input.GetKey(KeyCode.A) && Physics2D.Raycast(pivTI.transform.position, Vector3.left, rayLengthB)) || (Input.GetKey(KeyCode.D) && Physics2D.Raycast(pivTD.transform.position, Vector3.right, rayLengthB)))
            {
                speed = 0;
            }
            else
            {
                SetDefaultSpeed();
            }
        }
    }

    private void SetDefaultSpeed() //Establece la vecidad por defecto del jugador
    {
        speed = defaultSpeed;
    }

}