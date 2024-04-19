using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerLevel : MonoBehaviour
{
    private static int playerLevel = 1;
    private static int maxLevel = 18;
    private static int nextGoal;
    private static int scoringMargin = 2;

    private void Start()
    {
        LevelUp(true);
    }

    void Update()
    {
        if (playerLevel < maxLevel)
            if (PlayerScore.GetScore() == nextGoal)
            {
                LevelUp(false);

                Debug.Log("Tu nivel es: " + playerLevel);
                Debug.Log("Siguiente objetivo: " + nextGoal);
            }
    }

    private static void LevelUp(bool firstTime)
    {
        if (!firstTime)
            playerLevel++;

        nextGoal = playerLevel * scoringMargin; //This result serves to increase the difference in points between one level and another, increasingly greater
    }

    public static int GetNextGoal()
    {
        return nextGoal;
    }

    public static int GetPlayerLevel()
    {
        return playerLevel;
    }

    public static int GetMaxLevel()
    {
        return maxLevel;
    }
}
