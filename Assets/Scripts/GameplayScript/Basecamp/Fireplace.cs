using UnityEngine;

public class Fireplace : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        if(ResourceManager.GetBahan1Amount() >= 1)
        {
            ResourceManager.DecBahan1Amount();
        }
        else Debug.Log("You ran out of sticks!");
    }
}
