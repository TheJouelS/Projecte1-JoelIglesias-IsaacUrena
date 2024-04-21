using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MainCameraMovement : MonoBehaviour
{
    [SerializeField] private List<Transform> wayPoints = new List<Transform>();
    [SerializeField] private Transform[] sideWayPoints; //index 0 = leftWayPoint --- index 1 = rightWayPoint

    public float Speed = 13f;
    public float SideSpeed = 5f;
    public float NormalCameraSize = 5f;
    public float ZoomCameraSize = 4.2f;

    private static bool finishedAnimation = true;
    private int nextPoint = 0;
    private Vector3 initialPosition;

    private void Start()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
        if (!finishedAnimation)
            CameraLevelUpMovement();
        else if (SideCollisionScreen.GetLeftIsColliding())
            CameraLeftMovement();
        else if (SideCollisionScreen.GetRightIsColliding())
            CameraRightMovement();
        else
            CameraCenterMovement();
    }

    private void CameraLevelUpMovement()
    {
        Camera.main.orthographicSize = ZoomCameraSize;

        if (Vector3.Distance(transform.position, wayPoints[nextPoint].position) < 0.1f)
            nextPoint++;

        if (nextPoint >= wayPoints.Count)
        {
            transform.position = initialPosition;
            nextPoint = 0;
            finishedAnimation = true;
        }

        if (!finishedAnimation)
            transform.position = Vector3.MoveTowards(transform.position, wayPoints[nextPoint].position, Speed * Time.deltaTime);
        else
            Camera.main.orthographicSize = NormalCameraSize;
    }

    public static void RunAnimation()
    {
        finishedAnimation = false;
    }

    private void CameraLeftMovement()
    {
        transform.position = Vector3.MoveTowards(transform.position, sideWayPoints[0].position, SideSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, sideWayPoints[0].position) < 0.1f)
            transform.position = sideWayPoints[0].position;
    }

    private void CameraRightMovement()
    {
        transform.position = Vector3.MoveTowards(transform.position, sideWayPoints[1].position, SideSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, sideWayPoints[1].position) < 0.1f)
            transform.position = sideWayPoints[1].position;
    }

    private void CameraCenterMovement()
    {
        transform.position = Vector3.MoveTowards(transform.position, initialPosition, SideSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, initialPosition) < 0.1f)
            transform.position = initialPosition;
    }

}
