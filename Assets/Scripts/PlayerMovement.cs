using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    public ParticleSystem Dust;
    [SerializeField] private AudioSource jumpSoundeEffect;


    //Animation States
    const string IDLE_ANIMATION = "Idle_an";
    const string JUMP_ANIMATION = "Jump_an";
    const string RUN_ANIMATION = "Run_an";
    const string PICKAXE_ATTACK_ANIMATION = "Pickaxe_an";
    private float horizontal;
    public float speed = 5f;
    private float jumpingPower = 13f;
    private bool isFacingRight = true;
    private bool isJumpPressed;
    private bool isAttacking;
    private bool isAttackingPressed;
    public float knockBack;
    public float knockBackCount;
    public float knockBackLength;
    public bool knockFromRight;


    public Transform attackHitBox;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;

    private float coyoteTime = 0.2f;
     private float coyoteTimeCounter;

     private float jumpBufferTime = 0.2f;
     private float jumpBufferCounter;

     public int damageToGive;

     public bool doubleJump;
   
     [SerializeField] private Rigidbody2D rb;
     [SerializeField] private Transform groundCheck;
     [SerializeField] private LayerMask groundLayer;
     [SerializeField] private float attackDelay = 1f;
     Animator animator;
     private string currentState;
     void Start()
     {
        animator = GetComponent<Animator>();
     }


    // So I can make the Rigidbody acccessible to othe scripts 
    //While keeping the main one private with the player controller
     public Rigidbody2D PlayerRigidbody
    {
    get { return rb; }
    set { rb = value; }
    }

     
     


  
    void Update()
    {
         Move(Input.GetAxisRaw("Horizontal"));
        /*Returns a raw value of the axis input. Which means it will return either -1,0 or 1 Depending on the direction of the input
        -1 = (left,down), 1 = (right,up) 0 = there is no input*/
        //horizontal = Input.GetAxisRaw("Horizontal");
        //This allows a brief window which allows the player to jump even though they are not touching the ground
        //Just like coyote not falling straight away when he runs off the edge in roadrunner.

        if(isGrounded())
        {
            coyoteTimeCounter = coyoteTime;

        }else
        {
            coyoteTimeCounter -= Time.deltaTime;

        }
        //Allows the player buffer a jump while they are in the air, strictly a game feel thing.
        
        if(Input.GetButtonDown("Jump"))
        {
            jumpBufferCounter = jumpBufferTime;

        }else
        {
            jumpBufferCounter -= Time.deltaTime;

        }



        if(jumpBufferCounter > 0f && coyoteTimeCounter > 0f)
        {
            isJumpPressed = true;
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            jumpBufferCounter = 0f;
        }

        if(Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            coyoteTimeCounter = 0f;

        }

        
          
        Flip();

        //if(Input.GetMouseButtonDown(0))
        //{
        // PickAxe();
       // }
        
    }

    public void Jump()
    {
        if(isGrounded())
        {
             isJumpPressed = true;
             rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }else if (doubleJump)
        {
            doubleJump = false;
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);

        }
    }

    private IEnumerator RespawnPowerUp(GameObject powerUpObject,float respawnDelay)
{
    yield return new WaitForSeconds(respawnDelay);
    powerUpObject.SetActive(true);
}



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("PowerUp"))
        {
            collision.gameObject.SetActive(false);
            doubleJump = true;
        }

        StartCoroutine(RespawnPowerUp(collision.gameObject, 5f));
    }

     public void Move(float moveInput)
    {
         horizontal = moveInput;
        
    }
    /*Fixed update is a method used in Unity used for physics calculations.
    Its a special function that runs a fixed number of times per second.
    Itis designed to update the position, velocity and other physical properties*/
    private void FixedUpdate()
    {
        /*Overall, this code is setting the velocity of a Rigidbody2D component based on the horizontal input (stored in the "horizontal" variable) 
        and a speed value (stored in the "speed" variable).
        This can be used to move the object left and right in response to user input*/
        if(knockBackCount <= 0)
        {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        }else
        {
            if(knockFromRight)
                rb.velocity = new Vector2(-knockBack, knockBack);
            
            if(!knockFromRight)
                rb.velocity = new Vector2(knockBack, knockBack);
                knockBackCount -= Time.deltaTime;
            
        }

        //This is for the change in animation, I don't want this to overriding, things like the jump animation so we check if we are grounded first
        if(isGrounded() && !isAttacking){
            if(horizontal != 0f)
            {
                ChangeAnimationState(RUN_ANIMATION);
                CreateDust();

            }
            else if(horizontal == 0)
            {   
            ChangeAnimationState(IDLE_ANIMATION);
            }
    
        }

        if(isJumpPressed = true && !isGrounded() && !isAttacking){
            
            ChangeAnimationState(JUMP_ANIMATION);
        }

        if(isAttackingPressed)
        {
            isAttackingPressed = false;

            if(!isAttacking)
            {
                isAttacking = true;
                Attack();

            }
            Invoke("AttackComplete", attackDelay);

        }
        

        

    }


   
    public void PickAxe()
    {
        isAttackingPressed = true;
    }





    void Attack()
    {
        ChangeAnimationState(PICKAXE_ATTACK_ANIMATION);
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackHitBox.position, attackRange, enemyLayers);

        foreach(Collider2D enemy in hitEnemies)
        {
            Debug.Log("we hit" + enemy.name);
            enemy.GetComponent<EnemyHealthManager>().giveDamage(damageToGive);
            
        }


    }

    void OnDrawGizmosSelected()
    {
        if (attackHitBox == null)
            return;

        Gizmos.DrawWireSphere(attackHitBox.position, attackRange);
    }

    void AttackComplete()
    {
        isAttacking = false;

        if (isGrounded())
    {
        if (horizontal != 0f)
        {
            ChangeAnimationState(RUN_ANIMATION);
            CreateDust();
        }
        else
        {
            ChangeAnimationState(IDLE_ANIMATION);
        }
    }
    else
    {
        ChangeAnimationState(JUMP_ANIMATION);
    }
    }

    private bool isGrounded()
    {   
        /*Checks if the gameObject is currently on the ground. Creates a small gameObject
        slightly below the player*/
        //The "0.2f" value is the radius of the circle.
        //The "groundLayer" parameter is a layer mask that's used to filter out other objects that we don't want to check for collisions with.
        //The "groundCheck" transform is an empty game object that's placed at the bottom of the game object's sprite.
    
        return Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);

    }

    private void Flip()
    {   
        //If statement used to filp the player, checks what direction they are moving in
        //flips the player on the x Axis depending on the direction
        if(isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }

    }

    void ChangeAnimationState(string newState)
    {
        //stop the same animation from interrupting itself
        if (currentState == newState) return;
        
        //play the animation
        animator.Play(newState);

        currentState = newState;

    }

    void CreateDust()
    {
        Dust.Play();

    }
}
