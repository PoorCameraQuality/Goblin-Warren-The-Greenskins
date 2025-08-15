using UnityEngine;
using System.Collections.Generic;
using System;

namespace GoblinWarrens.Managers
{
    /// <summary>
    /// Centralized resource management system that tracks all player resources.
    /// Provides APIs for adding/removing resources and notifies listeners of changes.
    /// </summary>
    public class ResourceManager : MonoBehaviour
    {
        [Header("Initial Resource Values")]
        [SerializeField] private int _initialWood = 100;
        [SerializeField] private int _initialStone = 50;
        [SerializeField] private int _initialFood = 200;
        [SerializeField] private int _initialMagic = 25;
        
        [Header("Resource Limits")]
        [SerializeField] private int _maxResourceAmount = 9999;
        
        // Resource storage
        private Dictionary<ResourceType, int> _resourceTotals;
        
        // Events
        public event Action<ResourceType, int> OnResourceChanged;
        public event Action<ResourceType, int, int> OnResourceAdded; // type, amount, new total
        public event Action<ResourceType, int, int> OnResourceRemoved; // type, amount, new total
        
        /// <summary>
        /// Resource types available in the game
        /// </summary>
        public enum ResourceType
        {
            Wood,
            Stone,
            Food,
            Magic
        }
        
        // Public accessors for current resource amounts
        public int Wood => GetResourceAmount(ResourceType.Wood);
        public int Stone => GetResourceAmount(ResourceType.Stone);
        public int Food => GetResourceAmount(ResourceType.Food);
        public int Magic => GetResourceAmount(ResourceType.Magic);
        
        private void Awake()
        {
            InitializeResources();
            Debug.Log("[GW] ResourceManager Awake() completed");
        }
        
        /// <summary>
        /// Initializes the resource system with starting values
        /// </summary>
        public void Initialize()
        {
            if (_resourceTotals == null)
            {
                InitializeResources();
            }
            
            Debug.Log("[GW] ResourceManager initialized with starting resources");
            Debug.Log($"[GW] Wood: {_initialWood}, Stone: {_initialStone}, Food: {_initialFood}, Magic: {_initialMagic}");
        }
        
        /// <summary>
        /// Sets up the initial resource dictionary with starting values
        /// </summary>
        private void InitializeResources()
        {
            _resourceTotals = new Dictionary<ResourceType, int>
            {
                { ResourceType.Wood, _initialWood },
                { ResourceType.Stone, _initialStone },
                { ResourceType.Food, _initialFood },
                { ResourceType.Magic, _initialMagic }
            };
        }
        
        /// <summary>
        /// Adds resources of the specified type
        /// </summary>
        /// <param name="type">The type of resource to add</param>
        /// <param name="amount">The amount to add (must be positive)</param>
        /// <returns>True if resources were added successfully, false otherwise</returns>
        public bool AddResource(ResourceType type, int amount)
        {
            if (amount <= 0)
            {
                Debug.LogWarning($"[GW] Attempted to add non-positive amount ({amount}) of {type}");
                return false;
            }
            
            if (!_resourceTotals.ContainsKey(type))
            {
                Debug.LogError($"[GW] Resource type {type} not found in resource totals");
                return false;
            }
            
            int oldAmount = _resourceTotals[type];
            int newAmount = Mathf.Min(oldAmount + amount, _maxResourceAmount);
            int actualAdded = newAmount - oldAmount;
            
            _resourceTotals[type] = newAmount;
            
            Debug.Log($"[GW] Added {actualAdded} {type} (Total: {newAmount})");
            
            // Notify listeners
            OnResourceChanged?.Invoke(type, newAmount);
            OnResourceAdded?.Invoke(type, actualAdded, newAmount);
            
            return true;
        }
        
        /// <summary>
        /// Removes resources of the specified type if available
        /// </summary>
        /// <param name="type">The type of resource to remove</param>
        /// <param name="amount">The amount to remove (must be positive)</param>
        /// <returns>True if resources were removed successfully, false if insufficient</returns>
        public bool RemoveResource(ResourceType type, int amount)
        {
            if (amount <= 0)
            {
                Debug.LogWarning($"[GW] Attempted to remove non-positive amount ({amount}) of {type}");
                return false;
            }
            
            if (!_resourceTotals.ContainsKey(type))
            {
                Debug.LogError($"[GW] Resource type {type} not found in resource totals");
                return false;
            }
            
            int currentAmount = _resourceTotals[type];
            if (currentAmount < amount)
            {
                Debug.LogWarning($"[GW] Insufficient {type} to remove {amount} (Available: {currentAmount})");
                return false;
            }
            
            int newAmount = currentAmount - amount;
            _resourceTotals[type] = newAmount;
            
            Debug.Log($"[GW] Removed {amount} {type} (Total: {newAmount})");
            
            // Notify listeners
            OnResourceChanged?.Invoke(type, newAmount);
            OnResourceRemoved?.Invoke(type, amount, newAmount);
            
            return true;
        }
        
