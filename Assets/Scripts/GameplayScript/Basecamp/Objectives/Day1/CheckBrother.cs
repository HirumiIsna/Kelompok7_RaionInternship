using UnityEngine;
using UnityEngine.Events;

public class CheckBrother : MonoBehaviour
{
    public GameObject dialogueObject;

    public UnityEvent onObjectiveCompleted;

    void OnTriggerEnter2D(Collider2D other)
    {
        dialogueObject.SetActive(true);
        Destroy(gameObject,1f);
        onObjectiveCompleted.Invoke();
    }
   
}
