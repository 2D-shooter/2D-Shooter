using UnityEngine;
using TopDown.Core;
using TopDown.World;
using TopDown.Systems;

public class LevelTwoController : MonoBehaviour
{
    [Header("Phase 1")]
    [SerializeField] private int enemiesForBoss1 = 5;
    [SerializeField] private DoorController door1;
    [SerializeField] private GameObject boss1Prefab;
    [SerializeField] private Transform spawn1;

    [Header("Phase 2")]
    [SerializeField] private int totalEnemiesForBoss2 = 10;
    [SerializeField] private DoorController door2;
    [SerializeField] private GameObject boss2Prefab;
    [SerializeField] private Transform spawn2;

    [SerializeField] private GameObject levelExitObject;

    private int currentEnemyKills = 0;
    private int bossesDead = 0;
    private bool boss1Spawned = false;
    private bool boss2Spawned = false;

    private void Start()
    {
        if (ObjectiveManager.Instance != null)
        {
            // params: (enemies, boxes, bosses)
            ObjectiveManager.Instance.SetLevelGoals(totalEnemiesForBoss2, 0, 2);
        }

        if (levelExitObject != null) levelExitObject.SetActive(false);
    }

    public void NotifyEnemyDeath(Health.EntityType type)
    {
        if (type == Health.EntityType.Enemy)
        {
            currentEnemyKills++;
            if (currentEnemyKills >= enemiesForBoss1 && !boss1Spawned) SpawnBoss1();
            if (currentEnemyKills >= totalEnemiesForBoss2 && !boss2Spawned) SpawnBoss2();
        }

        if (type == Health.EntityType.Boss)
        {
            bossesDead++;
            // Exit only opens when BOTH bosses are dead
            if (bossesDead >= 2 && levelExitObject != null) levelExitObject.SetActive(true);
        }
    }

    private void SpawnBoss1()
    {
        boss1Spawned = true;
        if (door1 != null) door1.ToggleDoor();
        if (boss1Prefab != null) Instantiate(boss1Prefab, spawn1.position, Quaternion.identity);
    }

    private void SpawnBoss2()
    {
        boss2Spawned = true;
        if (door2 != null) door2.ToggleDoor();
        if (boss2Prefab != null) Instantiate(boss2Prefab, spawn2.position, Quaternion.identity);
    }
}