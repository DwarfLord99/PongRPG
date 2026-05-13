using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    private MovementController movementController;

    private InputAction moveAction;

    private Vector2 moveInput;

    void Awake()
    {
        movementController = GetComponent<MovementController>();

        moveAction = InputSystem.actions.FindAction("Move");
    }

    void Update()
    {
        HandleMovement();
        movementController.OnMove(moveInput);
    }

    void HandleMovement()
    {
        if (movementController != null)
        {
            moveInput = moveAction.ReadValue<Vector2>();
        }
    }
}
