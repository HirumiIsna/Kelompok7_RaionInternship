using UnityEngine;
using UnityEngine.Events;

public class CheckBrother : MonoBehaviour
{
    public GameObject dialogueObject;

    public UnityEvent onObjectiveCompleted;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            dialogueObject.SetActive(true);
            Destroy(gameObject,.25f);
            onObjectiveCompleted.Invoke();      
        }
    }
   
}
