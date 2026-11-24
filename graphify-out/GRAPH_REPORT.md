# Graph Report - Impfany  (2026-11-24)

## Corpus Check
- 76 files · ~72,163 words
- Verdict: corpus is large enough that graph structure adds value.
- Unclassified: 582 file(s) not represented in the graph (top: .meta 401, .asset 100, .shader 14)

## Summary
- 949 nodes · 1407 edges · 66 communities (56 shown, 10 thin omitted)
- Extraction: 95% EXTRACTED · 5% INFERRED · 0% AMBIGUOUS · INFERRED: 67 edges (avg confidence: 0.9)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `bf85866d`
- Run `git rev-parse HEAD` and compare to check if the graph is stale.
- Run `graphify update .` after code changes (no API cost).

## Community Hubs (Navigation)
- unityengine
- manifest.json
- dependencies
- com.unity.2d.common
- GameFlow
- EnemyTracker
- NumericTextPresenter
- packages-lock.json
- dependencies
- com.unity.2d.tilemap
- PlayerHealth
- com.unity.render-pipelines.core
- com.unity.modules.animation
- com.unity.modules.jsonserialize
- Hugo — Übergabe Waffen ↔ Spieler
- com.unity.modules.uielements
- com.unity.modules.jsonserialize
- MeleeConeIndicator
- Weapon
- MeleeConeAttackBehaviour
- WeaponSystem/README.md
- build_diagrams.py
- com.unity.modules.physics2d
- dependencies
- dependencies
- com.unity.modules.imgui
- dependencies
- Erklärung zur KI-Nutzung
- com.unity.test-framework
- com.unity.modules.imageconversion
- Merlin – Flow, UI & Audio (Entwurf zur Durchsicht)
- Jannis — Weapons & upgrades
- WeaponSettings
- com.unity.modules.ui
- Impfany rule-set and overview
- Waffensystem – Systemdiagramme
- Weapons & progression — gameplay decisions
- Next implementation — weapon-owned phases
- WeaponPhase
- com.unity.burst
- dependencies
- com.unity.2d.psdimporter
- com.unity.nuget.mono-cecil
- com.unity.collab-proxy
- com.unity.ide.rider
- com.unity.nuget.newtonsoft-json
- com.unity.modules.audio
- com.unity.modules.umbra
- com.unity.modules.unitywebrequestaudio
- com.unity.modules.androidjni
- com.unity.ide.visualstudio
- com.unity.mathematics
- dependencies
- com.unity.modules.ai
- com.unity.modules.unitywebrequest
- com.unity.modules.wind
- MonoBehaviour
- KI Nutzung Jannis
- hugo-ki-use.md
- Impfany/Handoff-Hugo-Waffen.md
- Impfany/Jannis.md
- Impfany/WeaponsAndProgression.md

## God Nodes (most connected - your core abstractions)
1. `GameFlow` - 33 edges
2. `Weapon` - 24 edges
3. `EnemyMovement` - 20 edges
4. `Impfany.Weapons` - 19 edges
5. `WeaponSettings` - 19 edges
6. `EnemyTracker` - 17 edges
7. `PlayerHealth` - 17 edges
8. `com.unity.modules.jsonserialize` - 17 edges
9. `ItemDebugPanel` - 15 edges
10. `SpawnEnemyScript` - 14 edges

## Surprising Connections (you probably didn't know these)
- `Prefab und Zielen` --references--> `Weapon`  [INFERRED]
  Docs/WeaponSystem/Handoff-Hugo-Waffen.md → ImpfanyGame/Assets/Features/Progression/Scripts/Weapon/Weapon.cs
- `What we are building` --references--> `Weapon`  [INFERRED]
  Docs/WeaponSystem/WeaponImplementationNext.md → ImpfanyGame/Assets/Features/Progression/Scripts/Weapon/Weapon.cs
