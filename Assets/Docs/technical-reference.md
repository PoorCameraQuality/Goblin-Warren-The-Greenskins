# Technical Reference – Goblin Warrens

## Project Overview
- **Game Type:** Single-player isometric colony simulator with RTS elements.
- **Engine:** Unity `2022.x LTS` (Lock this version to prevent compatibility issues.)
- **Language:** C# (Microsoft .NET 4.x Equivalent Runtime)
- **Rendering Pipeline:** Built-in RP *(change if using URP/HDRP)*  
- **Art Style:** 2D isometric (tilemap-based) with modular sprites.
- **Target Platform:** Windows PC (possible tablet support later).

---

## Architecture Standards
- **Folder Structure**
  ```
  /Assets
    /Art
    /Audio
    /Prefabs
    /Scenes
    /Scripts
      /Core
      /AI
      /Buildings
      /UI
      /Managers
    /ScriptableObjects
    /ThirdParty
    /Docs
  ```
- **Naming Conventions**
  - Classes: `PascalCase` (e.g., `WorkerAI`, `ResourceManager`)
  - Methods: `PascalCase` (e.g., `AssignTask()`)
  - Variables: `camelCase` (e.g., `currentTask`)
  - Constants: `ALL_CAPS` (e.g., `MAX_POPULATION`)
  - Private fields: `_camelCase` (e.g., `_currentHealth`)

---

## Core Systems

### 1. Camera
- Isometric perspective.
- Uses **Isometric Camera Controller** from Asset Store.
- Features: WASD/Arrow movement, mouse edge scrolling, scroll-wheel zoom.

### 2. Tilemap & Terrain
- Unity Tilemap for ground + obstacles.
- Infinite Isometric Terrain Generator (placeholder until final maps).
- Layers: `Ground`, `Buildings`, `Decor`, `Collision`.

### 3. Pathfinding & AI
- **A* Pathfinding Project (Free)** – Grid Graph with isometric coordinates.
- All moving entities inherit from a `Unit` base class.
- Worker AI runs state machine: `Idle → MoveToTarget → PerformTask → ReturnResources`.

### 4. Resource System
- Central `ResourceManager` tracks:
  ```csharp
  public enum ResourceType { Wood, Stone, Food, Magic }
  Dictionary<ResourceType, int> resourceTotals;
  ```
- Resources stored in ScriptableObjects for balance tweaking.

### 5. Workers
- **Goblin** – Standard worker.
- **Foblin** – Low-skill, half production speed.
- Worker tasks:
  - Gather resource → Deposit to stockpile
  - Build structure
  - Idle (await assignment)

### 6. Enemy AI
- Outposts spawn based on player actions.
- Raids triggered by:
  - Population threshold
  - Resource hoarding (Wood/Stone/Food)
- Enemy units use same A* pathfinding.

### 7. Hero Unit
- `GoblinShaman` – Support unit with spells.
- Gains XP from kills, construction boosts, or sacrifices.
- Example Abilities:
  - **Chant of Haste** – Boost build speed.
  - **Dark Harvest** – Converts death into magic resource.

---

## ScriptableObjects
Use for:
- Building definitions
- Resource data
- Enemy unit stats
- Tech tree entries

Example:
```csharp
[CreateAssetMenu(fileName="NewBuilding", menuName="Buildings/BuildingData")]
public class BuildingData : ScriptableObject {
    public string buildingName;
    public int woodCost;
    public int stoneCost;
    public float buildTime;
}
```

---

## UI Standards
- Top bar: Resource totals.
- Left panel: Construction menu.
- Notifications: Pop-up text (avoid modal blocking popups in gameplay).
- Use Unity’s **UI Toolkit** or Canvas system.

---

## Third-Party Assets Used
- **Isometric Camera Controller** – Unity Asset Store  
- **Isometric 2.5D Toolset** – Unity Asset Store  
- **A* Pathfinding Project (Free)** – https://arongranberg.com/astar  
- **Infinite Isometric Terrain Generator** – GitHub  

---

## Coding Rules for Cursor AI
- Always reference Unity API: https://docs.unity3d.com/ScriptReference/
- Only use Unity’s built-in namespaces unless otherwise stated.
- Follow existing patterns in this doc.
- Do not invent APIs—use tested Unity features.
- Never remove working code unless explicitly told.
- Keep methods small and focused (≤ 30 lines when possible).
- Always compile without errors before returning code.

---

## MVP Feature Scope
- Building placement & resource harvesting.
- Worker assignment & AI.
- Simple raids triggered by player growth.
- Hero with one working ability.
- Basic UI showing resources and construction.

---

## Testing & Error Prevention
- After any code change:
  - Press `Ctrl+Shift+B` to build scripts and check for compile errors.
  - If compile error → fix immediately before continuing.
- All new systems should have **Debug.Log()** markers for testing.
- Use Unity’s Play Mode Tests for verifying AI states.

---

## Prompting Cursor Example
When asking Cursor for help, use:
> “Edit `/Scripts/AI/WorkerAI.cs` to add a `FindNearestResource()` method. Use `ResourceManager` for resource data. Ensure code follows `technical-reference.md` and Unity API docs. No compile errors.”
