//////////////////////using UnityEngine;
//////////////////////using TMPro;
//////////////////////using System.Collections.Generic;

//////////////////////namespace TopDown.Systems
//////////////////////{
//////////////////////    public class ObjectiveManager : MonoBehaviour
//////////////////////    {
//////////////////////        public static ObjectiveManager Instance;

//////////////////////        [Header("Objective Settings")]
//////////////////////        [SerializeField] private string objectiveDescription = "Destroy all boxes";
//////////////////////        [SerializeField] private GameObject exitObject; // The box with "EXIT" text

//////////////////////        private List<BreakableObject> targets = new List<BreakableObject>();
//////////////////////        private int totalTargets;
//////////////////////        private int destroyedCount = 0;
//////////////////////        private bool isLevelComplete = false;

//////////////////////        private void Awake()
//////////////////////        {
//////////////////////            Instance = this;
//////////////////////            if (exitObject != null) exitObject.SetActive(false);
//////////////////////        }

//////////////////////        private void Start()
//////////////////////        {
//////////////////////            // Find all breakable objects currently in the level
//////////////////////            targets.AddRange(FindObjectsByType<BreakableObject>(FindObjectsSortMode.None));
//////////////////////            totalTargets = targets.Count;
//////////////////////            Debug.Log($"Level Started. Objectives: {totalTargets} boxes to destroy.");
//////////////////////        }

//////////////////////        public void RegisterObjectDestroyed()
//////////////////////        {
//////////////////////            destroyedCount++;
//////////////////////            Debug.Log($"Objective Progress: {destroyedCount}/{totalTargets}");

//////////////////////            if (destroyedCount >= totalTargets && !isLevelComplete)
//////////////////////            {
//////////////////////                CompleteObjectives();
//////////////////////            }
//////////////////////        }

//////////////////////        private void CompleteObjectives()
//////////////////////        {
//////////////////////            isLevelComplete = true;
//////////////////////            Debug.Log("All objectives done! Exit is now open.");

//////////////////////            if (exitObject != null)
//////////////////////            {
//////////////////////                exitObject.SetActive(true);
//////////////////////            }
//////////////////////        }

//////////////////////        public string GetObjectiveSummary()
//////////////////////        {
//////////////////////            return $"{objectiveDescription}: {destroyedCount}/{totalTargets}";
//////////////////////        }
//////////////////////    }
//////////////////////}

////////////////////using UnityEngine;
////////////////////using System.Collections.Generic;

////////////////////namespace TopDown.Systems
////////////////////{
////////////////////    public class ObjectiveManager : MonoBehaviour
////////////////////    {
////////////////////        public static ObjectiveManager Instance;

////////////////////        [Header("Level Settings")]
////////////////////        [SerializeField] private GameObject exitObject;

////////////////////        [Header("Objectives")]
////////////////////        [SerializeField] private int boxesToDestroy = 1;
////////////////////        [SerializeField] private int enemiesToKill = 1;

////////////////////        private int currentBoxesDestroyed = 0;
////////////////////        private int currentEnemiesKilled = 0;
////////////////////        private bool isLevelComplete = false;

////////////////////        private void Awake()
////////////////////        {
////////////////////            Instance = this;
////////////////////            if (exitObject != null) exitObject.SetActive(false);
////////////////////        }

////////////////////        // Call this when a Box breaks
////////////////////        public void RegisterBoxDestroyed()
////////////////////        {
////////////////////            currentBoxesDestroyed++;
////////////////////            CheckObjectives();
////////////////////        }

////////////////////        // Call this when an Enemy dies
////////////////////        public void RegisterEnemyKilled()
////////////////////        {
////////////////////            currentEnemiesKilled++;
////////////////////            CheckObjectives();
////////////////////        }

////////////////////        private void CheckObjectives()
////////////////////        {
////////////////////            bool boxesDone = currentBoxesDestroyed >= boxesToDestroy;
////////////////////            bool enemiesDone = currentEnemiesKilled >= enemiesToKill;

