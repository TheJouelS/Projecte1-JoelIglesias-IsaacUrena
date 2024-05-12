using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack_Angel : MonoBehaviour
{
    public string attackingTagAnimation = "isAttacking", playerBoddyTag = "PlayerBoddy";
    public float timerCooldown = 0.25f;
    public Angel_NotifyOnCollisionAttack NotifyCollisionPlayer;

    private Animator animator;
    private float timer;
    private bool canCount = false, isCollidingWithPlayer = false;

    void Start()
    {
        animator = GetComponent<Animator>();

        NotifyCollisionPlayer.NotifyCollisionEnter += IsCollidingWithPlayer;
        NotifyCollisionPlayer.NotifyCollisionExit += IsNotCollidingWithPlayer;
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
        {
            uint damageToPlayer = (uint)PlayerLevel.GetPlayerLevel() + 1;
            PlayerHealth.TakeDamage(damageToPlayer);
        }
    }
}
