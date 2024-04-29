using System;
using UnityEngine;

public class VikingAxe_NotifyOnCollision : MonoBehaviour
{
    public string tagToCheckPlayer = "PlayerBoddy";
    public event Action NotifyCollisionEnter;
    public event Action NotifyCollisionExit;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(tagToCheckPlayer))
            NotifyCollisionEnter?.Invoke();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(tagToCheckPlayer))
            NotifyCollisionExit?.Invoke();
    }
}
