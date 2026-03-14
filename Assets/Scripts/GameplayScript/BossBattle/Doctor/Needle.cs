using UnityEngine;

public class Needle : MonoBehaviour
{
    public float speed = 100f;
    private Vector2 moveDir;

    public void Initialize(Vector2 dir)
    {
        moveDir = dir.normalized;
        Destroy(gameObject, 3f);
    }

    void Update()
    {
        transform.Translate(moveDir * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().TakeDamage(20, null, 0);
        }
    }
}