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
        if(ResourceManager.GetBahanAmount(ResourceManager.ResourceType.Bahan1) >= 1)
        {
            ResourceManager.DecBahanAmount(ResourceManager.ResourceType.Bahan1, 1);
        }
        else Debug.Log("You ran out of sticks!");
    }
}
