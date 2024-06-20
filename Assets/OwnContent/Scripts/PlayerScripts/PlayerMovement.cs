using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] string horizontalInputAxisName = "Horizontal";
    [SerializeField] string buttonDownName = "Jump";
    [SerializeField] string playerWalkingTag = "isWalking", playerJumpingTag = "isJumping";

    public SpriteRenderer playerSprite;
    public Animator Animator;
    public NotifyOnCollision NotifyCollisionGround;
    public float Speed = 4.5f, JumpForce = 20f, jumpCooldown;

    private float timer;
    private bool isOnGround;
    private Rigidbody2D rgb;
    private bool lookingRight = true, isWalkig = false;

    private static Transform copy_playerPosition;

    private void Start()
    {
        rgb = GetComponent<Rigidbody2D>();
        timer = jumpCooldown;
        copy_playerPosition = transform;

        NotifyCollisionGround.NotifyCollisionEnterGround += SetGroundCollisionEnter;
        NotifyCollisionGround.NotifyCollisionExitGround += SetGroundCollisionExit;

        isWalkig = false;

        SoundManager.instance.s_walk.volume = 0.2f;
        SoundManager.instance.s_walk.pitch = 1f;
        SoundManager.instance.s_walk.loop = true;
    }

    void Update()
    {
        if (!GameFlowManager.gameIsPaused)
        {
            ProcessMovement();
            ProcessJump();
        }
        else
            SoundManager.instance.s_walk.Pause();
    }

    private void ProcessMovement()
    {
        float inputMovement = Input.GetAxis(horizontalInputAxisName);

        rgb.velocity = new Vector3(inputMovement * Speed, rgb.velocity.y);
        ManageOrientation(inputMovement);

        if (inputMovement != 0)
        {
            Animator.SetBool(playerWalkingTag, true);

            if (!isWalkig && isOnGround)
            {
                if (SoundManager.instance.s_walk != null)
                    SoundManager.instance.s_walk.Play();
                isWalkig = true;
            }
        }
        else
        {
            if (SoundManager.instance.s_walk != null)
                SoundManager.instance.s_walk.Pause();
            Animator.SetBool(playerWalkingTag, false);
        }

        copy_playerPosition = transform;
    }

    private void ManageOrientation(float horizontalInput)
    { 
        if ((lookingRight && horizontalInput < 0) || (!lookingRight && horizontalInput > 0))
        {
            lookingRight = !lookingRight;
            playerSprite.flipX = !lookingRight;

            isWalkig = false;
        }
    }

    private void ProcessJump()
    {
        if (timer > 0f)
            timer -= Time.deltaTime;

        if (Input.GetButtonDown(buttonDownName) && isOnGround && timer <= 0f)
            rgb.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
    }

    void SetGroundCollisionEnter()
    {
        isOnGround = true;
        timer = jumpCooldown;
        Animator.SetBool(playerJumpingTag, false);
    }

    void SetGroundCollisionExit()
    {
        isOnGround = false;
        isWalkig = false;
        Animator.SetBool(playerJumpingTag, true);

        if (SoundManager.instance.s_walk != null)
            SoundManager.instance.s_walk.Stop();
    }

    public bool GetPlayerIsOnGround()
    {
        return isOnGround;
    }

    public static Transform GetPlayerPosition()
    {
        return copy_playerPosition;
    }
}
