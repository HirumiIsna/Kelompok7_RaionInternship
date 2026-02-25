using UnityEngine;

public class Herbal : MonoBehaviour, IInteractable
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void CollectHerb()
    {
        Debug.Log("Herb Collected!");
        GameObject.FindFirstObjectByType<GameObjective>().IncreaseHerb();
        Destroy(gameObject); 
    }

    public bool CanInteract()
    {
        return true;
    }

    public void Interact()
    {
        if (CanInteract())
        {
            Debug.Log("Interact Herb");
            CollectHerb();
        }
    }
}
