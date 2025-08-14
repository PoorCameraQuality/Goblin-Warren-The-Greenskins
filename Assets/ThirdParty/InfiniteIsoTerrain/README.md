# Infinite Isometric Terrain Generator

## Installation Instructions

1. Download from GitHub: [Infinite Isometric Terrain Generator](https://github.com/search?q=infinite+isometric+terrain+generator)
2. Import the downloaded package into Unity
3. Move all imported files to this directory: `Assets/ThirdParty/InfiniteIsoTerrain/`

## Version
Latest GitHub version

## Usage in Goblin Warrens
- Procedural terrain generation for infinite world
- Isometric tile-based terrain system
- Dynamic chunk loading and unloading
- Biome-based terrain variation

## Configuration
- Chunk Size: 16x16 tiles
- Tile Size: 1.0 units
- Isometric Angle: 45 degrees
- Generation Seed: Configurable for consistent worlds

## Integration Points
- `TerrainManager.cs` - Main terrain system integration
- `ChunkLoader.cs` - Dynamic chunk management
- `BiomeSystem.cs` - Biome-based terrain generation
- `ResourceSpawner.cs` - Resource placement on terrain

## Features
- Procedural terrain generation
- Multiple biome types (forest, mountain, water, etc.)
- Resource node placement
- Building placement validation
- Performance optimized chunk loading

## Dependencies
- Unity 2022.x LTS
- .NET 4.x Scripting Runtime
- 2D Tilemap Editor (already included)

## License
GitHub license - see original repository for details
