using TMPro;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public uint currentLife;
    public string damagedTag, diedTag, colliderPlayerAttack;
    public bool isDying = false;
    public float timerCooldown;
    public GameObject floatingPoints, parentFloatingPoints, enemyParent;

    private Animator animator;
    private float timer;
    private bool canCount = false, canTakeDame = true;

    private void Start()
    {
        animator = GetComponent<Animator>();
        timer = timerCooldown;
    }

    private void Update()
    {
        if (canCount)
            timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            timer = timerCooldown;
            canCount = false;
            canTakeDame = true;
        }
    }

    public uint GetHearts()
    {
        return currentLife;
    }

    public void TakeDamage(uint damage)
    {
        var damagedPoints = Instantiate(floatingPoints, parentFloatingPoints.transform.position, Quaternion.identity, parentFloatingPoints.transform);
        damagedPoints.GetComponentInChildren<TextMeshPro>().text = damage.ToString();

        if (damage < currentLife) currentLife -= damage;
        else currentLife = 0;

        if (currentLife <= 0) Death();
        else animator.SetTrigger(damagedTag);
    }

    private void Death()
    {
        isDying = true;
        animator.SetTrigger(diedTag);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (canTakeDame)
        {
            if (collision.CompareTag(colliderPlayerAttack))
            {
                TakeDamage(AttackColliderController_Player.GetMeleeDamage());
                canCount = true;
                canTakeDame = false;
            }
        }
    }

    private void OnDestroy()
    {
        EnemySpawner.enemyCounter--;
    }

    //It's called through Animator:
    private void Disappear()
    {
        Destroy(enemyParent.gameObject);
    }

    private void ConvertSpriteToRed()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.red;
    }

    private void ReturnSpriteToWhite()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    }
}
