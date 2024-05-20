using UnityEngine;

public class MeadDrops : MonoBehaviour
{
    [SerializeField] string tagDropToTheGround = "DropsToTheGround", tagPlayerHole = "JugHole";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == tagDropToTheGround || collision.tag == tagPlayerHole)
        {
            if (collision.tag == tagPlayerHole)
                PlayerScore.instance.UpScore();

            Destroy(gameObject);
        }
    }
}
