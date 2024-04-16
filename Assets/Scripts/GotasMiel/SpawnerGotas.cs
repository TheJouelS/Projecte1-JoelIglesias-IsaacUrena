using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerGotas : MonoBehaviour
{
    [SerializeField] GameObject spawner;
    [SerializeField] float posX;
    [SerializeField] float posY;

    private static float spawnCooldown = 1.5f;
    private float spawnTime;
    private float randomNum;

    void Start()
    {
        spawnTime = spawnCooldown;

        Debug.Log(spawnCooldown);
    }

    void Update()
    {
        if (spawnTime > 0) spawnTime -= Time.deltaTime;

        if (spawnTime <= 0)
        {
            Spawn();
            spawnTime = spawnCooldown;
        }
    }

    private void Spawn()
    {
        randomNum = Random.Range(-posX, posX);

        var spawn = Instantiate(spawner);
        spawn.transform.position = new Vector3 (randomNum, posY, 0);
    }

    public static void SetSpawnCooldown()
    {
        float decrementoCooldown = 1.25f;
        spawnCooldown = spawnCooldown / decrementoCooldown;

        Debug.Log(spawnCooldown);
    }
}
