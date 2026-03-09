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
        if(ResourceManager.GetBahan2Amount() >= 2)
        {
            ResourceManager.DecBahan2Amount(sum);
            return true;
        }
        else
        {
            return false;
        }
    }

    public static bool TrySpendResourceHealth()
    {
        if(ResourceManager.GetBahan3Amount() >= 1)
        {
            ResourceManager.DecBahan3Amount();
            return true;
        }
        else
        {
            return false;
        }
    }
}
