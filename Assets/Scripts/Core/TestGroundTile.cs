using UnityEngine;
using UnityEngine.Tilemaps;

namespace GoblinWarrens.Core
{
    /// <summary>
    /// Simple test ground tile for isometric grid
    /// </summary>
    [CreateAssetMenu(fileName = "TestGroundTile", menuName = "GoblinWarrens/Tiles/TestGroundTile")]
    public class TestGroundTile : Tile
    {
        private void Awake()
        {
            // Configure tile for isometric grid
            this.sprite = CreateTestSprite();
            this.colliderType = Tile.ColliderType.None; // No collision for ground tiles
        }
        
        private Sprite CreateTestSprite()
        {
            // Create a simple colored square sprite
            Texture2D texture = new Texture2D(64, 64);
            Color groundColor = new Color(0.4f, 0.6f, 0.3f, 1f); // Green-brown
            
            for (int x = 0; x < 64; x++)
            {
                for (int y = 0; y < 64; y++)
                {
                    texture.SetPixel(x, y, groundColor);
                }
            }
            
            texture.Apply();
            return Sprite.Create(texture, new Rect(0, 0, 64, 64), new Vector2(0.5f, 0.5f), 64f);
        }
    }
}
