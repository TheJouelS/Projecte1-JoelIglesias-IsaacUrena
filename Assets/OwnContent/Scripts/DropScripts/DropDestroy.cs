using UnityEngine;

public class DropDestroy : MonoBehaviour
{
    public MeadDrops s_meadDrop;

    //It's called through Animator:
    public void DestroyGameObject()
    {
        s_meadDrop.DestroyGameObject();
    }
}
