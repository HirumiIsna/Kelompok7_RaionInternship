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
        if (context.started && interactableRange != null)
        {
            interactableRange.Interact();

            if (!interactableRange.CanInteract())
            {
                interactIcon.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.TryGetComponent(out IInteractable interactable) && interactable.CanInteract())
        {
            interactableRange = interactable;
            interactIcon.SetActive(true);
        }

        if (other.CompareTag("Bushes"))
        {
            Debug.Log("Player entered rumput area");
             // Play the sound effect here
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
