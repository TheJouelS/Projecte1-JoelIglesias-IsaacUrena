using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Xml.Linq;
using UnityEngine;

public class AxeSpawner : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] GameObject axeObject;
    [SerializeField] EnemyHealth enemyHealth_Viking;
    [SerializeField] float timeBetweenAxes;
    [SerializeField] string throwingTagAnimation = "isThrowing";

    public static bool playerIsUp = false;
    public bool axeIsReady = true, playerIsInRange = false;
    private float timer;

    private void Start()
    {
        timer = timeBetweenAxes;
    }

    void Update()
    {
        if (axeIsReady && !enemyHealth_Viking.isDying)
        {
            if (timer > 0f)
                timer -= Time.deltaTime;

            if (playerIsInRange && playerIsUp && timer <= 0f) 
                axeObject.SetActive(true);
        }
        else
            timer = timeBetweenAxes;
    }

    public void RunThrowingAnimation()
    {
        animator.SetBool(throwingTagAnimation, true);
    }

    public void StopThrowingAnimation()
    {
        animator.SetBool(throwingTagAnimation, false);
    }
}
