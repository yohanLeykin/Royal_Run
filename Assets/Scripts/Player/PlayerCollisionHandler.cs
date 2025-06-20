using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] float collisionCoolDown = 1f;
    [SerializeField] float adjustChangeMoveSpeedAmount = -2f;
    const string hitString = "Hit";
    float coolDownTimer = 0f;
    LevelGenerator levelGenerator;
    void Update()
    {
        coolDownTimer += Time.deltaTime;
    }
    void Start()
    {
        levelGenerator = FindFirstObjectByType<LevelGenerator>();
    }
    void OnCollisionEnter(Collision collision)
    {
        if (coolDownTimer < collisionCoolDown) return;
        animator.SetTrigger(hitString);
        levelGenerator.ChangeChunkMoveSpeed(adjustChangeMoveSpeedAmount);
        Debug.Log(collision.gameObject.name);
    }
}
