using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public uint currentLife;
    public string damagedTag, diedTag, colliderPlayerAttack;
    public bool isDying = false;
    public float timerCooldown;

    private Animator animator;
    private float timer;
    private bool canCount = false, canTakeDame = true;

    private void Start()
    {
        animator = GetComponent<Animator>();
        timer = timerCooldown;
    }

    private void Update()
    {
        if (canCount)
            timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            timer = timerCooldown;
            canCount = false;
            canTakeDame = true;
        }
    }

    public uint GetHearts()
    {
        return currentLife;
    }

    public void TakeDamage(uint damage)
    {
        if (damage < currentLife) currentLife -= damage;
        else currentLife = 0;

        if (currentLife <= 0) Death();
        else animator.SetTrigger(damagedTag);
    }

    private void Death()
    {
        isDying = true;
        animator.SetTrigger(diedTag);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (canTakeDame)
        {
            if (collision.CompareTag(colliderPlayerAttack))
            {
                TakeDamage(MeleeColliderAttack_Player.damage);
                canCount = true;
                canTakeDame = false;
            }
        }
    }

    //It's called through Animator:
    private void Disappear()
    {
        Destroy(gameObject);
    }

    private void ConvertSpriteToRed()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.red;
    }

    private void ReturnSpriteToWhite()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    }
}