- `Player/weapon integration — updated design` --references--> `Weapon`  [INFERRED]
  Docs/WeaponSystem/WeaponsAndProgression.md → ImpfanyGame/Assets/Features/Progression/Scripts/Weapon/Weapon.cs
- `Aktueller Aufbau` --references--> `HeartHUD`  [INFERRED]
  Docs/Merlin.md → ImpfanyGame/Assets/Features/GameFlow/Scripts/UI/HUD/HeartHUD.cs
- `Aktueller Aufbau` --references--> `WaveTextPresenter`  [INFERRED]
  Docs/Merlin.md → ImpfanyGame/Assets/Features/GameFlow/Scripts/UI/HUD/WaveTextPresenter.cs

## Import Cycles
- None detected.

## Communities (66 total, 10 thin omitted)

### Community 0 - "unityengine"
Cohesion: 0.06
Nodes (3): Impfany.Weapons, Impfany.Modifiers, PixelArtImportDefaults

### Community 1 - "manifest.json"
Cohesion: 0.04
Nodes (53): com.unity.2d.sprite, com.unity.2d.tilemap, com.unity.modules.animation, com.unity.modules.assetbundle, com.unity.modules.audio, com.unity.modules.director, com.unity.modules.imageconversion, com.unity.modules.imgui (+45 more)

### Community 2 - "dependencies"
Cohesion: 0.04
Nodes (54): dependencies, com.unity.2d.animation, com.unity.2d.aseprite, com.unity.2d.psdimporter, com.unity.2d.sprite, com.unity.2d.spriteshape, com.unity.2d.tilemap, com.unity.2d.tilemap.extras (+46 more)

### Community 3 - "com.unity.2d.common"
Cohesion: 0.18
Nodes (11): depth, source, url, version, dependencies, depth, source, url (+3 more)

### Community 4 - "GameFlow"
Cohesion: 0.09
Nodes (14): Aktueller Aufbau, DeleteCoin, CollectCoin, GameFlow, IsInShop, WaitingForShop, GameStatus, GameOver (+6 more)

### Community 5 - "EnemyTracker"
Cohesion: 0.12
Nodes (4): EnemyHealth, EnemyTracker, SpawnEnemyScript, PlayerSuperPosition

### Community 6 - "NumericTextPresenter"
Cohesion: 0.08
Nodes (13): Direkte Code-Unterstützung, KI-Nutzung – Merlin (Impfany), Mein Arbeitsablauf, Verwendetes Tool, LevelTextPresenter, TargetText, NumericTextPresenter, TargetText (+5 more)

### Community 7 - "packages-lock.json"
Cohesion: 0.05
Nodes (37): com.unity.2d.sprite, com.unity.2d.tilemap, com.unity.modules.animation, com.unity.modules.assetbundle, com.unity.modules.audio, com.unity.modules.director, com.unity.modules.imageconversion, com.unity.modules.imgui (+29 more)

### Community 8 - "dependencies"
Cohesion: 0.14
Nodes (15): dependencies, depth, source, version, dependencies, depth, source, version (+7 more)

### Community 9 - "com.unity.2d.tilemap"
Cohesion: 0.14
Nodes (15): dependencies, depth, dependencies, depth, source, url, version, source (+7 more)

### Community 10 - "PlayerHealth"
Cohesion: 0.06
Nodes (11): AttackPlayer, dropCoin, HeartHUD, CameraFollow, CameraZoom, IPlayerDamageable, PlayerAttack, PlayerHealth (+3 more)

### Community 11 - "com.unity.render-pipelines.core"
Cohesion: 0.09
Nodes (25): depth, source, version, dependencies, depth, source, version, dependencies (+17 more)

### Community 12 - "com.unity.modules.animation"
Cohesion: 0.18
Nodes (11): dependencies, depth, source, url, version, dependencies, depth, source (+3 more)

### Community 13 - "com.unity.modules.jsonserialize"
Cohesion: 0.18
Nodes (11): dependencies, depth, source, version, dependencies, depth, source, version (+3 more)

