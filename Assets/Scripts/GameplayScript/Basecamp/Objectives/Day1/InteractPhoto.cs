using UnityEngine;

public class InteractPhoto : MonoBehaviour, IInteractable
{
    public GameObject dialogueObject;

    public void Interact()
    {
        DialoguePhoto();
    }

    void DialoguePhoto()
    {
        Destroy(gameObject,.125f); 
        if(!dialogueObject) return;
        dialogueObject.SetActive(true);
    }
}