////////////////////            Debug.Log($"Progress -> Boxes: {currentBoxesDestroyed}/{boxesToDestroy} | Enemies: {currentEnemiesKilled}/{enemiesToKill}");

////////////////////            if (boxesDone && enemiesDone && !isLevelComplete)
////////////////////            {
////////////////////                CompleteLevel();
////////////////////            }
////////////////////        }

////////////////////        private void CompleteLevel()
////////////////////        {
////////////////////            isLevelComplete = true;
////////////////////            Debug.Log("All Objectives Clear! Exit Spawned.");
////////////////////            if (exitObject != null) exitObject.SetActive(true);
////////////////////        }

////////////////////        public string GetObjectiveSummary()
////////////////////        {
////////////////////            return $"Boxes Destroyed: {currentBoxesDestroyed}/{boxesToDestroy}\n" +
////////////////////                   $"Enemies Eliminated: {currentEnemiesKilled}/{enemiesToKill}";
////////////////////        }
////////////////////    }
////////////////////}

//////////////////using UnityEngine;
//////////////////using System;

//////////////////namespace TopDown.Systems
//////////////////{
//////////////////    public class ObjectiveManager : MonoBehaviour
//////////////////    {
//////////////////        public static ObjectiveManager Instance;

//////////////////        // Event to notify UI when something changes
//////////////////        public event Action OnObjectiveProgressChanged;

//////////////////        [Header("Level Settings")]
//////////////////        [SerializeField] private GameObject exitObject;

//////////////////        [Header("Objectives")]
//////////////////        [SerializeField] private int boxesToDestroy = 1;
//////////////////        [SerializeField] private int enemiesToKill = 1;

//////////////////        private int currentBoxesDestroyed = 0;
//////////////////        private int currentEnemiesKilled = 0;
//////////////////        private bool isLevelComplete = false;

//////////////////        private void Awake()
//////////////////        {
//////////////////            if (Instance == null) Instance = this;
//////////////////            if (exitObject != null) exitObject.SetActive(false);
//////////////////        }

//////////////////        public void RegisterBoxDestroyed()
//////////////////        {
//////////////////            currentBoxesDestroyed++;
//////////////////            CheckObjectives();
//////////////////        }

//////////////////        public void RegisterEnemyKilled()
//////////////////        {
//////////////////            currentEnemiesKilled++;
//////////////////            CheckObjectives();
//////////////////        }

//////////////////        private void CheckObjectives()
//////////////////        {
//////////////////            // Notify UI
//////////////////            OnObjectiveProgressChanged?.Invoke();

//////////////////            bool boxesDone = currentBoxesDestroyed >= boxesToDestroy;
//////////////////            bool enemiesDone = currentEnemiesKilled >= enemiesToKill;

//////////////////            if (boxesDone && enemiesDone && !isLevelComplete)
//////////////////            {
//////////////////                CompleteLevel();
//////////////////            }
//////////////////        }

//////////////////        private void CompleteLevel()
//////////////////        {
//////////////////            isLevelComplete = true;
//////////////////            if (exitObject != null) exitObject.SetActive(true);
//////////////////            Debug.Log("Objectives Complete. Exit Spawned.");
//////////////////        }

//////////////////        public string GetObjectiveSummary()
//////////////////        {
//////////////////            // Formats the text for the Top-Left UI
//////////////////            string boxStatus = currentBoxesDestroyed >= boxesToDestroy ? "<color=green>DONE</color>" : $"{currentBoxesDestroyed}/{boxesToDestroy}";
//////////////////            string enemyStatus = currentEnemiesKilled >= enemiesToKill ? "<color=green>DONE</color>" : $"{currentEnemiesKilled}/{enemiesToKill}";

//////////////////            return $"<b>OBJECTIVES</b>\n" +
//////////////////                   $"Boxes destroyed: {boxStatus}\n" +
//////////////////                   $"Enemies killed: {enemyStatus}";
//////////////////        }
//////////////////    }
//////////////////}

////////////////using UnityEngine;
////////////////using System;
////////////////using TopDown.Core; // Added to access Health.EntityType

