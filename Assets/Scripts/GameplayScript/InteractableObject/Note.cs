using UnityEngine;

public class Note : MonoBehaviour, IInteractable
{
    public GameObject noteUI;
    
    public void OpenNote()
    {
        noteUI.SetActive(true);
        Destroy(gameObject);
    }

    public void Interact()
    {
        OpenNote();
    }
}
