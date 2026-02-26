using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private GameObject _player;
    public float bulletSpeed;
    private Rigidbody2D _rb;
    private float _bulletLifetime;
    public int bulletDamage = 5;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _player = GameObject.FindGameObjectWithTag("Player");

        Vector3 direction = (_player.transform.position - transform.position).normalized;
        _rb.linearVelocity = new Vector2(direction.x, direction.y).normalized * bulletSpeed;

        float bulletRotation = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, bulletRotation);
    }

    // Update is called once per frame
    void Update()
    {
        _bulletLifetime += Time.deltaTime;

        if(_bulletLifetime > 6f) // 5 detik
        {
            Destroy(gameObject);
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player got hit by a Bullet");
            collision.gameObject.GetComponent<PlayerController>().TakeDamage(bulletDamage); // Damage yang diterima player
            Destroy(gameObject);
        }
    }
}
