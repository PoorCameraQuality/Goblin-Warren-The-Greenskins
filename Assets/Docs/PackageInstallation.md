# Package Installation Guide

## Unity Registry Packages

### ✅ Already Installed via manifest.json:
- **TextMeshPro** (com.unity.textmeshpro: 3.0.6)
- **2D Tilemap Editor** (com.unity.feature.2d: 2.0.1) - Already included
- **Cinemachine** (com.unity.cinemachine: 2.9.7)

### Installation Steps:
1. Open Unity Package Manager (Window → Package Manager)
2. Select "Unity Registry" from the dropdown
3. Search for and install:
   - TextMeshPro (if not already installed)
   - Cinemachine (if not already installed)

## Asset Store Packages

### Required Asset Store Imports:

#### 1. Isometric Camera Controller
- **Location**: Unity Asset Store
- **Search**: "Isometric Camera Controller"
- **Installation**: 
  1. Download from Asset Store
  2. Import into project
  3. Move to `Assets/ThirdParty/IsometricCamera/` (create folder if needed)

#### 2. Isometric 2.5D Toolset
- **Location**: Unity Asset Store
- **Search**: "Isometric 2.5D Toolset"
- **Installation**:
  1. Download from Asset Store
  2. Import into project
  3. Move to `Assets/ThirdParty/IsometricToolset/` (create folder if needed)

## External Downloads

### 1. A* Pathfinding Project (Free)
- **URL**: https://arongranberg.com/astar
- **Version**: Free version
- **Installation**:
  1. Download the free version
  2. Import the .unitypackage file
  3. Move all files to `Assets/ThirdParty/AstarPathfinding/`
  4. Follow the setup wizard if prompted

### 2. Infinite Isometric Terrain Generator
- **URL**: GitHub repository
- **Installation**:
  1. Download/clone from GitHub
  2. Import the package into Unity
  3. Move all files to `Assets/ThirdParty/InfiniteIsoTerrain/`

## Post-Installation Setup

### TextMeshPro Setup
1. Open Window → TextMeshPro → Import TMP Essential Resources
2. Import TMP Examples and Extras (optional)

### A* Pathfinding Setup
1. Open Window → A* Pathfinding Project → Graph Editor
2. Create a new Grid Graph
3. Configure for isometric movement:
   - Node Size: 1.0
   - Walkable Height: 2.0
   - Walkable Radius: 0.5
   - Isometric: Enabled

### Cinemachine Setup
1. Create a Virtual Camera in your scene
2. Configure for isometric view
3. Set up camera follow targets

## Package Dependencies

### Core Dependencies
- Unity 2022.x LTS
- .NET 4.x Scripting Runtime
- Input System (already installed)

### Package Compatibility
- TextMeshPro: Compatible with Unity 2022.x
- Cinemachine: Compatible with Unity 2022.x
- A* Pathfinding: Compatible with Unity 2022.x
- 2D Tilemap: Already included in Unity 2022.x

## Troubleshooting

### Common Issues:
1. **Package Import Errors**: Ensure Unity version compatibility
2. **Missing Dependencies**: Check manifest.json for required packages
3. **Asset Store Import Issues**: Try re-downloading the package
4. **A* Pathfinding Setup**: Follow the official documentation for grid setup

### Version Conflicts:
- All packages are tested with Unity 2022.x LTS
- If conflicts occur, check package version compatibility
- Update packages to latest compatible versions

## Integration Notes

### Architecture Integration:
- All packages will integrate with the existing architecture
- Follow the technical reference for proper integration
- Use the established folder structure for organization

### Performance Considerations:
- A* Pathfinding: Optimize graph updates for large maps
- Cinemachine: Use appropriate virtual camera settings
- TextMeshPro: Use appropriate font assets for performance
