using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Main Features")]
    [SerializeField] List<GameObject> enemyList;
    public float spawnCooldown, posX;

    [Header ("Viking Coordinates")]
    public float v_posY;

    [Header ("Angel Coordinates")]
    public float a_posY;

    private float timer;

    void Start()
    {
        timer = spawnCooldown;
    }

    void Update()
    {
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
        int randomNum = Random.Range(1, 2);
        var spawn = Instantiate(enemyList[randomNum]);

        // 1 for vikings --- 2 for angels:
        if (randomNum == 1)
            spawn.transform.position = new Vector3(posX, v_posY, 0);
        else
            spawn.transform.position = new Vector3(posX, a_posY, 0);

        spawn.transform.SetParent(this.transform);

        //We change the X coordinate for the next spawn position
        posX = -posX;
    }
}
