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

    private void Start()
    {
        rgb = GetComponent<Rigidbody2D>();
        NotifyCollisionGround.NotifyCollisionEnterGround += SetGroundCollisionEnter;
        NotifyCollisionGround.NotifyCollisionExitGround += SetGroundCollisionExit;
    }
    void Update()
    {
        rgb.velocity = new Vector3(Input.GetAxis(horizontalInputAxisName) * Speed, rgb.velocity.y);

        if (Input.GetButtonDown(buttonDownName) && isOnGround)
        {
            rgb.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
        }
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
