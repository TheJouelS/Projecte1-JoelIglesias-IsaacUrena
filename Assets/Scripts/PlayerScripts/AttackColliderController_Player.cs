using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AttackColliderController_Player : MonoBehaviour
{
    public Collider2D attackCollider, playerBoddy, playerBoddyWhenIsAttacking;
    public static uint setDamage;

    private void Start()
    {
        attackCollider.enabled = false;
        playerBoddyWhenIsAttacking.enabled = false;
    }

    private void EnableAttackCollider()
    {
        playerBoddy.enabled = false;
        playerBoddyWhenIsAttacking.enabled = true;
        attackCollider.enabled = true;
    }

    private void DisableAttackCollider()
    {
        attackCollider.enabled = false;
        playerBoddyWhenIsAttacking.enabled = false;
        playerBoddy.enabled = true;

        MeleeColliderAttack_Player.SetAttacked();
    }

    private void OnDestroy()
    {
        DisableAttackCollider();
    }
}
