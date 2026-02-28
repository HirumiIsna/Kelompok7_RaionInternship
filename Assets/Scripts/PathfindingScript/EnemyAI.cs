using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    public GameObject player;
    private Transform target;
    public float speed = 200f;
    public float nextWaypointDistance = 0.5f;
    public EnemyController enemyController;


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

        InvokeRepeating("UpdatePath", 0f, 0.5f);
    }

    void UpdatePath()
    {
        if (target == null) return;
        
        if(seeker.IsDone())
        seeker.StartPath(rb.position, target.position, OnPathComplete);
        
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
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

        if (reachedEndOfPath)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        if (enemyController.isKnockback == false)
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
