using UnityEngine;

public class EnemyMovement_Angel : MonoBehaviour
{
    public float speed, limitedPosition_Y, impulseDamage;
    public string playerTag = "PlayerBoddy", walkingTagAnimation = "isWalking";
    public AudioClip c_angelAttack, c_angelFlying;

    private Transform playerPosition;
    private SpriteRenderer spr;
    private Vector3 direction = Vector3.zero;
    private bool collidedWithPlayer = false;
    private Animator animator;
    private bool isDying = false;
    private Rigidbody2D rb;
    private AudioSource s_angelSounds;
    private float volumeOfFlyingSound;

    private void Start()
    {
        spr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        animator.SetBool(walkingTagAnimation, true);
        SetPlayerPosition();

        s_angelSounds = GetComponent<AudioSource>();
        s_angelSounds.clip = c_angelFlying;
        s_angelSounds.Play();
        s_angelSounds.loop = true;
        s_angelSounds.pitch = 1.4f;

        float n;
        if (EnemySpawner.enemyCounter <= 5)
            n = Random.Range(0.2f, 0.4f);
        else
            n = 0f;
        volumeOfFlyingSound = n;
        s_angelSounds.volume = volumeOfFlyingSound;
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
        if (!collidedWithPlayer)
        {
            s_angelSounds.Stop();
            s_angelSounds.clip = c_angelAttack;
            s_angelSounds.volume = 0.3f;
            s_angelSounds.pitch = 1f;
            s_angelSounds.Play();
        }

        collidedWithPlayer = true;
    }

    private void KeepMoving()
    {
        if (collidedWithPlayer)
        {
            s_angelSounds.Stop();
            s_angelSounds.clip = c_angelFlying;
            s_angelSounds.volume = volumeOfFlyingSound;
            s_angelSounds.pitch = 1.4f;
            s_angelSounds.Play();
        }

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
        s_angelSounds.Stop();
        isDying = true;
    }
}
