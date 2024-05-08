using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack_Viking : MonoBehaviour
{
    [SerializeField] LayerMask PlayerBoddyLayer;
    [SerializeField] Transform playerPosition;
    public bool DebugEditor = false;
    public AxeSpawner axeSpawner;
    public VikingAxe_NotifyOnCollision NotifyCollisionAxe;
    public string attackingTagAnimation = "isAttacking";
    public float rangedAttack, hitDistance;

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
        RaycastHit2D raycastHit2D;

        if (raycastHit2D = Physics2D.Raycast(transform.position, transform.right * transform.localScale.normalized.x, hitDistance, PlayerBoddyLayer))
        {
            uint damageToPlayer = (uint) PlayerLevel.GetPlayerLevel();
            PlayerHealth.TakeDamage(damageToPlayer);
        }
    }

    public void CallToStopThrowingAnimation()
    {
        axeSpawner.StopThrowingAnimation();
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (!DebugEditor)
            return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, rangedAttack);  
    }
#endif
}
