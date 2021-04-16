using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFearItemsManager : MonoBehaviour
{
    [SerializeField] GameObject[] items;
    [SerializeField] Transform[] spawnPositions;
    private GameObject lastSpawnedItem = null;
    private Transform lastSpawnPos = null;
    public float timeUntilSpawn;

    private void Update()
    {
        if (lastSpawnedItem == null)
            SpawnItem();
    }

    private void SpawnItem()
    {
        Transform randomPosition = spawnPositions[Random.Range(0, spawnPositions.Length)];
        if (lastSpawnPos.position == randomPosition.position)
            return;

        GameObject randomItem = items[Random.Range(0, items.Length)];
        lastSpawnedItem = Instantiate(randomItem, randomPosition.position, Quaternion.identity);
    }
}
