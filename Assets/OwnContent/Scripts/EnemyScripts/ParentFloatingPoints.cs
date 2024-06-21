using UnityEngine;

public class ParentFloatingPoints : MonoBehaviour
{
    public GameObject enemyToFollow;

    void Update()
    {
        transform.position = enemyToFollow.transform.position;
    }
}
