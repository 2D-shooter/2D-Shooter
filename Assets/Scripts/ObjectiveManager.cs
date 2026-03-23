

using UnityEngine;
using System;
using TopDown.Core;

namespace TopDown.Systems
{
    public class ObjectiveManager : MonoBehaviour
    {
        public static ObjectiveManager Instance;
        public event Action OnObjectiveProgressChanged;

        [Header("Current Goal Totals")]
        private int boxesToDestroy = 0;
        private int enemiesToKill = 0;
        private int bossesToKill = 0;

        [Header("Current Progress")]
        private int currentBoxesDestroyed = 0;
        private int currentEnemiesKilled = 0;
        private int currentBossesKilled = 0;

        private void Awake()
        {
            if (Instance == null) Instance = this;
        }

        // Updated to accept an 'int' for bosses instead of a 'bool'
        public void SetLevelGoals(int enemies, int boxes, int bosses)
        {
            enemiesToKill = enemies;
            boxesToDestroy = boxes;
            bossesToKill = bosses;

            // Reset all progress for the new level
            currentEnemiesKilled = 0;
            currentBoxesDestroyed = 0;
            currentBossesKilled = 0;

            OnObjectiveProgressChanged?.Invoke();
        }

        public void OnEntityResourceCheck(Health.EntityType type)
        {
            if (type == Health.EntityType.Box) currentBoxesDestroyed++;
            else if (type == Health.EntityType.Enemy) currentEnemiesKilled++;
            else if (type == Health.EntityType.Boss) currentBossesKilled++;

            OnObjectiveProgressChanged?.Invoke();
        }

        public string GetObjectiveSummary()
        {
            string summary = "<b>OBJECTIVES</b>\n";

            // Format: Kill Villains 0/10
            summary += $"Kill Villains: {currentEnemiesKilled}/{enemiesToKill}\n";

            // Format: Kill Bosses 0/2
            if (bossesToKill > 0)
            {
                summary += $"Kill Bosses: {currentBossesKilled}/{bossesToKill}\n";
            }

            if (boxesToDestroy > 0)
            {
                summary += $"Destroy Boxes: {currentBoxesDestroyed}/{boxesToDestroy}\n";
            }

            return summary;
        }
    }
}