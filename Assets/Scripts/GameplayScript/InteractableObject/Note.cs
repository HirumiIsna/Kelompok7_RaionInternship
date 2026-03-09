using UnityEngine;

public class Note : MonoBehaviour, IInteractable
{
    public GameObject noteUI;
    private bool isOpened = false;

    public void OpenNote()
    {
        isOpened = true;
        noteUI.SetActive(true);
        Time.timeScale = 0f;
        gameObject.SetActive(false);
    }

    public void CloseNote()
    {
        Time.timeScale = 1f;
    }

    public bool CanInteract()
    {
        return !isOpened;
    }

    public void Interact()
    {
        if(!CanInteract()) return;
        OpenNote();
    }
}
