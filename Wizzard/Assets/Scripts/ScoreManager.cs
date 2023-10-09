using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ScoreManager : MonoBehaviour
{
    public static int score;
    public static int bestScore;
    public TMP_Text output;
    public TMP_Text bestScoreOutput;
    
    public static ScoreManager current;
    private bool _isBestScoreLoaded = false;

    

    
    public  void AddPoints(int point)
    {
        score += point;
       UpdateScore();
    }

    public void UpdateScore()
    {
        output.text = "Score:" + score;
        if (_isBestScoreLoaded) {
            bestScoreOutput.text = "Your best score: " + bestScore;
        }
    }
    private void Awake()
    {
        current = this;
        output.text = "Score: 0";
       
        NetworkServices.Statistics.FetchBestScore(
            (f => {
                bestScore = (int)f;
                _isBestScoreLoaded = true;
            }),
            (error => {
                Debug.Log(error.GenerateErrorMessage());
            }));
    }
}
