//////////////////////////using UnityEngine;
//////////////////////////using TopDown.Core;
//////////////////////////using TopDown.World;

//////////////////////////public class LevelOneController : MonoBehaviour
//////////////////////////{
//////////////////////////    [Header("Requirements")]
//////////////////////////    [SerializeField] private int enemiesToKillForBoss = 8;

//////////////////////////    [Header("References")]
//////////////////////////    [SerializeField] private DoorController bossRoomDoor;
//////////////////////////    [SerializeField] private GameObject bossPrefab;
//////////////////////////    [SerializeField] private Transform bossSpawnPoint;
//////////////////////////    [SerializeField] private GameObject levelExit;

//////////////////////////    private int currentEnemyKills = 0;
//////////////////////////    private bool bossSpawned = false;
//////////////////////////    private bool levelComplete = false;

//////////////////////////    void Start()
//////////////////////////    {
//////////////////////////        // Ensure exit is hidden at start
//////////////////////////        if (levelExit != null) levelExit.SetActive(false);

//////////////////////////        // Subscribe to death events via a custom global action or 
//////////////////////////        // by checking the ObjectiveManager's state
//////////////////////////    }

//////////////////////////    // Call this from ObjectiveManager or directly from Health.cs
//////////////////////////    public void NotifyEnemyDeath(Health.EntityType type)
//////////////////////////    {
//////////////////////////        if (type == Health.EntityType.Enemy && !bossSpawned)
//////////////////////////        {
//////////////////////////            currentEnemyKills++;
//////////////////////////            Debug.Log($"Enemies Killed: {currentEnemyKills}/{enemiesToKillForBoss}");

//////////////////////////            if (currentEnemyKills >= enemiesToKillForBoss)
//////////////////////////            {
//////////////////////////                TriggerBossPhase();
//////////////////////////            }
//////////////////////////        }
//////////////////////////        else if (type == Health.EntityType.Boss)
//////////////////////////        {
//////////////////////////            TriggerVictory();
//////////////////////////        }
//////////////////////////    }

//////////////////////////    private void TriggerBossPhase()
//////////////////////////    {
//////////////////////////        bossSpawned = true;

//////////////////////////        // 1. Open the door
//////////////////////////        if (bossRoomDoor != null) bossRoomDoor.ToggleDoor();

//////////////////////////        // 2. Spawn the Boss
//////////////////////////        if (bossPrefab != null && bossSpawnPoint != null)
//////////////////////////        {
//////////////////////////            Instantiate(bossPrefab, bossSpawnPoint.position, Quaternion.identity);
//////////////////////////            Debug.Log("The Boss has arrived!");
//////////////////////////        }
//////////////////////////    }

//////////////////////////    private void TriggerVictory()
//////////////////////////    {
//////////////////////////        levelComplete = true;
//////////////////////////        if (levelExit != null)
//////////////////////////        {
//////////////////////////            levelExit.SetActive(true);
//////////////////////////            Debug.Log("Level Exit Spawned! Get to the extraction!");
//////////////////////////        }
//////////////////////////    }
//////////////////////////}

////////////////////////using UnityEngine;
////////////////////////using TopDown.Core;
////////////////////////using TopDown.World;

////////////////////////// MAKE SURE THE FILE IS NAMED: LevelOneController.cs
////////////////////////public class LevelOneController : MonoBehaviour
////////////////////////{
////////////////////////    [Header("Requirements")]
////////////////////////    [SerializeField] private int enemiesToKillForBoss = 8;

////////////////////////    [Header("References")]
////////////////////////    [SerializeField] private DoorController bossRoomDoor;
////////////////////////    [SerializeField] private GameObject bossPrefab;
////////////////////////    [SerializeField] private Transform bossSpawnPoint;
////////////////////////    [SerializeField] private GameObject levelExit;

////////////////////////    private int currentEnemyKills = 0;
////////////////////////    private bool bossSpawned = false;

////////////////////////    void Start()
////////////////////////    {
////////////////////////        // Ensure exit is hidden at start
////////////////////////        if (levelExit != null) levelExit.SetActive(false);
////////////////////////    }

