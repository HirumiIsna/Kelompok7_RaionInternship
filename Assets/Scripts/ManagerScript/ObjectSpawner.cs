using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [System.Serializable]
    public class SpawnAreaData
    {
        public GameObject spawnArea;

        [Header("Enemy Sum Spawn Behavior")]
        public int meleeEnemies;
        public int rangeEnemies;

        [Header("Enemy Choosen Type")]
        public int choosenMeleeType = 0;
    }

    public SpawnAreaData[] spawnAreas;
    public GameObject[] vlobPrefab;
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
                GameObject melee = Instantiate(vlobPrefab[areaData.choosenMeleeType], box.bounds.center, Quaternion.identity);
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
