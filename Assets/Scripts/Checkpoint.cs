using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
     public LevelManager levelManger;

    // Start is called before the first frame update
    void Start()
    {
        levelManger =  FindObjectOfType<LevelManager>();
        
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
         if(other.gameObject.CompareTag("Player"))
         {
            levelManger.currentCheckpoint = gameObject;

         }

    }
}
