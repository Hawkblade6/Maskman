using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Dialogue dialogue;

    public Dialogue Dialogue => dialogue;

    public Interactable Interactable { get; set; }


    
    public int jumpsLeft;
    public float maxHealth;
    public float weaponDamage;
    public float damageRadius;
    public float damageDistance;
    public float moveSpeed;
    public float jumpSpeed;
    public float dashSpeed;
    public float dashTime;
    public float dashInterval;
    public float attackInterval;
    public float hurtTime;
    public float hurtRecoverTime;
    public float deathDelay;

    public Vector2 attackForwardRecoil;
    public Vector2 hurtRecoil;
    public Vector2 deathRecoil;
    public Color invulnerableColor;
    public HealthBar healthBar;
    public SceneLoader sc;

    private AudioSource[] mySounds;
    private AudioSource stepSound;
    private AudioSource windSound;

    private int maxJumps = 2;
    private int gravity = 2;
    private float attackEffectLifeTime = 0.1f;
    private float currentHealth;
    private bool isGrounded;
    private bool canJump;
    private bool _canClimb;
    private bool _isClimb;
    private bool canDash;
    private bool dashreset;
    private bool isInputEnabled;
    private bool isFalling;
    private bool isAttackable;
    private bool moving;
    private bool jumping;

    private bool npc;

    private Animator animator;
    private Rigidbody2D rigidb;
    private Transform transf;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;

    // Start is called before the first frame update
    void Start()
    {
        isInputEnabled = true;
        dashreset = true;
        isAttackable = true;
        maxHealth = PlayerPrefs.GetInt("maxhp");
        healthBar.SetMaxHealth(maxHealth);
        currentHealth = PlayerPrefs.GetInt("currenthp");

        animator = gameObject.GetComponent<Animator>();
        rigidb = gameObject.GetComponent<Rigidbody2D>();
        transf = gameObject.GetComponent<Transform>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        boxCollider = gameObject.GetComponent<BoxCollider2D>();

        mySounds = GetComponents<AudioSource>();

        stepSound = mySounds[0];
        windSound = mySounds[1];
        maxJumps = PlayerPrefs.GetInt("maxJumps");
        weaponDamage = PlayerPrefs.GetInt("damage");
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
            SceneElemControl();
            AttackControl();
        }
    }

    private void UpdatePlayerState()
    {
        healthBar.SetHealth(PlayerPrefs.GetInt("currenthp"));
        isGrounded = IsGrounded();
        canJump = isGrounded;
        jumping = !canJump;

        animator.SetBool("IsGround", isGrounded);

        float verticalVelocity = rigidb.velocity.y;
        animator.SetBool("IsDown", verticalVelocity < 0);

        if (isGrounded && verticalVelocity == 0)
        {
            animator.SetBool("IsJump", false);
            animator.SetBool("IsDown", false);

            jumpsLeft = maxJumps;
            _isClimb = false;
            canDash = true;    
        }
    }

    private void Move()
    {
        // calculate movement
        float horizontalMovement = Input.GetAxis("Horizontal") * moveSpeed;

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
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D)) 
        {
            newVelocity.x = 0;
            rigidb.velocity = newVelocity;
        }

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
            moving = false;
        }
        else
        {
            //_animator.ResetTrigger("stopTrigger");
            moving = true;
        }
    }


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

        if (jumpsLeft > 0)
        {
            Debug.Log(jumpsLeft);
            Jump();
        }
    }

    private void Jump()
    {
        isAttackable = false;
        if (canJump || jumping)
        {
            jumping = true;
            moving = false;
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
            isAttackable = false;
        }

        if (Input.GetButtonUp("Jump") && !_isClimb && !isFalling)
        {
            isFalling = true;
            Fall();
        }
        else if(isGrounded)
        {
            isFalling = false;
            isAttackable = true;
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

    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    if (Input.GetButtonDown("Interact") && collision.tag == "NPC")
    //    {
    //            Debug.Log("Soy un NPC, hola");
    //    }
    //}

    private void AttackControl() {

        if (Input.GetButtonDown("Attack") && isAttackable && isGrounded) {
            canJump = false;
            Attack();

        }
    }

    private void Attack() { //mas tipos de ataques ej hacia abajo

        
        animator.SetBool("IsAttack",true);

        Vector2 detectDirection;
        detectDirection.x = transform.localScale.x;
        detectDirection.y = 0;

        Vector2 recoil;
        recoil.x = transform.localScale.x > 0 ? -attackForwardRecoil.x : attackForwardRecoil.x;
        recoil.y = attackForwardRecoil.y;

        StartCoroutine(attackCoroutine( attackEffectLifeTime, attackInterval, detectDirection, recoil));
    }

    private IEnumerator attackCoroutine( float effectDelay, float attackInterval, Vector2 detectDirection, Vector2 attackRecoil)
    {
        Vector2 origin = transform.position;
        LayerMask layerMask = LayerMask.GetMask("Enemy") ;


        RaycastHit2D ray = Physics2D.Raycast(origin, detectDirection, damageDistance);

        if (ray.collider != null)
        {
            if (ray.collider.tag.Equals("Enemy"))
            {
                ray.collider.GetComponent<BarraDeVida>().modificacionVida(-weaponDamage);
                ray.collider.GetComponent<ScriptDeEnemigo>().golpeado();
            }
        }

        yield return new WaitForSeconds(effectDelay);

        // attack cd
        isAttackable = false;
        yield return new WaitForSeconds(attackInterval);
        isAttackable = true;
        canJump = true;
        animator.SetBool("IsAttack", false);
    }

    public void hurt(float damage)
    {
        currentHealth -= damage;
        PlayerPrefs.SetInt("currenthp",(int)currentHealth);
        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            die();

        }
        else
        {

            // stop player movement
            Vector2 newVelocity;
            newVelocity.x = 0;
            newVelocity.y = 0;
            rigidb.velocity = newVelocity;

            // visual effect
            spriteRenderer.color = invulnerableColor;

            // death recoil
            Vector2 newForce;
            newForce.x = -transform.localScale.x * hurtRecoil.x;
            newForce.y = hurtRecoil.y;
            rigidb.AddForce(newForce, ForceMode2D.Impulse);

            isInputEnabled = false;

            StartCoroutine(recoverFromHurtCoroutine());
        }
    }

    private IEnumerator recoverFromHurtCoroutine()
    {
        yield return new WaitForSeconds(hurtTime);
        isInputEnabled = true;
        yield return new WaitForSeconds(hurtRecoverTime);
        spriteRenderer.color = Color.white;
        gameObject.layer = LayerMask.NameToLayer("Player");
    }

    private void die()
    {
        animator.SetTrigger("IsDead");

        isInputEnabled = false;
        gameObject.GetComponent<ParticleSystem>().Play();
        gameObject.transform.GetChild(0).GetComponent<ParticleSystem>().Play();
        // stop player movement
        Vector2 newVelocity;
        newVelocity.x = 0;
        newVelocity.y = 0;
        rigidb.velocity = newVelocity;

        // visual effect
        spriteRenderer.color = invulnerableColor;

        // death recoil
        Vector2 newVelocity2;
        newVelocity2.x = 0;
        newVelocity2.y = 0;
        rigidb.velocity = newVelocity2;

        StartCoroutine(deathCoroutine());
    }

    private IEnumerator deathCoroutine()
    {
        
        yield return new WaitForSeconds(deathDelay);
        Destroy(this);
        sc.DeathScene();
    }


    private void SceneElemControl() 
    {
        if (npc) 
        {
            if (Input.GetButtonDown("Interact")) 
            {
                npc = false;
                //Interactable.Interact(this);
                //dia.ShowDialogue(obj);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "NPC")
        {
            npc = true;
        }
        if (collision.tag == "HealthUp")
        {
            Destroy(collision.gameObject);
            int maxh = PlayerPrefs.GetInt("maxhp");
            maxh += 50;
            PlayerPrefs.SetInt("maxhp", maxh);
            healthBar.SetMaxHealth(PlayerPrefs.GetInt("maxhp"));
            PlayerPrefs.SetInt("currenthp", maxh);
            currentHealth = maxh;
        }
        if (collision.tag == "Cure") 
        {
            Destroy(collision.gameObject);
            int chp = PlayerPrefs.GetInt("currenthp");
            healthBar.SetHealth(chp + 40);
        }
        if (collision.tag == "DamageUp")
        {
            Destroy(collision.gameObject);
            int dmg = PlayerPrefs.GetInt("damage");
            dmg += 15;
            PlayerPrefs.SetInt("damage", dmg);
            weaponDamage = dmg;
        }
        if (collision.tag == "dobleSalto") {
            Destroy(collision.gameObject);
            PlayerPrefs.SetInt("maxJumps", 2);
            maxJumps = PlayerPrefs.GetInt("maxJumps");
        }
        if (collision.tag == "dashOn")
        {
            Destroy(collision.gameObject);
            PlayerPrefs.SetInt("canDash", 1);
            canDash = true;
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "NPC")
        {
            npc = false;
            //dia.CloseDialogueBox();
        }
    }


    #region //music
    private void StepSound() {

        

        if (moving)
        {
            stepSound.pitch = UnityEngine.Random.Range(0.75f,1.2f);
            stepSound.Play();
        }
        else {
            stepSound.Stop();
        }
    }

    private void WindSound() {

        windSound = GetComponent<AudioSource>();

        windSound.Play();
    }

    #endregion
}
