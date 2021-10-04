using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int health;
    public int jumpsLeft;
    public float moveSpeed;
    public float jumpSpeed;
    public float fallSpeed;
    public float dashSpeed;
    public float dashTime;
    public float dashInterval;

    private int maxJumps = 2;
    private bool _isGrounded;
    private bool _isClimb;
    private bool _isDasheable;
    private bool _isDashReset;
    private bool _isInputEnabled;
    private bool _isFalling;
    private bool _isAttackable;

    //private Animator _animator;
    private Rigidbody2D rigidbody;
    private Transform transform;
    //private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _boxCollider;

    // Start is called before the first frame update
    void Start()
    {
        _isInputEnabled = true;
        _isDashReset = true;
        _isAttackable = true;

        //_animator = gameObject.GetComponent<Animator>();
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
        transform = gameObject.GetComponent<Transform>();
        //_spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        _boxCollider = gameObject.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePlayerState();
        if (_isInputEnabled)
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
        _isGrounded = IsGrounded();
        //_animator.SetBool("IsGround", _isGrounded);

        float verticalVelocity = rigidbody.velocity.y;
        //_animator.SetBool("IsDown", verticalVelocity < 0);

        if (_isGrounded && verticalVelocity == 0)
        {
            //_animator.SetBool("IsJump", false);
            //_animator.ResetTrigger("IsJumpFirst");
            //_animator.ResetTrigger("IsJumpSecond");
            //_animator.SetBool("IsDown", false);

            jumpsLeft = maxJumps;
            _isClimb = false;
            _isDasheable = true;
            
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
        Vector2 newVelocity;
        newVelocity.x = horizontalMovement;
        newVelocity.y = rigidbody.velocity.y;
        rigidbody.velocity = newVelocity;

        if (!_isClimb)
        {
            // the sprite itself is inversed 
            float moveDirection = -base.transform.localScale.x * horizontalMovement;

            if (moveDirection < 0)
            {
                // flip player sprite
                //Vector3 newScale;
                //newScale.x = horizontalMovement < 0 ? 1 : -1;
                //newScale.y = 1;
                //newScale.z = 1;

                //transform.localScale = newScale;

                if (_isGrounded)
                {
                    // turn back animation
                   // _animator.SetTrigger("IsRotate");
                }
            }
            else if (moveDirection > 0)
            {
                // move forward
               // _animator.SetBool("IsRun", true);
            }
        }

        // stop
        if (Input.GetAxis("Horizontal") == 0)
        {
            //_animator.SetTrigger("stopTrigger");
            //_animator.ResetTrigger("IsRotate");
            //_animator.SetBool("IsRun", false);
        }
        else
        {
            //_animator.ResetTrigger("stopTrigger");
        }
    }

    private bool IsGrounded()
    {
        Vector2 origin = transform.position;

        float radius = 0.2f;

        // detect downwards
        Vector2 direction;
        direction.x = 0;
        direction.y = -1;

        float distance = 0.5f;
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
            Jump();
    }

    private void Jump()
    {
        Vector2 newVelocity;
        newVelocity.x = rigidbody.velocity.x;
        newVelocity.y = jumpSpeed;

        rigidbody.velocity = newVelocity;

        //_animator.SetBool("IsJump", true);
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

    private void FallControl()
    {
        if (Input.GetButtonUp("Jump") && !_isClimb)
        {
            _isFalling = true;
            Fall();
        }
        else
        {
            _isFalling = false;
        }
    }

    private void Fall()
    {
        Vector2 newVelocity;
        newVelocity.x = rigidbody.velocity.x;
        newVelocity.y = -fallSpeed;

        rigidbody.velocity = newVelocity;
    }

    private void DashControl()
    {
        if (Input.GetKeyDown(KeyCode.K) && _isDasheable && _isDashReset)
            Dash();
    }

    private void Dash()
    {
        // reject input during sprinting
        _isInputEnabled = false;
        _isDasheable = false;
        _isDashReset = false;

        Vector2 newVelocity;
        newVelocity.x = transform.localScale.x * (_isClimb ? dashSpeed : -dashSpeed);
        newVelocity.y = 0;

        rigidbody.velocity = newVelocity;

        if (_isClimb)
        {
            // sprint to the opposite direction
            Vector3 newScale;
            newScale.x = -transform.localScale.x;
            newScale.y = 1;
            newScale.z = 1;

            transform.localScale = newScale;
        }

        //_animator.SetTrigger("IsSprint");
        StartCoroutine(DashCoroutine(dashTime, dashInterval));
    }

    private IEnumerator DashCoroutine(float dashDelay, float dashInterval)
    {
        yield return new WaitForSeconds(dashDelay);
        _isInputEnabled = true;
        _isDasheable = true;

        yield return new WaitForSeconds(dashInterval);
        _isDashReset = true;
    }
}