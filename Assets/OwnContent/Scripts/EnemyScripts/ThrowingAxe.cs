using UnityEngine;

public class ThrowingAxe : MonoBehaviour
{
    [SerializeField] AxeSpawner axeSpawner;
    [SerializeField] Transform spawnerPosition;
    [SerializeField] string playerTag, outsideMap;
    [SerializeField] float speed, rotationSpeed;
    [SerializeField] uint damageToPlayer = 10;
    [SerializeField] Vector3 orientation;
    [SerializeField] int maxNumOfEnemiesOnScreen = 10;
    [SerializeField] float minVolumeValue = 0.2f, maxVolumeValue = 0.4f, pitchOfSound = 2f;

    private Transform playerPosition;
    private Vector3 direction;
    private Rigidbody2D rgb;
    private AudioSource s_flyingAxe;
    private bool stopSound;

    private void Start()
    {
        if (EnemySpawner.enemyCounter <= maxNumOfEnemiesOnScreen)
        {
            float n;
            n = Random.Range(minVolumeValue, maxVolumeValue);
            s_flyingAxe.volume = n;
        }
        else
            s_flyingAxe.volume = 0f;
    }

    public void OnEnable()
    {
        SetPlayerPosition();

        axeSpawner.RunThrowingAnimation();
        axeSpawner.axeIsReady = false;

        transform.position = spawnerPosition.position;
        direction = playerPosition.position - transform.position;

        rgb = GetComponent<Rigidbody2D>();
        rgb.AddForce(Vector2.up * 2, ForceMode2D.Impulse);

        stopSound = true;

        s_flyingAxe = GetComponent<AudioSource>();
        s_flyingAxe.Play();
        s_flyingAxe.loop = true;
        s_flyingAxe.pitch = pitchOfSound;
    }

    void Update()
    {
        transform.position = transform.position + direction.normalized * speed * Time.deltaTime;
        transform.Rotate(rotationSpeed * orientation * Time.deltaTime);

        if (GameFlowManager.gameIsPaused)
        {
            s_flyingAxe.Pause();
            stopSound = false;
        }
        else if (!stopSound)
        {
            s_flyingAxe.Play();
            stopSound = true;
        }
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
