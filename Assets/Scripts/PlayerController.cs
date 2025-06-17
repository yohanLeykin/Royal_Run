using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float xClamp = 4f;
    [SerializeField] float zClamp = 2f;
    Vector2 movement;
    Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        var currentPos = rb.position;
        var moveDirection = new Vector3(movement.x,0f,movement.y);
        var newPos = currentPos + moveDirection * (moveSpeed * Time.fixedDeltaTime);

        newPos.x = Mathf.Clamp(newPos.x,-xClamp,xClamp);
        newPos.z = Mathf.Clamp(newPos.z,-zClamp,zClamp);

        rb.MovePosition(newPos);
    }

    public void Move(InputAction.CallbackContext context)
    {

        movement = context.ReadValue<Vector2>();
        
    }
}
