using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NubesMovimientoScript : MonoBehaviour
{
    [SerializeField] private Transform[] wayPoints;
    [SerializeField] private float speed;

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
        if (collision.gameObject.CompareTag("Player"))
            collision.transform.SetParent(this.transform);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            collision.transform.SetParent(null);
    }
}
