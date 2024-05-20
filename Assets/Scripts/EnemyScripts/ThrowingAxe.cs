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

    public void OnEnable()
    {
        SetPlayerPosition();

        axeSpawner.RunThrowingAnimation();
        axeSpawner.axeIsReady = false;

        transform.position = spawnerPosition.position;
        direction = playerPosition.position - transform.position;

        rgb = GetComponent<Rigidbody2D>();
        rgb.AddForce(Vector2.up * 2, ForceMode2D.Impulse);
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
        axeSpawner.axeIsReady = true;
    }
}
