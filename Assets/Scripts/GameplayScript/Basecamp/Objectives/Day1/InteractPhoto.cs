using UnityEngine;
using UnityEngine.Events;

public class InteractPhoto : MonoBehaviour, IInteractable
{
    public GameObject dialogueObject;
    public string objectiveText;
    private bool isOpened = true;

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
        return !isOpened;
    }

    public void Interact()
    {
        if(!CanInteract()) return;
        DialoguePhoto();
    }

    void DialoguePhoto()
    {
        // Destroy(gameObject,.125f); 
        if(!dialogueObject) return;
        dialogueObject.SetActive(true);
        onObjectiveCompleted.Invoke();
    }

    public void LastObjectiveCompleted()
    {
        isOpened = false;
        objectives.UpdateText(objectiveText);
    }
}
