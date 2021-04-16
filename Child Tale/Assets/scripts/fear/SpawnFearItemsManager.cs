using UnityEngine;

public class SpawnFearItemsManager : MonoBehaviour
{
    [SerializeField] GameObject[] items;
    [SerializeField] Transform[] spawnPositions;
    private Transform lastSpawnPos;
    private GameObject lastSpawnedItem = null;

    private void Start()
        => lastSpawnPos = transform;

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
