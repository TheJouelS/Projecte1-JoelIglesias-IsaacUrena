using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float timeBetweenHit;

    public KeyCode keyButtonAttack;
    public Animator animator;
    public string attackingTagAnimation = "isAttacking", enemyTag = "Viking_Enemy";

    private float timeNextAttack;

    public static float copy_timeBetweenHit_Attack1;

    private void Start()
    {
        timeNextAttack = 0f;
        copy_timeBetweenHit_Attack1 = timeBetweenHit;
    }

    private void Update()
    {
        if (timeNextAttack > 0f)
            timeNextAttack -= Time.deltaTime;

        if (Input.GetKeyUp(keyButtonAttack) && timeNextAttack <= 0f)
        {
            Hit();
            CanvasManager.instance.IsUsingAttack(1);
            timeNextAttack = timeBetweenHit;
        }
    }

    private void Hit()
    {
        AttackColliderController_Player.setDamage = (uint) PlayerLevel.GetPlayerLevel();
        animator.SetTrigger(attackingTagAnimation); //This will active the AttackColliderController_Player Script functions
    }
}
