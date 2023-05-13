using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    private float fallDelay = 1f;
    private float respawnDelay = 5f;

    [SerializeField] private Rigidbody2D rb;
     private Vector2 initialPosition;
    private Quaternion initialRotation;


    private void Start()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(FallAndRespawn());

        }

    }

    private IEnumerator FallAndRespawn()
    {
        yield return new WaitForSeconds(fallDelay);
        rb.bodyType = RigidbodyType2D.Dynamic;
        yield return new WaitForSeconds(respawnDelay);


        rb.bodyType = RigidbodyType2D.Static;
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;
        transform.position = initialPosition;
        transform.rotation = initialRotation;

    } 
}
