using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [System.Serializable]
    public class SpawnAreaData
    {
        public GameObject spawnArea;
        public int meleeEnemies;
        public int rangeEnemies;
    }

    public SpawnAreaData[] spawnAreas;
    public GameObject meleePrefab;
    public GameObject rangePrefab;


    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
        foreach (SpawnAreaData areaData in spawnAreas)
        {
            BoxCollider2D box = areaData.spawnArea.GetComponent<BoxCollider2D>();

            // Spawn Melee
            for (int i = 0; i < areaData.meleeEnemies; i++)
            {
                Vector2 pos = GetRandomPointInBounds(box);
                Instantiate(meleePrefab, pos, Quaternion.identity);
            }

            // Spawn Range
            for (int i = 0; i < areaData.rangeEnemies; i++)
            {
                Vector2 pos = GetRandomPointInBounds(box);
                Instantiate(rangePrefab, pos, Quaternion.identity);
            }
        }
    }

    // void SpawnEnemies(int amount, GameObject enemyPrefab)
    // {
    //     foreach (var spawnArea in spawnAreas)
    //     {
    //         BoxCollider2D box = spawnArea.GetComponent<BoxCollider2D>();
            
    //         for (int i = 0; i < amount; i++)
    //         {
    //             Vector2 spawnPosition = GetRandomPointInBounds(box);
                
    //             Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    //         }
    //     }
    // }

    Vector2 GetRandomPointInBounds(BoxCollider2D box) //minggu ketiga kurevisi lagi
    {
        Bounds bounds = box.bounds;

        float randomX = Random.Range(bounds.min.x, bounds.max.x);
        float randomY = Random.Range(bounds.min.y, bounds.max.y);

        return new Vector2(randomX, randomY);
    }
}
