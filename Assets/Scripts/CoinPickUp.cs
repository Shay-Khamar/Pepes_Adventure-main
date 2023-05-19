using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickUp : MonoBehaviour
{
    public int pointsToAdd;
    AudioManger audioManger;

      private void Awake()
     {
        audioManger = GameObject.FindGameObjectWithTag("AudioTag").GetComponent<AudioManger>();
     }

    void OnTriggerEnter2D (Collider2D other)
    {
        if(other.GetComponent<PlayerMovement>() == null)
        return;
        audioManger.PlaySFX(audioManger.coin);
        ScoreManager.AddPoints(pointsToAdd);
        Destroy (gameObject);
      

    }
}
