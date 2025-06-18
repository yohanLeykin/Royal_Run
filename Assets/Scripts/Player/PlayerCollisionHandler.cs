using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    [SerializeField] float adjustChangeMoveSpeedAmount = -2f;
    LevelGenerator levelGenerator;
    void Start()
    {
        levelGenerator = FindFirstObjectByType<LevelGenerator>();
    }
    void OnCollisionEnter(Collision collision)
    {
        levelGenerator.ChangeChunkMoveSpeed(adjustChangeMoveSpeedAmount);
        Debug.Log(collision.gameObject.name);
    }
}
