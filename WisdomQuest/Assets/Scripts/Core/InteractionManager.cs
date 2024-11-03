using UnityEngine;
using System.Collections.Generic;

public class InteractionManager : MonoBehaviour
{
    [SerializeField] private float interactionRange = 2f;
    [SerializeField] private LayerMask interactableMask;
    
    private Camera mainCamera;
    private List<IInteractable> nearbyInteractables = new List<IInteractable>();
    
    private void Awake()
    {
        mainCamera = Camera.main;
    }
    
    private void Update()
    {
        DetectInteractables();
        HandleInteraction();
    }
    
    private void DetectInteractables()
    {
        nearbyInteractables.Clear();
        
        Collider[] colliders = Physics.OverlapSphere(transform.position, interactionRange, interactableMask);
        foreach (var collider in colliders)
        {
            if (collider.TryGetComponent<IInteractable>(out var interactable))
            {
                nearbyInteractables.Add(interactable);
            }
        }
    }
    
    private void HandleInteraction()
    {
        if (Input.GetKeyDown(KeyCode.E) && nearbyInteractables.Count > 0)
        {
            // Interact with the closest interactable
            IInteractable closest = GetClosestInteractable();
            closest?.Interact(GetComponent<PlayerController>());
        }
    }
    
    private IInteractable GetClosestInteractable()
    {
        IInteractable closest = null;
        float closestDistance = float.MaxValue;
        
        foreach (var interactable in nearbyInteractables)
        {
            float distance = Vector3.Distance(transform.position, 
                (interactable as MonoBehaviour).transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closest = interactable;
            }
        }
        
        return closest;
    }
}