using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    [SerializeField] private Transform[] wayPoints;
    [SerializeField] private float speed;
    [SerializeField] string namePlayerTag = "Player";

    private int nextPoint = 1;

    void Update()
    {
        if (Vector2.Distance(transform.position, wayPoints[nextPoint].position) < 0.1f)
            nextPoint++;

        if (nextPoint >= wayPoints.Length)
            nextPoint = 0;

        transform.position = Vector2.MoveTowards(transform.position, wayPoints[nextPoint].position, speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(namePlayerTag))
            collision.transform.SetParent(transform);
    }
}
