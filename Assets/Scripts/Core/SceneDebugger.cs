using UnityEngine;

namespace GoblinWarrens.Core
{
    /// <summary>
    /// Simple debug script to verify scene loading and component initialization
    /// </summary>
    public class SceneDebugger : MonoBehaviour
    {
        private void Awake()
        {
            Debug.Log("[GW] SceneDebugger: Awake() called - Scene is loading!");
            Debug.Log($"[GW] SceneDebugger: GameObject name: {gameObject.name}");
            Debug.Log($"[GW] SceneDebugger: Scene name: {UnityEngine.SceneManagement.SceneManager.GetActiveScene().name}");
        }

        private void Start()
        {
            Debug.Log("[GW] SceneDebugger: Start() called - Scene is ready!");
            
            // Check for key components
            var mainCamera = Camera.main;
            Debug.Log($"[GW] SceneDebugger: Main Camera found: {mainCamera != null}");
            if (mainCamera != null)
            {
                Debug.Log($"[GW] SceneDebugger: Main Camera name: {mainCamera.name}");
                Debug.Log($"[GW] SceneDebugger: Main Camera position: {mainCamera.transform.position}");
                
                var cameraController = mainCamera.GetComponent<IsometricCameraController>();
                Debug.Log($"[GW] SceneDebugger: IsometricCameraController found: {cameraController != null}");
            }
            
            var isometricGrid = FindObjectOfType<IsometricGrid>();
            Debug.Log($"[GW] SceneDebugger: IsometricGrid found: {isometricGrid != null}");
            if (isometricGrid != null)
            {
                Debug.Log($"[GW] SceneDebugger: IsometricGrid name: {isometricGrid.name}");
                Debug.Log($"[GW] SceneDebugger: IsometricGrid position: {isometricGrid.transform.position}");
            }
            
            // Check for tilemaps - use safe method for older Unity versions
            try
            {
                var tilemaps = FindObjectsOfType<UnityEngine.Tilemaps.Tilemap>();
                Debug.Log($"[GW] SceneDebugger: Found {tilemaps.Length} tilemaps in scene");
                foreach (var tilemap in tilemaps)
                {
                    if (tilemap != null)
                    {
                        Debug.Log($"[GW] SceneDebugger: Tilemap: {tilemap.name} on layer {tilemap.gameObject.layer}");
                    }
                }
            }
            catch (System.Exception e)
            {
                Debug.LogWarning($"[GW] SceneDebugger: Could not find tilemaps - {e.Message}");
            }
        }

        private void Update()
        {
            // Simple input test
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("[GW] SceneDebugger: SPACE key pressed - Input system is working!");
            }
        }
    }
}
