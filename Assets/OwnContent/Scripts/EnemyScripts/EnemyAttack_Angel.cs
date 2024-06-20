using UnityEngine;

public class EnemyAttack_Angel : MonoBehaviour
{
    public string attackingTagAnimation = "isAttacking", playerBoddyTag = "PlayerBoddy";
    public float timerCooldown = 0.25f;
    public Angel_NotifyOnCollisionAttack NotifyCollisionPlayer;
    public uint damageToPlayerRestValue = 1;

    private Animator animator;
    private float timer;
    private bool canCount = false, isCollidingWithPlayer = false;

    void Start()
    {
        animator = GetComponent<Animator>();

        NotifyCollisionPlayer.NotifyCollisionEnter += IsCollidingWithPlayer;
        NotifyCollisionPlayer.NotifyCollisionExit += IsNotCollidingWithPlayer;

        if (PlayerLevel.GetPlayerLevel() >= damageToPlayerRestValue * 2)
            damageToPlayerRestValue = (uint)PlayerLevel.GetPlayerLevel() - damageToPlayerRestValue;
    }

    void Update()
    {
        if (canCount)
            timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            animator.SetBool(attackingTagAnimation, false);
            timer = timerCooldown;
            canCount = false;
        }
    }

    private void IsCollidingWithPlayer()
    {
        isCollidingWithPlayer = true;
    }

    private void IsNotCollidingWithPlayer()
    {
        isCollidingWithPlayer = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(playerBoddyTag))
            animator.SetBool(attackingTagAnimation, true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(playerBoddyTag))
            canCount = true;
    }

    //It's called through Animator:
    private void PlayerTakesDamage()
    {
        if (isCollidingWithPlayer)
            PlayerHealth.TakeDamage(damageToPlayerRestValue);
    }
}
