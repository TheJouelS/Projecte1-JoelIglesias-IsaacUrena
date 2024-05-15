using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerLevel : MonoBehaviour
{
    public int playerLevel = 1;
    public int maxLevel = 18;
    public int nextGoal;
    public int scoringMargin = 5; //USAR PARA PONER NIVEL!!!!!!!!!!!!!!!!!! en nextGoal (en vez del 1 hardcodeado que hay)

    /*
    private static int playerLevel = 1;
    private static int maxLevel = 18;
    private static int nextGoal;
    public static int scoringMargin = 5;*/

    public static PlayerLevel instance;

    private void Awake()
    {
        if (PlayerLevel.instance == null)
            PlayerLevel.instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        LevelUp(true);
    }

    void Update()
    {
        if (playerLevel < maxLevel)
            if (PlayerScore.GetScore() >= nextGoal)
            {
                LevelUp(false);

                Debug.Log("Tu nivel es: " + playerLevel);
                Debug.Log("Siguiente objetivo: " + nextGoal);
            }
    }

    private void LevelUp(bool firstTime)
    {
        if (!firstTime)
            playerLevel++;

        nextGoal = playerLevel * 1; //This result serves to increase the difference in points between one level and another, increasingly greater
    }

    public static int GetNextGoal()
    {
        return instance.nextGoal;
    }

    public static int GetPlayerLevel()
    {
        return instance.playerLevel;
    }

    public static int GetMaxLevel()
    {
        return instance.maxLevel;
    }
}
