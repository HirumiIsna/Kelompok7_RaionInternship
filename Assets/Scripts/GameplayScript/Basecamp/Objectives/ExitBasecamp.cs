using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitBasecamp : MonoBehaviour, IInteractable
{
    public bool canExit = false;
    public string objectiveText;
    private BasecampObjectives objectives;
    
    void Start()
    {
        canExit = true;
        GameObject obj = GameObject.FindGameObjectWithTag("BasecampObjective");
        if(obj != null)
        {
            objectives = obj.GetComponent<BasecampObjectives>();
        }
    }

    public bool CanInteract()
    {
        return !canExit;
    }

    public void Interact()
    {
        if(!CanInteract()) return;
        GameManager.instance.NextDay();
    }

    public void LastObjectiveCompleted()
    {
        canExit = false;
        objectives.UpdateText(objectiveText);
    }
}
