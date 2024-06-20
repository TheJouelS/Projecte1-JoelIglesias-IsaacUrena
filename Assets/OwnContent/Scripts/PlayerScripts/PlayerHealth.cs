using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float timeBeingDamaged;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private static bool isBeingDamaged = false;
    private static int copy_currentLife;
    private static int copy_maxLife;
    private static float copy_timeBeingDamaged;

    private void Start()
    {
        if (PlayerLevel.GetPlayerLevel() == 1)
        {
            copy_maxLife = PlayerLevel.GetPlayerLevel() + 2;
            copy_currentLife = copy_maxLife;
            copy_timeBeingDamaged = timeBeingDamaged;
        }

        SoundManager.instance.s_takeDamage.volume = 0.3f;
        SoundManager.instance.s_takeDamage.pitch = 1f;
        SoundManager.instance.s_takeDamage.loop = false;
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

    public static void TakeDamage(uint damage, bool itIsFromAnAxe = false)
    {
        if (copy_currentLife > 0 && !itIsFromAnAxe)
            copy_currentLife--;

        PlayerScore.instance.ReduceScore(isPlayerAtZero(), damage);
        isBeingDamaged = true;

        SoundManager.instance.s_takeDamage.Play();
    }

    public static void Heal() //For purple drops (will be implemented in Delivery 3)
    {
        if (copy_currentLife < copy_maxLife)
            copy_currentLife++;
    }

    public static void SetLifeNewLevel()
    {
        copy_maxLife++;
        copy_currentLife = copy_maxLife;
    }

    private static void ResetCurrentValues()
    {
        copy_currentLife = copy_maxLife;
    }

    public static bool isPlayerAtZero() 
    {
        return copy_currentLife == 0;
    }

    public static int GetCurrentLife()
    {
        return copy_currentLife;
    }

    public static int GetMaxLife()
    {
        return copy_maxLife;
    }
}
