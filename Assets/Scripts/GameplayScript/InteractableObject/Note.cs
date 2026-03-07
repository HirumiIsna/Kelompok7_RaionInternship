using UnityEngine;

public class Note : MonoBehaviour, IInteractable
{
    public GameObject noteUI;
    
    public void OpenNote()
    {
        noteUI.SetActive(true);
        Time.timeScale = 0f;
        gameObject.SetActive(false);
    }

    public void CloseNote()
    {
        Time.timeScale = 1f;
    }

    public void Interact()
    {
        OpenNote();
    }
}
