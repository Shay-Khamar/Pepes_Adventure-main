using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ScoreManager : MonoBehaviour
{
    public static int score;
    TextMeshProUGUI text;

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();

        score = 0;
    }

    void Update()
    {
         if(score < 0)
         {
            score = 0;
         }


        text.text = "" + score;
    }

    public static  void  AddPoints(int pointsToAdd)
    {
        score  += pointsToAdd;
    }

    public static void Reset()
    {
        score = 0;
    }
}
