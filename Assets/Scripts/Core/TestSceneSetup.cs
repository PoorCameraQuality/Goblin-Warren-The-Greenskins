using UnityEngine;
// TODO: Uncomment when managers are implemented
// using GoblinWarrens.Managers;
// using GoblinWarrens.UI;

namespace GoblinWarrens.Core
{
    /// <summary>
    /// Test scene setup helper that configures all systems for testing.
    /// This script helps verify that all managers are working correctly.
    /// </summary>
    public class TestSceneSetup : MonoBehaviour
    {
        [Header("Test Controls")]
        [SerializeField] private bool _autoSetupOnStart = true;
        [SerializeField] private KeyCode _testResourceAdd = KeyCode.R;
        [SerializeField] private KeyCode _testResourceRemove = KeyCode.T;
        [SerializeField] private KeyCode _testPauseResume = KeyCode.Space;
        [SerializeField] private KeyCode _testGameOver = KeyCode.G;
        [SerializeField] private KeyCode _testRestart = KeyCode.Return;
        
        [Header("Resource Test Values")]
        [SerializeField] private int _testResourceAmount = 50;
        
        private GameManager _gameManager;
        // TODO: Uncomment these as each manager is implemented
        // private ResourceManager _resourceManager;
        // private UIManager _uiManager;
        // private AIManager _aiManager;
        // private EnemyManager _enemyManager;
        
        private void Start()
        {
            if (_autoSetupOnStart)
            {
                SetupTestScene();
            }
        }
        
        private void Update()
        {
            HandleTestInput();
        }
        
        /// <summary>
        /// Sets up the test scene with all required components
        /// </summary>
        public void SetupTestScene()
        {
            Debug.Log("[GW] === TEST SCENE SETUP STARTING ===");
            
            // Find or create GameManager
            _gameManager = FindObjectOfType<GameManager>();
            if (_gameManager == null)
            {
                GameObject gameManagerObj = new GameObject("GameManager");
                _gameManager = gameManagerObj.AddComponent<GameManager>();
                Debug.Log("[GW] Created GameManager");
            }
            
            // TODO: Uncomment these as each manager is implemented
            // Find or create ResourceManager
            // _resourceManager = FindObjectOfType<ResourceManager>();
            // if (_resourceManager == null)
            // {
            //     GameObject resourceManagerObj = new GameObject("ResourceManager");
            //     _resourceManager = resourceManagerObj.AddComponent<ResourceManager>();
            //     Debug.Log("[GW] Created ResourceManager");
            // }
            
            // Find or create UIManager
            // _uiManager = FindObjectOfType<UIManager>();
            // if (_uiManager == null)
            // {
            //     GameObject uiManagerObj = new GameObject("UIManager");
            //     _uiManager = uiManagerObj.AddComponent<UIManager>();
            //     Debug.Log("[GW] Created UIManager");
            // }
            
            // Find or create AIManager
            // _aiManager = FindObjectOfType<AIManager>();
            // if (_aiManager == null)
            // {
            //     GameObject aiManagerObj = new GameObject("AIManager");
            //     _enemyManager = aiManagerObj.AddComponent<AIManager>();
            //     Debug.Log("[GW] Created AIManager");
            // }
            
            // Find or create EnemyManager
            // _enemyManager = FindObjectOfType<EnemyManager>();
            // if (_enemyManager == null)
            // {
            //     GameObject enemyManagerObj = new GameObject("EnemyManager");
            //     _enemyManager = enemyManagerObj.AddComponent<EnemyManager>();
            //     Debug.Log("[GW] Created EnemyManager");
            // }
            
            // Connect managers to GameManager
            ConnectManagersToGameManager();
            
            // Subscribe to events for testing
            SubscribeToEvents();
            
            Debug.Log("[GW] === TEST SCENE SETUP COMPLETED ===");
            Debug.Log("[GW] Press R to add resources, T to remove resources");
            Debug.Log("[GW] Press Space to pause/resume, G for game over, Enter to restart");
        }
        
        /// <summary>
        /// Connects all managers to the GameManager for proper initialization
        /// </summary>
        private void ConnectManagersToGameManager()
        {
            // TODO: Uncomment when managers are implemented
            // Use reflection to set private fields (for testing purposes)
            // var gameManagerType = typeof(GameManager);
            
            // var resourceManagerField = gameManagerType.GetField("_resourceManager", 
            //     System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            // var uiManagerField = gameManagerType.GetField("_uiManager", 
            //     System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            // var aiManagerField = gameManagerType.GetField("_aiManager", 
            //     System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            // var enemyManagerField = gameManagerType.GetField("_enemyManager", 
            //     System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            
            // if (resourceManagerField != null) resourceManagerField.SetValue(_gameManager, _resourceManager);
            // if (uiManagerField != null) uiManagerField.SetValue(_gameManager, _uiManager);
            // if (aiManagerField != null) aiManagerField.SetValue(_gameManager, _aiManager);
            // if (enemyManagerField != null) enemyManagerField.SetValue(_gameManager, _enemyManager);
            
            Debug.Log("[GW] Manager connection skipped - managers not yet implemented");
        }
        
