using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;

public class DropSpawner : MonoBehaviour
{
    [SerializeField] GameObject generatedObject;
    [SerializeField] GameObject objectContainer;

    public float PosX = 8.3f;
    public float PosY = 6f;
    public float SpawnCooldown = 1.5f, CooldownDecrementValue = 1.25f;

    private static bool reduceSpawnCooldown = false;

    private float spawnTime;
    private float randomNum;

    void Start()
    {
        spawnTime = SpawnCooldown;
    }

    void Update()
    {
        if (spawnTime > 0) spawnTime -= Time.deltaTime;

        if (spawnTime <= 0)
        {
            Spawn();
            spawnTime = SpawnCooldown;
        }

        if (reduceSpawnCooldown)
            SetSpawnCooldown();
    }

    private void Spawn()
    {
        randomNum = Random.Range(-PosX, PosX);

        var spawn = Instantiate(generatedObject);
        spawn.transform.position = new Vector3 (randomNum, PosY, 0);
        spawn.transform.SetParent(objectContainer.transform);
    }

    private void SetSpawnCooldown()
    {
        SpawnCooldown = SpawnCooldown / CooldownDecrementValue;
        reduceSpawnCooldown = false;
    }

    public static void ReduceSpawnCooldown()
    {
        reduceSpawnCooldown = true;
    }
}