### Community 14 - "Hugo — Übergabe Waffen ↔ Spieler"
Cohesion: 0.13
Nodes (13): Ablauf, Dash, Hugo — Übergabe Waffen ↔ Spieler, Kleine gemeinsame Schnittstelle, Prefab und Zielen, Vereinbarter Nahkampf — aktueller Umfang, Vorhandene Settings, Wer baut was? (+5 more)

### Community 15 - "com.unity.modules.uielements"
Cohesion: 0.13
Nodes (15): dependencies, depth, source, url, version, depth, source, version (+7 more)

### Community 16 - "com.unity.modules.jsonserialize"
Cohesion: 0.05
Nodes (45): dependencies, depth, source, version, dependencies, depth, source, version (+37 more)

### Community 18 - "Weapon"
Cohesion: 0.23
Nodes (8): Building separately in Unity, Reusable attack behaviours, WeaponSandboxTester, Weapon, CurrentPhase, PhaseElapsed, Settings, WeaponAttackBehaviour

### Community 19 - "MeleeConeAttackBehaviour"
Cohesion: 0.23
Nodes (3): IEnemyDamageable, WeaponDamageDummy, MeleeConeAttackBehaviour

### Community 20 - "WeaponSystem/README.md"
Cohesion: 0.35
Nodes (3): Team folder structure, Systemdiagramme, WeaponSystem – Waffen und Progression

### Community 21 - "build_diagrams.py"
Cohesion: 0.24
Nodes (4): box(), decision(), edge(), text()

### Community 22 - "com.unity.modules.physics2d"
Cohesion: 0.17
Nodes (12): dependencies, depth, source, url, version, dependencies, depth, source (+4 more)

### Community 23 - "dependencies"
Cohesion: 0.18
Nodes (11): dependencies, depth, source, version, dependencies, depth, source, url (+3 more)

### Community 24 - "dependencies"
Cohesion: 0.33
Nodes (6): dependencies, depth, source, version, dependencies, com.unity.modules.accessibility

### Community 25 - "com.unity.modules.imgui"
Cohesion: 0.18
Nodes (11): dependencies, depth, source, version, dependencies, depth, source, version (+3 more)

### Community 26 - "dependencies"
Cohesion: 0.20
Nodes (10): depth, source, version, dependencies, depth, source, url, version (+2 more)

### Community 27 - "Erklärung zur KI-Nutzung"
Cohesion: 0.20
Nodes (9): Angaben zum Projekt, Eigene Leistung, Einen Merge-Konflikt lösen, Erklärung, Erklärung zur KI-Nutzung, Fehlermeldungen verstehen, Syntax und Konzepte erklären lassen, Verwendetes KI-Werkzeug (+1 more)

### Community 28 - "com.unity.test-framework"
Cohesion: 0.33
Nodes (6): dependencies, depth, source, url, version, com.unity.test-framework.performance

### Community 29 - "com.unity.modules.imageconversion"
Cohesion: 0.18
Nodes (11): dependencies, depth, source, version, dependencies, dependencies, depth, source (+3 more)

### Community 30 - "Merlin – Flow, UI & Audio (Entwurf zur Durchsicht)"
Cohesion: 0.22
Nodes (6): Aktueller Button- und Anzeigeweg, Meine Zuständigkeit, Merlin – Flow, UI & Audio (Entwurf zur Durchsicht), Offene Anschlüsse und nächste Schritte, Technische Fundstellen, Dokumentation

### Community 31 - "Jannis — Weapons & upgrades"
Cohesion: 0.40
Nodes (5): How an attack works, How this connects to your work, Jannis — Weapons & upgrades, Planned weapon attributes, Upgrades come next

### Community 32 - "WeaponSettings"
Cohesion: 0.12
Nodes (13): WeaponDebugAttackBehaviour, WeaponPhaseSettings, Duration, LockAim, MovementSpeedMultiplier, WeaponSettings, Attack, AttackAngle (+5 more)

