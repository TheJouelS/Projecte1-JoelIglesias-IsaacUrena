using UnityEngine;

public class ThrowingAxe : MonoBehaviour
{
    [SerializeField] AxeSpawner axeSpawner;
    [SerializeField] Transform spawnerPosition;
    [SerializeField] string playerTag, outsideMap;
    [SerializeField] float speed, rotationSpeed;
    [SerializeField] uint damageToPlayer = 10;
    [SerializeField] Vector3 orientation;

    private Transform playerPosition;
    private Vector3 direction;
    private Rigidbody2D rgb;
    private AudioSource s_flyingAxe;

    public void OnEnable()
    {
        SetPlayerPosition();

        axeSpawner.RunThrowingAnimation();
        axeSpawner.axeIsReady = false;

        transform.position = spawnerPosition.position;
        direction = playerPosition.position - transform.position;

        rgb = GetComponent<Rigidbody2D>();
        rgb.AddForce(Vector2.up * 2, ForceMode2D.Impulse);

        s_flyingAxe = GetComponent<AudioSource>();
        s_flyingAxe.Play();
        s_flyingAxe.loop = true;
        s_flyingAxe.pitch = 2f;

        float n;
        if (EnemySpawner.enemyCounter <= 5)
            n = Random.Range(0.2f, 0.4f);
        else
            n = 0f;
        s_flyingAxe.volume = n;
    }

    void Update()
    {
        transform.position = transform.position + direction.normalized * speed * Time.deltaTime;
        transform.Rotate(rotationSpeed * orientation * Time.deltaTime);
    }

    private void SetPlayerPosition()
    {
        playerPosition = PlayerMovement.GetPlayerPosition();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(playerTag))
        {
            PlayerHealth.TakeDamage(damageToPlayer, true);
            gameObject.SetActive(false);
        }

        if (collision.CompareTag(outsideMap))
            gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        s_flyingAxe.Stop();
        axeSpawner.axeIsReady = true;
    }
}
