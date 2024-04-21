using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudPath : MonoBehaviour
{
    [SerializeField] private Transform wayPoint;

    public float Speed = 0.25f;

    void Update()
    {
        if (Vector2.Distance(transform.position, wayPoint.position) < 0.1f)
            Destroy(gameObject);

        transform.position = Vector2.MoveTowards(transform.position, wayPoint.position, Speed * Time.deltaTime);
    }
}