### Community 33 - "com.unity.modules.ui"
Cohesion: 0.20
Nodes (10): dependencies, depth, source, version, dependencies, depth, source, version (+2 more)

### Community 34 - "Impfany rule-set and overview"
Cohesion: 0.25
Nodes (8): Arena:, Enemy's:, Impfany rule-set and overview, Items:, Other Information, Player:, Soul Lantern, Weapons:

### Community 35 - "Waffensystem – Systemdiagramme"
Cohesion: 0.25
Nodes (8): 01 · Aufrufweg – Vom Klick zum Lebenspunkte-Abzug, 02 · Zustände – Ein Aufruf, ein kompletter Ablauf, 03 · Schaden – Ein Gegner bekommt nur einen Treffer, 04 · Geometrie – Eine Form, zwei Verwendungen, 05 · Darstellung – Zielen, treffen, Trefferort zeigen, 06 · Unity & Zusammenarbeit – Zusammenbauen und unabhängig testen, Pflege, Waffensystem – Systemdiagramme

### Community 36 - "Weapons & progression — gameplay decisions"
Cohesion: 0.25
Nodes (8): Agreed weapon behaviour, Cards — from the Overview, Current melee scope — agreed with Hugo, Input buffer and dash — agreed, Player/weapon integration — updated design, Proposed details, Souls and lantern — agreed, Weapons & progression — gameplay decisions

### Community 37 - "Next implementation — weapon-owned phases"
Cohesion: 0.29
Nodes (6): Build in small steps, Documentation entry point, Next implementation — weapon-owned phases, Resume checkpoint — 13 November 2026, Saved work we keep, What we are building

### Community 38 - "WeaponPhase"
Cohesion: 0.33
Nodes (5): WeaponPhase, Attack, Charge, Idle, Recover

### Community 39 - "com.unity.burst"
Cohesion: 0.18
Nodes (12): dependencies, depth, source, url, version, dependencies, depth, source (+4 more)

### Community 40 - "dependencies"
Cohesion: 0.33
Nodes (6): dependencies, depth, source, version, dependencies, com.unity.modules.hierarchycore

### Community 41 - "com.unity.2d.psdimporter"
Cohesion: 0.33
Nodes (6): dependencies, depth, source, url, version, com.unity.2d.psdimporter

### Community 42 - "com.unity.nuget.mono-cecil"
Cohesion: 0.33
Nodes (6): dependencies, depth, source, url, version, com.unity.nuget.mono-cecil

### Community 43 - "com.unity.collab-proxy"
Cohesion: 0.33
Nodes (6): dependencies, depth, source, url, version, com.unity.collab-proxy

### Community 44 - "com.unity.ide.rider"
Cohesion: 0.33
Nodes (6): dependencies, depth, source, url, version, com.unity.ide.rider

### Community 45 - "com.unity.nuget.newtonsoft-json"
Cohesion: 0.33
Nodes (6): dependencies, depth, source, url, version, com.unity.nuget.newtonsoft-json

### Community 46 - "com.unity.modules.audio"
Cohesion: 0.08
Nodes (26): dependencies, depth, source, version, dependencies, depth, source, version (+18 more)

### Community 47 - "com.unity.modules.umbra"
Cohesion: 0.40
Nodes (5): dependencies, depth, source, version, com.unity.modules.umbra

### Community 48 - "com.unity.modules.unitywebrequestaudio"
Cohesion: 0.40
Nodes (5): dependencies, depth, source, version, com.unity.modules.unitywebrequestaudio

### Community 49 - "com.unity.modules.androidjni"
Cohesion: 0.40
Nodes (5): dependencies, depth, source, version, com.unity.modules.androidjni

### Community 50 - "com.unity.ide.visualstudio"
Cohesion: 0.20
Nodes (10): dependencies, depth, source, url, version, depth, source, version (+2 more)

