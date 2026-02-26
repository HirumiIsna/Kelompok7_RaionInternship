using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private int _sumMeleeEnemies;
    [SerializeField] private int _sumRangeEnemies;
    [SerializeField] private int _sumHerb;
    public GameObject[] prefabs;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < _sumMeleeEnemies; i++) // Melee Enemy 
        {
            Vector2 spawnPosition;

            do
            {
                spawnPosition = new Vector2(Random.Range(-16f, 16f), Random.Range(-16f, 16f));
            } 
            while (spawnPosition.magnitude < 5f);

            Instantiate(prefabs[0], spawnPosition, Quaternion.identity);
        }

        for (int i = 0; i < _sumRangeEnemies; i++) // Range Enemy 
        {
            Vector2 spawnPosition;

            do
            {
                spawnPosition = new Vector2(Random.Range(-16f, 16f), Random.Range(-16f, 16f));
            } 
            while (spawnPosition.magnitude < 5f);

            Instantiate(prefabs[1], spawnPosition, Quaternion.identity);
        }

        for (int i = 0; i < _sumHerb; i++) // Herb
        {
            Vector2 spawnPosition;

            do
            {
                spawnPosition = new Vector2(Random.Range(-16f, 16f), Random.Range(-16f, 16f));
            } 
            while (spawnPosition.magnitude < 5f);

            Instantiate(prefabs[2], spawnPosition, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
