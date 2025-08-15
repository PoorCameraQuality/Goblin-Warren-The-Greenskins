using UnityEngine;
using System.Collections.Generic;

namespace GoblinWarrens.Core
{
    /// <summary>
    /// Central game coordinator that initializes all systems and manages game state.
    /// Follows singleton pattern for global access to core systems.
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        [Header("Game State")]
        [SerializeField] private GameState _currentGameState = GameState.Running;
        
        [Header("System References")]
        // TODO: Uncomment these as each manager is implemented
        // [SerializeField] private ResourceManager _resourceManager;
        // [SerializeField] private UIManager _uiManager;
        // [SerializeField] private AIManager _aiManager;
        // [SerializeField] private EnemyManager _enemyManager;
        
        // Singleton instance
        public static GameManager Instance { get; private set; }
        
        // Public accessors for other systems
        // TODO: Uncomment these as each manager is implemented
        // public ResourceManager ResourceManager => _resourceManager;
        // public UIManager UIManager => _uiManager;
        // public AIManager AIManager => _aiManager;
        // public EnemyManager EnemyManager => _enemyManager;
        public GameState CurrentGameState => _currentGameState;
        
        // Events
        public System.Action<GameState> OnGameStateChanged;
        
        /// <summary>
        /// Game states that control overall game flow
        /// </summary>
        public enum GameState
        {
            Paused,
            Running,
            GameOver
        }
        
        private void Awake()
        {
            // Singleton pattern
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                Debug.Log("[GW] GameManager initialized as singleton");
            }
            else
            {
                Debug.LogWarning("[GW] Multiple GameManager instances detected. Destroying duplicate.");
                Destroy(gameObject);
            }
        }
        
        private void Start()
        {
            InitializeSystems();
            Debug.Log("[GW] GameManager Start() completed");
        }
        
        /// <summary>
        /// Initializes all core systems in dependency order
        /// </summary>
        private void InitializeSystems()
        {
            Debug.Log("[GW] Initializing core systems...");
            
            // TODO: Uncomment these as each manager is implemented
            // Initialize ResourceManager first (no dependencies)
            // if (_resourceManager != null)
            // {
            //     _resourceManager.Initialize();
            //     Debug.Log("[GW] ResourceManager initialized");
            // }
            // else
            // {
            //     Debug.LogError("[GW] ResourceManager reference missing!");
            // }
            
            // Initialize UIManager (depends on ResourceManager)
            // if (_uiManager != null)
            // {
            //     _uiManager.Initialize();
            //     Debug.Log("[GW] UIManager initialized");
            // }
            // else
            // {
            //     Debug.LogWarning("[GW] UIManager reference missing - UI will not function");
            // }
            
            // Initialize AIManager (no dependencies yet)
            // if (_aiManager != null)
            // {
            //     _aiManager.Initialize();
            //     Debug.Log("[GW] AIManager initialized");
            // }
            // else
            //     Debug.LogWarning("[GW] AIManager reference missing - AI will not function");
            // }
            
            // Initialize EnemyManager (depends on ResourceManager and AIManager)
            // if (_enemyManager != null)
            // {
            //     _enemyManager.Initialize();
            //     Debug.Log("[GW] EnemyManager initialized");
            // }
            // else
            // {
            //     Debug.LogWarning("[GW] EnemyManager reference missing - enemies will not spawn");
            // }
            
            Debug.Log("[GW] All core systems initialized successfully");
        }
        
        /// <summary>
        /// Changes the current game state and notifies listeners
        /// </summary>
        /// <param name="newState">The new game state to transition to</param>
        public void ChangeGameState(GameState newState)
        {
            if (_currentGameState == newState) return;
            
            GameState previousState = _currentGameState;
            _currentGameState = newState;
            
            Debug.Log($"[GW] Game state changed from {previousState} to {newState}");
            
            // Handle state-specific logic
            switch (newState)
            {
                case GameState.Paused:
                    Time.timeScale = 0f;
                    break;
                case GameState.Running:
                    Time.timeScale = 1f;
                    break;
                case GameState.GameOver:
                    Time.timeScale = 0f;
                    Debug.Log("[GW] Game Over - all systems should stop");
                    break;
            }
            
            // Notify listeners
            OnGameStateChanged?.Invoke(newState);
        }
        
        /// <summary>
        /// Pauses the game
        /// </summary>
        public void PauseGame()
        {
            ChangeGameState(GameState.Paused);
        }
        
        /// <summary>
        /// Resumes the game
        /// </summary>
        public void ResumeGame()
        {
            ChangeGameState(GameState.Running);
        }
        
        /// <summary>
        /// Triggers game over state
        /// </summary>
        public void GameOver()
        {
            ChangeGameState(GameState.GameOver);
        }
        
        /// <summary>
        /// Restarts the game by resetting all systems
        /// </summary>
        public void RestartGame()
        {
            Debug.Log("[GW] Restarting game...");
            
            // Reset game state
            ChangeGameState(GameState.Running);
            
            // TODO: Uncomment these as each manager is implemented
            // Reset all managers
            // if (_resourceManager != null) _resourceManager.ResetToDefaults();
            // if (_enemyManager != null) _enemyManager.ResetToDefaults();
            
            Debug.Log("[GW] Game restarted successfully");
        }
        
        private void OnDestroy()
        {
            if (Instance == this)
            {
                Instance = null;
                Debug.Log("[GW] GameManager singleton destroyed");
            }
        }
    }
}