### Community 51 - "com.unity.mathematics"
Cohesion: 0.29
Nodes (7): dependencies, dependencies, depth, source, url, version, com.unity.mathematics

### Community 52 - "dependencies"
Cohesion: 0.33
Nodes (6): dependencies, depth, source, url, version, com.unity.2d.aseprite

### Community 55 - "com.unity.modules.ai"
Cohesion: 0.40
Nodes (5): dependencies, depth, source, version, com.unity.modules.ai

### Community 57 - "com.unity.modules.unitywebrequest"
Cohesion: 0.20
Nodes (10): dependencies, depth, source, version, dependencies, depth, source, version (+2 more)

### Community 59 - "com.unity.modules.wind"
Cohesion: 0.40
Nodes (5): dependencies, depth, source, version, com.unity.modules.wind

### Community 61 - "MonoBehaviour"
Cohesion: 0.06
Nodes (11): EnemyMovement, ItemDebugPanel, ItemBuff, IModifiable, Stat, MoveSpeed, Modifier, StatModifiers (+3 more)

## Knowledge Gaps
- **122 isolated node(s):** `IsInShop`, `WaitingForShop`, `StartMenu`, `Playing`, `Shop` (+117 more)
  These have ≤1 connection - possible missing edges or undocumented components. (Counts symbols only; 540 node(s) total have ≤1 connection when file, concept and rationale nodes are included.)
- **10 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `dependencies` connect `dependencies` to `com.unity.2d.common`, `packages-lock.json`, `dependencies`, `com.unity.2d.tilemap`, `com.unity.render-pipelines.core`, `com.unity.modules.animation`, `com.unity.modules.jsonserialize`, `com.unity.modules.uielements`, `com.unity.modules.jsonserialize`, `com.unity.modules.physics2d`, `dependencies`, `com.unity.modules.imgui`, `dependencies`, `com.unity.test-framework`, `com.unity.modules.imageconversion`, `com.unity.modules.ui`, `com.unity.burst`, `dependencies`, `com.unity.2d.psdimporter`, `com.unity.nuget.mono-cecil`, `com.unity.collab-proxy`, `com.unity.ide.rider`, `com.unity.nuget.newtonsoft-json`, `com.unity.modules.audio`, `com.unity.modules.umbra`, `com.unity.modules.unitywebrequestaudio`, `com.unity.modules.androidjni`, `com.unity.ide.visualstudio`, `com.unity.mathematics`, `dependencies`, `com.unity.modules.ai`, `com.unity.modules.unitywebrequest`, `com.unity.modules.wind`?**
  _High betweenness centrality (0.159) - this node is a cross-community bridge._
- **What connects `IsInShop`, `WaitingForShop`, `StartMenu` to the rest of the system?**
  _122 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `unityengine` be split into smaller, more focused modules?**
  _Cohesion score 0.0632996632996633 - nodes in this community are weakly interconnected._
- **Why does `Weapon` connect `Weapon` to `unityengine`, `WeaponSettings`, `Weapons & progression — gameplay decisions`, `Next implementation — weapon-owned phases`, `WeaponPhase`, `PlayerHealth`, `Hugo — Übergabe Waffen ↔ Spieler`, `MeleeConeIndicator`?**
  _High betweenness centrality (0.042) - this node is a cross-community bridge._
- **Should `manifest.json` be split into smaller, more focused modules?**
  _Cohesion score 0.037037037037037035 - nodes in this community are weakly interconnected._
- **Why does `GameFlow` connect `GameFlow` to `unityengine`, `PlayerHealth`, `EnemyTracker`, `NumericTextPresenter`?**
  _High betweenness centrality (0.030) - this node is a cross-community bridge._
- **Should `dependencies` be split into smaller, more focused modules?**
  _Cohesion score 0.037037037037037035 - nodes in this community are weakly interconnected._