using UnityEngine;
using UnityEngine.Events;

public class ObjectiveTrigger : MonoBehaviour
{
    public GameObjective objective;
    public string objectiveText1;
    public string objectiveText2;

    public UnityEvent onObjectiveTrigger;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            objective.ChangeObjective(objectiveText1, objectiveText2);
            onObjectiveTrigger.Invoke();
            Destroy(gameObject, 1f);
        }
    }
}
