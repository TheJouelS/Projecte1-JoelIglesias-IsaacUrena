using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement_Angel : MonoBehaviour
{
    [SerializeField] Transform playerPosition;

    public float speed;
    public string playerTag = "PlayerBoddy";
    //public string walkingTagAnimation = "isWalking";

    private SpriteRenderer spr;
    private Vector3 direction = Vector3.zero;
    private bool collidedWithPlayer = false;

    private void Start()
    {
        spr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        ProcessMovement();
    }

    private void ProcessMovement()
    {
        if (!collidedWithPlayer)
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
}
