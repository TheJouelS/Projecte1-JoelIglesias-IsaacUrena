using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Transform hitController_Right, hitController_Left;
    [SerializeField] private float hitRadius;
    [SerializeField] private uint hitDamage;
    [SerializeField] private float timeBetweenHit;
    [SerializeField] private float timeNextAttack;

    public KeyCode keyButtonAttack;
    public string attackingTagAnimation = "isAttacking", enemyTag = "Viking_Enemy";

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (timeNextAttack > 0f)
            timeNextAttack -= Time.deltaTime;

        if (Input.GetKeyUp(keyButtonAttack) && timeNextAttack <= 0f)
        {
            Hit();
            timeNextAttack = timeBetweenHit;
        }
    }

    private void Hit()
    {
        animator.SetTrigger(attackingTagAnimation);

        Collider2D[] objectsRight = Physics2D.OverlapCircleAll(hitController_Right.position, hitRadius);
        Collider2D[] objectsLeft = Physics2D.OverlapCircleAll(hitController_Left.position, hitRadius);

        foreach (Collider2D collider in objectsRight)
            if (collider.CompareTag(enemyTag))
                collider.transform.GetComponent<EnemyHealth_Viking>().TakeDamage(hitDamage);

        foreach (Collider2D collider in objectsLeft)
            if (collider.CompareTag(enemyTag))
                collider.transform.GetComponent<EnemyHealth_Viking>().TakeDamage(hitDamage);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(hitController_Right.position, hitRadius);
        Gizmos.DrawWireSphere(hitController_Left.position, hitRadius);
    }
}
