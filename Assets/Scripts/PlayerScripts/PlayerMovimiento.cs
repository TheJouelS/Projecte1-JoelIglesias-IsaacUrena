using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovimiento : MonoBehaviour
{
    public float speed = 0f, jumpForce = 5f;
    private bool isOnGround;
    private string horizontalInputAxisName = "Horizontal";
    private Rigidbody2D rgb;

    private void Start()
    {
        rgb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        rgb.velocity = new Vector3(Input.GetAxis(horizontalInputAxisName) * speed, rgb.velocity.y);

        if (Input.GetButtonDown("Jump") && isOnGround)
        {
            rgb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
            isOnGround = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
            isOnGround = false;
    }
}
