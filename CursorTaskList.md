# Goblin Warrens – Cursor Task List (Unity 6000.2.0)

This checklist is designed for step‑by‑step development with Cursor.  
For each task, copy the **Prompt** block into Cursor. Keep `/docs/technical-reference.md` and `/docs/ArchitectureOverview.md` in your repo and reference them in every prompt.

---

## Pre‑Setup (do manually)
- [ ] Create a new **Unity 6000.2.0f1** 2D project.
- [ ] Install required assets:
  - Isometric Camera Controller (Unity Asset Store)
  - Isometric 2.5D Toolset (Unity Asset Store)
  - A* Pathfinding Project (Free) – https://arongranberg.com/astar
  - Infinite Isometric Terrain Generator – GitHub
- [ ] Add `/docs/technical-reference.md` and `/docs/ArchitectureOverview.md` to the repo.
- [ ] Initialize Git and make the first commit.
- [ ] In Unity Hub, set the project’s Unity version to **6000.2.0f1** and disable auto‑upgrade.

---

## Task 1 – Folder Structure
**Goal:** Create the canonical folder layout from the technical reference.
**Prompt:**
```
Create the Unity folder structure described in `/docs/technical-reference.md`:
/Assets/Art, /Assets/Audio, /Assets/Prefabs, /Assets/Scenes, /Assets/Scripts/{Core,AI,Buildings,UI,Managers}, /Assets/ScriptableObjects, /Assets/ThirdParty, /Assets/Docs.
Return only the file/directory operations needed (no placeholders that cause compile errors).
```

---

## Task 2 – GameManager
**Goal:** Central bootstrapper for managers and game state.
**Prompt:**
```
Create `/Assets/Scripts/Core/GameManager.cs`.
Singleton MonoBehaviour. Initializes ResourceManager, UIManager, AIManager, EnemyManager on Start.
Implements simple game states (Paused, Running). 
Read and follow `/docs/technical-reference.md` and `/docs/ArchitectureOverview.md`.
Return complete, compiling C# code with all using statements.
```

---

## Task 3 – ResourceManager
**Goal:** Centralized resource accounting with events.
**Prompt:**
```
Create `/Assets/Scripts/Managers/ResourceManager.cs`.
Enum ResourceType { Wood, Stone, Food, Magic }.
Dictionary<ResourceType,int> totals. Public AddResource, RemoveResource, GetResourceAmount.
Raise C# event OnResourceChanged(ResourceType type, int newValue).
Follow the repo docs. Compile with zero errors.
```

---

## Task 4 – UIManager (Resource Bar)
**Goal:** Live update resource UI.
**Prompt:**
```
Create `/Assets/Scripts/UI/UIManager.cs`.
Subscribes to ResourceManager.OnResourceChanged and updates TextMeshPro labels for each resource.
Expose serialized fields for TMP references; do not create the UI here.
No gameplay logic in UI. Follow repo docs. Compile cleanly.
```

---

## Task 5 – MapManager + Infinite Terrain
**Goal:** Tilemap + infinite isometric terrain access.
**Prompt:**
```
Create `/Assets/Scripts/Managers/MapManager.cs`.
Integrate the Infinite Isometric Terrain Generator with Unity Tilemap.
Expose: bool IsTileWalkable(Vector3Int cell), Vector3Int GetNearestResourceNode(ResourceType type, Vector3Int fromCell).
Document assumptions. Follow ArchitectureOverview. Compile cleanly.
```

---

## Task 6 – AIManager (A* integration)
**Goal:** Central unit registry and movement helpers.
**Prompt:**
```
Create `/Assets/Scripts/Managers/AIManager.cs`.
Integrate A* Pathfinding Project (GridGraph for isometric). 
Methods: RegisterUnit(Unit u), UnregisterUnit(Unit u), MoveUnitTo(Unit u, Vector3 worldTarget).
Provide minimal A* setup accessors. No compile errors.
```

---

