using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int health;
    public int jumpsLeft;
    public float moveSpeed;
    public float jumpSpeed;
    //public float fallSpeed;
    public float dashSpeed;
    public float dashTime;
    public float dashInterval;

    private int maxJumps = 1;
    private int gravity = 2;
    private bool isGrounded;
    private bool canJump;
    private bool _canClimb;
    private bool _isClimb;
    private bool canDash;
    private bool dashreset;
    private bool isInputEnabled;
    private bool isFalling;
    private bool _isAttackable;
    
    private Animator animator;
    private Rigidbody2D rigidb;
    private Transform transf;
    //private SpriteRenderer _spriteRenderer;
    private BoxCollider2D boxCollider;

    // Start is called before the first frame update
    void Start()
    {
        isInputEnabled = true;
        dashreset = true;
        _isAttackable = true;

        animator = gameObject.GetComponent<Animator>();
        rigidb = gameObject.GetComponent<Rigidbody2D>();
        transf = gameObject.GetComponent<Transform>();
        //_spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        boxCollider = gameObject.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePlayerState();
        if (isInputEnabled)
        {
            Move();
            JumpControl();
            FallControl();
            DashControl();
            //AttackControl();
        }
    }

    private void UpdatePlayerState()
    {
        isGrounded = IsGrounded();
        canJump = isGrounded;

        animator.SetBool("IsGround", isGrounded);

        float verticalVelocity = rigidb.velocity.y;
        animator.SetBool("IsDown", verticalVelocity < 0);

        if (isGrounded && verticalVelocity == 0)
        {
            animator.SetBool("IsJump", false);
            //_animator.ResetTrigger("IsJumpFirst");
            //_animator.ResetTrigger("IsJumpSecond");
            animator.SetBool("IsDown", false);

            jumpsLeft = maxJumps;
            _isClimb = false;
            canDash = true;
            
        }
        else if (_isClimb)
        {
            // one remaining jump chance after climbing
            jumpsLeft = maxJumps - 1;
        }
    }

    private void Move()
    {
        // calculate movement
        float horizontalMovement = Input.GetAxis("Horizontal") * moveSpeed;

        // set velocity
        //Rigidbody2D.velocity = new Vector2(horizontal * speed, Rigidbody2D.velocity.y);
        Vector2 newVelocity;
        newVelocity.x = horizontalMovement;
        newVelocity.y = rigidb.velocity.y;
        rigidb.velocity = newVelocity;

        if (Input.GetKeyDown(KeyCode.A)) //Control por teclado
        {
            rigidb.AddForce(Vector2.right);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            newVelocity.x = 0;
            rigidb.velocity = newVelocity;
        }
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D)) // Sigue deslizandose aunque se deje de pulsar la tecla <---- solucionar?
        {
            Debug.Log("ad");
            newVelocity.x = 0;
            rigidb.velocity = newVelocity;
            Debug.Log(rigidb.velocity);
        }

        //if (Input.GetKeyDown(KeyCode.P))
        //{
        //    SaveSystem.SavePlayer(this);
        //}
        //if (Input.GetKeyDown(KeyCode.L))
        //{
        //    PlayerData data = SaveSystem.LoadPlayer();
        //    Vector2 pos;
        //    pos.x = data.position[0];
        //    pos.y = data.position[1];
        //    transform.position = pos;
        //}




        if (!_isClimb)
        {
             
            float moveDirection = base.transform.localScale.x * horizontalMovement;

            if (moveDirection < 0)
            {
                // girar el sprite
                Vector3 newScale;
                newScale.x = horizontalMovement < 0 ? -1 : 1;
                newScale.y = 1;
                newScale.z = 1;

                transf.localScale = newScale;

                if (isGrounded)
                {
                    // turn back animation
                    //animator.SetTrigger("IsRotate");
                }
            }
            else if (moveDirection > 0)
            {
                animator.SetBool("IsRun", true);
            }
        }

        // stop
        if (Input.GetAxis("Horizontal") == 0)
        {
            //_animator.SetTrigger("stopTrigger");
            //_animator.ResetTrigger("IsRotate");
            animator.SetBool("IsRun", false);
        }
        else
        {
            //_animator.ResetTrigger("stopTrigger");
        }
    }

    //void OnDrawGizmosSelected()       
    //{
    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawSphere(transform.position, 0.7f);

    //}

    //private bool CheckWallCollision() 
    //{
    //    Vector2 origin = transform.position;



    //    // detect sides
    //    Vector2 directionLeft;
    //    directionLeft.x = -1;
    //    directionLeft.y = 0;

    //    Vector2 directionRight;
    //    directionRight.x = 1;
    //    directionRight.y = 0;

    //    float distance = 0.6f;
    //    LayerMask layerMask = LayerMask.GetMask("Plataforma");

    //    RaycastHit2D hitRecL = Physics2D.Raycast(origin, directionLeft, distance);
    //    RaycastHit2D hitRecR = Physics2D.Raycast(origin, directionRight, distance);

        
    //    return (hitRecL.collider != null || hitRecR.collider != null);

    //}

    private bool IsGrounded()
    {
        Vector2 origin = transf.position;

        float radius = 0.2f;

        // detect downwards
        Vector2 direction;
        direction.x = 0;
        direction.y = -1;

        float distance = 0.6f;
        LayerMask layerMask = LayerMask.GetMask("Plataforma");

        RaycastHit2D hitRec = Physics2D.CircleCast(origin, radius, direction, distance, layerMask);
        return hitRec.collider != null;
    }

    private void JumpControl()
    {
        if (!Input.GetButtonDown("Jump")) 
        {
            return;
        }


        if (_isClimb)
        {
            //ClimbJump();
        }
        else if (jumpsLeft > 0)
        {
            
            Jump();
        }
    }

    private void Jump()
    {
        if (canJump)
        {
            Vector2 newVelocity;
            newVelocity.x = rigidb.velocity.x;
            newVelocity.y = jumpSpeed;

            rigidb.velocity = newVelocity;

            animator.SetBool("IsJump", true);
            jumpsLeft -= 1;
            if (jumpsLeft == 0)
            {
                //_animator.SetTrigger("IsJumpSecond");
            }
            else if (jumpsLeft == 1)
            {
                //_animator.SetTrigger("IsJumpFirst");
            }
        }   
    }

    private void FallControl()
    {
        if (rigidb.velocity.y < 0)
        {
            isFalling = true;
        }

        if (Input.GetButtonUp("Jump") && !_isClimb && !isFalling)
        {
            isFalling = true;
            Fall();
        }
        else if(isGrounded)
        {
            isFalling = false;
        }
    }

    private void Fall()
    {

        animator.SetBool("IsDown", true);
        Vector2 newVelocity;
        newVelocity.x = rigidb.velocity.x;
        newVelocity.y = 0;

        rigidb.velocity = newVelocity;
    }

    private void DashControl()
    {
        if (Input.GetButtonDown("Dash") && canDash && dashreset)
            Dash();
    }

    private void Dash()
    {
        // reject input during sprinting
        isInputEnabled = false;
        canDash = false;
        dashreset = false;

        Vector2 newVelocity;
        newVelocity.x = transf.localScale.x * (_isClimb ? -dashSpeed : dashSpeed); 
        newVelocity.y = 0;

        rigidb.velocity = newVelocity;

        if (_isClimb)
        {
            // opposite side dash
            Vector3 newScale;
            newScale.x = -transf.localScale.x;
            newScale.y = 1;
            newScale.z = 1;

            transf.localScale = newScale;
        }

        animator.SetTrigger("IsSprint");
        StartCoroutine(DashCoroutine(dashTime, dashInterval));
        rigidb.gravityScale = 0;
    }

    private IEnumerator DashCoroutine(float dashDelay, float dashInterval)
    {
        yield return new WaitForSeconds(dashDelay);
        isInputEnabled = true;
        canDash = true;
        rigidb.gravityScale = gravity;

        yield return new WaitForSeconds(dashInterval);
        dashreset = true;
    }
}
