using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionDetector : MonoBehaviour
{
    private IInteractable interactableRange = null;
    public GameObject interactIcon;

    void Start()
    {
        interactIcon.SetActive(false);
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            interactableRange?.Interact();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.TryGetComponent(out IInteractable interactable))
        {
            interactableRange = interactable;
            interactIcon.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.TryGetComponent(out IInteractable interactable) && interactable == interactableRange)
        {
            interactableRange = null;
            interactIcon.SetActive(false);
        }
    }
}
