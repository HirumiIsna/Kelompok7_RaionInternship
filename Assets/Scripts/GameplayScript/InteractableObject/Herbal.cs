using UnityEngine;

public class Herbal : MonoBehaviour, IInteractable
{
    public void CollectHerb()
    {
        // GameObject.FindFirstObjectByType<GameObjective>().IncreaseHerb();
        Destroy(gameObject); 
    }

    public void Interact()
    {
        CollectHerb();
    }
}
