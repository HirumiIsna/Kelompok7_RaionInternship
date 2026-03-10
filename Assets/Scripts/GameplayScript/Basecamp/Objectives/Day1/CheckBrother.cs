using UnityEngine;

public class CheckBrother : MonoBehaviour
{
    public GameObject dialogueObject;

    void OnTriggerEnter2D(Collider2D other)
    {
        dialogueObject.SetActive(true);
        Destroy(gameObject,1f);
    }
   
}
