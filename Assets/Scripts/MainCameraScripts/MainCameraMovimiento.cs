using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraMovimiento : MonoBehaviour
{
    [SerializeField] private List<Transform> wayPoints = new List<Transform>();
    [SerializeField] private float speed;

    private int nextPoint = 0;
    public static bool levelUp = false;

    void Update()
    {
        if (levelUp)
        {
            Camera.main.orthographicSize = 4.2f;

            if (Vector3.Distance(transform.position, wayPoints[nextPoint].position) == 0f)
                nextPoint++;

            if (nextPoint >= wayPoints.Count)
            {
                nextPoint = 0;
                levelUp = false;
            }

            if (levelUp)
                transform.position = Vector3.MoveTowards(transform.position, wayPoints[nextPoint].position, speed * Time.deltaTime);
            else
                Camera.main.orthographicSize = 5f;
        }
    }
}
