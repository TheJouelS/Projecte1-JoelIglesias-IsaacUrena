using System.Threading;
using Unity.Collections;
using UnityEngine;

public class MeadDrops : MonoBehaviour
{
    [SerializeField] string tagDropToTheGround = "DropsToTheGround", tagPlayerHole = "JugHole";
    [SerializeField] float timerFlip = 0.5f;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Animator animator;

    private Rigidbody2D rgb;
    private float timer;

    private void Start()
    {
        rgb = GetComponent<Rigidbody2D>();

        timer = timerFlip;
        animator.enabled = false;

        SoundManager.instance.s_catchDrops.loop = false;
        SoundManager.instance.s_catchDrops.pitch = 1f;
        SoundManager.instance.s_catchDrops.volume = 1f;
    }

    private void Update()
    {
        if (timer > 0)
            timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
            timer = timerFlip;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == tagDropToTheGround || collision.tag == tagPlayerHole)
        {
            if (collision.tag == tagPlayerHole)
            {
                SoundManager.instance.s_catchDrops.Play();
                PlayerScore.instance.UpScore();
                DestroyGameObject();
            }

            if (collision.tag == tagDropToTheGround)
                animator.enabled = true;

            rgb.bodyType = RigidbodyType2D.Static;
        }
    }

    public void DestroyGameObject()
    {
        Destroy(gameObject);
    }
}
