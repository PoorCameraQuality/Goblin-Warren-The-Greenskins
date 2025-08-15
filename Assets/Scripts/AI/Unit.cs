using UnityEngine;
// TODO: Uncomment when managers are implemented
// using GoblinWarrens.Managers;

namespace GoblinWarrens.AI
{
    /// <summary>
    /// Base class for all AI units in the game.
    /// Provides shared stats, movement capabilities, and lifecycle management.
    /// </summary>
    public abstract class Unit : MonoBehaviour
    {
        [Header("Unit Stats")]
        [SerializeField] protected float _maxHealth = 100f;
        [SerializeField] protected float _currentHealth = 100f;
        [SerializeField] protected float _moveSpeed = 5f;
        [SerializeField] protected float _defaultMoveSpeed = 5f;
        
        [Header("Movement")]
        [SerializeField] protected float _stoppingDistance = 0.1f;
        [SerializeField] protected bool _isMoving = false;
        
        // Movement target
        protected Vector3 _moveTarget;
        protected bool _hasMoveTarget = false;
        
        // Events
        public System.Action<Unit> OnUnitSpawned;
        public System.Action<Unit> OnUnitDeath;
        public System.Action<Unit, Vector3> OnMovementStarted;
        public System.Action<Unit, Vector3> OnMovementCompleted;
        public System.Action<Unit, float> OnHealthChanged;
        
        // Properties
        public float MaxHealth => _maxHealth;
        public float CurrentHealth => _currentHealth;
        public float MoveSpeed => _moveSpeed;
        public bool IsMoving => _isMoving;
        public bool HasMoveTarget => _hasMoveTarget;
        public Vector3 MoveTarget => _moveTarget;
        
        // Health percentage
        public float HealthPercentage => _maxHealth > 0 ? _currentHealth / _maxHealth : 0f;
        
        // Is unit alive
        public bool IsAlive => _currentHealth > 0f;
        
        protected virtual void Awake()
        {
            // Initialize default values
            _currentHealth = _maxHealth;
            _moveSpeed = _defaultMoveSpeed;
            
            Debug.Log($"[GW] Unit {name} initialized with {_maxHealth} health, {_moveSpeed} speed");
        }
        
        protected virtual void Start()
        {
            // TODO: Uncomment when AIManager is implemented
            // Register with AIManager if available
            // if (GameManager.Instance?.AIManager != null)
            // {
            //     GameManager.Instance.AIManager.RegisterUnit(this);
            //     Debug.Log($"[GW] Unit {name} registered with AIManager");
            // }
            // else
            // {
            //     Debug.LogWarning($"[GW] Unit {name} could not register with AIManager - not available");
            // }
            
            // Notify spawn event
            OnUnitSpawned?.Invoke(this);
        }
        
        protected virtual void Update()
        {
            if (!IsAlive) return;
            
            // Handle movement
            if (_hasMoveTarget && _isMoving)
            {
                UpdateMovement();
            }
        }
        
        /// <summary>
        /// Moves the unit to a specific world position
        /// </summary>
        /// <param name="worldTarget">The target world position</param>
        /// <returns>True if movement was initiated successfully, false otherwise</returns>
        public virtual bool MoveTo(Vector3 worldTarget)
        {
            if (!IsAlive) return false;
            
            _moveTarget = worldTarget;
            _hasMoveTarget = true;
            _isMoving = true;
            
            Debug.Log($"[GW] Unit {name} moving to {worldTarget}");
            
            // Notify movement started
            OnMovementStarted?.Invoke(this, worldTarget);
            
            return true;
        }
        
        /// <summary>
        /// Stops the unit's current movement
        /// </summary>
        public virtual void StopMovement()
        {
            if (!_isMoving) return;
            
            _isMoving = false;
            _hasMoveTarget = false;
            
            Debug.Log($"[GW] Unit {name} stopped moving");
        }
        
        /// <summary>
        /// Updates the unit's movement towards the target
        /// </summary>
        protected virtual void UpdateMovement()
        {
            if (!_hasMoveTarget || !_isMoving) return;
            
            Vector3 direction = (_moveTarget - transform.position).normalized;
            float distance = Vector3.Distance(transform.position, _moveTarget);
            
            // Check if we've reached the target
            if (distance <= _stoppingDistance)
            {
                // Arrived at target
                _isMoving = false;
                _hasMoveTarget = false;
                
                Debug.Log($"[GW] Unit {name} reached target at {_moveTarget}");
                
                // Notify movement completed
                OnMovementCompleted?.Invoke(this, _moveTarget);
                return;
            }
            
            // Move towards target
            Vector3 newPosition = transform.position + direction * _moveSpeed * Time.deltaTime;
            transform.position = newPosition;
            
            // Optional: Rotate towards movement direction
            if (direction != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(direction);
            }
        }
        