////////////////////////    public void NotifyEnemyDeath(Health.EntityType type)
////////////////////////    {
////////////////////////        if (type == Health.EntityType.Enemy && !bossSpawned)
////////////////////////        {
////////////////////////            currentEnemyKills++;
////////////////////////            Debug.Log($"Enemies Killed: {currentEnemyKills}/{enemiesToKillForBoss}");

////////////////////////            if (currentEnemyKills >= enemiesToKillForBoss)
////////////////////////            {
////////////////////////                TriggerBossPhase();
////////////////////////            }
////////////////////////        }
////////////////////////        // We don't need to handle Boss death here anymore because 
////////////////////////        // ObjectiveManager handles the final win condition!
////////////////////////    }

////////////////////////    private void TriggerBossPhase()
////////////////////////    {
////////////////////////        bossSpawned = true;

////////////////////////        // 1. Open the door
////////////////////////        if (bossRoomDoor != null) bossRoomDoor.ToggleDoor();

////////////////////////        // 2. Spawn the Boss
////////////////////////        if (bossPrefab != null && bossSpawnPoint != null)
////////////////////////        {
////////////////////////            Instantiate(bossPrefab, bossSpawnPoint.position, Quaternion.identity);
////////////////////////            Debug.Log("The Boss has arrived!");
////////////////////////        }
////////////////////////    }
////////////////////////}

//////////////////////using UnityEngine;
//////////////////////using TopDown.Core;
//////////////////////using TopDown.World;

//////////////////////// SAVE THIS FILE AS: LevelOneController.cs
//////////////////////public class LevelOneController : MonoBehaviour
//////////////////////{
//////////////////////    [Header("Requirements")]
//////////////////////    [SerializeField] private int enemiesToKillForBoss = 8;

//////////////////////    [Header("References")]
//////////////////////    [SerializeField] private DoorController bossRoomDoor;
//////////////////////    [SerializeField] private GameObject bossPrefab;
//////////////////////    [SerializeField] private Transform bossSpawnPoint;
//////////////////////    [SerializeField] private GameObject levelExit;

//////////////////////    private int currentEnemyKills = 0;
//////////////////////    private bool bossSpawned = false;

//////////////////////    void Start()
//////////////////////    {
//////////////////////        if (levelExit != null) levelExit.SetActive(false);
//////////////////////    }

//////////////////////    public void NotifyEnemyDeath(Health.EntityType type)
//////////////////////    {
//////////////////////        if (type == Health.EntityType.Enemy && !bossSpawned)
//////////////////////        {
//////////////////////            currentEnemyKills++;
//////////////////////            if (currentEnemyKills >= enemiesToKillForBoss)
//////////////////////            {
//////////////////////                TriggerBossPhase();
//////////////////////            }
//////////////////////        }
//////////////////////    }

//////////////////////    private void TriggerBossPhase()
//////////////////////    {
//////////////////////        bossSpawned = true;

//////////////////////        // Open the door to the boss room
//////////////////////        if (bossRoomDoor != null) bossRoomDoor.ToggleDoor();

//////////////////////        // Spawn the boss at the designated spot
//////////////////////        if (bossPrefab != null && bossSpawnPoint != null)
//////////////////////        {
//////////////////////            Instantiate(bossPrefab, bossSpawnPoint.position, Quaternion.identity);
//////////////////////            Debug.Log("BOSS SPAWNED!");
//////////////////////        }
//////////////////////    }
//////////////////////}

////////////////////using UnityEngine;
////////////////////using TopDown.Core;
////////////////////using TopDown.World;

////////////////////public class LevelOneController : MonoBehaviour
////////////////////{
////////////////////    [Header("Requirements")]
////////////////////    [SerializeField] private int enemiesToKillForBoss = 8;

////////////////////    [Header("References")]
////////////////////    [SerializeField] private DoorController bossRoomDoor;
////////////////////    [SerializeField] private GameObject bossPrefab;
////////////////////    [SerializeField] private Transform bossSpawnPoint;
////////////////////    [SerializeField] private GameObject levelExit;