////////////////namespace TopDown.Systems
////////////////{
////////////////    public class ObjectiveManager : MonoBehaviour
////////////////    {
////////////////        public static ObjectiveManager Instance;
////////////////        public event Action OnObjectiveProgressChanged;

////////////////        [Header("Level Settings")]
////////////////        [SerializeField] private GameObject exitObject;

////////////////        [Header("Objectives")]
////////////////        [SerializeField] private int boxesToDestroy = 0;
////////////////        [SerializeField] private int enemiesToKill = 8;
////////////////        [SerializeField] private bool bossMustDie = true;

////////////////        private int currentBoxesDestroyed = 0;
////////////////        private int currentEnemiesKilled = 0;
////////////////        private bool isBossDead = false;
////////////////        private bool isLevelComplete = false;

////////////////        private void Awake()
////////////////        {
////////////////            if (Instance == null) Instance = this;
////////////////            if (exitObject != null) exitObject.SetActive(false);
////////////////        }

////////////////        // This is the missing function that Health.cs is looking for!
////////////////        public void OnEntityResourceCheck(Health.EntityType type)
////////////////        {
////////////////            if (type == Health.EntityType.Box)
////////////////            {
////////////////                currentBoxesDestroyed++;
////////////////            }
////////////////            else if (type == Health.EntityType.Enemy)
////////////////            {
////////////////                currentEnemiesKilled++;
////////////////            }
////////////////            else if (type == Health.EntityType.Boss)
////////////////            {
////////////////                isBossDead = true;
////////////////            }

////////////////            CheckObjectives();
////////////////        }

////////////////        private void CheckObjectives()
////////////////        {
////////////////            OnObjectiveProgressChanged?.Invoke();

////////////////            bool boxesDone = currentBoxesDestroyed >= boxesToDestroy;
////////////////            bool enemiesDone = currentEnemiesKilled >= enemiesToKill;
////////////////            bool bossDone = !bossMustDie || isBossDead;

////////////////            if (boxesDone && enemiesDone && bossDone && !isLevelComplete)
////////////////            {
////////////////                CompleteLevel();
////////////////            }
////////////////        }

////////////////        private void CompleteLevel()
////////////////        {
////////////////            isLevelComplete = true;
////////////////            if (exitObject != null) exitObject.SetActive(true);
////////////////            Debug.Log("Objectives Complete. Exit Spawned.");
////////////////        }

////////////////        public string GetObjectiveSummary()
////////////////        {
////////////////            string boxStatus = currentBoxesDestroyed >= boxesToDestroy ? "<color=green>DONE</color>" : $"{currentBoxesDestroyed}/{boxesToDestroy}";
////////////////            string enemyStatus = currentEnemiesKilled >= enemiesToKill ? "<color=green>DONE</color>" : $"{currentEnemiesKilled}/{enemiesToKill}";
////////////////            string bossStatus = isBossDead ? "<color=green>DEAD</color>" : "ALIVE";

////////////////            string text = $"<b>OBJECTIVES</b>\n";
////////////////            if (boxesToDestroy > 0) text += $"Boxes: {boxStatus}\n";
////////////////            text += $"Enemies: {enemyStatus}\n";
////////////////            if (bossMustDie) text += $"Boss: {bossStatus}";

////////////////            return text;
////////////////        }
////////////////    }
////////////////}

//////////////using UnityEngine;
//////////////using System;
//////////////using TopDown.Core;

//////////////namespace TopDown.Systems
//////////////{
//////////////    public class ObjectiveManager : MonoBehaviour
//////////////    {
//////////////        public static ObjectiveManager Instance;
//////////////        public event Action OnObjectiveProgressChanged;

//////////////        [Header("Level Settings")]
//////////////        [SerializeField] private GameObject exitObject;

//////////////        [Header("Objectives")]
//////////////        [SerializeField] private int boxesToDestroy = 0;
//////////////        [SerializeField] private int enemiesToKill = 8;
//////////////        [SerializeField] private bool bossMustDie = true;

