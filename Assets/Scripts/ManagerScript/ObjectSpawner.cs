using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [System.Serializable]
    public class SpawnAreaData
    {
        public GameObject spawnArea;

        [Header("Enemy Sum Spawn Behavior")]
        public int melee1Enemies;
        public int melee2Enemies;
        public int rangeEnemies;

        [Header("Enemies Prefab")]
        public GameObject choosenMeleePrefab;
        public GameObject choosenMelee2Prefab;
        public GameObject choosenRangePrefab;
    }

    public SpawnAreaData[] spawnAreas;
    private NPCPatrol npcPatrol;

    void Start()
    {
        foreach (SpawnAreaData areaData in spawnAreas)
        {
            BoxCollider2D box = areaData.spawnArea.GetComponent<BoxCollider2D>();

            // Spawn Melee
            for (int i = 0; i < areaData.melee1Enemies; i++)
            {
                if(areaData.choosenMeleePrefab == null) continue;
                GameObject melee = Instantiate(areaData.choosenMeleePrefab, box.bounds.center, Quaternion.identity);
                NPCPatrol npcPatrol = melee.GetComponent<NPCPatrol>();
                npcPatrol.GetSpawnArea(areaData.spawnArea.transform);
            }

            for (int i = 0; i < areaData.melee2Enemies; i++)
            {
                if(areaData.choosenMelee2Prefab == null) continue;
                GameObject melee2 = Instantiate(areaData.choosenMelee2Prefab, box.bounds.center, Quaternion.identity);
                NPCPatrol npcPatrol = melee2.GetComponent<NPCPatrol>();
                npcPatrol.GetSpawnArea(areaData.spawnArea.transform);
            }

            // Spawn Range
            for (int i = 0; i < areaData.rangeEnemies; i++)
            {
                if(areaData.choosenRangePrefab == null) continue;
                GameObject range = Instantiate(areaData.choosenRangePrefab, box.bounds.center, Quaternion.identity);
                NPCPatrol npcPatrol = range.GetComponent<NPCPatrol>();
                npcPatrol.GetSpawnArea(areaData.spawnArea.transform);
            }
        }
    }
}