////////////////////    private int currentEnemyKills = 0;
////////////////////    private bool bossSpawned = false;

////////////////////    void Start()
////////////////////    {
////////////////////        if (levelExit != null) levelExit.SetActive(false);
////////////////////    }

////////////////////    public void NotifyEnemyDeath(Health.EntityType type)
////////////////////    {
////////////////////        if (type == Health.EntityType.Enemy && !bossSpawned)
////////////////////        {
////////////////////            currentEnemyKills++;
////////////////////            if (currentEnemyKills >= enemiesToKillForBoss)
////////////////////            {
////////////////////                TriggerBossPhase();
////////////////////            }
////////////////////        }
////////////////////    }

////////////////////    private void TriggerBossPhase()
////////////////////    {
////////////////////        bossSpawned = true;

////////////////////        if (bossRoomDoor != null) bossRoomDoor.ToggleDoor();

////////////////////        if (bossPrefab != null && bossSpawnPoint != null)
////////////////////        {
////////////////////            Instantiate(bossPrefab, bossSpawnPoint.position, Quaternion.identity);
////////////////////            Debug.Log("BOSS SPAWNED!");
////////////////////        }
////////////////////    }
////////////////////}

//////////////////using UnityEngine;
//////////////////using TopDown.Core;
//////////////////using TopDown.World;

//////////////////public class LevelOneController : MonoBehaviour
//////////////////{
//////////////////    [Header("Requirements")]
//////////////////    [SerializeField] private int enemiesToKillForBoss = 8;

//////////////////    [Header("References")]
//////////////////    [SerializeField] private DoorController bossRoomDoor;
//////////////////    [SerializeField] private GameObject bossPrefab;
//////////////////    [SerializeField] private Transform bossSpawnPoint;
//////////////////    [SerializeField] private GameObject levelExit;

//////////////////    private int currentEnemyKills = 0;
//////////////////    private bool bossSpawned = false;

//////////////////    void Start()
//////////////////    {
//////////////////        // Hide exit at the start of the level
//////////////////        if (levelExit != null) levelExit.SetActive(false);
//////////////////    }

//////////////////    public void NotifyEnemyDeath(Health.EntityType type)
//////////////////    {
//////////////////        // Only count regular "Enemy" types toward the boss spawn
//////////////////        if (type == Health.EntityType.Enemy && !bossSpawned)
//////////////////        {
//////////////////            currentEnemyKills++;
//////////////////            Debug.Log($"Progress: {currentEnemyKills}/{enemiesToKillForBoss}");

//////////////////            if (currentEnemyKills >= enemiesToKillForBoss)
//////////////////            {
//////////////////                TriggerBossPhase();
//////////////////            }
//////////////////        }
//////////////////    }

//////////////////    private void TriggerBossPhase()
//////////////////    {
//////////////////        bossSpawned = true;

//////////////////        // Open the duplicated "BossDoor"
//////////////////        if (bossRoomDoor != null)
//////////////////        {
//////////////////            bossRoomDoor.ToggleDoor();
//////////////////        }

//////////////////        // Spawn the Boss
//////////////////        if (bossPrefab != null && bossSpawnPoint != null)
//////////////////        {
//////////////////            Instantiate(bossPrefab, bossSpawnPoint.position, Quaternion.identity);
//////////////////            Debug.Log("BOSS SPAWNED!");
//////////////////        }
//////////////////    }
//////////////////}

////////////////using UnityEngine;
////////////////using TopDown.Core;
////////////////using TopDown.World;

////////////////public class LevelOneController : MonoBehaviour
////////////////{
////////////////    [SerializeField] private int enemiesToKillForBoss = 8;
////////////////    [SerializeField] private DoorController bossRoomDoor;
////////////////    [SerializeField] private GameObject bossPrefab;
////////////////    [SerializeField] private Transform bossSpawnPoint;
////////////////    [SerializeField] private GameObject levelExit;

////////////////    private int currentEnemyKills = 0;
////////////////    private bool bossSpawned = false;

