using UnityEngine;

namespace GoblinWarrens.Core
{
    /// <summary>
    /// Simple test script to verify basic Unity functionality works
    /// </summary>
    public class SimpleTest : MonoBehaviour
    {
        private void Awake()
        {
            Debug.Log("[GW] SimpleTest: Awake() called - Basic Unity functionality working!");
            Debug.Log($"[GW] SimpleTest: GameObject name: {gameObject.name}");
            Debug.Log($"[GW] SimpleTest: Transform position: {transform.position}");
        }

        private void Start()
        {
            Debug.Log("[GW] SimpleTest: Start() called - Scene is ready!");
            
            // Test basic Unity features
            Debug.Log($"[GW] SimpleTest: Time.time = {Time.time}");
            Debug.Log($"[GW] SimpleTest: Screen.width = {Screen.width}, Screen.height = {Screen.height}");
            
            // Test if we can find objects
            var cameras = FindObjectsOfType<Camera>();
            Debug.Log($"[GW] SimpleTest: Found {cameras.Length} cameras in scene");
            
            // Test if we can access components
            var myTransform = GetComponent<Transform>();
            if (myTransform != null)
            {
                Debug.Log($"[GW] SimpleTest: Transform component working - position: {myTransform.position}");
            }
            else
            {
                Debug.LogError("[GW] SimpleTest: Transform component not found!");
            }
        }

        private void Update()
        {
            // Simple input test
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("[GW] SimpleTest: SPACE key pressed - Input system is working!");
            }
            
            // Test if we can modify transform
            if (Input.GetKey(KeyCode.T))
            {
                transform.Rotate(0, 1, 0);
                Debug.Log($"[GW] SimpleTest: Rotating - current rotation: {transform.rotation.eulerAngles}");
            }
        }
    }
}
