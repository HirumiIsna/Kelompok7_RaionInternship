using UnityEngine;

public class BackTo : MonoBehaviour, IInteractable
{
    public Transform teleportTargetPosition;
    public GameObject player;

    public void Interact()
    {
        TeleportToTarget();
    }

    public void TeleportToTarget()
    {
        player.transform.position = teleportTargetPosition.position;
    }
}
