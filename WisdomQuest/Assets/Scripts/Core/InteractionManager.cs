using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionManager : MonoBehaviour
{
    [Header("Detection")]
    [SerializeField] private float interactionRange = 3f;
    [SerializeField] private LayerMask interactionMask;
    
    private PlayerInputs playerInputs;
    private IInteractable currentInteractable;
    private PlayerController playerController;
    
    private void Awake()
    {
        playerInputs = new PlayerInputs();
        playerInputs.Player.Interact.performed += OnInteract;
        playerController = GetComponent<PlayerController>();
    }
    
    private void OnEnable()
    {
        playerInputs.Enable();
    }
    
    private void OnDisable()
    {
        playerInputs.Disable();
    }
    
    private void Update()
    {
        DetectInteractable();
    }
    
    private void DetectInteractable()
    {
        // Raycast forward from player
        Ray ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, interactionRange, interactionMask))
        {
            // Try to get IInteractable from hit object
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();
            if (interactable != null)
            {
                if (currentInteractable != interactable)
                {
                    currentInteractable = interactable;
                    UIManager.Instance.ShowInteractionPrompt(interactable.GetInteractionPrompt());
                }
            }
            else
            {
                ClearInteractable();
            }
        }
        else
        {
            ClearInteractable();
        }
    }
    
    private void ClearInteractable()
    {
        if (currentInteractable != null)
        {
            currentInteractable = null;
            UIManager.Instance.HideInteractionPrompt();
        }
    }
    
    private void OnInteract(InputAction.CallbackContext context)
    {
        if (currentInteractable != null)
        {
            currentInteractable.Interact(playerController);
        }
    }
}