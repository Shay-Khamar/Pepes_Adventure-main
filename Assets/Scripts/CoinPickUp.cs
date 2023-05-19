using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickUp : MonoBehaviour
{
    public int pointsToAdd;

    [SerializeField] private AudioSource collectionSoundEffect;


    void OnTriggerEnter2D (Collider2D other)
    {
        if(other.GetComponent<PlayerMovement>() == null)
        return;
        collectionSoundEffect.Play();
        ScoreManager.AddPoints(pointsToAdd);
        Destroy (gameObject);
      

    }
}
