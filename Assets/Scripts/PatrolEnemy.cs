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
    
   
    // Update is called once per frame
    void Update()
    {
        hit = Physics2D.Raycast(groundCheck.position, -transform.up, 1f, groundLayers);
        
    }
    
    private void FixedUpdate()
    {
        if (hit.collider != false)
        {
            if(isFacingRight)
            {
                erb.velocity = new Vector2(speed, erb.velocity.y);
            } else
            {
                erb.velocity = new Vector2(-speed, erb.velocity.y);

            }

        }else
        {
            isFacingRight = !isFacingRight;
            transform.localScale = new Vector3(-transform.localScale.x,1f,1f);


        }
    }
}
