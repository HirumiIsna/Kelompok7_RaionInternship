using UnityEngine;
using UnityEngine.Events;

public class PickupDagger : MonoBehaviour, IInteractable
{
    public GameObject dialogueObject;
    public string objectiveText;
    private bool isFinished = true;

    public UnityEvent onObjectiveCompleted;

    private BasecampObjectives objectives;
    
    void Start()
    {
        GameObject obj = GameObject.FindGameObjectWithTag("BasecampObjective");
        if(obj != null)
        {
            objectives = obj.GetComponent<BasecampObjectives>();
        }
    }

    public bool CanInteract()
    {
        return !isFinished;
    }

    public void Interact()
    {
        if(!CanInteract()) return;
        DialoguePickupDagger();
    }

    void DialoguePickupDagger()
    {
        Destroy(gameObject,.125f); 
        if(!dialogueObject) return;
        dialogueObject.SetActive(true);
        onObjectiveCompleted.Invoke();
    }

    public void LastObjectiveCompleted()
    {
        isFinished = false;
        objectives.UpdateText(objectiveText);
    }
}