//////////////        private int currentBoxesDestroyed = 0;
//////////////        private int currentEnemiesKilled = 0;
//////////////        private bool isBossDead = false;
//////////////        private bool isLevelComplete = false;

//////////////        private void Awake()
//////////////        {
//////////////            if (Instance == null) Instance = this;
//////////////            if (exitObject != null) exitObject.SetActive(false);
//////////////        }

//////////////        public void OnEntityResourceCheck(Health.EntityType type)
//////////////        {
//////////////            if (type == Health.EntityType.Box) currentBoxesDestroyed++;
//////////////            else if (type == Health.EntityType.Enemy) currentEnemiesKilled++;
//////////////            else if (type == Health.EntityType.Boss) isBossDead = true;

//////////////            CheckObjectives();
//////////////        }

//////////////        private void CheckObjectives()
//////////////        {
//////////////            OnObjectiveProgressChanged?.Invoke();

//////////////            bool boxesDone = currentBoxesDestroyed >= boxesToDestroy;
//////////////            bool enemiesDone = currentEnemiesKilled >= enemiesToKill;
//////////////            bool bossDone = !bossMustDie || isBossDead;

//////////////            if (boxesDone && enemiesDone && bossDone && !isLevelComplete)
//////////////            {
//////////////                CompleteLevel();
//////////////            }
//////////////        }

//////////////        private void CompleteLevel()
//////////////        {
//////////////            isLevelComplete = true;
//////////////            if (exitObject != null) exitObject.SetActive(true);
//////////////            Debug.Log("Level Complete! Exit is open.");
//////////////        }

//////////////        public string GetObjectiveSummary()
//////////////        {
//////////////            string boxStatus = currentBoxesDestroyed >= boxesToDestroy ? "<color=green>DONE</color>" : $"{currentBoxesDestroyed}/{boxesToDestroy}";
//////////////            string enemyStatus = currentEnemiesKilled >= enemiesToKill ? "<color=green>DONE</color>" : $"{currentEnemiesKilled}/{enemiesToKill}";
//////////////            string bossStatus = isBossDead ? "<color=green>DEAD</color>" : "ALIVE";

//////////////            string summary = "<b>OBJECTIVES</b>\n";
//////////////            if (boxesToDestroy > 0) summary += $"Boxes: {boxStatus}\n";
//////////////            summary += $"Enemies: {enemyStatus}\n";
//////////////            if (bossMustDie) summary += $"Boss: {bossStatus}";

//////////////            return summary;
//////////////        }
//////////////    }
//////////////}

////////////using UnityEngine;
////////////using System;
////////////using TopDown.Core;

////////////namespace TopDown.Systems
////////////{
////////////    public class ObjectiveManager : MonoBehaviour
////////////    {
////////////        public static ObjectiveManager Instance;
////////////        public event Action OnObjectiveProgressChanged;

////////////        [Header("Level Settings")]
////////////        [SerializeField] private GameObject exitObject;

////////////        [Header("Objectives")]
////////////        [SerializeField] private int boxesToDestroy = 0;
////////////        [SerializeField] private int enemiesToKill = 8;
////////////        [SerializeField] private bool bossMustDie = true;

////////////        private int currentBoxesDestroyed = 0;
////////////        private int currentEnemiesKilled = 0;
////////////        private bool isBossDead = false;
////////////        private bool isLevelComplete = false;

////////////        private void Awake()
////////////        {
////////////            if (Instance == null) Instance = this;
////////////            if (exitObject != null) exitObject.SetActive(false);
////////////        }

////////////        public void OnEntityResourceCheck(Health.EntityType type)
////////////        {
////////////            if (type == Health.EntityType.Box) currentBoxesDestroyed++;
////////////            else if (type == Health.EntityType.Enemy) currentEnemiesKilled++;
////////////            else if (type == Health.EntityType.Boss) isBossDead = true;

////////////            CheckObjectives();
////////////        }

////////////        private void CheckObjectives()
////////////        {
////////////            OnObjectiveProgressChanged?.Invoke();