        /// <summary>
        /// Gets the current amount of a specific resource type
        /// </summary>
        /// <param name="type">The resource type to query</param>
        /// <returns>The current amount of the resource, or 0 if type not found</returns>
        public int GetResourceAmount(ResourceType type)
        {
            if (_resourceTotals != null && _resourceTotals.ContainsKey(type))
            {
                return _resourceTotals[type];
            }
            
            Debug.LogWarning($"[GW] Resource type {type} not found, returning 0");
            return 0;
        }
        
        /// <summary>
        /// Checks if the player has enough of a specific resource
        /// </summary>
        /// <param name="type">The resource type to check</param>
        /// <param name="amount">The amount required</param>
        /// <returns>True if player has sufficient resources, false otherwise</returns>
        public bool HasResource(ResourceType type, int amount)
        {
            return GetResourceAmount(type) >= amount;
        }
        
        /// <summary>
        /// Attempts to consume multiple resources at once
        /// </summary>
        /// <param name="costs">Dictionary of resource types and amounts to consume</param>
        /// <returns>True if all resources were consumed successfully, false if any were insufficient</returns>
        public bool TryConsumeResources(Dictionary<ResourceType, int> costs)
        {
            if (costs == null || costs.Count == 0)
            {
                Debug.LogWarning("[GW] Attempted to consume resources with null or empty costs");
                return false;
            }
            
            // First, check if we have all required resources
            foreach (var cost in costs)
            {
                if (!HasResource(cost.Key, cost.Value))
                {
                    Debug.LogWarning($"[GW] Insufficient {cost.Key} for cost {cost.Value}");
                    return false;
                }
            }
            
            // If we have all resources, consume them
            foreach (var cost in costs)
            {
                RemoveResource(cost.Key, cost.Value);
            }
            
            Debug.Log("[GW] Successfully consumed multiple resources");
            return true;
        }
        
        /// <summary>
        /// Gets a dictionary of all current resource amounts
        /// </summary>
        /// <returns>Copy of current resource totals</returns>
        public Dictionary<ResourceType, int> GetAllResources()
        {
            if (_resourceTotals == null)
            {
                InitializeResources();
            }
            
            return new Dictionary<ResourceType, int>(_resourceTotals);
        }
        
        /// <summary>
        /// Resets all resources to their initial values
        /// </summary>
        public void ResetToDefaults()
        {
            InitializeResources();
            Debug.Log("[GW] Resources reset to default values");
            
            // Notify listeners of all resource changes
            foreach (var resource in _resourceTotals)
            {
                OnResourceChanged?.Invoke(resource.Key, resource.Value);
            }
        }
        
        /// <summary>
        /// Sets a specific resource to a new value
        /// </summary>
        /// <param name="type">The resource type to set</param>
        /// <param name="amount">The new amount (clamped to max limit)</param>
        public void SetResource(ResourceType type, int amount)
        {
            if (!_resourceTotals.ContainsKey(type))
            {
                Debug.LogError($"[GW] Resource type {type} not found in resource totals");
                return;
            }
            
            int clampedAmount = Mathf.Clamp(amount, 0, _maxResourceAmount);
            int oldAmount = _resourceTotals[type];
            
            _resourceTotals[type] = clampedAmount;
            
            Debug.Log($"[GW] Set {type} from {oldAmount} to {clampedAmount}");
            
            // Notify listeners
            OnResourceChanged?.Invoke(type, clampedAmount);
        }
        
        /// <summary>
        /// Gets the maximum resource amount allowed
        /// </summary>
        public int MaxResourceAmount => _maxResourceAmount;
        
        /// <summary>
        /// Gets the initial resource values
        /// </summary>
        public int InitialWood => _initialWood;
        public int InitialStone => _initialStone;
        public int InitialFood => _initialFood;
        public int InitialMagic => _initialMagic;
    }
}


