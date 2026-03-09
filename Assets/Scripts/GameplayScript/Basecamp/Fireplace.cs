using UnityEngine;

public class Fireplace : MonoBehaviour, IInteractable
{
    private bool isLit = false;

    public bool CanInteract()
    {
        return !isLit;
    }

    public void Interact()
    {
        if(!CanInteract()) return;
        if(ResourceManager.GetBahan1Amount() >= 1)
        {
            ResourceManager.DecBahan1Amount();
        }
        else Debug.Log("You ran out of sticks!");
    }
}
