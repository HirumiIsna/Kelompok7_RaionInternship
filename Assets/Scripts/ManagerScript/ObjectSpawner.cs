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
    private NPCPatrol npcPatrol;

    void Start()
    {
        foreach (SpawnAreaData areaData in spawnAreas)
        {
            BoxCollider2D box = areaData.spawnArea.GetComponent<BoxCollider2D>();

            // Spawn Melee
            for (int i = 0; i < areaData.meleeEnemies; i++)
            {
                GameObject melee = Instantiate(meleePrefab, box.bounds.center, Quaternion.identity);
                NPCPatrol npcPatrol = melee.GetComponent<NPCPatrol>();
                npcPatrol.GetSpawnArea(areaData.spawnArea.transform);
            }

            // Spawn Range
            for (int i = 0; i < areaData.rangeEnemies; i++)
            {
                GameObject range = Instantiate(rangePrefab, box.bounds.center, Quaternion.identity);
                NPCPatrol npcPatrol = range.GetComponent<NPCPatrol>();
                npcPatrol.GetSpawnArea(areaData.spawnArea.transform);
            }
        }
    }
}