////////////            bool boxesDone = boxesToDestroy <= 0 || currentBoxesDestroyed >= boxesToDestroy;
////////////            bool enemiesDone = enemiesToKill <= 0 || currentEnemiesKilled >= enemiesToKill;
////////////            bool bossDone = !bossMustDie || isBossDead;

////////////            if (boxesDone && enemiesDone && bossDone && !isLevelComplete)
////////////            {
////////////                CompleteLevel();
////////////            }
////////////        }

////////////        private void CompleteLevel()
////////////        {
////////////            isLevelComplete = true;
////////////            if (exitObject != null) exitObject.SetActive(true);
////////////            Debug.Log("Level Complete!");
////////////        }

////////////        public string GetObjectiveSummary()
////////////        {
////////////            string summary = "<b>OBJECTIVES</b>\n";

////////////            // Only show Boxes if we actually have them as an objective
////////////            if (boxesToDestroy > 0)
////////////            {
////////////                string boxStatus = currentBoxesDestroyed >= boxesToDestroy ? "<color=green>DONE</color>" : $"{currentBoxesDestroyed}/{boxesToDestroy}";
////////////                summary += $"Boxes: {boxStatus}\n";
////////////            }

////////////            // Show Enemy count
////////////            if (enemiesToKill > 0)
////////////            {
////////////                string enemyStatus = currentEnemiesKilled >= enemiesToKill ? "<color=green>DONE</color>" : $"{currentEnemiesKilled}/{enemiesToKill}";
////////////                summary += $"Villains: {enemyStatus}\n";
////////////            }

////////////            // Show Boss status
////////////            if (bossMustDie)
////////////            {
////////////                string bossStatus = isBossDead ? "<color=green>ELIMINATED</color>" : "<color=red>ALIVE</color>";
////////////                summary += $"Boss: {bossStatus}";
////////////            }

////////////            return summary;
////////////        }
////////////    }
////////////}

//////////using UnityEngine;
//////////using System;
//////////using TopDown.Core;

//////////namespace TopDown.Systems
//////////{
//////////    public class ObjectiveManager : MonoBehaviour
//////////    {
//////////        public static ObjectiveManager Instance;
//////////        public event Action OnObjectiveProgressChanged;

//////////        [Header("Level Settings")]
//////////        [SerializeField] private GameObject exitObject;

//////////        [Header("Objectives")]
//////////        [SerializeField] private int boxesToDestroy = 0;
//////////        [SerializeField] private int enemiesToKill = 8;
//////////        [SerializeField] private bool bossMustDie = true;

//////////        private int currentBoxesDestroyed = 0;
//////////        private int currentEnemiesKilled = 0;
//////////        private bool isBossDead = false;
//////////        private bool isLevelComplete = false;

//////////        private void Awake()
//////////        {
//////////            if (Instance == null) Instance = this;
//////////            if (exitObject != null) exitObject.SetActive(false);
//////////        }

//////////        public void OnEntityResourceCheck(Health.EntityType type)
//////////        {
//////////            if (type == Health.EntityType.Box) currentBoxesDestroyed++;
//////////            else if (type == Health.EntityType.Enemy) currentEnemiesKilled++;
//////////            else if (type == Health.EntityType.Boss) isBossDead = true;

//////////            CheckObjectives();
//////////        }

//////////        private void CheckObjectives()
//////////        {
//////////            OnObjectiveProgressChanged?.Invoke();

//////////            bool boxesDone = boxesToDestroy <= 0 || currentBoxesDestroyed >= boxesToDestroy;
//////////            bool enemiesDone = enemiesToKill <= 0 || currentEnemiesKilled >= enemiesToKill;
//////////            bool bossDone = !bossMustDie || isBossDead;

//////////            if (boxesDone && enemiesDone && bossDone && !isLevelComplete)
//////////            {
//////////                CompleteLevel();
//////////            }
//////////        }

//////////        private void CompleteLevel()
//////////        {
//////////            isLevelComplete = true;
//////////            if (exitObject != null) exitObject.SetActive(true);
//////////            Debug.Log("LEVEL COMPLETE: Exit Spawned!");
//////////        }

