using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 720f;
    
    [Header("References")]
    [SerializeField] private Transform cameraTarget;
    
    private Vector2 moveInput;
    private CharacterController characterController;
    private Camera mainCamera;
    
    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        mainCamera = Camera.main;
    }
    
    private void Update()
    {
        HandleMovement();
    }
    
    private void HandleMovement()
    {
        // Convert input to world-space movement
        Vector3 movement = new Vector3(moveInput.x, 0f, moveInput.y);
        movement = mainCamera.transform.TransformDirection(movement);
        movement.y = 0f;
        movement = movement.normalized * moveSpeed;
        
        // Apply movement
        characterController.SimpleMove(movement);
        
        // Rotate player to face movement direction
        if (movement != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
        }
    }
    
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
}