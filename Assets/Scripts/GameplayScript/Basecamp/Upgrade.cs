using UnityEngine;

public class Upgrade : MonoBehaviour, IInteractable
{
    public GameObject upgradeCanvas;

    public void Interact()
    {
        upgradeCanvas.SetActive(true);
    }

    public static bool TrySpendResource()
    {
        if(ResourceManager.GetBahan1Amount() >= 1 && ResourceManager.GetBahan2Amount() >= 1)
        {
            ResourceManager.DecBahan1Amount();
            ResourceManager.DecBahan2Amount();
            return true;
        }
        else
        {
            return false;
        }
    }
}
