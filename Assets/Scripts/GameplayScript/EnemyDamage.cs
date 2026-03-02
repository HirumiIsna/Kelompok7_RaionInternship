using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    private GameObject _player;

    // Melee Enemy
    public int bodyDamage = 10;

    void Start() {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            DealDamage();
        }
    }

    public void DealDamage()
    {
        PlayerController playerController = _player.GetComponent<PlayerController>();
        if (playerController != null)
        {
            playerController.TakeDamage(bodyDamage, transform, 7f);
        }
    }

}
