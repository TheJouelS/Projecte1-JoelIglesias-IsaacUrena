using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] string horizontalInputAxisName = "Horizontal";
    [SerializeField] string buttonDownName = "Jump";

    public NotifyOnCollision NotifyCollisionGround;
    public float Speed = 4.5f, JumpForce = 20f;

    private bool isOnGround;
    private Rigidbody2D rgb;
    private bool lookingRight = false;

    private void Start()
    {
        rgb = GetComponent<Rigidbody2D>();
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
    }

    private void ManageOrientation(float horizontalInput)
    {
        if ((lookingRight && horizontalInput < 0) || (!lookingRight && horizontalInput > 0))
        {
            lookingRight = !lookingRight;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y);
        }
    }

    private void ProcessJump()
    {
        if (Input.GetButtonDown(buttonDownName) && isOnGround)
            rgb.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
    }

    void SetGroundCollisionEnter()
    {
        isOnGround = true;
    }

    void SetGroundCollisionExit()
    {
        isOnGround = false;
    }

    public bool GetPlayerIsOnGround()
    {
        return isOnGround;
    }
}