        /// <summary>
        /// Subscribes to manager events for testing and monitoring
        /// </summary>
        private void SubscribeToEvents()
        {
            // TODO: Uncomment when managers are implemented
            // if (_resourceManager != null)
            // {
            //     _resourceManager.OnResourceChanged += OnResourceChanged;
            //     _resourceManager.OnResourceAdded += OnResourceAdded;
            //     _resourceManager.OnResourceRemoved += OnResourceRemoved;
            // }
            
            if (_gameManager != null)
            {
                _gameManager.OnGameStateChanged += OnGameStateChanged;
            }
            
            Debug.Log("[GW] Manager events subscription skipped - managers not yet implemented");
        }
        
        /// <summary>
        /// Handles test input for manual testing
        /// </summary>
        private void HandleTestInput()
        {
            if (Input.GetKeyDown(_testResourceAdd))
            {
                TestAddResources();
            }
            
            if (Input.GetKeyDown(_testResourceRemove))
            {
                TestRemoveResources();
            }
            
            if (Input.GetKeyDown(_testPauseResume))
            {
                TestPauseResume();
            }
            
            if (Input.GetKeyDown(_testGameOver))
            {
                TestGameOver();
            }
            
            if (Input.GetKeyDown(_testRestart))
            {
                TestRestart();
            }
        }
        
        /// <summary>
        /// Tests adding resources to all types
        /// </summary>
        private void TestAddResources()
        {
            // TODO: Uncomment when ResourceManager is implemented
            // if (_resourceManager == null) return;
            
            Debug.Log("[GW] === TESTING RESOURCE ADDITION ===");
            Debug.Log("[GW] ResourceManager not yet implemented - test skipped");
            
            // _resourceManager.AddResource(ResourceManager.ResourceType.Wood, _testResourceAmount);
            // _resourceManager.AddResource(ResourceManager.ResourceType.Stone, _testResourceAmount);
            // _resourceManager.AddResource(ResourceManager.ResourceType.Food, _testResourceAmount);
            // _resourceManager.AddResource(ResourceManager.ResourceType.Magic, _testResourceAmount);
            
            // Debug.Log($"[GW] Added {_testResourceAmount} of each resource type");
        }
        
        /// <summary>
        /// Tests removing resources from all types
        /// </summary>
        private void TestRemoveResources()
        {
            // TODO: Uncomment when ResourceManager is implemented
            // if (_resourceManager == null) return;
            
            Debug.Log("[GW] === TESTING RESOURCE REMOVAL ===");
            Debug.Log("[GW] ResourceManager not yet implemented - test skipped");
            
            // _resourceManager.RemoveResource(ResourceManager.ResourceType.Wood, _testResourceAmount / 2);
            // _resourceManager.RemoveResource(ResourceManager.ResourceType.Stone, _testResourceAmount / 2);
            // _resourceManager.RemoveResource(ResourceManager.ResourceType.Food, _testResourceAmount / 2);
            // _resourceManager.RemoveResource(ResourceManager.ResourceType.Magic, _testResourceAmount / 2);
            
            // Debug.Log($"[GW] Removed {_testResourceAmount / 2} of each resource type");
        }
        
        /// <summary>
        /// Tests pause/resume functionality
        /// </summary>
        private void TestPauseResume()
        {
            if (_gameManager == null) return;
            
            if (_gameManager.CurrentGameState == GameManager.GameState.Running)
            {
                Debug.Log("[GW] === TESTING PAUSE ===");
                _gameManager.PauseGame();
            }
            else if (_gameManager.CurrentGameState == GameManager.GameState.Paused)
            {
                Debug.Log("[GW] === TESTING RESUME ===");
                _gameManager.ResumeGame();
            }
        }
        
        /// <summary>
        /// Tests game over functionality
        /// </summary>
        private void TestGameOver()
        {
            if (_gameManager == null) return;
            
            Debug.Log("[GW] === TESTING GAME OVER ===");
            _gameManager.GameOver();
        }
        
        /// <summary>
        /// Tests game restart functionality
        /// </summary>
        private void TestRestart()
        {
            if (_gameManager == null) return;
            
            Debug.Log("[GW] === TESTING GAME RESTART ===");
            _gameManager.RestartGame();
        }
        
        // TODO: Uncomment when ResourceManager is implemented
        // Event handlers for monitoring
        // private void OnResourceChanged(ResourceManager.ResourceType type, int newValue)
        // {
        //     Debug.Log($"[GW] Resource changed: {type} = {newValue}");
        // }
        
        // private void OnResourceAdded(ResourceManager.ResourceType type, int amount, int newTotal)
        // {
        //     Debug.Log($"[GW] Resource added: {amount} {type} (Total: {newTotal})");
        // }
        
        // private void OnResourceRemoved(ResourceManager.ResourceType type, int amount, int newTotal)
        // {
        //     Debug.Log($"[GW] Resource removed: {amount} {type} (Total: {newTotal})");
        // }
        
        private void OnGameStateChanged(GameManager.GameState newState)
        {
            Debug.Log($"[GW] Game state changed to: {newState}");
        }
        
        private void OnDestroy()
        {
            // TODO: Uncomment when managers are implemented
            // Unsubscribe from events
            // if (_resourceManager != null)
            // {
            //     _resourceManager.OnResourceChanged -= OnResourceChanged;
            //     _resourceManager.OnResourceAdded -= OnResourceAdded;
            //     _resourceManager.OnResourceRemoved -= OnResourceRemoved;
            // }
            
            if (_gameManager != null)
            {
                _gameManager.OnGameStateChanged -= OnGameStateChanged;
            }
        }
    }
}