////////////////    void Start() { if (levelExit != null) levelExit.SetActive(false); }

////////////////    public void NotifyEnemyDeath(Health.EntityType type)
////////////////    {
////////////////        if (type == Health.EntityType.Enemy && !bossSpawned)
////////////////        {
////////////////            currentEnemyKills++;
////////////////            if (currentEnemyKills >= enemiesToKillForBoss) TriggerBossPhase();
////////////////        }
////////////////    }

////////////////    private void TriggerBossPhase()
////////////////    {
////////////////        bossSpawned = true;
////////////////        if (bossRoomDoor != null) bossRoomDoor.ToggleDoor();
////////////////        if (bossPrefab != null && bossSpawnPoint != null)
////////////////            Instantiate(bossPrefab, bossSpawnPoint.position, Quaternion.identity);
////////////////    }
////////////////}

//////////////using UnityEngine;
//////////////using TopDown.Core;
//////////////using TopDown.World;

//////////////public class LevelOneController : MonoBehaviour
//////////////{
//////////////    [SerializeField] private int enemiesToKillForBoss = 8;
//////////////    [SerializeField] private DoorController bossRoomDoor;
//////////////    [SerializeField] private GameObject bossPrefab;
//////////////    [SerializeField] private Transform bossSpawnPoint;

//////////////    private int currentEnemyKills = 0;
//////////////    private bool bossSpawned = false;

//////////////    public void NotifyEnemyDeath(Health.EntityType type)
//////////////    {
//////////////        if (type == Health.EntityType.Enemy && !bossSpawned)
//////////////        {
//////////////            currentEnemyKills++;
//////////////            if (currentEnemyKills >= enemiesToKillForBoss)
//////////////            {
//////////////                TriggerBossPhase();
//////////////            }
//////////////        }
//////////////    }

//////////////    private void TriggerBossPhase()
//////////////    {
//////////////        bossSpawned = true;
//////////////        if (bossRoomDoor != null) bossRoomDoor.ToggleDoor();
//////////////        if (bossPrefab != null && bossSpawnPoint != null)
//////////////        {
//////////////            Instantiate(bossPrefab, bossSpawnPoint.position, Quaternion.identity);
//////////////        }
//////////////    }
//////////////}

////////////using UnityEngine;
////////////using TopDown.Core;
////////////using TopDown.World;

////////////public class LevelOneController : MonoBehaviour
////////////{
////////////    [SerializeField] private int enemiesToKillForBoss = 8;
////////////    [SerializeField] private DoorController bossRoomDoor;
////////////    [SerializeField] private GameObject bossPrefab;
////////////    [SerializeField] private Transform bossSpawnPoint;

////////////    private int currentEnemyKills = 0;
////////////    private bool bossSpawned = false;

////////////    public void NotifyEnemyDeath(Health.EntityType type)
////////////    {
////////////        if (type == Health.EntityType.Enemy && !bossSpawned)
////////////        {
////////////            currentEnemyKills++;
////////////            if (currentEnemyKills >= enemiesToKillForBoss) TriggerBossPhase();
////////////        }
////////////    }

////////////    private void TriggerBossPhase()
////////////    {
////////////        bossSpawned = true;
////////////        if (bossRoomDoor != null) bossRoomDoor.ToggleDoor();
////////////        if (bossPrefab != null && bossSpawnPoint != null)
////////////            Instantiate(bossPrefab, bossSpawnPoint.position, Quaternion.identity);
////////////    }
////////////}

//////////using UnityEngine;
//////////using TopDown.Core;
//////////using TopDown.World;

//////////public class LevelOneController : MonoBehaviour
//////////{
//////////    [SerializeField] private int enemiesToKillForBoss = 8;
//////////    [SerializeField] private DoorController bossRoomDoor;
//////////    [SerializeField] private GameObject bossPrefab;
//////////    [SerializeField] private Transform bossSpawnPoint;

//////////    private int currentEnemyKills = 0;
//////////    private bool bossSpawned = false;

