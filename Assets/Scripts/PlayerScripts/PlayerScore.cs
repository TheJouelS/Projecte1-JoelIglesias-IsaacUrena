using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    private static int score = 0;

    public static void UpScore()
    {
        if (score < PlayerLevel.GetNextGoal())
        {
            score++;
            Debug.Log("Tu puntuación Suma: " + score);
        }
    }

    public static void ReduceScore(bool reset, uint decrementValue = 1)
    {
        if (reset)
        {
            score = 0;
            Debug.Log("Tu puntuación se resetea: " + score);

        }
        else
        {
            if (decrementValue <= score)
            {
                score -= (int) decrementValue;
                Debug.Log("Tu puntuación resta: " + score);
            }
        }
    }

    public static int GetScore()
    {
        return score;
    }
}
