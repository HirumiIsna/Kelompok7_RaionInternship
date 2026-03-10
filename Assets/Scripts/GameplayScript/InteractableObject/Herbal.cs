using UnityEngine;
using UnityEngine.Events;

public class Herbal : MonoBehaviour, IInteractable
{
    public GameObject dialogueObject;
    
    private bool isPickedUp = false;

    public UnityEvent pickHerb;

    public bool CanInteract()
    {
        return !isPickedUp;
    }

    public void Interact()
    {
        if (!CanInteract()) return;
        CollectHerb();
    }

    public void CollectHerb()
    {
        isPickedUp = true;
        pickHerb.Invoke();
        Destroy(gameObject,.125f); 
        if(!dialogueObject) return;
        dialogueObject.SetActive(true);
    }

}
