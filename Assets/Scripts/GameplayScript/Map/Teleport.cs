using UnityEngine;
using Unity.Cinemachine;

public class Teleport : MonoBehaviour, IInteractable
{
    [SerializeField] PolygonCollider2D mapBoundary;
    [SerializeField] Transform teleportTarget;
    private GameObject player;
    CinemachineConfiner2D confiner;
    
    private void Awake()
    {
        confiner = FindFirstObjectByType<CinemachineConfiner2D>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public bool CanInteract()
    {
        return true;
    }

    public void Interact()
    {
        confiner.BoundingShape2D = mapBoundary;
        player.transform.position = teleportTarget.position;
    }
}
