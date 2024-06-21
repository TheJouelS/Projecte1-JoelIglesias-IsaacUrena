using UnityEngine;

public class SideCollisionScreen : MonoBehaviour
{
    public string LeftTagCollision = "LeftSideCollision", RightTagCollision = "RightSideCollision";
    private static bool leftScreenIsColliding = false, rightScreenIsColliding = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == LeftTagCollision)
            leftScreenIsColliding = true;

        if (collision.tag == RightTagCollision)
            rightScreenIsColliding = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == LeftTagCollision)
            leftScreenIsColliding = false;

        if (collision.tag == RightTagCollision)
            rightScreenIsColliding = false;
    }

    public static bool GetLeftIsColliding()
    {
        return leftScreenIsColliding;
    }

    public static bool GetRightIsColliding()
    {
        return rightScreenIsColliding;
    }
}
