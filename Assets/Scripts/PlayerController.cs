using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    Vector2 movement;
     Rigidbody rb;
    [SerializeField] float moveSpeed = 5f;

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
        rb.MovePosition(newPos);
    }

    public void Move(InputAction.CallbackContext context)
    {

        movement = context.ReadValue<Vector2>();
    }
}
