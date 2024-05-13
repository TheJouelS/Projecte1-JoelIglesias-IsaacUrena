using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

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
    private bool lookingRight = true;

    private static Transform copy_playerPosition;

    private void Start()
    {
        rgb = GetComponent<Rigidbody2D>();
        timer = jumpCooldown;
        copy_playerPosition = transform;

        NotifyCollisionGround.NotifyCollisionEnterGround += SetGroundCollisionEnter;
        NotifyCollisionGround.NotifyCollisionExitGround += SetGroundCollisionExit;
    }
    void Update()
    {
        ProcessMovement();
        ProcessJump();
    }

    private void ProcessMovement()
    {
        float inputMovement = Input.GetAxis(horizontalInputAxisName);

        rgb.velocity = new Vector3(inputMovement * Speed, rgb.velocity.y);
        ManageOrientation(inputMovement);

        if (inputMovement != 0)
            Animator.SetBool(playerWalkingTag, true);
        else
            Animator.SetBool(playerWalkingTag, false);

        copy_playerPosition = transform;
    }

    private void ManageOrientation(float horizontalInput)
    { 
        if ((lookingRight && horizontalInput < 0) || (!lookingRight && horizontalInput > 0))
        {
            lookingRight = !lookingRight;
            playerSprite.flipX = !lookingRight;
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
        Animator.SetBool(playerJumpingTag, true);
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