//////////        public string GetObjectiveSummary()
//////////        {
//////////            string summary = "<b>OBJECTIVES</b>\n";

//////////            if (boxesToDestroy > 0)
//////////            {
//////////                string boxStatus = currentBoxesDestroyed >= boxesToDestroy ? "<color=green>DONE</color>" : $"{currentBoxesDestroyed}/{boxesToDestroy}";
//////////                summary += $"Boxes: {boxStatus}\n";
//////////            }

//////////            if (enemiesToKill > 0)
//////////            {
//////////                string enemyStatus = currentEnemiesKilled >= enemiesToKill ? "<color=green>DONE</color>" : $"{currentEnemiesKilled}/{enemiesToKill}";
//////////                summary += $"Villains: {enemyStatus}\n";
//////////            }

//////////            if (bossMustDie)
//////////            {
//////////                string bossStatus = isBossDead ? "<color=green>DEAD</color>" : "<color=red>ALIVE</color>";
//////////                summary += $"Boss: {bossStatus}";
//////////            }

//////////            return summary;
//////////        }
//////////    }
//////////}

////////using UnityEngine;
////////using System;
////////using TopDown.Core;

////////namespace TopDown.Systems
////////{
////////    public class ObjectiveManager : MonoBehaviour
////////    {
////////        public static ObjectiveManager Instance;
////////        public event Action OnObjectiveProgressChanged;

////////        [SerializeField] private GameObject exitObject;
////////        [SerializeField] private int boxesToDestroy = 0;
////////        [SerializeField] private int enemiesToKill = 8;
////////        [SerializeField] private bool bossMustDie = true;

////////        private int currentBoxesDestroyed = 0;
////////        private int currentEnemiesKilled = 0;
////////        private bool isBossDead = false;

////////        private void Awake()
////////        {
////////            if (Instance == null) Instance = this;
////////            if (exitObject != null) exitObject.SetActive(false);
////////        }

////////        public void OnEntityResourceCheck(Health.EntityType type)
////////        {
////////            if (type == Health.EntityType.Box) currentBoxesDestroyed++;
////////            else if (type == Health.EntityType.Enemy) currentEnemiesKilled++;
////////            else if (type == Health.EntityType.Boss) isBossDead = true;
////////            CheckObjectives();
////////        }

////////        private void CheckObjectives()
////////        {
////////            OnObjectiveProgressChanged?.Invoke();
////////            if (currentBoxesDestroyed >= boxesToDestroy && currentEnemiesKilled >= enemiesToKill && (!bossMustDie || isBossDead))
////////            {
////////                if (exitObject != null) exitObject.SetActive(true);
////////            }
////////        }

////////        public string GetObjectiveSummary()
////////        {
////////            string summary = "<b>OBJECTIVES</b>\n";
////////            if (boxesToDestroy > 0) summary += $"Boxes: {currentBoxesDestroyed}/{boxesToDestroy}\n";
////////            summary += $"Villains: {currentEnemiesKilled}/{enemiesToKill}\n";
////////            if (bossMustDie) summary += $"Boss: {(isBossDead ? "DONE" : "ALIVE")}";
////////            return summary;
////////        }
////////    }
////////}

//////using UnityEngine;
//////using System;
//////using TopDown.Core;

//////namespace TopDown.Systems
//////{
//////    public class ObjectiveManager : MonoBehaviour
//////    {
//////        public static ObjectiveManager Instance;
//////        public event Action OnObjectiveProgressChanged;

//////        [Header("Level Settings")]
//////        [SerializeField] private GameObject exitObject;

//////        [Header("Objectives")]
//////        [SerializeField] private int boxesToDestroy = 0;
//////        [SerializeField] private int enemiesToKill = 8;
//////        [SerializeField] private bool bossMustDie = true;

//////        private int currentBoxesDestroyed = 0;
//////        private int currentEnemiesKilled = 0;
//////        private bool isBossDead = false;

