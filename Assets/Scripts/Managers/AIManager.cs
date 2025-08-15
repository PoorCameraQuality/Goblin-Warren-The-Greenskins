using UnityEngine;
using System.Collections.Generic;
using System;
using GoblinWarrens.AI;

namespace GoblinWarrens.Managers
{
    /// <summary>
    /// Central AI management system that handles unit registration, movement, and pathfinding.
    /// Integrates with A* Pathfinding Project for isometric grid-based movement.
    /// </summary>
    public class AIManager : MonoBehaviour
    {
        [Header("A* Pathfinding Settings")]
        [SerializeField] private bool _useAStarPathfinding = true;
        [SerializeField] private float _pathfindingUpdateRate = 0.1f;
        [SerializeField] private int _maxPathfindingAttempts = 3;
        
        [Header("Movement Settings")]
        [SerializeField] private float _defaultMoveSpeed = 5f;
        [SerializeField] private float _pathfindingTimeout = 5f;
        
        // Unit registry
        private List<Unit> _registeredUnits;
        private Dictionary<int, Unit> _unitsById;
        
        // A* Pathfinding integration (placeholder for now)
        private bool _aStarInitialized = false;
        
        // Events
        public event Action<Unit> OnUnitRegistered;
        public event Action<Unit> OnUnitUnregistered;
        public event Action<Unit, Vector3> OnUnitMovementStarted;
        public event Action<Unit, Vector3> OnUnitMovementCompleted;
        public event Action<Unit, Vector3> OnUnitMovementFailed;
        
        // Properties
        public bool IsAStarAvailable => _useAStarPathfinding && _aStarInitialized;
        public int RegisteredUnitCount => _registeredUnits?.Count ?? 0;
        public float DefaultMoveSpeed => _defaultMoveSpeed;
        
        private void Awake()
        {
            InitializeUnitRegistry();
            Debug.Log("[GW] AIManager Awake() completed");
        }
        
        /// <summary>
        /// Initializes the AI system and A* pathfinding
        /// </summary>
        public void Initialize()
        {
            InitializeUnitRegistry();
            InitializeAStarPathfinding();
            
            Debug.Log("[GW] AIManager initialized successfully");
            Debug.Log($"[GW] A* Pathfinding: {(_aStarInitialized ? "Available" : "Not Available")}");
        }
        
        /// <summary>
        /// Sets up the unit registry system
        /// </summary>
        private void InitializeUnitRegistry()
        {
            if (_registeredUnits == null)
            {
                _registeredUnits = new List<Unit>();
                _unitsById = new Dictionary<int, Unit>();
            }
        }
        
        /// <summary>
        /// Initializes A* Pathfinding Project integration
        /// </summary>
        private void InitializeAStarPathfinding()
        {
            if (!_useAStarPathfinding)
            {
                Debug.Log("[GW] A* Pathfinding disabled in settings");
                return;
            }
            
            // TODO: Initialize A* Pathfinding Project GridGraph for isometric coordinates
            // This is a placeholder until the A* package is properly integrated
            Debug.LogWarning("[GW] A* Pathfinding Project integration not yet implemented");
            Debug.LogWarning("[GW] Units will use direct movement until A* is integrated");
            
            _aStarInitialized = false;
        }
        
        /// <summary>
        /// Registers a unit with the AI system
        /// </summary>
        /// <param name="unit">The unit to register</param>
        /// <returns>True if registration was successful, false otherwise</returns>
        public bool RegisterUnit(Unit unit)
        {
            if (unit == null)
            {
                Debug.LogError("[GW] Attempted to register null unit");
                return false;
            }
            
            if (_registeredUnits.Contains(unit))
            {
                Debug.LogWarning($"[GW] Unit {unit.name} is already registered");
                return false;
            }
            
            _registeredUnits.Add(unit);
            _unitsById[unit.GetInstanceID()] = unit;
            
            Debug.Log($"[GW] Registered unit: {unit.name} (Total: {_registeredUnits.Count})");
            
            // Notify listeners
            OnUnitRegistered?.Invoke(unit);
            
            return true;
        }
        
        /// <summary>
        /// Unregisters a unit from the AI system
        /// </summary>
        /// <param name="unit">The unit to unregister</param>
        /// <returns>True if unregistration was successful, false otherwise</returns>
        public bool UnregisterUnit(Unit unit)
        {
            if (unit == null)
            {
                Debug.LogError("[GW] Attempted to unregister null unit");
                return false;
            }
            
            if (!_registeredUnits.Contains(unit))
            {
                Debug.LogWarning($"[GW] Unit {unit.name} is not registered");
                return false;
            }
            
            _registeredUnits.Remove(unit);
            _unitsById.Remove(unit.GetInstanceID());
            
            Debug.Log($"[GW] Unregistered unit: {unit.name} (Total: {_registeredUnits.Count})");
            
            // Notify listeners
            OnUnitUnregistered?.Invoke(unit);
            
            return true;
        }
        
