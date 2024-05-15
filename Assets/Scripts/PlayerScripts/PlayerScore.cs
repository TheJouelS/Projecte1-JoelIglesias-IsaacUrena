using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    public int score = 0;

    public static PlayerScore instance;

    private void Awake()
    {
        if (PlayerScore.instance == null)
            PlayerScore.instance = this;
        else
            Destroy(gameObject);
    }

    public void UpScore()
    {
        if (PlayerLevel.GetPlayerLevel() >= PlayerLevel.GetMaxLevel() / 2)
        {
            if (score + 2 <= PlayerLevel.GetNextGoal())
                score += 2;
            else score++;
        }
        else
        {
            if (score < PlayerLevel.GetNextGoal())
                score++;
        }

        //Debug.Log("Tu puntuación Suma: " + score);
    }

    public void ReduceScore(bool playerLifeIsAtZero, uint decrementValue = 1)
    {
        if (playerLifeIsAtZero)
        {
            score = (int) Mathf.Round(score / 2);
            //Debug.Log("Tu puntuación se divide: " + score);
        }
        else
        {
            if (decrementValue <= score)
                score -= (int)decrementValue;
            else
                score = 0;
            //Debug.Log("Tu puntuación RESTA, ahora tienes: " + score);
        }
    }

    public static void ResetScore()
    {
        PlayerScore.instance.score = 0;
        //Debug.Log("Tu puntuación se resetea: " + score);
    }

    public static int GetScore()
    {
        return PlayerScore.instance.score;
    }
}
