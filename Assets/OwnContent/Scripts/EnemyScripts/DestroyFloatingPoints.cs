using UnityEngine;

public class DestroyFloatingPoints : MonoBehaviour
{
    [SerializeField] GameObject parentFloatingPoints;

    //It's called through Animator:
    public void DestroyGameObject()
    {
        Destroy(parentFloatingPoints);
    }
}
