using UnityEngine;
using UnityEngine.Events;

public class SecretNote : MonoBehaviour, IInteractable
{
    public UnityEvent objectiveFinished;
    public GameObject objectiveManager;
    bool isPicked = false;

    public void Awake()
    {
        objectiveManager = GameObject.FindGameObjectWithTag("ObjectiveManager");
    }

    public bool CanInteract()
    {
        return !isPicked;
    }

    public void Interact()
    {
        if(!CanInteract()) return;
        isPicked = true;
        GoodEnding();
        objectiveFinished.Invoke();
        Destroy(gameObject, .125f);
    }

    public void GoodEnding()
    {
        objectiveManager.GetComponent<GameObjective>().goodEnding = true;
    }
}
