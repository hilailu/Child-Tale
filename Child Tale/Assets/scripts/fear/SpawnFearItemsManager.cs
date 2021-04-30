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

    // Спаун айтемов в рандомнов месте(почти), но не в таком же месте как спаун прошлого айтема
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