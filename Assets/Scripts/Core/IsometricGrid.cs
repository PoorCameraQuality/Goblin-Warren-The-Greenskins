using UnityEngine;
using UnityEngine.Tilemaps;

namespace GoblinWarrens.Core
{
    /// <summary>
    /// Manages the isometric grid system for the game world
    /// </summary>
    public class IsometricGrid : MonoBehaviour
    {
        [Header("Grid Configuration")]
        [SerializeField] private Grid grid;
        [SerializeField] private Tilemap groundTilemap;
        [SerializeField] private Tilemap buildingTilemap;
        [SerializeField] private Tilemap resourceTilemap;
        
        [Header("Grid Settings")]
        [SerializeField] private Vector3 cellSize = new Vector3(1f, 0.5f, 1f);
        [SerializeField] private Vector3 cellGap = Vector3.zero;
        [SerializeField] private GridLayout.CellLayout cellLayout = GridLayout.CellLayout.Isometric;
        [SerializeField] private GridLayout.CellSwizzle cellSwizzle = GridLayout.CellSwizzle.XYZ;
        
        [Header("Test Configuration")]
        [SerializeField] private TileBase testGroundTile;
        [SerializeField] private int testGridSize = 10;
        
        private void Awake()
        {
            InitializeGrid();
            CreateTestTiles();
        }
        
        /// <summary>
        /// Initialize the isometric grid with proper settings
        /// </summary>
        private void InitializeGrid()
        {
            if (grid == null)
            {
                grid = GetComponent<Grid>();
                if (grid == null)
                {
                    grid = gameObject.AddComponent<Grid>();
                }
            }
            
            // Configure grid for isometric layout
            grid.cellSize = cellSize;
            grid.cellGap = cellGap;
            grid.cellLayout = cellLayout;
            grid.cellSwizzle = cellSwizzle;
            
            Debug.Log("IsometricGrid: Grid initialized with isometric layout");
        }
        
        /// <summary>
        /// Create test tiles for ground layer
        /// </summary>
        private void CreateTestTiles()
        {
            if (groundTilemap == null || testGroundTile == null)
            {
                Debug.LogWarning("IsometricGrid: Missing ground tilemap or test tile");
                return;
            }
            
            // Create a simple test pattern
            for (int x = -testGridSize; x <= testGridSize; x++)
            {
                for (int y = -testGridSize; y <= testGridSize; y++)
                {
                    Vector3Int tilePosition = new Vector3Int(x, y, 0);
                    groundTilemap.SetTile(tilePosition, testGroundTile);
                }
            }
            
            Debug.Log($"IsometricGrid: Created test tiles in {testGridSize * 2 + 1}x{testGridSize * 2 + 1} grid");
        }
        
        /// <summary>
        /// Convert world position to grid coordinates
        /// </summary>
        public Vector3Int WorldToGrid(Vector3 worldPosition)
        {
            return grid.WorldToCell(worldPosition);
        }
        
        /// <summary>
        /// Convert grid coordinates to world position
        /// </summary>
        public Vector3 GridToWorld(Vector3Int gridPosition)
        {
            return grid.CellToWorld(gridPosition);
        }
        
        /// <summary>
        /// Check if a grid position is walkable
        /// </summary>
        public bool IsWalkable(Vector3Int gridPosition)
        {
            // Check if there's a ground tile
            if (groundTilemap.GetTile(gridPosition) == null)
                return false;
                
            // Check if there's a building blocking the path
            if (buildingTilemap.GetTile(gridPosition) != null)
                return false;
                
            return true;
        }
        
        /// <summary>
        /// Get the tilemap for a specific layer
        /// </summary>
        public Tilemap GetTilemap(string layerName)
        {
            switch (layerName.ToLower())
            {
                case "ground":
                    return groundTilemap;
                case "building":
                    return buildingTilemap;
                case "resource":
                    return resourceTilemap;
                default:
                    Debug.LogWarning($"IsometricGrid: Unknown layer '{layerName}'");
                    return null;
            }
        }
    }
}
