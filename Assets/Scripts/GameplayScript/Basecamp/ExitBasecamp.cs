using UnityEngine;

public class ExitBasecamp : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject startMenuController;
    public int lastSceneIndex;

    void Start()
    {
        if (startMenuController == null)
        {
            startMenuController = GameObject.Find("StartMenuController");
        }
    }

    public void Interact()
    {
        startMenuController.GetComponent<StartMenuController>().NextDay(lastSceneIndex);
    }
}
