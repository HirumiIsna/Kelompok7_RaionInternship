using UnityEngine;

public class DoctorShop : MonoBehaviour, IInteractable
{
    public GameObject shopPanel;

    public GameObject objectiveGameObject;

    private GameObjective objective;

    void Start()
    {
        objective = objectiveGameObject.GetComponent<GameObjective>();        
    }

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
        if(ResourceManager.GetBahanAmount(ResourceManager.ResourceType.Bahan4) >= 2 && objective.currentPotion < objective.maxPotion)
        {
            ResourceManager.DecBahanAmount(ResourceManager.ResourceType.Bahan4, 2);
            objective.IncreasePotion();
        }
    }
}
