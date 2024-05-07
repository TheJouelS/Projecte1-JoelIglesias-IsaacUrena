using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyHealth_Viking : MonoBehaviour
{
    public uint currentLife;
    public string vikingDamagedTag, vikingDiedTag;
    public bool isDying = false;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
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
        else animator.SetTrigger(vikingDamagedTag);
    }

    private void Death()
    {
        isDying = true;
        animator.SetTrigger(vikingDiedTag);
    }

    //It's called through Animator:
    private void Disappear()
    {
        Destroy(gameObject);
    }
}
