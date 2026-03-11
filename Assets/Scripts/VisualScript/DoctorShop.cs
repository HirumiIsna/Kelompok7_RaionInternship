using UnityEngine;
using UnityEngine.Events;

public class DoctorShop : MonoBehaviour, IInteractable
{
    public GameObject shopPanel;

    public UnityEvent onBuyPotion;

    public bool CanInteract()
    {
        return true;
    }

    public void Interact()
    {
        shopPanel.SetActive(true);
    }

    public void BuyPotion()
    {
        // if(ResourceManager.GetBahanAmount(ResourceManager.ResourceType.Bahan4) >= 0)
        // {
            // ResourceManager.DecBahanAmount(ResourceManager.ResourceType.Bahan4, 2);
            onBuyPotion.Invoke();
        // }
    }
}
