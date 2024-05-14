using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemyAttack_Viking : MonoBehaviour
{
    [SerializeField] LayerMask PlayerBoddyLayer;
    public bool DebugEditor = false;
    public AxeSpawner axeSpawner;
    public VikingAxe_NotifyOnCollision NotifyCollisionAxe;
    public string attackingTagAnimation = "isAttacking";
    public float rangedAttack, hitDistance, timerCooldown = 0.25f;
    public uint damageToPlayerRestValue = 3;

    private Transform playerPosition;
    private Animator animator;
    private float timer;
    private bool canCount = false;

    void Start()
    {
        animator = GetComponent<Animator>();

        NotifyCollisionAxe.NotifyCollisionEnter += RunAttack;
        NotifyCollisionAxe.NotifyCollisionExit += ExitAttack;

        SetPlayerPosition();
        timer = timerCooldown;

        if (PlayerLevel.GetPlayerLevel() >= damageToPlayerRestValue * 2)
            damageToPlayerRestValue = (uint)PlayerLevel.GetPlayerLevel() - damageToPlayerRestValue;
    }

    private void Update()
    {
        SetPlayerPosition();
        PlayerIsInRange();

        if (canCount)
            timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            animator.SetBool(attackingTagAnimation, false);
            timer = timerCooldown;
            canCount = false;
        }
    }
    private void SetPlayerPosition()
    {
        playerPosition = PlayerMovement.GetPlayerPosition();
    }

    private void PlayerIsInRange()
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
        canCount = true;
    }

    //It's called through Animator:
    private void PlayerTakesDamage()
    {
        RaycastHit2D raycastHit2D;

        if (raycastHit2D = Physics2D.Raycast(transform.position, transform.right * transform.localScale.normalized.x, hitDistance, PlayerBoddyLayer))
        {
            PlayerHealth.TakeDamage(damageToPlayerRestValue);
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
