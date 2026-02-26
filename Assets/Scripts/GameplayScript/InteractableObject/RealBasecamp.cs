using UnityEngine;

public class RealBasecamp : MonoBehaviour, IInteractable
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
