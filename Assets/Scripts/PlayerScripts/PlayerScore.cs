using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    private static int score = 0;

    public static void SetScore(bool reset)
    {
        if(reset)
            score = 0;
        else if (score < PlayerLevel.GetNextGoal())
        {
            score++;
            Debug.Log("Tu puntuación: " + score);
        }
    }

    public static int GetScore()
    {
        return score;
    }
}
