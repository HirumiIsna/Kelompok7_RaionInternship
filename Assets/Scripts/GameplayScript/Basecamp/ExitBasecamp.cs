using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitBasecamp : MonoBehaviour, IInteractable
{
    public int lastSceneIndex = 1;

    void Start()
    {
    
    }

    public void Interact()
    {
        Debug.Log(SceneManager.GetActiveScene().buildIndex);
        GameManager.instance.NextDay();
    }
}
