using System.Collections.Generic;
using UnityEngine;

public class CloudsSpawner : MonoBehaviour
{
    [SerializeField] List<GameObject> generatedObject;
    [SerializeField] GameObject objectContainer;

    public float PosX = -15f, minPosY = 2.5f, maxPosY = 4f;
    public float SpawnCooldown = 40f;

    private float spawnTime;
    private float randomY;
    private int randomIndex;

    void Start()
    {
        spawnTime = SpawnCooldown;
    }

    void Update()
    {
        if (spawnTime > 0) spawnTime -= Time.deltaTime;

        if (spawnTime <= 0)
        {
            SpawnCloud();
            spawnTime = SpawnCooldown;
        }
    }

    private void SpawnCloud()
    {
        randomY = Random.Range(minPosY, maxPosY);
        randomIndex = Random.Range(0, generatedObject.Count);

        var spawn = Instantiate(generatedObject[randomIndex]);
        spawn.transform.position = new Vector3(PosX, randomY, 0);
        spawn.transform.SetParent(objectContainer.transform);
    }
}
