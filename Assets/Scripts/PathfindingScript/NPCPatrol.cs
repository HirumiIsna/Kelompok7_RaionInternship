using UnityEngine;
using System.Collections;

public class NPC : MonoBehaviour
{
    [SerializeField] private Vector2[] patrolPoints;
    public float speed = 2;

    public float pauseDuration = 1.5f;

    private bool isPaused;
    private int currentPatrolIndex;
    private Vector2 target;

    private Rigidbody2D rb;

    private EnemyAI enemyAI;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        patrolPoints = new Vector2[3];

        for (int i = 0; i < patrolPoints.Length; i++)
        {
            patrolPoints[i] = new Vector2(
                Random.Range(transform.position.x, transform.position.x + 5),
                Random.Range(transform.position.y, transform.position.y + 5)
            );
        }
        rb = GetComponent<Rigidbody2D>();
        target = patrolPoints[currentPatrolIndex];
        enemyAI = GetComponent<EnemyAI>();
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
        Vector2 direction = ((Vector3)target - transform.position).normalized;
        if(direction.x < 0 && transform.localScale.x > 0 || direction.x > 0 && transform.localScale.x < 0)
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z); 

        rb.linearVelocity = direction * speed;

        if(Vector2.Distance(transform.position, target) < 0.1f) StartCoroutine(SetPatrolPoint());
    }

    private IEnumerator SetPatrolPoint()
    {
        isPaused = true;

        yield return new WaitForSeconds(pauseDuration);

        currentPatrolIndex = (currentPatrolIndex + 1)%patrolPoints.Length;
        target = patrolPoints[currentPatrolIndex];
        Debug.Log("Patrolling to: " + target);
        isPaused = false;
    }
}
