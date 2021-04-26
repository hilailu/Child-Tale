using UnityEngine;

public class SpawnFearItemsManager : MonoBehaviour
{
    [SerializeField] GameObject[] items;
    [SerializeField] Transform[] spawnPositions;
    private Vector3 lastSpawnPos;
    private GameObject lastSpawnedItem = null;

    private void Start()
        => lastSpawnPos = transform.position;

    private void Update()
    {
        if (!Photon.Pun.PhotonNetwork.OfflineMode) return;

        if (lastSpawnedItem == null)
            SpawnItem();
    }

    private void SpawnItem()
    {
        Transform randomPosition = spawnPositions[Random.Range(0, spawnPositions.Length)];
        if (lastSpawnPos == randomPosition.position)
            return;

        GameObject randomItem = items[Random.Range(0, items.Length)];
        lastSpawnedItem = Instantiate(randomItem, randomPosition.position, Quaternion.identity);
        lastSpawnPos = lastSpawnedItem.transform.position;
    }
}