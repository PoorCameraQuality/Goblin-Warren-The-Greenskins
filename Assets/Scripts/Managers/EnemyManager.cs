using UnityEngine;
using System.Collections.Generic;

namespace GoblinWarrens.Managers
{
    /// <summary>
    /// Manages enemy spawning, outposts, and raid triggers.
    /// Monitors player progress to determine when to spawn enemies.
    /// </summary>
    public class EnemyManager : MonoBehaviour
    {
        [Header("Raid Triggers")]
        [SerializeField] private int _populationThreshold = 10;
        [SerializeField] private int _resourceThreshold = 500;
        [SerializeField] private float _raidCheckInterval = 5f;
        
        [Header("Enemy Spawning")]
        [SerializeField] private bool _enableEnemySpawning = true;
        [SerializeField] private float _spawnInterval = 30f;
        
        [Header("Debug")]
        [SerializeField] private bool _showDebugInfo = true;
        
        private bool _isInitialized = false;
        private float _lastRaidCheck = 0f;
        private float _lastSpawnCheck = 0f;
        private int _enemyCount = 0;
        
        private void Awake()
        {
            Debug.Log("[GW] EnemyManager Awake() completed");
        }
        
        /// <summary>
        /// Initializes the enemy management system
        /// </summary>
        public void Initialize()
        {
            if (_isInitialized) return;
            
            _isInitialized = true;
            _enemyCount = 0;
            
            Debug.Log("[GW] EnemyManager initialized successfully");
            Debug.Log($"[GW] Raid triggers: Population > {_populationThreshold}, Resources > {_resourceThreshold}");
        }
        
        private void Update()
        {
            if (!_isInitialized) return;
            
            CheckRaidTriggers();
            CheckEnemySpawning();
        }
        
        /// <summary>
        /// Checks if raid conditions are met
        /// </summary>
        private void CheckRaidTriggers()
        {
            if (Time.time - _lastRaidCheck < _raidCheckInterval) return;
            
            _lastRaidCheck = Time.time;
            
            // TODO: Uncomment when ResourceManager is implemented
            // if (GameManager.Instance?.ResourceManager == null) return;
            
            // var resourceManager = GameManager.Instance.ResourceManager;
            // int totalResources = resourceManager.Wood + resourceManager.Stone + resourceManager.Food;
            
            // Placeholder: assume no resources for now
            int totalResources = 0;
            
            // Check population threshold (placeholder until we have actual population tracking)
            bool populationTriggered = false; // TODO: Implement population tracking
            
            // Check resource threshold
            bool resourceTriggered = totalResources > _resourceThreshold;
            
            if (resourceTriggered)
            {
                Debug.Log($"[GW] Resource threshold exceeded! Total: {totalResources} > {_resourceThreshold}");
                TriggerRaid("Resource Hoarding");
            }
            
            if (populationTriggered)
            {
                Debug.Log("[GW] Population threshold exceeded! Triggering raid...");
                TriggerRaid("Population Growth");
            }
        }
        
        /// <summary>
        /// Checks if new enemies should be spawned
        /// </summary>
        private void CheckEnemySpawning()
        {
            if (!_enableEnemySpawning) return;
            if (Time.time - _lastSpawnCheck < _spawnInterval) return;
            
            _lastSpawnCheck = Time.time;
            
            // Simple spawning logic - just increment counter for now
            _enemyCount++;
            Debug.Log($"[GW] Enemy spawned! Total enemies: {_enemyCount}");
            
            if (_showDebugInfo)
            {
                Debug.Log($"[GW] Enemy spawning enabled. Next spawn in {_spawnInterval} seconds");
            }
        }
        
        /// <summary>
        /// Triggers a raid event
        /// </summary>
        /// <param name="reason">Reason for the raid</param>
        private void TriggerRaid(string reason)
        {
            Debug.LogWarning($"[GW] RAID TRIGGERED! Reason: {reason}");
            
            // TODO: Implement actual raid spawning
            // For now, just log the event
            
            if (_showDebugInfo)
            {
                Debug.Log($"[GW] Raid would spawn enemies here. Current enemy count: {_enemyCount}");
            }
        }
        
        /// <summary>
        /// Gets the current enemy count
        /// </summary>
        public int EnemyCount => _enemyCount;
        
        /// <summary>
        /// Gets whether enemy spawning is enabled
        /// </summary>
        public bool IsEnemySpawningEnabled => _enableEnemySpawning;
        
        /// <summary>
        /// Gets the current raid threshold values
        /// </summary>
        public int PopulationThreshold => _populationThreshold;
        public int ResourceThreshold => _resourceThreshold;
        
        /// <summary>
        /// Resets the enemy manager to default state
        /// </summary>
        public void ResetToDefaults()
        {
            _enemyCount = 0;
            _lastRaidCheck = 0f;
            _lastSpawnCheck = 0f;
            
            Debug.Log("[GW] EnemyManager reset to defaults");
        }
        
        /// <summary>
        /// Enables or disables enemy spawning
        /// </summary>
        /// <param name="enabled">Whether to enable enemy spawning</param>
        public void SetEnemySpawning(bool enabled)
        {
            _enableEnemySpawning = enabled;
            Debug.Log($"[GW] Enemy spawning {(enabled ? "enabled" : "disabled")}");
        }
        
        /// <summary>
        /// Sets new raid trigger thresholds
        /// </summary>
        /// <param name="populationThreshold">New population threshold</param>
        /// <param name="resourceThreshold">New resource threshold</param>
        public void SetRaidThresholds(int populationThreshold, int resourceThreshold)
        {
            _populationThreshold = Mathf.Max(1, populationThreshold);
            _resourceThreshold = Mathf.Max(1, resourceThreshold);
            
            Debug.Log($"[GW] Raid thresholds updated: Population > {_populationThreshold}, Resources > {_resourceThreshold}");
        }
    }
}


