using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMovement : MonoBehaviour
{
    [SerializeField] private Transform[] wayPoints;
    [SerializeField] private float speed;
    [SerializeField] string namePlayerTag = "Player";

    private int nextPoint = 1;
    private bool platformOrder = true;

    void Update()
    {
        if (platformOrder && nextPoint + 1 >= wayPoints.Length)
            platformOrder = false;

        if (!platformOrder && nextPoint <= 0)
            platformOrder = true;

        if (Vector2.Distance(transform.position, wayPoints[nextPoint].position) < 0.1f)
        {
            if (platformOrder)
                nextPoint += 1;
            else
                nextPoint -= 1;
        }

        transform.position = Vector2.MoveTowards(transform.position, wayPoints[nextPoint].position, speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(namePlayerTag))
            collision.transform.SetParent(transform);
    }
}