//////        private void Awake()
//////        {
//////            if (Instance == null) Instance = this;
//////            if (exitObject != null) exitObject.SetActive(false);
//////        }

//////        public void OnEntityResourceCheck(Health.EntityType type)
//////        {
//////            if (type == Health.EntityType.Box) currentBoxesDestroyed++;
//////            else if (type == Health.EntityType.Enemy) currentEnemiesKilled++;
//////            else if (type == Health.EntityType.Boss) isBossDead = true;

//////            CheckObjectives();
//////        }

//////        private void CheckObjectives()
//////        {
//////            OnObjectiveProgressChanged?.Invoke();

//////            bool boxesDone = boxesToDestroy <= 0 || currentBoxesDestroyed >= boxesToDestroy;
//////            bool enemiesDone = enemiesToKill <= 0 || currentEnemiesKilled >= enemiesToKill;
//////            bool bossDone = !bossMustDie || isBossDead;

//////            if (boxesDone && enemiesDone && bossDone)
//////            {
//////                if (exitObject != null) exitObject.SetActive(true);
//////            }
//////        }

//////        public string GetObjectiveSummary()
//////        {
//////            string summary = "<b>OBJECTIVES</b>\n";
//////            if (boxesToDestroy > 0) summary += $"Boxes: {currentBoxesDestroyed}/{boxesToDestroy}\n";
//////            summary += $"Villains: {currentEnemiesKilled}/{enemiesToKill}\n";
//////            if (bossMustDie) summary += $"Boss: {(isBossDead ? "ELIMINATED" : "ALIVE")}";
//////            return summary;
//////        }
//////    }
//////}

////using UnityEngine;
////using System;
////using TopDown.Core;

////namespace TopDown.Systems
////{
////    public class ObjectiveManager : MonoBehaviour
////    {
////        public static ObjectiveManager Instance;
////        public event Action OnObjectiveProgressChanged;

////        [SerializeField] private GameObject exitObject;
////        [SerializeField] private int boxesToDestroy = 0;
////        [SerializeField] private int enemiesToKill = 8;
////        [SerializeField] private bool bossMustDie = true;

////        private int currentBoxesDestroyed = 0;
////        private int currentEnemiesKilled = 0;
////        private bool isBossDead = false;

////        private void Awake()
////        {
////            if (Instance == null) Instance = this;
////            if (exitObject != null) exitObject.SetActive(false);
////        }

////        public void OnEntityResourceCheck(Health.EntityType type)
////        {
////            if (type == Health.EntityType.Box) currentBoxesDestroyed++;
////            else if (type == Health.EntityType.Enemy) currentEnemiesKilled++;
////            else if (type == Health.EntityType.Boss) isBossDead = true;
////            CheckObjectives();
////        }

////        private void CheckObjectives()
////        {
////            OnObjectiveProgressChanged?.Invoke();
////            bool boxesDone = boxesToDestroy <= 0 || currentBoxesDestroyed >= boxesToDestroy;
////            bool enemiesDone = enemiesToKill <= 0 || currentEnemiesKilled >= enemiesToKill;
////            bool bossDone = !bossMustDie || isBossDead;

////            if (boxesDone && enemiesDone && bossDone)
////            {
////                if (exitObject != null) exitObject.SetActive(true);
////            }
////        }

////        public string GetObjectiveSummary()
////        {
////            string summary = "<b>OBJECTIVES</b>\n";
////            if (boxesToDestroy > 0) summary += $"Boxes: {currentBoxesDestroyed}/{boxesToDestroy}\n";
////            summary += $"Villains: {currentEnemiesKilled}/{enemiesToKill}\n";
////            if (bossMustDie) summary += $"Boss: {(isBossDead ? "ELIMINATED" : "ALIVE")}";
////            return summary;
////        }
////    }
////}

//using UnityEngine;
//using System;
//using TopDown.Core;

//namespace TopDown.Systems
//{
//    public class ObjectiveManager : MonoBehaviour
//    {
//        public static ObjectiveManager Instance;
//        public event Action OnObjectiveProgressChanged;

