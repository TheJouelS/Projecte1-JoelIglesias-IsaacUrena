using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private static uint currentLife = 6;
    private static uint maxLife = 6;

    public static void TakeDamage()
    {
        if (currentLife > 0)
            currentLife--;
    }

    public static void Heal()//Para gotas azules...
    {
        if (currentLife < maxLife)
            currentLife++;
    }

    public static void SetLifeNewLevel()
    {
        maxLife++;
        currentLife = maxLife;
    }

    public static uint GetHearts()
    {
        return currentLife;
    }
}
