using UnityEngine;

public class DetectionTrigger : MonoBehaviour
{
    public bool isPlayerEnter = false;
    EnemyAI enemyAi;

    // private void OnTriggerEnter2D(Collider2D other) {
    //     EnemyAI enemyAi = GetComponentInParent<EnemyAI>();
    //     if(other.CompareTag("Player"))
    //     {
    //         enemyAi.DetectPlayer();
    //     }
    // }
}
