using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack_Viking : MonoBehaviour
{
    [SerializeField] Transform playerPosition;
    public bool Debug = false;
    public AxeSpawner axeSpawner;
    public VikingAxe_NotifyOnCollision NotifyCollisionAxe;
    public string attackingTagAnimation = "isAttacking";
    public float rangedAttack;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();

        NotifyCollisionAxe.NotifyCollisionEnter += RunAttack;
        NotifyCollisionAxe.NotifyCollisionExit += ExitAttack;
    }

    private void Update()
    {
        Vector3 direction = playerPosition.position - transform.position;
        
        if (direction.magnitude <= rangedAttack)
            axeSpawner.playerIsInRange = true;
        else
            axeSpawner.playerIsInRange = false;
    }

    private void RunAttack()
    {
        animator.SetBool(attackingTagAnimation, true);
    }

    private void ExitAttack()
    {
        animator.SetBool(attackingTagAnimation, false);
    }

    //It's called through Animator:
    private void PlayerTakesDamage()
    {
        uint damageToPlayer = (uint) PlayerLevel.GetPlayerLevel();
        PlayerHealth.TakeDamage(damageToPlayer);
    }

    public void CallToStopThrowingAnimation()
    {
        axeSpawner.StopThrowingAnimation();
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (!Debug)
            return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, rangedAttack);  
    }
#endif
}
