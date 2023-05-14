using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public ParticleSystem Dust;


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

    private float coyoteTime = 0.2f;
     private float coyoteTimeCounter;

     private float jumpBufferTime = 0.2f;
     private float jumpBufferCounter;
   
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

     public Rigidbody2D PlayerRigidbody
    {
    get { return rb; }
    set { rb = value; }
    }

     
     


  
    void Update()
    {   
        /*Returns a raw value of the axis input. Which means it will return either -1,0 or 1 Depending on the direction of the input
        -1 = (left,down), 1 = (right,up) 0 = there is no input*/
        horizontal = Input.GetAxisRaw("Horizontal");
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

        if(Input.GetMouseButtonDown(0))
        {
         isAttackingPressed = true;
        }
        
    }
    /*Fixed update is a method used in Unity used for physics calculations.
    Its a special function that runs a fixed number of times per second.
    Itis designed to update the position, velocity and other physical properties*/
    private void FixedUpdate()
    {
        /*Overall, this code is setting the velocity of a Rigidbody2D component based on the horizontal input (stored in the "horizontal" variable) 
        and a speed value (stored in the "speed" variable).
        This can be used to move the object left and right in response to user input*/
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);

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
                ChangeAnimationState(PICKAXE_ATTACK_ANIMATION);

            }
            Invoke("AttackComplete", attackDelay);

        }
        

        

    }

    void AttackComplete()
    {
        isAttacking = false;
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
