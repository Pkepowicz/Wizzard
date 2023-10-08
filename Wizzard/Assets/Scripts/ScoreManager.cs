using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ScoreManager : MonoBehaviour
{
    public static int score;
    public TMP_Text output;
    
    public static ScoreManager current;
    

    

    
    public  void AddPoints(int point)
    {
        score += point;
       UpdateScore();
    }

    public void UpdateScore()
    {
        output.text = "Score:" + score;
        
    }
    private void Awake()
    {
        current = this;
        output.text = "Score:0";
       
    }
}
