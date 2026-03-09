using UnityEngine;

public class InteractPhoto : MonoBehaviour, IInteractable
{
    public GameObject dialogueObject;
    private bool isOpened = false;

    public bool CanInteract()
    {
        return !isOpened;
    }

    public void Interact()
    {
        if(!CanInteract()) return;
        DialoguePhoto();
    }

    void DialoguePhoto()
    {
        // Destroy(gameObject,.125f); 
        if(!dialogueObject) return;
        dialogueObject.SetActive(true);
    }
}
