using UnityEngine;
using System.Collections;
using Unity.Cinemachine;

public class Tangga : MonoBehaviour, IInteractable
{
    [SerializeField] Collider2D mapBoundary;
    CinemachineConfiner2D confiner;
    public Transform teleportTargetPosition;
    public GameObject player;
    public GameObject endUi;

    private void Awake()
    {
        endUi.SetActive(false);
        confiner = FindFirstObjectByType<CinemachineConfiner2D>();
    }

    public void Interact()
    {
        TeleportToTarget();
        // confiner.BoundingShape2D = mapBoundary;
    }

    public void TeleportToTarget()
    {
        player.transform.position = teleportTargetPosition.position;
        StartCoroutine(StartEnding());
    }

    public void ButtonToMenu()
    {
        AudioManager.instance.PlayMusic();
        GameManager.instance.BackToMenu();
    }

    private IEnumerator StartEnding()
    {
        AudioManager.instance.PlayRain();
        yield return new WaitForSeconds(3f);
        endUi.SetActive(true);
    }

}
