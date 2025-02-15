using UnityEngine;

[RequireComponent(typeof(MovementController))]
[RequireComponent(typeof(FPSCameraController))]
[RequireComponent(typeof(InteractionController))]
public class PlayerInputManager : MonoBehaviour {
    MovementController movementControler => GetComponent<MovementController>();
    FPSCameraController cameraController => GetComponent<FPSCameraController>();
    InteractionController interactionController => GetComponent<InteractionController>();

    [Header("Input Settings")]
    [SerializeField] bool canMove = true;
    [SerializeField] bool canRotate = true;

    CharacterActions inputActions;
    Vector2 moveDirection;
    Vector2 mouseInput;

    private void OnEnable() {
        inputActions.Enable();
    }
    
    private void OnDisable() {
        inputActions.Disable();
    }
    
    private void Awake() {
        inputActions = new();

        inputActions.MainGameActions.LookUp_X.performed += ctx => mouseInput.x = ctx.ReadValue<float>();
        inputActions.MainGameActions.LookUp_Y.performed += ctx => mouseInput.y = ctx.ReadValue<float>();

        inputActions.MainGameActions.Movement.performed += ctx => moveDirection = ctx.ReadValue<Vector2>();
        inputActions.MainGameActions.Movement.canceled += _ => moveDirection = Vector3.zero;

        inputActions.MainGameActions.Jump.performed += _ => movementControler.JumpInput();
        inputActions.MainGameActions.Interact.performed += _ => interactionController.InteractWithObject();
    }

    private void Update() {
        if (canMove) 
            movementControler.MovementInput(moveDirection);
        if (canRotate)
            cameraController.MouseAxisInput(mouseInput);
    }
}
