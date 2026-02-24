using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    public Node currentNode;
    public StateMachine currentState;
    public List<Node> path;

    public PlayerController player;
    public float speed = 4f;
    public EnemyController enemyController;
    public enum StateMachine
    {
        Idle,
        Chase,
        Evade
    }


    void Start()
    {
        currentState = StateMachine.Idle;
        enemyController = GetComponent<EnemyController>();
    }

    void Update()
    {
        switch(currentState)
        {
            case StateMachine.Idle:
                Idle();
                break;
            case StateMachine.Chase:
                Chase();
                break;
            case StateMachine.Evade:
                Evade();
                break;
        }

        bool playerSeen = Vector2.Distance(transform.position, player.transform.position) < 10f; //atur float 5 sesuai preferensi

        if(playerSeen == false && currentState != StateMachine.Idle && enemyController.currentHealth > 20) //currentHealth > 20% maxHealth
        {
            currentState = StateMachine.Idle;
            path.Clear();
        }
        else if(playerSeen == true && currentState != StateMachine.Chase && enemyController.currentHealth > 20) //currentHealth > 20% maxHealth
        {
            currentState = StateMachine.Chase;
            path.Clear();
        }
        else if(currentState != StateMachine.Evade && enemyController.currentHealth <= 20) //currentHealth <= 20% maxHealth
        {
            currentState = StateMachine.Evade;
            path.Clear();
        }

        CreatePath();
    }

    void Idle()
    {
        if(path.Count == 0)
        {
            path = AStarManager.instance.GeneratePath(currentNode, AStarManager.instance.NodesInScene()
                                                     [Random.Range(0, AStarManager.instance.NodesInScene().Length)]);    
        }
    }

    void Chase()
    {
        if(path.Count == 0)
        {
            path = AStarManager.instance.GeneratePath(currentNode, AStarManager.instance.FindNearestNode(player.transform.position));
        }
    }

    void Evade()
    {
        if(path.Count == 0)
        {
            path = AStarManager.instance.GeneratePath(currentNode, AStarManager.instance.FindFurthestNode(player.transform.position));
        }
    }

    void CreatePath()
    {
        if(path.Count > 0)
        {
            int x = 0;
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(path[x].transform.position.x, 
                                                    path[x].transform.position.y), speed * Time.deltaTime);

            if(Vector2.Distance(transform.position, path[x].transform.position) < 0.1f)
            {
                currentNode = path[x];
                path.RemoveAt(x);
            }
        }
    }

}
