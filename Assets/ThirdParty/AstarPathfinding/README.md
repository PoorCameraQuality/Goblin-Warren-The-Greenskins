# A* Pathfinding Project

## Installation Instructions

1. Download the free version from: https://arongranberg.com/astar
2. Import the downloaded package into Unity
3. Move all imported files to this directory: `Assets/ThirdParty/AstarPathfinding/`

## Version
Free Version (Latest)

## Usage in Goblin Warrens
- Used for all unit pathfinding (workers, enemies, hero)
- Integrated with AIManager system
- Supports isometric grid-based movement
- Handles dynamic obstacle avoidance

## Configuration
- Grid Graph: Isometric tile-based navigation
- Node Size: 1.0 (matches tile size)
- Walkable Height: 2.0 (for units)
- Walkable Radius: 0.5 (for unit collision)

## Integration Points
- `AIManager.cs` - Main integration point
- `UnitController.cs` - Individual unit pathfinding
- `WorkerAI.cs` - Worker movement and task execution
- `EnemyAI.cs` - Enemy movement and targeting

## Dependencies
- Unity 2022.x LTS
- .NET 4.x Scripting Runtime

## License
Free version license - see original package for details