//        [Header("Level Settings")]
//        [SerializeField] private GameObject exitObject;

//        [Header("Objectives")]
//        [SerializeField] private int boxesToDestroy = 0;
//        [SerializeField] private int enemiesToKill = 8;
//        [SerializeField] private bool bossMustDie = true;

//        private int currentBoxesDestroyed = 0;
//        private int currentEnemiesKilled = 0;
//        private bool isBossDead = false;

//        private void Awake()
//        {
//            if (Instance == null) Instance = this;
//            if (exitObject != null) exitObject.SetActive(false);
//        }

//        public void OnEntityResourceCheck(Health.EntityType type)
//        {
//            if (type == Health.EntityType.Box) currentBoxesDestroyed++;
//            else if (type == Health.EntityType.Enemy) currentEnemiesKilled++;
//            else if (type == Health.EntityType.Boss) isBossDead = true;
//            CheckObjectives();
//        }

//        private void CheckObjectives()
//        {
//            OnObjectiveProgressChanged?.Invoke();
//            bool boxesDone = boxesToDestroy <= 0 || currentBoxesDestroyed >= boxesToDestroy;
//            bool enemiesDone = enemiesToKill <= 0 || currentEnemiesKilled >= enemiesToKill;
//            bool bossDone = !bossMustDie || isBossDead;

//            if (boxesDone && enemiesDone && bossDone)
//            {
//                if (exitObject != null) exitObject.SetActive(true);
//            }
//        }

//        public string GetObjectiveSummary()
//        {
//            string summary = "<b>OBJECTIVES</b>\n";
//            if (boxesToDestroy > 0) summary += $"Boxes: {currentBoxesDestroyed}/{boxesToDestroy}\n";
//            summary += $"Villains: {currentEnemiesKilled}/{enemiesToKill}\n";
//            if (bossMustDie) summary += $"Boss: {(isBossDead ? "ELIMINATED" : "ALIVE")}";
//            return summary;
//        }
//    }
//}

using UnityEngine;
using System;
using TopDown.Core;

namespace TopDown.Systems
{
    public class ObjectiveManager : MonoBehaviour
    {
        public static ObjectiveManager Instance;
        public event Action OnObjectiveProgressChanged;

        [SerializeField] private GameObject exitObject;
        [SerializeField] private int boxesToDestroy = 0;
        [SerializeField] private int enemiesToKill = 8;
        [SerializeField] private bool bossMustDie = true;

        private int currentBoxesDestroyed = 0;
        private int currentEnemiesKilled = 0;
        private bool isBossDead = false;

        private void Awake()
        {
            if (Instance == null) Instance = this;
            if (exitObject != null) exitObject.SetActive(false);
        }

        public void OnEntityResourceCheck(Health.EntityType type)
        {
            if (type == Health.EntityType.Box) currentBoxesDestroyed++;
            else if (type == Health.EntityType.Enemy) currentEnemiesKilled++;
            else if (type == Health.EntityType.Boss) isBossDead = true;
            CheckObjectives();
        }

        private void CheckObjectives()
        {
            OnObjectiveProgressChanged?.Invoke();
            bool boxesDone = boxesToDestroy <= 0 || currentBoxesDestroyed >= boxesToDestroy;
            bool enemiesDone = enemiesToKill <= 0 || currentEnemiesKilled >= enemiesToKill;
            bool bossDone = !bossMustDie || isBossDead;

            if (boxesDone && enemiesDone && bossDone)
            {
                if (exitObject != null) exitObject.SetActive(true);
            }
        }

        public string GetObjectiveSummary()
        {
            string summary = "<b>OBJECTIVES</b>\n";
            if (boxesToDestroy > 0) summary += $"Boxes: {currentBoxesDestroyed}/{boxesToDestroy}\n";
            summary += $"Villains: {currentEnemiesKilled}/{enemiesToKill}\n";
            if (bossMustDie) summary += $"Boss: {(isBossDead ? "DONE" : "ALIVE")}";
            return summary;
        }
    }
}