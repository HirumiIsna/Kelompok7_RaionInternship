using UnityEngine;

public class Upgrade : MonoBehaviour, IInteractable
{
    public GameObject upgradeCanvas;
    private bool isUpgraded = false;

    public bool CanInteract()
    {
        return !isUpgraded;
    }

    public void Interact()
    {
        if(!CanInteract()) return;
        upgradeCanvas.SetActive(true);
    }

    public static bool TrySpendResourceDamage(int sum)
    {
        if(ResourceManager.GetBahanAmount(ResourceManager.ResourceType.Bahan2) >= 2)
        {
            ResourceManager.DecBahanAmount(ResourceManager.ResourceType.Bahan2, sum);
            return true;
        }
        else
        {
            return false;
        }
    }

    public static bool TrySpendResourceHealth()
    {
        if(ResourceManager.GetBahanAmount(ResourceManager.ResourceType.Bahan3) >= 1)
        {
            ResourceManager.DecBahanAmount(ResourceManager.ResourceType.Bahan3, 1);
            return true;
        }
        else
        {
            return false;
        }
    }
}