//////////    public void NotifyEnemyDeath(Health.EntityType type)
//////////    {
//////////        if (type == Health.EntityType.Enemy && !bossSpawned)
//////////        {
//////////            currentEnemyKills++;
//////////            if (currentEnemyKills >= enemiesToKillForBoss) TriggerBossPhase();
//////////        }
//////////    }

//////////    private void TriggerBossPhase()
//////////    {
//////////        bossSpawned = true;
//////////        if (bossRoomDoor != null) bossRoomDoor.ToggleDoor();
//////////        if (bossPrefab != null && bossSpawnPoint != null)
//////////            Instantiate(bossPrefab, bossSpawnPoint.position, Quaternion.identity);
//////////    }
//////////}

////////using UnityEngine;
////////using TopDown.Core;
////////using TopDown.World;

////////public class LevelOneController : MonoBehaviour
////////{
////////    [SerializeField] private int enemiesToKillForBoss = 1;
////////    [SerializeField] private DoorController bossRoomDoor;
////////    [SerializeField] private GameObject bossPrefab;
////////    [SerializeField] private Transform bossSpawnPoint;

////////    private int currentEnemyKills = 0;
////////    private bool bossSpawned = false;

////////    public void NotifyEnemyDeath(Health.EntityType type)
////////    {
////////        if (type == Health.EntityType.Enemy && !bossSpawned)
////////        {
////////            currentEnemyKills++;
////////            if (currentEnemyKills >= enemiesToKillForBoss) TriggerBossPhase();
////////        }
////////    }

////////    private void TriggerBossPhase()
////////    {
////////        bossSpawned = true;
////////        if (bossRoomDoor != null) bossRoomDoor.ToggleDoor();
////////        if (bossPrefab != null && bossSpawnPoint != null)
////////            Instantiate(bossPrefab, bossSpawnPoint.position, Quaternion.identity);
////////    }
////////}

//////using UnityEngine;
//////using TopDown.Core;
//////using TopDown.World;

//////public class LevelOneController : MonoBehaviour
//////{
//////    [SerializeField] private int enemiesToKillForBoss = 1;
//////    [SerializeField] private DoorController bossRoomDoor;
//////    [SerializeField] private GameObject bossPrefab;
//////    [SerializeField] private Transform bossSpawnPoint;

//////    private int currentEnemyKills = 0;
//////    private bool bossSpawned = false;

//////    public void NotifyEnemyDeath(Health.EntityType type)
//////    {
//////        // 1. SIGNAL RECEIVED CHECK
//////        Debug.Log($"<color=cyan>LevelController: Notified of {type} death.</color>");

//////        if (type == Health.EntityType.Enemy && !bossSpawned)
//////        {
//////            currentEnemyKills++;

//////            // 2. PROGRESS CHECK
//////            Debug.Log($"<color=yellow>Kill Progress: {currentEnemyKills} / {enemiesToKillForBoss}</color>");

//////            if (currentEnemyKills >= enemiesToKillForBoss)
//////            {
//////                TriggerBossPhase();
//////            }
//////        }
//////    }

//////    private void TriggerBossPhase()
//////    {
//////        Debug.Log("<color=green>Requirement met! Triggering Boss Phase.</color>");
//////        bossSpawned = true;

//////        if (bossRoomDoor != null)
//////        {
//////            bossRoomDoor.ToggleDoor();
//////        }
//////        else
//////        {
//////            Debug.LogError("BOSS DOOR MISSING: Drag the BossDoor into the Inspector slot!");
//////        }

//////        if (bossPrefab != null && bossSpawnPoint != null)
//////        {
//////            Instantiate(bossPrefab, bossSpawnPoint.position, Quaternion.identity);
//////        }
//////    }
//////}

////using UnityEngine;
////using TopDown.Core;
////using TopDown.World;

////public class LevelOneController : MonoBehaviour
////{
////    [SerializeField] private int enemiesToKillForBoss = 1; // MAKE SURE THIS IS 1 IN THE INSPECTOR
////    [SerializeField] private DoorController bossRoomDoor;
////    [SerializeField] private GameObject bossPrefab;
////    [SerializeField] private Transform bossSpawnPoint;

