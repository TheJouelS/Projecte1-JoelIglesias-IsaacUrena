using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEditor;
using UnityEngine;

public class EnemyMovement_Viking : MonoBehaviour
{
    public AxeSpawner axeSpawner;
    public VikingAxe_NotifyOnCollision NotifyCollisionAxe;
    public float speed;
    public float impulseDamage;
    public string walkingTagAnimation = "isWalking";

    private Transform playerPosition;
    private Animator animator;
    private Vector3 direction = Vector3.zero;
    private bool collidedWithPlayer = false;
    private bool isDying = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool(walkingTagAnimation, true);

        NotifyCollisionAxe.NotifyCollisionEnter += StopMoving;
        NotifyCollisionAxe.NotifyCollisionExit += KeepMoving;

        SetPlayerPosition();

        //animator.SetFloat("animSpeed", 0f);
    }

    void Update()
    {
        SetPlayerPosition();
        ProcessMovement();
        ManageOrientation();
    }

    private void ProcessMovement()
    {
        if (!(AxeSpawner.playerIsUp && axeSpawner.playerIsInRange))
        {
            if (!collidedWithPlayer && !isDying)
            {
                if (playerPosition != null)
                {
                    direction.x = playerPosition.position.x - transform.position.x;
                    transform.position = transform.position + direction.normalized * speed * Time.deltaTime;// * TimeManager.instance.CustomTimeDilation;
                }
            }
        }
    }

    private void SetPlayerPosition()
    {
        if (PlayerMovement.GetPlayerPosition() != null)
            playerPosition = PlayerMovement.GetPlayerPosition();
        else
            playerPosition = transform;
    }

    private void ManageOrientation()
    {
        if (direction.x < -0.1f)
            transform.localScale = new Vector2 (Mathf.Abs(transform.localScale.x) * (-1), transform.localScale.y);

        if (direction.x > 0.1f)
            transform.localScale = new Vector2 (Mathf.Abs(transform.localScale.x), transform.localScale.y);
    }

    private void StopMoving()
    {
        collidedWithPlayer = true;
    }

    private void KeepMoving()
    {
        collidedWithPlayer = false;
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