        /// <summary>
        /// Moves a unit to a specific world position using available pathfinding
        /// </summary>
        /// <param name="unit">The unit to move</param>
        /// <param name="worldTarget">The target world position</param>
        /// <returns>True if movement was initiated successfully, false otherwise</returns>
        public bool MoveUnitTo(Unit unit, Vector3 worldTarget)
        {
            if (unit == null)
            {
                Debug.LogError("[GW] Attempted to move null unit");
                return false;
            }
            
            if (!_registeredUnits.Contains(unit))
            {
                Debug.LogError($"[GW] Unit {unit.name} is not registered with AIManager");
                return false;
            }
            
            Debug.Log($"[GW] Moving unit {unit.name} to {worldTarget}");
            
            // Notify listeners of movement start
            OnUnitMovementStarted?.Invoke(unit, worldTarget);
            
            // For now, use direct movement until A* is integrated
            if (IsAStarAvailable)
            {
                // TODO: Implement A* pathfinding here
                Debug.LogWarning("[GW] A* pathfinding not yet implemented, using direct movement");
            }
            
            // Use the unit's built-in movement system
            bool movementStarted = unit.MoveTo(worldTarget);
            
            if (movementStarted)
            {
                Debug.Log($"[GW] Unit {unit.name} movement initiated successfully");
            }
            else
            {
                Debug.LogWarning($"[GW] Unit {unit.name} failed to start movement");
                OnUnitMovementFailed?.Invoke(unit, worldTarget);
            }
            
            return movementStarted;
        }
        
        /// <summary>
        /// Gets all currently registered units
        /// </summary>
        /// <returns>List of registered units</returns>
        public List<Unit> GetAllRegisteredUnits()
        {
            if (_registeredUnits == null)
            {
                InitializeUnitRegistry();
            }
            
            return new List<Unit>(_registeredUnits);
        }
        
        /// <summary>
        /// Gets a unit by its instance ID
        /// </summary>
        /// <param name="instanceId">The unit's instance ID</param>
        /// <returns>The unit if found, null otherwise</returns>
        public Unit GetUnitById(int instanceId)
        {
            if (_unitsById != null && _unitsById.ContainsKey(instanceId))
            {
                return _unitsById[instanceId];
            }
            
            return null;
        }
        
        /// <summary>
        /// Gets all units of a specific type
        /// </summary>
        /// <typeparam name="T">The type of unit to find</typeparam>
        /// <returns>List of units of the specified type</returns>
        public List<T> GetUnitsOfType<T>() where T : Unit
        {
            List<T> unitsOfType = new List<T>();
            
            if (_registeredUnits != null)
            {
                foreach (var unit in _registeredUnits)
                {
                    if (unit is T typedUnit)
                    {
                        unitsOfType.Add(typedUnit);
                    }
                }
            }
            
            return unitsOfType;
        }
        
        /// <summary>
        /// Checks if a unit is registered with the AI system
        /// </summary>
        /// <param name="unit">The unit to check</param>
        /// <returns>True if the unit is registered, false otherwise</returns>
        public bool IsUnitRegistered(Unit unit)
        {
            return unit != null && _registeredUnits != null && _registeredUnits.Contains(unit);
        }
        
        /// <summary>
        /// Updates the movement speed for all registered units
        /// </summary>
        /// <param name="speedMultiplier">Multiplier to apply to current speeds</param>
        public void UpdateAllUnitSpeeds(float speedMultiplier)
        {
            if (_registeredUnits == null) return;
            
            foreach (var unit in _registeredUnits)
            {
                if (unit != null)
                {
                    unit.UpdateMoveSpeed(unit.MoveSpeed * speedMultiplier);
                }
            }
            
            Debug.Log($"[GW] Updated all unit speeds by multiplier: {speedMultiplier}");
        }
        
        /// <summary>
        /// Resets all units to their default movement speeds
        /// </summary>
        public void ResetAllUnitSpeeds()
        {
            if (_registeredUnits == null) return;
            
            foreach (var unit in _registeredUnits)
            {
                if (unit != null)
                {
                    unit.ResetMoveSpeed();
                }
            }
            
            Debug.Log("[GW] Reset all unit speeds to default values");
        }
        
        /// <summary>
        /// Clears all registered units (useful for scene transitions)
        /// </summary>
        public void ClearAllUnits()
        {
            if (_registeredUnits != null)
            {
                int count = _registeredUnits.Count;
                _registeredUnits.Clear();
                _unitsById.Clear();
                
                Debug.Log($"[GW] Cleared {count} registered units");
            }
        }
        
        /// <summary>
        /// Gets the nearest unit to a specified position
        /// </summary>
        /// <param name="position">The position to search from</param>
        /// <param name="maxDistance">Maximum distance to search (0 = unlimited)</param>
        /// <returns>The nearest unit, or null if none found</returns>
        public Unit GetNearestUnit(Vector3 position, float maxDistance = 0f)
        {
            if (_registeredUnits == null || _registeredUnits.Count == 0)
            {
                return null;
            }
            
            Unit nearestUnit = null;
            float nearestDistance = float.MaxValue;
            
            foreach (var unit in _registeredUnits)
            {
                if (unit == null) continue;
                
                float distance = Vector3.Distance(position, unit.transform.position);
                
                if (maxDistance > 0f && distance > maxDistance) continue;
                
                if (distance < nearestDistance)
                {
                    nearestDistance = distance;
                    nearestUnit = unit;
                }
            }
            
            return nearestUnit;
        }
        
        private void OnDestroy()
        {
            ClearAllUnits();
            Debug.Log("[GW] AIManager destroyed, all units cleared");
        }
    }
}


