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
        isPicked = true;
        objectiveFinished.Invoke();
        Destroy(gameObject, .125f);
    }
}
