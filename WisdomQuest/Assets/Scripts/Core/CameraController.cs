using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [Header("Follow Settings")]
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset = new Vector3(0, 2, -6);
    [SerializeField] private float smoothTime = 0.3f;
    
    [Header("Mouse Control")]
    [SerializeField] private float rotationSpeed = 2f;
    [SerializeField] private float minVerticalAngle = -30f;
    [SerializeField] private float maxVerticalAngle = 60f;
    
    private float currentRotationX;
    private float currentRotationY;
    private Vector3 currentVelocity = Vector3.zero;
    private PlayerInputs playerInputs;
    private Vector2 lookInput;
    
    private void Awake()
    {
        playerInputs = new PlayerInputs();
        playerInputs.Player.Look.performed += OnLook;
        playerInputs.Player.Look.canceled += OnLook;
        
        // Initialize rotation to current camera angles
        Vector3 angles = transform.eulerAngles;
        currentRotationX = angles.y;
        currentRotationY = angles.x;
    }
    
    private void OnEnable()
    {
        playerInputs.Enable();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    
    private void OnDisable()
    {
        playerInputs.Disable();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    
    private void LateUpdate()
    {
        if (target == null) return;
        
        // Handle mouse input
        currentRotationX += lookInput.x * rotationSpeed;
        currentRotationY -= lookInput.y * rotationSpeed;
        currentRotationY = Mathf.Clamp(currentRotationY, minVerticalAngle, maxVerticalAngle);
        
        // Calculate rotation and position
        Quaternion rotation = Quaternion.Euler(currentRotationY, currentRotationX, 0);
        Vector3 targetPosition = target.position + rotation * offset;
        
        // Apply smooth movement
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothTime);
        transform.rotation = rotation;
    }
    
    private void OnLook(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
    }
} 