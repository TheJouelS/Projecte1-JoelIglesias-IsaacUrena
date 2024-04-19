using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraMovement : MonoBehaviour
{
    [SerializeField] private List<Transform> wayPoints = new List<Transform>();
    
    public float Speed = 13f;
    public float NormalCameraSize = 5f;
    public float ZoomCameraSize = 4.2f;

    private static bool finishedAnimation = true;
    private int nextPoint = 0;
    private Vector3 initialPosition;

    private void Start()
    {
        initialPosition = transform.position;
    }

    void Update() //Aqui poner lo de los bordes
    {


        if (!finishedAnimation)
            CameraLevelUpMovement();
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
}
