using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEditor;
using UnityEngine;

public class EnemyMovement_Viking : MonoBehaviour
{
    [SerializeField] Transform playerPosition;

    public VikingAxe_NotifyOnCollision NotifyCollisionAxe;
    public float speed;
    public string walkingTagAnimation = "isWalking";

    private SpriteRenderer spr;
    private Animator animator;
    private Vector3 direction = Vector3.zero;
    private bool collidedWithPlayer = false;

    private void Start()
    {
        spr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        animator.SetBool(walkingTagAnimation, true);

        NotifyCollisionAxe.NotifyCollisionEnter += StopMoving;
        NotifyCollisionAxe.NotifyCollisionExit += KeepMoving;
    }

    void Update()
    {
        ProcessMovement();
    }

    private void ProcessMovement()
    {
        if (!collidedWithPlayer)
        {
            direction.x = playerPosition.position.x - transform.position.x;
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
}
