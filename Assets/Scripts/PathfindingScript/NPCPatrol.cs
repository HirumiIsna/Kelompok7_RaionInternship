using UnityEngine;
using System.Collections;

public class NPCPatrol : MonoBehaviour
{
    public float speed = 2;

    public float pauseDuration = 1.5f;

    private bool isPaused;

    public float wanderWidth;
    public float wanderHeight;
    public Vector2 startingPosition;
    public Vector2 target;

    private Rigidbody2D rb;

    private EnemyAI enemyAI;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        enemyAI = GetComponent<EnemyAI>();
        target = GetRandomTarget();
    }

    public void GetSpawnArea(Transform spawnArea)
    {
        BoxCollider2D box = spawnArea.GetComponent<BoxCollider2D>();

        wanderWidth = box.size.x * spawnArea.localScale.x;
        wanderHeight = box.size.y * spawnArea.localScale.y;

        startingPosition = box.bounds.center;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(startingPosition, new Vector3(wanderWidth, wanderHeight, 0));
    }

    // Update is called once per frame
    void Update()
    {
        if(enemyAI.isChasing) return;


        if (isPaused)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        if(Vector2.Distance(transform.position, target) < .1f) StartCoroutine(PauseAndSwitch());

        Vector2 direction = (target - (Vector2)transform.position).normalized;
        if(direction.x < 0 && transform.localScale.x > 0 || direction.x > 0 && transform.localScale.x < 0)
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z); 

        rb.linearVelocity = direction * speed;
    }

    private Vector2 GetRandomTarget() //ganti buat ngecek boundaries collider
    {
        float halfWidth = wanderWidth/2;
        float halfHeight = wanderHeight/2;

        int edge = Random.Range(0,4);

        switch (edge)
        {
            case 0:
                return new Vector2(startingPosition.x - halfWidth, 
                Random.Range(startingPosition.y - halfHeight, startingPosition.y + halfHeight)); //kiri, atas bawah random
            case 1:
                return new Vector2(startingPosition.x + halfWidth, 
                Random.Range(startingPosition.y - halfHeight, startingPosition.y + halfHeight)); //kanan, atas bawah random
            case 2:
                return new Vector2(Random.Range(startingPosition.x - halfWidth, startingPosition.x + halfWidth), 
                startingPosition.y - halfHeight); //atas, kiri kanan random
            default:
                return new Vector2(Random.Range(startingPosition.x - halfWidth, startingPosition.x + halfWidth), 
                startingPosition.y + halfHeight); //atas, kiri kanan random
        };
    }
    
    private IEnumerator PauseAndSwitch()
    {
        isPaused = true;
        yield return new WaitForSeconds(pauseDuration);

        target = GetRandomTarget();
        isPaused = false;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        StartCoroutine(PauseAndSwitch());
    }
}
