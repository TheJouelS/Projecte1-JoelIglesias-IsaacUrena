using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Main Features")]
    [SerializeField] List<GameObject> enemyList;
    public float spawnCooldown, decrementCooldown, posX;

    [Header ("Viking Coordinates")]
    public float v_posY;

    [Header ("Angel Coordinates")]
    public float a_posY;

    private float timer;
    private static bool canDecrementCooldown = false;

    void Start()
    {
        timer = spawnCooldown;
    }

    void Update()
    {
        if (canDecrementCooldown)
            SetCooldown();

        if (timer > 0)
            timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            Spawn();
            timer = spawnCooldown;
        }
    }

    private void Spawn()
    {
        GameObject spawn = null;

        if (PlayerLevel.GetPlayerLevel() < 5 && PlayerLevel.GetPlayerLevel() > 1)
        {
            spawn = Instantiate(enemyList[0]);
            spawn.transform.position = new Vector3(posX, v_posY, 0);
        }
        else if (PlayerLevel.GetPlayerLevel() >= 5)
        {
            int randomNum = Random.Range(0, 2);
            spawn = Instantiate(enemyList[randomNum]);

            //randomNum: 0 for vikings --- 1 for angels:
            if (randomNum == 0)
                spawn.transform.position = new Vector3(posX, v_posY, 0);
            else
                spawn.transform.position = new Vector3(posX, a_posY, 0);
        }

        if (spawn != null)
        {
            spawn.transform.SetParent(this.transform);

            //We change the X coordinate for the next spawn position
            posX = -posX;
        }
    }

    private void SetCooldown()
    {
        spawnCooldown -= decrementCooldown;
        canDecrementCooldown = false;
    }

    public static void CanDecrementCooldown()
    {
        canDecrementCooldown = true;
    }
}
