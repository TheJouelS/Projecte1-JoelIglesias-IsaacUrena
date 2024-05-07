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
            Debug.Log("Tu puntuaci�n Suma: " + score);
        }
    }

    public static void ReduceScore(bool reset, uint decrementValue = 1)
    {
        if (reset)
        {
            score = 0;
            Debug.Log("Tu puntuaci�n se resetea: " + score);

        }
        else
        {
            if (decrementValue <= score)
            {
                score -= (int) decrementValue;
                Debug.Log("Tu puntuaci�n resta: " + score);
            }
        }
    }

    public static int GetScore()
    {
        return score;
    }
}
