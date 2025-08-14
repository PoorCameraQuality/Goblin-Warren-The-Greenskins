# Architecture Overview – Goblin Warrens

## Purpose
This document describes the high-level architecture of the Goblin Warrens game.  
It maps how core systems interact, so AI assistants (e.g., Cursor) and developers understand dependencies and data flow.

---

## Core Systems and Their Interactions

### 1. GameManager
**Responsibilities:**
- Initializes all systems on scene load.
- Holds references to main managers (ResourceManager, AIManager, UIManager, EnemyManager).
- Handles game state transitions (Paused, Running, GameOver).

**Interactions:**
- Calls `ResourceManager` to load initial resource totals.
- Signals `UIManager` when resources or state change.
- Updates `AIManager` on time scale changes.

---

### 2. ResourceManager
**Responsibilities:**
- Tracks all player resources.
- Provides APIs for adding/removing resources.
- Notifies UIManager when values change.

**Interactions:**
- Called by `WorkerAI` and `BuildingManager` to deposit/consume resources.
- Used by `EnemyManager` to check raid triggers.

---

### 3. AIManager
**Responsibilities:**
- Controls all unit AI behaviors (workers, enemies, hero).
- Interfaces with the **A* Pathfinding Project** for movement.
- Updates unit states each frame.

**Interactions:**
- Receives unit task assignments from `TaskManager`.
- Reports resource deliveries to `ResourceManager`.
- Signals `CombatManager` when units engage in combat.

---

### 4. TaskManager
**Responsibilities:**
- Stores available jobs (e.g., gather wood, build structure).
- Assigns jobs to free workers based on priority.

**Interactions:**
- Reads resource nodes from `MapManager`.
- Updates `AIManager` to assign tasks to specific workers.
- Called by `UIManager` when player queues construction.

---

### 5. BuildingManager
**Responsibilities:**
- Places and constructs buildings.
- Validates build locations against `MapManager` collision grid.
- Tracks construction progress.

**Interactions:**
- Consumes resources from `ResourceManager` on build start.
- Registers completed buildings with `MapManager`.
- Updates `UIManager` on build progress.

---

### 6. EnemyManager
**Responsibilities:**
- Spawns enemy outposts and raids.
- Determines raid strength based on player progress.

**Interactions:**
- Reads from `ResourceManager` and population count for raid triggers.
- Assigns enemy units to `AIManager` for pathfinding and combat.
- Notifies `UIManager` of raid warnings.

---

### 7. Hero System
**Responsibilities:**
- Manages the Goblin Shaman stats, abilities, and XP.
- Applies buffs/debuffs to friendly units or enemies.

**Interactions:**
- Uses `AIManager` for movement.
- Consumes `Magic` resource from `ResourceManager` for abilities.
- Triggers visual/audio effects via `FXManager`.

---

### 8. MapManager
**Responsibilities:**
- Manages tilemap data and collision layers.
- Provides queries like "IsTileWalkable" and "GetNearestResourceNode".

**Interactions:**
- Used by `BuildingManager` for placement validation.
- Consulted by `AIManager` for pathfinding node setup.

---

### 9. UIManager
**Responsibilities:**
- Displays resource totals, unit counts, and alerts.
- Handles player input for building and unit commands.

**Interactions:**
- Reads from `ResourceManager` for UI updates.
- Sends construction requests to `BuildingManager`.
- Issues unit commands to `AIManager`.

---

### 10. FXManager (Optional Early)
**Responsibilities:**
- Handles all particle effects, sound effects, and animations for events.
- Keeps them decoupled from gameplay logic.

**Interactions:**
- Triggered by `Hero System` for spell effects.
- Triggered by `CombatManager` for battle VFX.

---

## Data Flow Diagram (Simplified)

[UIManager] <--> [GameManager] <--> [ResourceManager]
     |                 |                  ^
     v                 v                  |
[TaskManager] --> [AIManager] --> [MapManager]
     ^                                   |
     |                                   v
[BuildingManager] <--> [EnemyManager] <--> [Hero System]

---

## Notes for AI Coding Assistants
- Always check `/docs/technical-reference.md` for coding standards before implementing anything.
- Maintain **loose coupling**: Managers communicate via public methods/events, not hard-coded dependencies unless necessary.
- Keep gameplay logic and presentation (UI, FX) separate.
- Avoid direct resource changes inside AI or Building scripts—use `ResourceManager` APIs.

---

## MVP Scope in Architecture
For MVP, focus only on:
- `GameManager`
- `ResourceManager`
- `AIManager` (workers only)
- `TaskManager`
- `BuildingManager`
- `UIManager` (basic resource display + construction menu)
