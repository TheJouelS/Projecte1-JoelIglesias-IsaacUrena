using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack_Viking : MonoBehaviour
{
    public VikingAxe_NotifyOnCollision NotifyCollisionAxe;
    public string attackingTagAnimation = "isAttacking";

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        NotifyCollisionAxe.NotifyCollisionEnter += RunAttack;
        NotifyCollisionAxe.NotifyCollisionExit += ExitAttack;
    }

    private void RunAttack()
    {
        animator.SetBool(attackingTagAnimation, true);
    }

    private void ExitAttack()
    {
        animator.SetBool(attackingTagAnimation, false);
    }
}
