using UnityEngine;

public class FlameSlash : MonoBehaviour
{
    public float flameDamage;

    private void Start()
    {
        Destroy(gameObject, 2f);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.isTrigger) return;

        if(other.gameObject.CompareTag("Boss"))
        {
            if(other.TryGetComponent(out BossScript boss))
            {
                boss.TakeDamage(flameDamage);
            }
            else if(other.TryGetComponent(out JesterScript jester))
            {
                jester.TakeDamage(flameDamage);
            }

        }else if(other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            EnemyController enemy = other.gameObject.GetComponent<EnemyController>();
            enemy.TakeDamage((int)flameDamage);
        }
    }

    public void SetFlameDamage(int damage)
    {
        flameDamage = (float)(damage - damage * 0.25f);
    }
}
