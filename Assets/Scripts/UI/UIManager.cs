using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace GoblinWarrens.UI
{
    /// <summary>
    /// Manages the user interface and connects to other systems for data display.
    /// Handles resource display and basic UI initialization.
    /// </summary>
    public class UIManager : MonoBehaviour
    {
        [Header("Resource Display")]
        [SerializeField] private TextMeshProUGUI _woodText;
        [SerializeField] private TextMeshProUGUI _stoneText;
        [SerializeField] private TextMeshProUGUI _foodText;
        [SerializeField] private TextMeshProUGUI _magicText;
        
        [Header("Game State Display")]
        [SerializeField] private TextMeshProUGUI _gameStateText;
        [SerializeField] private Button _pauseButton;
        [SerializeField] private Button _resumeButton;
        
        [Header("Debug Info")]
        [SerializeField] private TextMeshProUGUI _debugText;
        
        private bool _isInitialized = false;
        
        private void Awake()
        {
            Debug.Log("[GW] UIManager Awake() completed");
        }
        
        /// <summary>
        /// Initializes the UI system and subscribes to events
        /// </summary>
        public void Initialize()
        {
            if (_isInitialized) return;
            
            SetupButtons();
            UpdateGameStateDisplay("Initializing...");
            UpdateDebugInfo("UI System Ready");
            
            _isInitialized = true;
            Debug.Log("[GW] UIManager initialized successfully");
        }
        
        /// <summary>
        /// Sets up button listeners and initial UI state
        /// </summary>
        private void SetupButtons()
        {
            if (_pauseButton != null)
            {
                _pauseButton.onClick.AddListener(OnPauseButtonClicked);
                _pauseButton.interactable = true;
            }
            
            if (_resumeButton != null)
            {
                _resumeButton.onClick.AddListener(OnResumeButtonClicked);
                _resumeButton.interactable = false;
            }
        }
        
        /// <summary>
        /// Updates the resource display with current values
        /// </summary>
        /// <param name="resourceType">Type of resource</param>
        /// <param name="amount">Current amount</param>
        public void UpdateResourceDisplay(ResourceType resourceType, int amount)
        {
            switch (resourceType)
            {
                case ResourceType.Wood:
                    if (_woodText != null) _woodText.text = $"Wood: {amount}";
                    break;
                case ResourceType.Stone:
                    if (_stoneText != null) _stoneText.text = $"Stone: {amount}";
                    break;
                case ResourceType.Food:
                    if (_foodText != null) _foodText.text = $"Food: {amount}";
                    break;
                case ResourceType.Magic:
                    if (_magicText != null) _magicText.text = $"Magic: {amount}";
                    break;
            }
        }
        
        /// <summary>
        /// Updates the game state display
        /// </summary>
        /// <param name="stateText">Text to display</param>
        public void UpdateGameStateDisplay(string stateText)
        {
            if (_gameStateText != null)
            {
                _gameStateText.text = $"Game State: {stateText}";
            }
        }
        
        /// <summary>
        /// Updates the debug information display
        /// </summary>
        /// <param name="debugInfo">Debug information to display</param>
        public void UpdateDebugInfo(string debugInfo)
        {
            if (_debugText != null)
            {
                _debugText.text = $"Debug: {debugInfo}";
            }
        }
        
        /// <summary>
        /// Updates button states based on game state
        /// </summary>
        /// <param name="isPaused">Whether the game is currently paused</param>
        public void UpdateButtonStates(bool isPaused)
        {
            if (_pauseButton != null)
            {
                _pauseButton.interactable = !isPaused;
            }
            
            if (_resumeButton != null)
            {
                _resumeButton.interactable = isPaused;
            }
        }
        
        /// <summary>
        /// Handles pause button click
        /// </summary>
        private void OnPauseButtonClicked()
        {
            // TODO: Uncomment when GameManager is properly connected
            // if (GameManager.Instance != null)
            // {
            //     GameManager.Instance.PauseGame();
            //     UpdateButtonStates(true);
            //     UpdateGameStateDisplay("Paused");
            //     UpdateDebugInfo("Game paused by user");
            // }
            
            // Placeholder functionality
            UpdateButtonStates(true);
            UpdateGameStateDisplay("Paused (Placeholder)");
            UpdateDebugInfo("Game pause requested - GameManager not connected");
        }
        
        /// <summary>
        /// Handles resume button click
        /// </summary>
        private void OnResumeButtonClicked()
        {
            // TODO: Uncomment when GameManager is properly connected
            // if (GameManager.Instance != null)
            // {
            //     GameManager.Instance.ResumeGame();
            //     UpdateButtonStates(false);
            //     UpdateGameStateDisplay("Running");
            //     UpdateDebugInfo("Game resumed by user");
            // }
            
            // Placeholder functionality
            UpdateButtonStates(false);
            UpdateGameStateDisplay("Running (Placeholder)");
            UpdateDebugInfo("Game resume requested - GameManager not connected");
        }
        
        /// <summary>
        /// Cleans up button listeners
        /// </summary>
        private void OnDestroy()
        {
            if (_pauseButton != null)
            {
                _pauseButton.onClick.RemoveListener(OnPauseButtonClicked);
            }
            
            if (_resumeButton != null)
            {
                _resumeButton.onClick.RemoveListener(OnResumeButtonClicked);
            }
        }
    }
    
    /// <summary>
    /// Resource types for UI display (matches ResourceManager)
    /// </summary>
    public enum ResourceType
    {
        Wood,
        Stone,
        Food,
        Magic
    }
}


