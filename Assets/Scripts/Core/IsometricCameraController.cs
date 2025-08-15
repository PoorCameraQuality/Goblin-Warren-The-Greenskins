using UnityEngine;

namespace GoblinWarrens.Core
{
    /// <summary>
    /// Full-featured isometric camera controller with movement, panning, tilting, and zooming
    /// Compatible with Unity 6000.1.15f1
    /// </summary>
    public class IsometricCameraController : MonoBehaviour
    {
        [Header("Movement Settings")]
        public float moveSpeed = 15f;
        public float panSpeed = 20f;
        public float rotationSpeed = 100f;
        public float tiltSpeed = 80f;
        public float zoomSpeed = 8f;
        
        [Header("Bounds")]
        public float minY = 3f;
        public float maxY = 25f;
        public float minTilt = 20f;
        public float maxTilt = 70f;
        public float minZoom = 2f;
        public float maxZoom = 20f;
        
        [Header("Smooth Movement")]
        public float movementSmoothing = 5f;
        public float rotationSmoothing = 8f;
        public float zoomSmoothing = 6f;
        
        private Camera targetCamera;
        private Vector3 targetPosition;
        private Quaternion targetRotation;
        private float targetOrthographicSize;
        
        // Input state
        private bool isPanning = false;
        private Vector3 lastMousePosition;
        
        private void Start()
        {
            targetCamera = GetComponent<Camera>();
            if (targetCamera == null)
            {
                Debug.LogError("[GW] IsometricCameraController: No Camera component found!");
                return;
            }
            
            // Initialize targets with current values
            targetPosition = transform.position;
            targetRotation = transform.rotation;
            targetOrthographicSize = targetCamera.orthographicSize;
            
            Debug.Log("[GW] IsometricCameraController: Full isometric camera system enabled!");
            Debug.Log("[GW] Controls: WASD = Move, Mouse = Pan/Rotate/Tilt, Scroll = Zoom");
        }
        
        private void Update()
        {
            if (targetCamera == null) return;
            
            HandleMovement();
            HandleMouseControls();
            HandleZoom();
            ApplySmoothMovement();
        }
        
        private void HandleMovement()
        {
            Vector3 moveDirection = Vector3.zero;
            
            // WASD movement
            if (Input.GetKey(KeyCode.W))
                moveDirection += transform.forward;
            if (Input.GetKey(KeyCode.S))
                moveDirection -= transform.forward;
            if (Input.GetKey(KeyCode.A))
                moveDirection -= transform.right;
            if (Input.GetKey(KeyCode.D))
                moveDirection += transform.right;
            if (Input.GetKey(KeyCode.Q))
                moveDirection += Vector3.up;
            if (Input.GetKey(KeyCode.E))
                moveDirection += Vector3.down;
                
            // Apply movement
            if (moveDirection.magnitude > 0)
            {
                Vector3 newPosition = targetPosition + moveDirection * moveSpeed * Time.deltaTime;
                
                // Clamp Y position
                newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);
                
                targetPosition = newPosition;
            }
        }
        
        private void HandleMouseControls()
        {
            // Right-click + drag to rotate and tilt
            if (Input.GetMouseButton(1))
            {
                if (!isPanning)
                {
                    isPanning = true;
                    lastMousePosition = Input.mousePosition;
                }
                
                Vector3 mouseDelta = Input.mousePosition - lastMousePosition;
                
                // Rotate around Y axis (left/right)
                float yawRotation = mouseDelta.x * rotationSpeed * Time.deltaTime;
                targetRotation = Quaternion.Euler(0, yawRotation, 0) * targetRotation;
                
                // Tilt around X axis (up/down)
                float pitchRotation = -mouseDelta.y * tiltSpeed * Time.deltaTime;
                Vector3 currentEuler = targetRotation.eulerAngles;
                float newPitch = currentEuler.x + pitchRotation;
                
                // Clamp pitch to prevent flipping
                if (newPitch > 180f)
                    newPitch -= 360f;
                newPitch = Mathf.Clamp(newPitch, minTilt, maxTilt);
                
                targetRotation = Quaternion.Euler(newPitch, currentEuler.y, currentEuler.z);
                
                lastMousePosition = Input.mousePosition;
            }
            else
            {
                isPanning = false;
            }
            
            // Middle mouse + drag to pan
            if (Input.GetMouseButton(2))
            {
                if (!isPanning)
                {
                    isPanning = true;
                    lastMousePosition = Input.mousePosition;
                }
                
                Vector3 mouseDelta = Input.mousePosition - lastMousePosition;
                
                // Convert screen delta to world movement
                Vector3 panDirection = new Vector3(-mouseDelta.x, 0, -mouseDelta.y);
                Vector3 worldPan = transform.right * panDirection.x + transform.forward * panDirection.z;
                
                targetPosition += worldPan * panSpeed * Time.deltaTime;
                
                // Clamp Y position
                targetPosition.y = Mathf.Clamp(targetPosition.y, minY, maxY);
                
                lastMousePosition = Input.mousePosition;
            }
        }
        
        private void HandleZoom()
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            if (Mathf.Abs(scroll) > 0.01f)
            {
                if (targetCamera.orthographic)
                {
                    // Orthographic camera - adjust size
                    float newSize = targetOrthographicSize - scroll * zoomSpeed;
                    targetOrthographicSize = Mathf.Clamp(newSize, minZoom, maxZoom);
                }
                else
                {
                    // Perspective camera - move forward/back
                    Vector3 zoomDirection = transform.forward * scroll * zoomSpeed;
                    targetPosition += zoomDirection;
                    
                    // Clamp Y position
                    targetPosition.y = Mathf.Clamp(targetPosition.y, minY, maxY);
                }
            }
        }
        
        private void ApplySmoothMovement()
        {
            // Smooth position
            transform.position = Vector3.Lerp(transform.position, targetPosition, movementSmoothing * Time.deltaTime);
            
            // Smooth rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSmoothing * Time.deltaTime);
            
            // Smooth zoom
            if (targetCamera.orthographic)
            {
                targetCamera.orthographicSize = Mathf.Lerp(targetCamera.orthographicSize, targetOrthographicSize, zoomSmoothing * Time.deltaTime);
            }
        }
        
        private void OnGUI()
        {
            // Display controls and camera info on screen
            GUILayout.BeginArea(new Rect(10, 10, 350, 200));
            GUILayout.Label("Isometric Camera Controls", GUI.skin.box);
            GUILayout.Label("WASD - Move Camera");
            GUILayout.Label("Q/E - Move Up/Down");
            GUILayout.Label("Right Click + Drag - Rotate & Tilt");
            GUILayout.Label("Middle Mouse + Drag - Pan");
            GUILayout.Label("Mouse Wheel - Zoom");
            GUILayout.Space(5);
            GUILayout.Label($"Position: {transform.position}");
            GUILayout.Label($"Rotation: {transform.eulerAngles}");
            if (targetCamera != null && targetCamera.orthographic)
            {
                GUILayout.Label($"Orthographic Size: {targetCamera.orthographicSize:F1}");
            }
            GUILayout.EndArea();
        }
        
        // Public methods for external control
        public void SetTargetPosition(Vector3 position)
        {
            targetPosition = position;
            targetPosition.y = Mathf.Clamp(targetPosition.y, minY, maxY);
        }
        
        public void SetTargetRotation(Quaternion rotation)
        {
            targetRotation = rotation;
        }
        
        public void SetTargetZoom(float zoom)
        {
            targetOrthographicSize = Mathf.Clamp(zoom, minZoom, maxZoom);
        }
    }
}
