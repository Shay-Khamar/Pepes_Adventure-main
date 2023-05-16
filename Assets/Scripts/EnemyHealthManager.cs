using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{

    public int EnemyHealth;

    public int pointsOnDeath;

    public bool takingDamage;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(EnemyHealth <= 0)
        {
            ScoreManager.AddPoints(pointsOnDeath);
            Destroy(gameObject);
        }
    }

    public void giveDamage(int damageToGive)
    {
        takingDamage = true;
        EnemyHealth -= damageToGive;
        takingDamage = false;
    }
}
