using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolEnemy : MonoBehaviour
{
    public float speed = 2f;
    public Rigidbody2D erb;
    public LayerMask groundLayers;

    public Transform groundCheck;

    bool isFacingRight = true;

    RaycastHit2D hit;

     public EnemyHealthManager healthManager;
     bool isStunned = false;
     float stunDuration = 1f;
     float stunTimer = 0f;

    
   
    // Update is called once per frame
    void Update()
    {
        hit = Physics2D.Raycast(groundCheck.position, -transform.up, 1f, groundLayers);

        if(isStunned)
        {
            if (stunTimer > 0f)
            {
                stunTimer -= Time.deltaTime;
            }
            else
            {
                isStunned = false;
            }
        }
        
    }
    
     private void FixedUpdate()
    {
        if (!isStunned)
        {
            if (hit.collider != null)
            {
                if (isFacingRight)
                {
                    erb.velocity = new Vector2(speed, erb.velocity.y);
                }
                else
                {
                    erb.velocity = new Vector2(-speed, erb.velocity.y);
                }
            }
            else
            {
                isFacingRight = !isFacingRight;
                transform.localScale = new Vector3(-transform.localScale.x, 1f, 1f);
            }
        }
    }

    public void ApplyStun(float duration)
    {
        isStunned = true;
        stunTimer = duration;
        erb.velocity = Vector2.zero; // Stop the enemy's movement while stunned
    }


}
