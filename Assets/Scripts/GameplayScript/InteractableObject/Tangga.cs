using UnityEngine;
using System.Collections;

public class Tangga : MonoBehaviour, IInteractable
{
    public Transform teleportTargetPosition;
    public GameObject player;
    public GameObject endUi;

    public void Interact()
    {
        TeleportToTarget();
    }

    public void TeleportToTarget()
    {
        player.transform.position = teleportTargetPosition.position;
        StartCoroutine(StartEnding());
    }

    public void ButtonToMenu()
    {
        GameManager.instance.BackToMenu();
    }

    private IEnumerator StartEnding()
    {
        yield return new WaitForSeconds(3f);
        endUi.SetActive(true);
    }

}
