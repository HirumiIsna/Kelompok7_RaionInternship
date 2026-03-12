using UnityEngine;
using UnityEngine.Events;

public class Potion : MonoBehaviour, IInteractable
{
    public UnityEvent objectiveFinished;

    bool isPicked = false;

    public bool CanInteract()
    {
        return !isPicked;
    }

    public void Interact()
    {
        if(!CanInteract()) return;
        Debug.Log("Interacted!");
        Destroy(gameObject, .125f);
        isPicked = true;
        objectiveFinished.Invoke();
    }
}
