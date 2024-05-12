using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EnemyMovement_Angel : MonoBehaviour
{
    [SerializeField] Transform playerPosition;

    public float speed, limitedPosition_Y, impulseDamage;
    public string playerTag = "PlayerBoddy", walkingTagAnimation = "isWalking";

    private SpriteRenderer spr;
    private Vector3 direction = Vector3.zero;
    private bool collidedWithPlayer = false;
    private Animator animator;
    private bool isDying = false;

    private void Start()
    {
        spr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        animator.SetBool(walkingTagAnimation, true);
    }

    void Update()
    {
        ProcessMovement();
    }

    private void ProcessMovement()
    {
        if (!collidedWithPlayer && !isDying && transform.position.y <= limitedPosition_Y && playerPosition.position.y <= limitedPosition_Y)
        {
            direction.x = playerPosition.position.x - transform.position.x;
            transform.position = transform.position + Vector3.right * direction.normalized.x * speed * Time.deltaTime;
        }
        else if (!collidedWithPlayer && !isDying)
        {
            direction = playerPosition.position - transform.position;
            transform.position = transform.position + direction.normalized * speed * Time.deltaTime;
        }

        ManageOrientation();
    }

    private void ManageOrientation()
    {
        if (direction.x < -0.1f)
            spr.flipX = true;

        if (direction.x > 0.1f)
            spr.flipX = false;
    }

    private void StopMoving()
    {
        collidedWithPlayer = true;
    }

    private void KeepMoving()
    {
        collidedWithPlayer = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(playerTag))
            StopMoving();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(playerTag))
            KeepMoving();
    }

    //It's called through Animator:
    private void IsTakingDamage()
    {
        if (direction.x < 0f)
            transform.position = transform.position + Vector3.right * impulseDamage;
        else
            transform.position = transform.position + Vector3.left * impulseDamage;
    }

    private void IsDying()
    {
        IsTakingDamage();
        isDying = true;
    }
}
