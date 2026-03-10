using UnityEngine;

public class PickupDagger : MonoBehaviour, IInteractable
{
    public GameObject dialogueObject;
    private bool isFinished = false;

    public bool CanInteract()
    {
        return !isFinished;
    }

    public void Interact()
    {
        if(!CanInteract()) return;
        DialoguePickupDagger();
    }

    void DialoguePickupDagger()
    {
        Destroy(gameObject,.125f); 
        if(!dialogueObject) return;
        dialogueObject.SetActive(true);
    }
}
