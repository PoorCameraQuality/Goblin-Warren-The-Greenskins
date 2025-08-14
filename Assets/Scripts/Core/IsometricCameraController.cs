using UnityEngine;

namespace GoblinWarrens.Core
{
    /// <summary>
    /// Controls the isometric camera with edge scrolling and zoom functionality
    /// </summary>
    public class IsometricCameraController : MonoBehaviour
    {
        [Header("Camera Settings")]
        [SerializeField] private Camera targetCamera;
        [SerializeField] private float moveSpeed = 10f;
        [SerializeField] private float zoomSpeed = 5f;
        [SerializeField] private float minZoom = 3f;
        [SerializeField] private float maxZoom = 15f;
        [SerializeField] private float edgeScrollThreshold = 0.1f;
        
        [Header("Boundaries")]
        [SerializeField] private float minX = -50f;
        [SerializeField] private float maxX = 50f;
        [SerializeField] private float minZ = -50f;
        [SerializeField] private float maxZ = 50f;
        
        private Vector3 targetPosition;
        private float targetOrthographicSize;
        
        private void Start()
        {
            if (targetCamera == null)
            {
                targetCamera = GetComponent<Camera>();
                if (targetCamera == null)
                {
                    targetCamera = Camera.main;
                }
            }
            
            // Set initial position and zoom
            targetPosition = transform.position;
            targetOrthographicSize = targetCamera.orthographicSize;
            
            // Configure camera for isometric view
            ConfigureCamera();
            
            Debug.Log("IsometricCameraController: Camera initialized for isometric view");
        }
        
        private void Update()
        {
            HandleInput();
            UpdateCamera();
        }
        
        /// <summary>
        /// Configure camera for isometric perspective
        /// </summary>
        private void ConfigureCamera()
        {
            if (targetCamera == null) return;
            
            // Set to orthographic for isometric view
            targetCamera.orthographic = true;
            
            // Set initial isometric angle (45 degrees)
            transform.rotation = Quaternion.Euler(45f, 0f, 0f);
            
            // Set initial position for good overview
            transform.position = new Vector3(0f, 10f, -10f);
            targetPosition = transform.position;
            
            Debug.Log("IsometricCameraController: Camera configured for isometric view");
        }
        
        /// <summary>
        /// Handle keyboard and mouse input for camera movement
        /// </summary>
        private void HandleInput()
        {
            Vector3 moveDirection = Vector3.zero;
            
            // Keyboard input
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
                moveDirection += Vector3.forward;
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
                moveDirection += Vector3.back;
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
                moveDirection += Vector3.left;
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
                moveDirection += Vector3.right;
            
            // Edge scrolling
            Vector3 mousePosition = Input.mousePosition;
            float screenWidth = Screen.width;
            float screenHeight = Screen.height;
            
            if (mousePosition.x < screenWidth * edgeScrollThreshold)
                moveDirection += Vector3.left;
            else if (mousePosition.x > screenWidth * (1f - edgeScrollThreshold))
                moveDirection += Vector3.right;
                
            if (mousePosition.y < screenHeight * edgeScrollThreshold)
                moveDirection += Vector3.back;
            else if (mousePosition.y > screenHeight * (1f - edgeScrollThreshold))
                moveDirection += Vector3.forward;
            
            // Apply movement
            if (moveDirection != Vector3.zero)
            {
                targetPosition += moveDirection.normalized * moveSpeed * Time.deltaTime;
            }
            
            // Zoom input
            float scrollDelta = Input.GetAxis("Mouse ScrollWheel");
            if (scrollDelta != 0f)
            {
                targetOrthographicSize -= scrollDelta * zoomSpeed;
                targetOrthographicSize = Mathf.Clamp(targetOrthographicSize, minZoom, maxZoom);
            }
        }
        
        /// <summary>
        /// Update camera position and zoom smoothly
        /// </summary>
        private void UpdateCamera()
        {
            // Clamp position to boundaries
            targetPosition.x = Mathf.Clamp(targetPosition.x, minX, maxX);
            targetPosition.z = Mathf.Clamp(targetPosition.z, minZ, maxZ);
            
            // Smooth movement
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 5f);
            
            // Smooth zoom
            if (targetCamera != null)
            {
                targetCamera.orthographicSize = Mathf.Lerp(targetCamera.orthographicSize, targetOrthographicSize, Time.deltaTime * 5f);
            }
        }
        
        /// <summary>
        /// Set camera target position (for following objects)
        /// </summary>
        public void SetTarget(Vector3 target)
        {
            targetPosition = new Vector3(target.x, transform.position.y, target.z);
        }
        
        /// <summary>
        /// Set camera zoom level
        /// </summary>
        public void SetZoom(float zoom)
        {
            targetOrthographicSize = Mathf.Clamp(zoom, minZoom, maxZoom);
        }
        
        /// <summary>
        /// Get current camera bounds in world space
        /// </summary>
        public Bounds GetCameraBounds()
        {
            if (targetCamera == null) return new Bounds();
            
            float height = targetCamera.orthographicSize * 2f;
            float width = height * targetCamera.aspect;
            
            Vector3 center = targetCamera.transform.position;
            Vector3 size = new Vector3(width, 0f, height);
            
            return new Bounds(center, size);
        }
    }
}
