using Unity.Mathematics;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    [SerializeField] GameObject fencePrefab;
    [SerializeField] float[] lanes = { -2.5f, 0f, 2.5f };
    [SerializeField] float fenceHeight = 1f;
    void Start()
    {
        SpawnFence();
    }

    void SpawnFence()
    {
        int randomLaneIndex = UnityEngine.Random.Range(0,lanes.Length);
        var spawnPosition = new Vector3(lanes[randomLaneIndex],transform.position.y - fenceHeight,transform.position.z);
        Instantiate(fencePrefab, spawnPosition, quaternion.identity,this.transform);
    }
}
