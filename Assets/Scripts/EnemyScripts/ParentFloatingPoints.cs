using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentFloatingPoints : MonoBehaviour
{
    public GameObject enemyToFollow;

    void Update()
    {
        transform.position = enemyToFollow.transform.position;
    }
}
