using UnityEngine;

public class AttackParent : MonoBehaviour
{
    [SerializeField] private float attackRadius = 1f;
    public Transform castOrigin;
    public Vector2 mousePosition;
    public LayerMask enemyLayer;

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

    public void TryAttack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(castOrigin.position, attackRadius, enemyLayer);
        if(hitEnemies.Length == 0) return; // Kalo ga kena enemy, gak kenapa-napa
        Debug.Log("Succesfully hit an Enemy!");
    }
}
