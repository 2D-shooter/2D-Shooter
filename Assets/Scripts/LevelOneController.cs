

using UnityEngine;
using TopDown.Core;
using TopDown.World;
using TopDown.Systems; // Added this to find ObjectiveManager

public class LevelOneController : MonoBehaviour
{
    [SerializeField] private int enemiesToKillForBoss = 8;
    [SerializeField] private DoorController bossRoomDoor;
    [SerializeField] private GameObject bossPrefab;
    [SerializeField] private Transform bossSpawnPoint;

    [Header("Exit Settings")]
    [SerializeField] private GameObject levelExitObject;

    private int currentEnemyKills = 0;
    private bool bossSpawned = false;

    private void Start()
    {
        if (ObjectiveManager.Instance != null)
        {
            // params: (enemies, boxes, bosses)
            ObjectiveManager.Instance.SetLevelGoals(enemiesToKillForBoss, 0, 1);
        }

        if (levelExitObject != null) levelExitObject.SetActive(false);
    }

    public void NotifyEnemyDeath(Health.EntityType type)
    {
        if (type == Health.EntityType.Enemy && !bossSpawned)
        {
            currentEnemyKills++;
            if (currentEnemyKills >= enemiesToKillForBoss) TriggerBossPhase();
        }

        if (type == Health.EntityType.Boss)
        {
            Debug.Log("<color=cyan>Boss Defeated! Level Exit is now open.</color>");
            if (levelExitObject != null) levelExitObject.SetActive(true);
        }
    }

    private void TriggerBossPhase()
    {
        bossSpawned = true;

        if (bossRoomDoor != null) 
            bossRoomDoor.ToggleDoor();

        if (bossPrefab != null && bossSpawnPoint != null)
            Instantiate(bossPrefab, bossSpawnPoint.position, Quaternion.identity);

        if (MusicManager.Instance != null)
        {
            MusicManager.Instance.PlayBossMusic();
        }
    }
}