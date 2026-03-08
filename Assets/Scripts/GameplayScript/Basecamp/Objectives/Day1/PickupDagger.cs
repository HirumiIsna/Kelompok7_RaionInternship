using UnityEngine;

public class PickupDagger : MonoBehaviour, IInteractable
{
    public GameObject dialogueObject;
    // private bool isFinished = false;

    public void Interact()
    {
        DialoguePickupDagger();
    }

    void DialoguePickupDagger()
    {
        Destroy(gameObject,.125f); 
        if(!dialogueObject) return;
        dialogueObject.SetActive(true);
    }
}
