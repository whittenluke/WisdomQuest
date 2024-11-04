using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset = new Vector3(0, 5, -5);
    [SerializeField] private float smoothTime = 0.3f;
    
    private Vector3 velocity = Vector3.zero;
    
    private void LateUpdate()
    {
        if (target == null) return;
        
        // Calculate desired position
        Vector3 targetPosition = target.position + offset;
        
        // Smoothly move camera
        transform.position = Vector3.SmoothDamp(
            transform.position,
            targetPosition,
            ref velocity,
            smoothTime
        );
        
        // Look at target
        transform.LookAt(target.position);
    }
} 