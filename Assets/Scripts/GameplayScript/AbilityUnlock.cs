using UnityEngine;

public class AbilityUnlock : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            PlayerController playerController = other.GetComponent<PlayerController>();
            playerController.isAbilityUnlock = true;

            Destroy(gameObject);
        }
    }
   
}
