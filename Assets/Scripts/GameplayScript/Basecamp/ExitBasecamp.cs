using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitBasecamp : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        GameManager.instance.NextDay();
    }
}
