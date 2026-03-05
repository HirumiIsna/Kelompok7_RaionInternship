using UnityEngine;

public class Herbal : MonoBehaviour, IInteractable
{
    public GameObject dialogueObject;
    
    public void Interact()
    {
        CollectHerb();
    }

    public void CollectHerb()
    {
        GameObject.FindFirstObjectByType<GameObjective>().IncreaseHerb();
        dialogueObject.SetActive(true);
        Destroy(gameObject); 
    }

}
