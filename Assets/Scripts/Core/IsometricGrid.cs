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
            Debug.Log("[GW] IsometricGrid: Awake() called");
            InitializeGrid();
            CreateTestTiles();
        }
        
        /// <summary>
        /// Initialize the isometric grid with proper settings
        /// </summary>
        private void InitializeGrid()
        {
            Debug.Log("[GW] IsometricGrid: InitializeGrid() called");
            
            if (grid == null)
            {
                Debug.Log("[GW] IsometricGrid: Grid component is null, trying to get it");
                grid = GetComponent<Grid>();
                if (grid == null)
                {
                    Debug.Log("[GW] IsometricGrid: No Grid component found, adding one");
                    grid = gameObject.AddComponent<Grid>();
                }
            }
            
            Debug.Log($"[GW] IsometricGrid: Grid component found: {grid != null}");
            if (grid != null)
            {
                Debug.Log($"[GW] IsometricGrid: Grid component type: {grid.GetType()}");
                
                // Configure grid for isometric layout
                grid.cellSize = cellSize;
                grid.cellGap = cellGap;
                grid.cellLayout = cellLayout;
                grid.cellSwizzle = cellSwizzle;
                
                Debug.Log($"[GW] IsometricGrid: Grid configured - CellSize: {grid.cellSize}, Layout: {grid.cellLayout}");
                Debug.Log("[GW] IsometricGrid: Grid initialized with isometric layout");
            }
            else
            {
                Debug.LogError("[GW] IsometricGrid: Failed to get or create Grid component!");
            }
        }
        
        /// <summary>
        /// Create test tiles for ground layer
        /// </summary>
        private void CreateTestTiles()
        {
            Debug.Log("[GW] IsometricGrid: CreateTestTiles() called");
            Debug.Log($"[GW] IsometricGrid: groundTilemap is null: {groundTilemap == null}");
            Debug.Log($"[GW] IsometricGrid: testGroundTile is null: {testGroundTile == null}");
            
            if (groundTilemap == null || testGroundTile == null)
            {
                Debug.LogWarning("[GW] IsometricGrid: Missing ground tilemap or test tile");
                Debug.LogWarning($"[GW] IsometricGrid: groundTilemap: {groundTilemap}");
                Debug.LogWarning($"[GW] IsometricGrid: testGroundTile: {testGroundTile}");
                return;
            }
            
            Debug.Log($"[GW] IsometricGrid: About to create tiles in {testGridSize * 2 + 1}x{testGridSize * 2 + 1} grid");
            
            if (groundTilemap != null)
            {
                Debug.Log($"[GW] IsometricGrid: Ground tilemap type: {groundTilemap.GetType()}");
            }
            if (testGroundTile != null)
            {
                Debug.Log($"[GW] IsometricGrid: Test ground tile type: {testGroundTile.GetType()}");
            }
            
            int tilesCreated = 0;
            // Create a simple test pattern
            for (int x = -testGridSize; x <= testGridSize; x++)
            {
                for (int y = -testGridSize; y <= testGridSize; y++)
                {
                    Vector3Int tilePosition = new Vector3Int(x, y, 0);
                    if (groundTilemap != null && testGroundTile != null)
                    {
                        groundTilemap.SetTile(tilePosition, testGroundTile);
                        tilesCreated++;
                    }
                }
            }
            
            Debug.Log($"[GW] IsometricGrid: Successfully created {tilesCreated} test tiles");
            Debug.Log($"[GW] IsometricGrid: Created test tiles in {testGridSize * 2 + 1}x{testGridSize * 2 + 1} grid");
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
                    Debug.LogWarning($"[GW] IsometricGrid: Unknown layer '{layerName}'");
                    return null;
            }
        }
    }
}
