using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeColliderAttack_Player : MonoBehaviour
{
    public string enemyTag = "Viking_Enemy"; //El de Angel no hace falta, hace la misma función que vikingo
    
    public static uint damage;
    private static bool attacked = false;

    private void Start()
    {
        damage = AttackColliderController_Player.setDamage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(enemyTag))
            if (!attacked)
            {
                attacked = true;
                //collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(damage);
            }
    }

    public static void SetAttacked()
    {
        attacked = false;
    }
}
