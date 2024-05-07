using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private uint currentLife, maxLife;
    [SerializeField] private float timeBeingDamaged;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private static bool isBeingDamaged = false;
    private static uint copy_currentLife;
    private static uint copy_maxLife;
    private static float copy_timeBeingDamaged;
    private static Color normalColor;

    private void Start()
    {
        copy_currentLife = currentLife;
        copy_maxLife = maxLife;
        copy_timeBeingDamaged = timeBeingDamaged;
        normalColor = spriteRenderer.color;
    }

    private void Update()
    {
        if (isBeingDamaged)
            ChangeSpriteColor();

        if (isPlayerAtZero())
            ResetCurrentValues();
    }

    private void ChangeSpriteColor() 
    {
        spriteRenderer.color = Color.red;

        if (copy_timeBeingDamaged > 0f)
            copy_timeBeingDamaged -= Time.deltaTime;
        else
        {
            spriteRenderer.color = Color.white;
            copy_timeBeingDamaged = timeBeingDamaged;
            isBeingDamaged = false;
        }
    }

    public static void TakeDamage(uint damage)
    {
        if (copy_currentLife > 0)
            copy_currentLife--;

        PlayerScore.ReduceScore(isPlayerAtZero(), damage);
        isBeingDamaged = true;
    }

    public static void Heal() //Para gotas azules...
    {
        if (copy_currentLife < copy_maxLife)
            copy_currentLife++;
    }

    public static void SetLifeNewLevel()
    {
        copy_maxLife++;
        copy_currentLife = copy_maxLife;

        Debug.Log("Max: " + copy_maxLife + " | Current: " + copy_currentLife);
    }

    private static void ResetCurrentValues()
    {
        copy_currentLife = copy_maxLife;
    }

    public static bool isPlayerAtZero() 
    {
        return copy_currentLife == 0;
    }
}
