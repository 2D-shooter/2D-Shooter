////using UnityEngine;
////using TMPro;
////using System.Collections.Generic;

////namespace TopDown.Systems
////{
////    public class ObjectiveManager : MonoBehaviour
////    {
////        public static ObjectiveManager Instance;

////        [Header("Objective Settings")]
////        [SerializeField] private string objectiveDescription = "Destroy all boxes";
////        [SerializeField] private GameObject exitObject; // The box with "EXIT" text

////        private List<BreakableObject> targets = new List<BreakableObject>();
////        private int totalTargets;
////        private int destroyedCount = 0;
////        private bool isLevelComplete = false;

////        private void Awake()
////        {
////            Instance = this;
////            if (exitObject != null) exitObject.SetActive(false);
////        }

////        private void Start()
////        {
////            // Find all breakable objects currently in the level
////            targets.AddRange(FindObjectsByType<BreakableObject>(FindObjectsSortMode.None));
////            totalTargets = targets.Count;
////            Debug.Log($"Level Started. Objectives: {totalTargets} boxes to destroy.");
////        }

////        public void RegisterObjectDestroyed()
////        {
////            destroyedCount++;
////            Debug.Log($"Objective Progress: {destroyedCount}/{totalTargets}");

////            if (destroyedCount >= totalTargets && !isLevelComplete)
////            {
////                CompleteObjectives();
////            }
////        }

////        private void CompleteObjectives()
////        {
////            isLevelComplete = true;
////            Debug.Log("All objectives done! Exit is now open.");

////            if (exitObject != null)
////            {
////                exitObject.SetActive(true);
////            }
////        }

////        public string GetObjectiveSummary()
////        {
////            return $"{objectiveDescription}: {destroyedCount}/{totalTargets}";
////        }
////    }
////}

//using UnityEngine;
//using System.Collections.Generic;

//namespace TopDown.Systems
//{
//    public class ObjectiveManager : MonoBehaviour
//    {
//        public static ObjectiveManager Instance;

//        [Header("Level Settings")]
//        [SerializeField] private GameObject exitObject;

//        [Header("Objectives")]
//        [SerializeField] private int boxesToDestroy = 1;
//        [SerializeField] private int enemiesToKill = 1;

//        private int currentBoxesDestroyed = 0;
//        private int currentEnemiesKilled = 0;
//        private bool isLevelComplete = false;

//        private void Awake()
//        {
//            Instance = this;
//            if (exitObject != null) exitObject.SetActive(false);
//        }

//        // Call this when a Box breaks
//        public void RegisterBoxDestroyed()
//        {
//            currentBoxesDestroyed++;
//            CheckObjectives();
//        }

//        // Call this when an Enemy dies
//        public void RegisterEnemyKilled()
//        {
//            currentEnemiesKilled++;
//            CheckObjectives();
//        }

//        private void CheckObjectives()
//        {
//            bool boxesDone = currentBoxesDestroyed >= boxesToDestroy;
//            bool enemiesDone = currentEnemiesKilled >= enemiesToKill;

//            Debug.Log($"Progress -> Boxes: {currentBoxesDestroyed}/{boxesToDestroy} | Enemies: {currentEnemiesKilled}/{enemiesToKill}");

//            if (boxesDone && enemiesDone && !isLevelComplete)
//            {
//                CompleteLevel();
//            }
//        }

//        private void CompleteLevel()
//        {
//            isLevelComplete = true;
//            Debug.Log("All Objectives Clear! Exit Spawned.");
//            if (exitObject != null) exitObject.SetActive(true);
//        }

//        public string GetObjectiveSummary()
//        {
//            return $"Boxes Destroyed: {currentBoxesDestroyed}/{boxesToDestroy}\n" +
//                   $"Enemies Eliminated: {currentEnemiesKilled}/{enemiesToKill}";
//        }
//    }
//}

using UnityEngine;
using System;

namespace TopDown.Systems
{
    public class ObjectiveManager : MonoBehaviour
    {
        public static ObjectiveManager Instance;

        // Event to notify UI when something changes
        public event Action OnObjectiveProgressChanged;

        [Header("Level Settings")]
        [SerializeField] private GameObject exitObject;

        [Header("Objectives")]
        [SerializeField] private int boxesToDestroy = 1;
        [SerializeField] private int enemiesToKill = 1;

        private int currentBoxesDestroyed = 0;
        private int currentEnemiesKilled = 0;
        private bool isLevelComplete = false;

        private void Awake()
        {
            if (Instance == null) Instance = this;
            if (exitObject != null) exitObject.SetActive(false);
        }

        public void RegisterBoxDestroyed()
        {
            currentBoxesDestroyed++;
            CheckObjectives();
        }

        public void RegisterEnemyKilled()
        {
            currentEnemiesKilled++;
            CheckObjectives();
        }

        private void CheckObjectives()
        {
            // Notify UI
            OnObjectiveProgressChanged?.Invoke();

            bool boxesDone = currentBoxesDestroyed >= boxesToDestroy;
            bool enemiesDone = currentEnemiesKilled >= enemiesToKill;

            if (boxesDone && enemiesDone && !isLevelComplete)
            {
                CompleteLevel();
            }
        }

        private void CompleteLevel()
        {
            isLevelComplete = true;
            if (exitObject != null) exitObject.SetActive(true);
            Debug.Log("Objectives Complete. Exit Spawned.");
        }

        public string GetObjectiveSummary()
        {
            // Formats the text for the Top-Left UI
            string boxStatus = currentBoxesDestroyed >= boxesToDestroy ? "<color=green>DONE</color>" : $"{currentBoxesDestroyed}/{boxesToDestroy}";
            string enemyStatus = currentEnemiesKilled >= enemiesToKill ? "<color=green>DONE</color>" : $"{currentEnemiesKilled}/{enemiesToKill}";

            return $"<b>OBJECTIVES</b>\n" +
                   $"Boxes destroyed: {boxStatus}\n" +
                   $"Enemies killed: {enemyStatus}";
        }
    }
}