using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private uint hitDamage;
    [SerializeField] private float timeBetweenHit;
    [SerializeField] private GameObject playerBoddy;

    public KeyCode keyButtonAttack;
    public Animator animator;
    public string attackingTagAnimation = "isAttacking", enemyTag = "Viking_Enemy";

    private float timeNextAttack;

    private void Start()
    {
        AttackColliderController_Player.setDamage = hitDamage;
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
        animator.SetTrigger(attackingTagAnimation); //This will active the AttackColliderController_Player Script functions
    }
}
