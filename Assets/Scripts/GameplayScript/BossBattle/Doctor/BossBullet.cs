using UnityEngine;
using System.Collections;

public class BossBullet : MonoBehaviour
{
    public float speed = 5f;
    private Vector2 direction;

    public void Initialize(Vector2 dir)
    {
        direction = dir;
        Destroy(gameObject, 5f);
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().TakeDamage(5, transform, 5);
        }
    }

    public void DeflectBullet()
    {
        StartCoroutine(Deflect());
    }

    private IEnumerator Deflect()
    {
        Time.timeScale = 0f;
        Destroy(gameObject.GetComponent<BoxCollider2D>());
        yield return new WaitForSecondsRealtime(.25f);
        Time.timeScale = 1f;
        Destroy(gameObject);
    }
}