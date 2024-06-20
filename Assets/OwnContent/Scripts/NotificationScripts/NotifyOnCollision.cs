using System;
using UnityEngine;

public class NotifyOnCollision : MonoBehaviour
{
    public string TagToCheckGround = "Ground";
    public event Action NotifyCollisionEnterGround;
    public event Action NotifyCollisionExitGround;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == TagToCheckGround)
        {
            NotifyCollisionEnterGround?.Invoke();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == TagToCheckGround)
        {
            NotifyCollisionExitGround?.Invoke();
        }
    }
}
