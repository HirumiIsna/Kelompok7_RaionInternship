using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    public GameObject player;
    private Transform target;
    public float speed = 200f;
    public float nextWaypointDistance = 0.5f;
    public EnemyController enemyController;
    private bool isChasing = false;
    public GameObject rangeArea;
    private float xPos;
    private Vector2 circlePoint;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    private Seeker seeker;
    Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        target = player.transform;
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        enemyController = GetComponent<EnemyController>();
        
        InvokeRepeating("UpdatePath", 0f, 1f);

        switch (gameObject.name)
        {
            case "MeleeEnemy":
            return;
            case "RangeEnemy":
            xPos = rangeArea.transform.localScale.x;
            break;
        }
    }

    void UpdatePath()
    {
        if (target == null) return;
             
        Vector2 dir = (rb.position - (Vector2)target.position).normalized;

        circlePoint = (Vector2)target.position + dir * (xPos/2);

        if (seeker.IsDone())
        seeker.StartPath(rb.position, target.position, OnPathComplete);
        {
            switch (gameObject.name)
            {
                case "MeleeEnemy":
                    seeker.StartPath(rb.position, target.position, OnPathComplete);
                    break;
                case "RangeEnemy":
                    seeker.StartPath(rb.position, circlePoint, OnPathComplete);
                    break;
            }
        }
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 1;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player"))
        {
            isChasing = true;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (path == null) return;

        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        if (reachedEndOfPath) //gak akan nyampe sini methodnya
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        if (enemyController.isKnockback == false && isChasing)
        {
            Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
         
            rb.linearVelocity = direction * speed * Time.fixedDeltaTime;
        }

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
        
    }
}
