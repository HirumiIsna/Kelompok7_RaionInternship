using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour
{
    private GameObject _player;
    public float bulletSpeed;
    private Rigidbody2D _rb;
    private float _bulletLifetime;
    public int bulletDamage = 5;
    private Transform emptyTransform; //biarin aja 

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _player = GameObject.FindGameObjectWithTag("Player");

        Vector3 direction = (_player.transform.position - transform.position).normalized;
        _rb.linearVelocity = new Vector2(direction.x, direction.y).normalized * bulletSpeed;

        float bulletRotation = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, bulletRotation);
    }

    void Update()
    {
        _bulletLifetime += Time.deltaTime;

        if(_bulletLifetime > 5f) 
        {
            Destroy(gameObject);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            // Debug.Log("Player got hit by a Bullet");
            collision.gameObject.GetComponent<PlayerController>().TakeDamage(bulletDamage, emptyTransform, 0f); // Damage yang diterima player
            Destroy(gameObject);
        }
    }

    public void DeflectArrow()
    {
        StartCoroutine(Deflect());
    }

    private IEnumerator Deflect()
    {
        Time.timeScale = 0.2f;
        Destroy(gameObject.GetComponent<BoxCollider2D>());
        yield return new WaitForSeconds(.125f);
        Time.timeScale = 1f;
        Destroy(gameObject);
    }

}
