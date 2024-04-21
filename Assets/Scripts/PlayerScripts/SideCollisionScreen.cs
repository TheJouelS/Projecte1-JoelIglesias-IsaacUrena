using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideCollisionScreen : MonoBehaviour
{
    public string LeftTagCollision = "LeftSideCollision", RightTagCollision = "RightSideCollision";
    private static bool leftIsColliding = false, rightIsColliding = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == LeftTagCollision)
            leftIsColliding = true;

        if (collision.tag == RightTagCollision)
            rightIsColliding = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == LeftTagCollision)
            leftIsColliding = false;

        if (collision.tag == RightTagCollision)
            rightIsColliding = false;
    }

    public static bool GetLeftIsColliding()
    {
        return leftIsColliding;
    }

    public static bool GetRightIsColliding()
    {
        return rightIsColliding;
    }
}
