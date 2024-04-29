using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyHealth_Viking : MonoBehaviour
{
    public uint currentLife;
    public uint maxLife;
    public string vikingDamagedTag, vikingDiedTag;

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
        if (currentLife > 0)
            currentLife -= damage;

        if (currentLife == 0)
            Death();
        else
            animator.SetTrigger(vikingDamagedTag);
    }

    private void Death()
    {
        animator.SetTrigger(vikingDiedTag);
    }

    private void Disappear()
    {
        Destroy(gameObject);
    }
}