////    private int currentEnemyKills = 0;
////    private bool bossSpawned = false;

////    public void NotifyEnemyDeath(Health.EntityType type)
////    {
////        // THIS WILL TELL US IF THE SIGNAL ARRIVED
////        Debug.Log($"<color=cyan>LevelController: Received signal for {type}. Total kills: {currentEnemyKills + 1}</color>");

////        if (type == Health.EntityType.Enemy && !bossSpawned)
////        {
////            currentEnemyKills++;
////            if (currentEnemyKills >= enemiesToKillForBoss)
////            {
////                TriggerBossPhase();
////            }
////        }
////    }

////    private void TriggerBossPhase()
////    {
////        Debug.Log("<color=green>LevelController: All kills reached! Opening Door...</color>");
////        bossSpawned = true;

////        if (bossRoomDoor != null)
////        {
////            bossRoomDoor.ToggleDoor();
////        }
////        else
////        {
////            Debug.LogError("BOSSDOOR ERROR: You haven't dragged the Door into the slot in the Inspector!");
////        }

////        if (bossPrefab != null && bossSpawnPoint != null)
////            Instantiate(bossPrefab, bossSpawnPoint.position, Quaternion.identity);
////    }
////}

//using UnityEngine;
//using TopDown.Core;
//using TopDown.World;

//public class LevelOneController : MonoBehaviour
//{
//    [SerializeField] private int enemiesToKillForBoss = 1;
//    [SerializeField] private DoorController bossRoomDoor;
//    [SerializeField] private GameObject bossPrefab;
//    [SerializeField] private Transform bossSpawnPoint;

//    private int currentEnemyKills = 0;
//    private bool bossSpawned = false;

//    public void NotifyEnemyDeath(Health.EntityType type)
//    {
//        if (type == Health.EntityType.Enemy && !bossSpawned)
//        {
//            currentEnemyKills++;
//            if (currentEnemyKills >= enemiesToKillForBoss) TriggerBossPhase();
//        }
//    }

//    private void TriggerBossPhase()
//    {
//        bossSpawned = true;
//        if (bossRoomDoor != null)
//        {
//            bossRoomDoor.ToggleDoor();
//        }

//        if (bossPrefab != null && bossSpawnPoint != null)
//        {
//            Instantiate(bossPrefab, bossSpawnPoint.position, Quaternion.identity);
//        }
//    }
//}

using UnityEngine;
using TopDown.Core;
using TopDown.World;

public class LevelOneController : MonoBehaviour
{
    [SerializeField] private int enemiesToKillForBoss = 8;
    [SerializeField] private DoorController bossRoomDoor;
    [SerializeField] private GameObject bossPrefab;
    [SerializeField] private Transform bossSpawnPoint;

    [Header("Exit Settings")]
    [SerializeField] private GameObject levelExitObject; // Drag your LevelExit here!

    private int currentEnemyKills = 0;
    private bool bossSpawned = false;

    private void Awake()
    {
        // Ensure the exit is hidden when the level starts
        if (levelExitObject != null) levelExitObject.SetActive(false);
    }

    public void NotifyEnemyDeath(Health.EntityType type)
    {
        // 1. Handle regular enemies to open the door
        if (type == Health.EntityType.Enemy && !bossSpawned)
        {
            currentEnemyKills++;
            if (currentEnemyKills >= enemiesToKillForBoss) TriggerBossPhase();
        }

        // 2. Handle the Boss death to show the exit
        if (type == Health.EntityType.Boss)
        {
            Debug.Log("<color=cyan>Boss Defeated! Level Exit is now open.</color>");
            if (levelExitObject != null) levelExitObject.SetActive(true);
        }
    }

    private void TriggerBossPhase()
    {
        bossSpawned = true;
        if (bossRoomDoor != null) bossRoomDoor.ToggleDoor();
        if (bossPrefab != null && bossSpawnPoint != null)
            Instantiate(bossPrefab, bossSpawnPoint.position, Quaternion.identity);
    }
}