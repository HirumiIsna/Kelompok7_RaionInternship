using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitBasecamp : MonoBehaviour, IInteractable
{
    private bool canExit = false;

    public bool CanInteract()
    {
        return !canExit;
    }

    public void Interact()
    {
        if(!CanInteract()) return;
        GameManager.instance.NextDay();
    }
}
