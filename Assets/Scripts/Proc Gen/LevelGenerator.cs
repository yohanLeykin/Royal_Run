using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [Header("References")]
    [SerializeField] CameraController cameraController;
    [SerializeField] GameObject chunkPrefab;
    [SerializeField] Transform chunkParent;
    [SerializeField] ScoreManager scoreManager;

     [Header("Level Settings")]
    [SerializeField] int startingChunkAmout = 12;
    [Tooltip("Do not change chunk length value unless chunk prefab size reflects change")]
    [SerializeField] float chunkLength = 10f;
    [SerializeField] float moveSpeed = 8f;
    [SerializeField] float minMoveSpeed = 2f;
    [SerializeField] float maxMoveSpeed = 20f;
    [SerializeField] float minGravityZ = -22f;
    [SerializeField] float maxGravityZ = -2f;


    private List<GameObject> chunks = new List<GameObject>();
    void OnTransformChildrenChanged()
    {

    }

    void Start()
    {
        SpawnStartingChunks();
    }

    void FixedUpdate()
    {
        MoveChunks();
    }

    public void ChangeChunkMoveSpeed(float speedAmount)
    {
        float newMoveSpeed = moveSpeed + speedAmount;
        newMoveSpeed = Mathf.Clamp(newMoveSpeed,minMoveSpeed,maxMoveSpeed);


        if (newMoveSpeed != moveSpeed)
        {
            moveSpeed = newMoveSpeed;

            float newGravityZ = Physics.gravity.z - speedAmount;
            newGravityZ = Mathf.Clamp(newGravityZ, minGravityZ, maxGravityZ);
            Physics.gravity = new Vector3(Physics.gravity.x, Physics.gravity.y, newGravityZ);   
            cameraController.ChangeCameraFOV(speedAmount);
        }
        


    }

    private void SpawnStartingChunks()
    {
        for (int i = 0; i < startingChunkAmout; i++)
        {
            SpawnChunk();
        }
    }

    private void SpawnChunk()
    {
        float spawnPositionZ = CalculatePositionZ();
        var chunkSpawnPos = new Vector3(transform.position.x, transform.position.y, spawnPositionZ);
        GameObject newChunkGO = Instantiate(chunkPrefab, chunkSpawnPos, Quaternion.identity, chunkParent);
        chunks.Add(newChunkGO);
        Chunk newChunk = newChunkGO.GetComponent<Chunk>();
        newChunk.Init(this,scoreManager);
    }

    private float CalculatePositionZ()
    {
        float spawnPositionZ;

        if (chunks.Count == 0)
        {
            spawnPositionZ = transform.position.z;
        }
        else
        {
            //spawnPositionZ = transform.position.z + (i * chunkLength);
            spawnPositionZ = chunks[chunks.Count - 1].transform.position.z + chunkLength;
        }

        return spawnPositionZ;
    }

    private void MoveChunks()
    {
        for (int i = 0; i < chunks.Count; i++)
        {
            GameObject chunk = chunks[i];
            chunk.transform.Translate(-transform.forward * (moveSpeed * Time.fixedDeltaTime));

            if (chunk.transform.position.z <= Camera.main.transform.position.z - chunkLength)
            {
                chunks.Remove(chunk);
                Destroy(chunk);
                SpawnChunk();
            }
        }
    }
}
