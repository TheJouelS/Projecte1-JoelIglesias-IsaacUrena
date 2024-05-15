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
    public int scoringMargin = 5; //USAR PARA PONER NIVEL!!!!!!!!!!!!!!!!!! en nextGoal (en vez del 1 hardcodeado que hay)

    private void Start()
    {
        playerLevel = 1;
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

        nextGoal = playerLevel * 1; //This result serves to increase the difference in points between one level and another, increasingly greater
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
