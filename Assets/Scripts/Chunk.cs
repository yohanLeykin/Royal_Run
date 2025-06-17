using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    [SerializeField] GameObject fencePrefab;
    [SerializeField] GameObject applePrefab;
    [SerializeField] GameObject coinPrefab;

    [SerializeField] float[] lanes = { -2.5f, 0f, 2.5f };
    [SerializeField] float fenceHeight = 1f;
    [SerializeField] float appleHeight = 0f;

    [SerializeField] float appleSpawnChance = 0.3f;
    [SerializeField] float coinSpawnChance = 0.5f;
    List<int> availableLanes = new List<int> { 0, 1, 2 };
    void Start()
    {
        SpawnFences();
        SpawnApple();
        SpawnCoin();
    }

    private void SpawnCoin()
    {
        if (UnityEngine.Random.value > coinSpawnChance || availableLanes.Count <= 0 ) return;
        int numberOfCoins = UnityEngine.Random.Range(1, 5);
        int selectedLane = SelectLane();
        for (int i = 0; i < numberOfCoins; i++)
        {
            var spawnPosition = new Vector3(lanes[selectedLane], transform.position.y + appleHeight, transform.position.z - 1.6f + (i * 1.5f));
            Instantiate(coinPrefab, spawnPosition, quaternion.identity, this.transform);

        }
    }

    void SpawnFences()
    {
        int fencesToSpawn = UnityEngine.Random.Range(0, lanes.Length);

        for (int i = 0; i < fencesToSpawn; i++)
        {
            if (availableLanes.Count <= 0) break;

            int selectedLane = SelectLane();
            var spawnPosition = new Vector3(lanes[selectedLane], transform.position.y - fenceHeight, transform.position.z);
            Instantiate(fencePrefab, spawnPosition, quaternion.identity, this.transform);
        }
    }

    private int SelectLane()
    {
        int randomLaneIndex = UnityEngine.Random.Range(0, availableLanes.Count);
        int selectedLane = availableLanes[randomLaneIndex];
        availableLanes.RemoveAt(randomLaneIndex);
        return selectedLane;
    }

    void SpawnApple()
    {
            if (UnityEngine.Random.value > appleSpawnChance || availableLanes.Count <= 0 ) return;
            int selectedLane = SelectLane();
            var spawnPosition = new Vector3(lanes[selectedLane], transform.position.y + appleHeight, transform.position.z - 1.6f);
            Instantiate(applePrefab, spawnPosition, quaternion.identity, this.transform);
    }
}
