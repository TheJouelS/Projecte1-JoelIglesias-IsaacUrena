using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EnemyMovement_Angel : MonoBehaviour
{
    public float speed, limitedPosition_Y, impulseDamage;
    public string playerTag = "PlayerBoddy", walkingTagAnimation = "isWalking";

    private Transform playerPosition;
    private SpriteRenderer spr;
    private Vector3 direction = Vector3.zero;
    private bool collidedWithPlayer = false;
    private Animator animator;
    private bool isDying = false;
    private Rigidbody2D rb;

    private void Start()
    {
        spr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        animator.SetBool(walkingTagAnimation, true);
        SetPlayerPosition();
    }

    void Update()
    {
        SetPlayerPosition();
    }
    void FixedUpdate()
    {
        ProcessMovement();
    }

    private void SetPlayerPosition()
    {
        if (PlayerMovement.GetPlayerPosition() != null)
            playerPosition = PlayerMovement.GetPlayerPosition();
        else
            playerPosition = transform;
    }

    private void ProcessMovement()
    {
        if (!collidedWithPlayer && !isDying && rb.position.y <= limitedPosition_Y && playerPosition.position.y <= limitedPosition_Y)
        {
            if (playerPosition != null)
            {
                direction.x = playerPosition.position.x - rb.position.x;
                rb.position = rb.position + Vector2.right * direction.normalized.x * speed * Time.fixedDeltaTime;
            }
        }
        else if (!collidedWithPlayer && !isDying)
        {
            if (playerPosition != null)
            {
                direction = (Vector2)playerPosition.position - rb.position;
                rb.position = rb.position + (Vector2)direction.normalized * speed * Time.fixedDeltaTime;
            }
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
            rb.position = rb.position + Vector2.right * impulseDamage;
        else
            rb.position = rb.position + Vector2.left * impulseDamage;
    }

    private void IsDying()
    {
        IsTakingDamage();
        isDying = true;
    }
}
