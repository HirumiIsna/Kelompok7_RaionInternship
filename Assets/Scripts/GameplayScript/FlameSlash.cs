using UnityEngine;

public class FlameSlash : MonoBehaviour
{
    public float flameDamage;

    private void Start()
    {
        Destroy(gameObject, 0.5f);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.isTrigger) return;

        if(other.gameObject.CompareTag("Boss"))
        {
            BossScript boss = other.gameObject.GetComponent<BossScript>();
            boss.TakeDamage(flameDamage); //ubah biar nyesuaiin scaling damagenya
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
