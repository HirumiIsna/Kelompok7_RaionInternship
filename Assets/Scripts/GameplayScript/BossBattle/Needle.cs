using UnityEngine;

public class Needle : MonoBehaviour
{
    public float speed = 15f;
    private Vector2 moveDir;

    public void Initialize(Vector2 dir)
    {
        moveDir = dir;
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
            other.GetComponent<PlayerController>().TakeDamage(10, null, 0);
        }
    }
}