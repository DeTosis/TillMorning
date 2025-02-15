using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class MovementController : MonoBehaviour {
    CharacterController controller => GetComponent<CharacterController>();

    [Header("Movement Settings")]
    [SerializeField] float movementSpeed;
    [SerializeField] float jumpHeight;
    [SerializeField] float gravity;
    [SerializeField] LayerMask groundLayer;

    [Header("Debug")]
    [SerializeField] Vector2 horizontalInput = Vector2.zero;
    [SerializeField] Vector3 horizontalVelocity = Vector3.zero;
    [SerializeField] public bool isMoving;
    
    [SerializeField] float sphereRayCastRadius = 0.15f;
    [SerializeField] bool isJumping;
    [SerializeField] bool isGrounded;
    [SerializeField] Vector3 verticalVelocity = Vector3.zero;

    public void MovementInput(Vector2 moveDirection) {
        horizontalInput = moveDirection;
    }

    public void JumpInput() {
        isJumping = true;
    }

    private void Update() {
        isMoving = controller.velocity.magnitude > 0;

        PerformJump();
        PerformMovement();
    }

    void PerformMovement() {
        horizontalVelocity = new();
        horizontalVelocity = (transform.right * horizontalInput.x + transform.forward * horizontalInput.y) * movementSpeed;
        controller.Move(horizontalVelocity * Time.deltaTime);
    }

    void PerformJump() {
        isGrounded = Physics.CheckSphere(
            new Vector3(transform.position.x, transform.position.y - transform.localScale.y, transform.position.z),
            sphereRayCastRadius, groundLayer);

        if (isGrounded) verticalVelocity = Vector3.zero;
        if (isJumping && isGrounded) {
            verticalVelocity.y = Mathf.Sqrt(-2f * jumpHeight * gravity);
            isJumping = false;
        }

        verticalVelocity.y += gravity * Time.deltaTime;
        controller.Move(verticalVelocity * Time.deltaTime);
    }
}
