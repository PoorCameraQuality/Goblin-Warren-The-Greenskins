# Goblin Warrens - Unity Project

## Project Overview
A single-player isometric colony simulator with RTS elements, built in Unity 2022.x LTS.

## Project Structure

### Assets Organization
```
/Assets
  /Art              # 2D sprites, textures, and visual assets
  /Audio            # Sound effects and music
  /Prefabs          # Reusable game objects
  /Scenes           # Unity scene files
    Main.unity      # Default game scene
  /Scripts          # C# scripts organized by system
    /Core           # Core game systems and base classes
    /AI             # AI behavior and pathfinding
    /Buildings      # Building system and construction
    /UI             # User interface scripts
    /Managers       # Game managers and controllers
  /ScriptableObjects # Game data and configuration
  /ThirdParty       # Third-party assets and plugins
  /Docs             # Project documentation
```

### Key Documentation
- `technical-reference.md` - Technical specifications and coding standards
- `ArchitectureOverview.md` - System architecture and data flow
- `README.md` - This file

## Development Setup

### Prerequisites
- Unity 2022.x LTS
- .NET 4.x Scripting Runtime
- Git LFS for large assets

### Project Settings
- **Scripting Runtime**: .NET 4.x
- **API Compatibility**: .NET Framework
- **Input Handling**: Both (Old + New Input System)
- **Transparency Sort**: Custom Axis (x=0, y=1, z=0) for isometric depth sorting
- **Fixed Timestep**: 0.02 (50Hz)

### Version Control
- Git repository with comprehensive .gitignore
- Git LFS for large assets (PNG, PSD, WAV, MP3, FBX, UnityPackage)
- Conventional commit messages

## Architecture

### Core Systems
1. **GameManager** - Main game controller and system initialization
2. **ResourceManager** - Resource tracking and management
3. **AIManager** - Unit AI and pathfinding (A* integration)
4. **TaskManager** - Job assignment and task management
5. **BuildingManager** - Construction and building placement
6. **EnemyManager** - Enemy spawning and raid mechanics
7. **UIManager** - User interface and input handling

### Key Features
- Isometric camera with edge scrolling and zoom
- A* pathfinding for all moving entities
- Resource gathering and management
- Building construction system
- Worker AI with state machine
- Enemy raid system
- Hero unit with abilities

## Development Guidelines

### Coding Standards
- Follow PascalCase for classes and methods
- Use camelCase for variables
- Keep methods small and focused (≤30 lines)
- Use ScriptableObjects for game data
- Maintain loose coupling between systems

### Asset Management
- Use Git LFS for large assets
- Follow Unity naming conventions
- Organize assets in appropriate folders
- Use meta files for asset configuration

## Getting Started

1. Clone the repository
2. Open in Unity 2022.x LTS
3. Open `Assets/Scenes/Main.unity`
4. Review documentation in `Assets/Docs/`
5. Start development following the architecture guidelines

## Third-Party Assets
- **Isometric Camera Controller** - Unity Asset Store
- **Isometric 2.5D Toolset** - Unity Asset Store  
- **A* Pathfinding Project** - Free version
- **Infinite Isometric Terrain Generator** - GitHub

## License
This project follows the Unity license terms for the game engine and third-party assets.
