using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
   public int maxHealthPoints;
   public static int healthPoints;

   TextMeshProUGUI text;

   private LevelManager levelManager;

   public bool isDead;

   void Start() 
   {
    text = GetComponent<TextMeshProUGUI>();

    healthPoints = maxHealthPoints;

    levelManager = FindObjectOfType<LevelManager>();

    isDead = false;
   }


   void Update ()
   {
    if(healthPoints <= 0 && !isDead)
    {
        levelManager.RespawnPlayer();
        isDead = true;
    }

     text.text = "" + healthPoints;

   }


   public static void HurtPlayer(int damageToGive)
   {
    healthPoints -= damageToGive;
   }

   public void FullHealth()
   {

     healthPoints = maxHealthPoints;

   }


}