## Task 7 – Unit Base Class
**Goal:** Shared stats + movement API.
**Prompt:**
```
Create `/Assets/Scripts/AI/Unit.cs` (abstract).
Fields: maxHealth, currentHealth, moveSpeed. 
Method: MoveTo(Vector3 worldTarget) that delegates to AIManager/A*.
Virtual methods: OnSpawn(), OnDeath(). Compile without errors.
```

---

## Task 8 – WorkerAI (state machine)
**Goal:** Basic gather/build loop.
**Prompt:**
```
Create `/Assets/Scripts/AI/WorkerAI.cs` inheriting Unit.
States: Idle, MoveToTarget, PerformTask, ReturnResources.
Implements: AssignGatherTask(ResourceType type), AssignBuildTask(BuildingData building, Vector3Int cell).
Uses MapManager for resource node lookup; deposits via ResourceManager.
Add clear Debug.Log with [GW] prefix. Follow repo docs. No compile errors.
```

---

## Task 9 – TaskManager (job board)
**Goal:** Centralized job queue with priorities.
**Prompt:**
```
Create `/Assets/Scripts/Managers/TaskManager.cs`.
Data model for Job { type, priority, targetCell, payload }.
APIs: AddJob(Job j), TryAssignNextJob(WorkerAI worker), GetPendingJobs().
Integrates with AIManager to route workers. Compile cleanly.
```

---

## Task 10 – BuildingManager (placement & construction)
**Goal:** Place buildings, consume resources, build over time.
**Prompt:**
```
Create `/Assets/Scripts/Managers/BuildingManager.cs`.
Validate placement with MapManager; on placement, deduct cost via ResourceManager.
Track construction progress; on complete, register structure with MapManager.
Public: TryPlaceBuilding(BuildingData data, Vector3Int cell). Compile with zero errors.
```

---

## Task 11 – EnemyManager (outposts & raids)
**Goal:** Spawn enemy outposts and trigger raids.
**Prompt:**
```
Create `/Assets/Scripts/Managers/EnemyManager.cs`.
Spawn simple enemy outposts. Trigger raids when thresholds exceeded (population or resource totals).
Issue move/attack commands via AIManager. No compile errors.
```

---

## Task 12 – Hero System (Goblin Shaman)
**Goal:** One hero with one working ability.
**Prompt:**
```
Create `/Assets/Scripts/AI/HeroAI.cs` inheriting Unit.
Implements XP gain and one ability: ChantOfHaste(duration, percent).
Ability temporarily increases worker build speed. Uses ResourceManager for magic cost.
Provide simple cooldown. Compile cleanly.
```

---

## Task 13 – UI: Build Menu
**Goal:** Player can request constructions.
**Prompt:**
```
Extend `/Assets/Scripts/UI/UIManager.cs` to add build menu hook-ups.
Emit construction requests to BuildingManager (no gameplay logic in UI).
Use serialized references for buttons/prefabs. Minimal stubs are OK; compile cleanly.
```

---

## Task 14 – MVP Test Scene & Hooks
**Goal:** Verifiable end-to-end loop.
**Prompt:**
```
Create a `SetupTestScene()` helper (editor-only or runtime guarded) that:
- Spawns camera + isometric controller.
- Creates a small tilemap area.
- Spawns one stockpile building prefab, one worker, and a few resource nodes.
- Demonstrates loop: worker gathers → deposits → builds → UI updates.
Add [GW] Debug.Log at key steps. Compile cleanly.
```

---

## MVP Exit Criteria
- [ ] Place a building and deduct resources.
- [ ] Worker gathers and deposits resources.
- [ ] Resource totals update in the UI.
- [ ] Enemy raid triggers after a defined threshold.
- [ ] Hero casts one working ability that affects build speed.

---

## Tips for Cursor Prompts
- Always include: “Follow `/docs/technical-reference.md` and `/docs/ArchitectureOverview.md`. Target Unity **6000.2.0f1**. No compile errors.”
- Keep each request focused on **one file or system**.
- If a change needs extra files, have Cursor **list them first** and confirm.
