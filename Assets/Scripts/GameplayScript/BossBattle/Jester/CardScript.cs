using UnityEngine;
using System.Collections;

public class CardScript : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 5f);
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
