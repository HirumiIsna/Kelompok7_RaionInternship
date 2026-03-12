using UnityEngine;
using System.Collections;

public class AttackParent : MonoBehaviour
{
    [SerializeField] private float attackRadius = 1f;
    public Transform castOrigin;
    public Vector2 mousePosition;
    public LayerMask enemyLayer;
    public float knockbackForce; 
    public GameObject screenFlash;

    // Update is called once per frame
    void Update()
    {
        transform.right = (mousePosition - (Vector2)transform.position).normalized;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 position = castOrigin == null ? Vector3.zero : castOrigin.position;
        Gizmos.DrawWireSphere(position, attackRadius);
    }

    public void TryAttack(int damage)
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(castOrigin.position, attackRadius, enemyLayer);
        if(hitEnemies.Length == 0) return; // Kalo ga kena enemy, gak kenapa-napa
        foreach (Collider2D enemy in hitEnemies)
        {

            //sumpah cik gak efficient banget harus satu satu (pengen ku revisi tapi gak ada waktunya)
            EnemyController enemyController = enemy.GetComponent<EnemyController>();
            BulletController bulletController = enemy.GetComponent<BulletController>();
            BossScript bossScript = enemy.GetComponent<BossScript>();
            JesterScript jesterScript = enemy.GetComponent<JesterScript>();
            BossBullet bossBullet = enemy.GetComponent<BossBullet>();
            CardScript cardScript = enemy.GetComponent<CardScript>();

            if (enemyController != null && !enemy.isTrigger)
            {
                enemyController.TakeDamage(damage);
                enemyController.Knockback(transform, knockbackForce); 
                // enemyController.Particle(transform);
            }
            else if (bulletController || bossBullet || cardScript)
            {
                AudioManager.instance.PlayDeflect();
                if(bulletController) bulletController.DeflectArrow();
                if(bossBullet) bossBullet.DeflectBullet();
                if(cardScript) cardScript.DeflectBullet();
                StartCoroutine(DeflectFlash());
            }
            else if (bossScript || jesterScript)
            {
                if(bossScript) bossScript.TakeDamage(damage);
                if(jesterScript) jesterScript.TakeDamage(damage);
            }
        }
    }

    private IEnumerator DeflectFlash()
    {
        screenFlash.SetActive(true);
        yield return new WaitForSeconds(0.125f);
        screenFlash.SetActive(false);
    }
}