        /// <summary>
        /// Updates the unit's movement speed
        /// </summary>
        /// <param name="newSpeed">The new movement speed</param>
        public virtual void UpdateMoveSpeed(float newSpeed)
        {
            float oldSpeed = _moveSpeed;
            _moveSpeed = Mathf.Max(0f, newSpeed);
            
            Debug.Log($"[GW] Unit {name} speed changed from {oldSpeed} to {_moveSpeed}");
        }
        
        /// <summary>
        /// Resets the unit's movement speed to default
        /// </summary>
        public virtual void ResetMoveSpeed()
        {
            float oldSpeed = _moveSpeed;
            _moveSpeed = _defaultMoveSpeed;
            
            Debug.Log($"[GW] Unit {name} speed reset from {oldSpeed} to {_moveSpeed}");
        }
        
        /// <summary>
        /// Takes damage and updates health
        /// </summary>
        /// <param name="damage">Amount of damage to take</param>
        /// <returns>True if unit died, false if still alive</returns>
        public virtual bool TakeDamage(float damage)
        {
            if (!IsAlive) return false;
            
            float oldHealth = _currentHealth;
            _currentHealth = Mathf.Max(0f, _currentHealth - damage);
            
            Debug.Log($"[GW] Unit {name} took {damage} damage. Health: {oldHealth} -> {_currentHealth}");
            
            // Notify health change
            OnHealthChanged?.Invoke(this, _currentHealth);
            
            // Check if unit died
            if (_currentHealth <= 0f)
            {
                Die();
                return true;
            }
            
            return false;
        }
        
        /// <summary>
        /// Heals the unit
        /// </summary>
        /// <param name="healAmount">Amount of health to restore</param>
        public virtual void Heal(float healAmount)
        {
            if (!IsAlive) return;
            
            float oldHealth = _currentHealth;
            _currentHealth = Mathf.Min(_maxHealth, _currentHealth + healAmount);
            
            Debug.Log($"[GW] Unit {name} healed {healAmount}. Health: {oldHealth} -> {_currentHealth}");
            
            // Notify health change
            OnHealthChanged?.Invoke(this, _currentHealth);
        }
        
        /// <summary>
        /// Sets the unit's health to a specific value
        /// </summary>
        /// <param name="newHealth">The new health value</param>
        public virtual void SetHealth(float newHealth)
        {
            float oldHealth = _currentHealth;
            _currentHealth = Mathf.Clamp(newHealth, 0f, _maxHealth);
            
            Debug.Log($"[GW] Unit {name} health set from {oldHealth} to {_currentHealth}");
            
            // Notify health change
            OnHealthChanged?.Invoke(this, _currentHealth);
            
            // Check if unit died
            if (_currentHealth <= 0f && oldHealth > 0f)
            {
                Die();
            }
        }
        
        /// <summary>
        /// Handles unit death
        /// </summary>
        protected virtual void Die()
        {
            if (!IsAlive) return;
            
            Debug.Log($"[GW] Unit {name} has died!");
            
            // Stop movement
            StopMovement();
            
            // Notify death event
            OnUnitDeath?.Invoke(this);
            
            // TODO: Uncomment when AIManager is implemented
            // Unregister from AIManager
            // if (GameManager.Instance?.AIManager != null)
            // {
            //     GameManager.Instance.AIManager.UnregisterUnit(this);
            // }
            
            // Call virtual method for derived classes
            OnDeath();
        }
        
        /// <summary>
        /// Virtual method called when unit spawns
        /// </summary>
        protected virtual void OnSpawn()
        {
            // Override in derived classes for custom spawn behavior
        }
        
        /// <summary>
        /// Virtual method called when unit dies
        /// </summary>
        protected virtual void OnDeath()
        {
            // Override in derived classes for custom death behavior
        }
        
        /// <summary>
        /// Gets the distance to another unit
        /// </summary>
        /// <param name="otherUnit">The other unit to measure distance to</param>
        /// <returns>Distance between units</returns>
        public float GetDistanceTo(Unit otherUnit)
        {
            if (otherUnit == null) return float.MaxValue;
            return Vector3.Distance(transform.position, otherUnit.transform.position);
        }
        
        /// <summary>
        /// Gets the distance to a world position
        /// </summary>
        /// <param name="worldPosition">The world position to measure distance to</param>
        /// <returns>Distance to the position</returns>
        public float GetDistanceTo(Vector3 worldPosition)
        {
            return Vector3.Distance(transform.position, worldPosition);
        }
        
        protected virtual void OnDestroy()
        {
            // TODO: Uncomment when AIManager is implemented
            // Unregister from AIManager if still registered
            // if (GameManager.Instance?.AIManager != null)
            // {
            //     GameManager.Instance.AIManager.UnregisterUnit(this);
            // }
            
            Debug.Log($"[GW] Unit {name} destroyed");
        }
    }
}


