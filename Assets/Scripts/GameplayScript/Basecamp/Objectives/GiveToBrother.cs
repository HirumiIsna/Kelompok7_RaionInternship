using UnityEngine;
using UnityEngine.Events;

public class GiveToBrother : MonoBehaviour, IInteractable
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
        onObjectiveCompleted.Invoke();
        objectives.UpdateText(objectiveText);
    }

    public void LastObjectiveCompleted()
    {
        isFinished = false;
        objectives.UpdateText(objectiveText);
    }
}
