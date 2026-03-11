using UnityEngine;

public class FlameSlash : MonoBehaviour
{
    public float flameDamage;

    public void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Boss"))
        {
            BossScript boss = other.gameObject.GetComponent<BossScript>();
            boss.TakeDamage(flameDamage); //ubah biar nyesuaiin scaling damagenya
            Destroy(gameObject);
        }else if(other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            EnemyController enemy = other.gameObject.GetComponent<EnemyController>();
            enemy.TakeDamage((int)flameDamage);
            Destroy(gameObject);
        }
    }

    public void SetFlameDamage(int damage)
    {
        flameDamage = (float)(damage - damage * 0.25f);
    }
}
