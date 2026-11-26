# Graph Report - Impfany  (2026-11-26)

## Corpus Check
- 85 files · ~109,179 words
- Verdict: corpus is large enough that graph structure adds value.
- Unclassified: 1108 file(s) not represented in the graph (top: .meta 905, .asset 100, .prefab 23)

## Summary
- 1026 nodes · 1570 edges · 76 communities (60 shown, 16 thin omitted)
- Extraction: 95% EXTRACTED · 5% INFERRED · 0% AMBIGUOUS · INFERRED: 76 edges (avg confidence: 0.89)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `103c5594`
- Run `git rev-parse HEAD` and compare to check if the graph is stale.
- Run `graphify update .` after code changes (no API cost).

## Community Hubs (Navigation)
- unityengine
- manifest.json
- dependencies
- com.unity.2d.common
- GameFlow
- com.unity.2d.sprite
- NumericTextPresenter
- packages-lock.json
- dependencies
- com.unity.2d.tilemap
- PlayerHealth
- com.unity.render-pipelines.core
- com.unity.modules.animation
- com.unity.modules.jsonserialize
- Hugo — Übergabe Waffen ↔ Spieler
- Merlin – Flow, UI & Audio (Entwurf zur Durchsicht)
- com.unity.modules.cloth
- MeleeConeIndicator
- Weapon
- MeleeConeAttackBehaviour
- WeaponSystem/README.md
- build_diagrams.py
- com.unity.modules.physics2d
- dependencies
- com.unity.collections
- com.unity.modules.uielements
- com.unity.test-framework
- Erklärung zur KI-Nutzung
- Impfany rule-set and overview
- dependencies
- EnemyTracker
- Jannis — Weapons & upgrades
- WeaponSettings
- com.unity.modules.ui
- Waffensystem – Systemdiagramme
- CameraZoom
- Weapons & progression — gameplay decisions
- EnemyTracker
- com.unity.modules.subsystems
- MonoBehaviour
- com.unity.modules.terrain
- EnemyMovement
- EnemyHitFlash
- com.unity.collab-proxy
- com.unity.ide.rider
- Hugo — Übergabe Waffen ↔ Spieler
- com.unity.modules.audio
- PlayerAnimator
- com.unity.modules.unitywebrequestaudio
- com.unity.modules.imgui
- com.unity.modules.ui
- com.unity.mathematics
- dependencies
- com.unity.modules.ai
- com.unity.modules.physics
- com.unity.modules.umbra
- Next implementation — weapon-owned phases
- com.unity.modules.unitywebrequest
- com.unity.modules.vehicles
- com.unity.modules.wind
- com.unity.modules.vr
- ItemDebugPanel
- KI Nutzung Jannis
- hugo-ki-use.md
- com.unity.modules.xr
- com.unity.timeline
- MonoBehaviour
- Impfany · Lerngraph für Experience Engineering
- com.unity.modules.imageconversion
- Impfany/Handoff-Hugo-Waffen.md
- Impfany/Jannis.md
- Impfany/WeaponsAndProgression.md
- WeaponDebugAttackBehaviour

## God Nodes (most connected - your core abstractions)
1. `GameFlow` - 44 edges
2. `EnemyMovement` - 30 edges
3. `Weapon` - 28 edges
4. `Impfany.Weapons` - 23 edges
5. `WeaponSettings` - 22 edges
6. `PlayerHealth` - 19 edges
7. `ItemDebugPanel` - 18 edges
8. `EnemyTracker` - 17 edges
9. `SpawnEnemyScript` - 17 edges
10. `NumericTextPresenter` - 17 edges

## Surprising Connections (you probably didn't know these)
- `Prefab und Zielen` --references--> `Weapon`  [INFERRED]
  Docs/WeaponSystem/Handoff-Hugo-Waffen.md → ImpfanyGame/Assets/Features/Weapons/Scripts/Weapon.cs
- `What we are building` --references--> `Weapon`  [INFERRED]
  Docs/WeaponSystem/WeaponImplementationNext.md → ImpfanyGame/Assets/Features/Weapons/Scripts/Weapon.cs
- `Player/weapon integration — updated design` --references--> `Weapon`  [INFERRED]
  Docs/WeaponSystem/WeaponsAndProgression.md → ImpfanyGame/Assets/Features/Weapons/Scripts/Weapon.cs
- `Aktueller Aufbau` --references--> `HeartHUD`  [INFERRED]
  Docs/Merlin.md → ImpfanyGame/Assets/Features/GameFlow/Scripts/UI/HUD/HeartHUD.cs
- `Aktueller Aufbau` --references--> `WaveTextPresenter`  [INFERRED]
  Docs/Merlin.md → ImpfanyGame/Assets/Features/GameFlow/Scripts/UI/HUD/WaveTextPresenter.cs

## Import Cycles
- None detected.

## Communities (76 total, 16 thin omitted)

### Community 0 - "unityengine"
Cohesion: 0.06
Nodes (9): Impfany.Weapons, Impfany.Modifiers, PixelArtImportDefaults, dropCoin, MeleeConeShape, WeaponPhaseSettings, Duration, LockAim (+1 more)

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
Cohesion: 0.08
Nodes (14): Aktueller Aufbau, DeleteCoin, CollectCoin, GameFlow, IsInShop, WaitingForShop, GameStatus, GameOver (+6 more)

### Community 5 - "com.unity.2d.sprite"
Cohesion: 0.18
Nodes (11): dependencies, depth, source, url, version, dependencies, depth, source (+3 more)

### Community 6 - "NumericTextPresenter"
Cohesion: 0.06
Nodes (19): Direkte Code-Unterstützung, KI-Nutzung – Merlin (Impfany), Mein Arbeitsablauf, Verwendetes Tool, LevelTextPresenter, TargetText, NumericTextPresenter, TargetText (+11 more)

### Community 7 - "packages-lock.json"
Cohesion: 0.05
Nodes (37): com.unity.2d.sprite, com.unity.2d.tilemap, com.unity.modules.animation, com.unity.modules.assetbundle, com.unity.modules.audio, com.unity.modules.director, com.unity.modules.imageconversion, com.unity.modules.imgui (+29 more)

### Community 8 - "dependencies"
Cohesion: 0.14
Nodes (15): dependencies, depth, source, version, dependencies, depth, source, version (+7 more)

### Community 9 - "com.unity.2d.tilemap"
Cohesion: 0.13
Nodes (16): dependencies, depth, dependencies, depth, source, url, version, source (+8 more)

### Community 10 - "PlayerHealth"
Cohesion: 0.09
Nodes (6): HeartHUD, IPlayerDamageable, PlayerHealth, CurrentHealth, MaxHealth, PlayerCoinHeal

### Community 11 - "com.unity.render-pipelines.core"
Cohesion: 0.09
Nodes (25): depth, source, version, dependencies, depth, source, version, dependencies (+17 more)

### Community 12 - "com.unity.modules.animation"
Cohesion: 0.18
Nodes (11): dependencies, depth, source, url, version, dependencies, depth, source (+3 more)

### Community 13 - "com.unity.modules.jsonserialize"
Cohesion: 0.40
Nodes (5): dependencies, depth, source, version, com.unity.modules.jsonserialize

### Community 14 - "Hugo — Übergabe Waffen ↔ Spieler"
Cohesion: 0.22
Nodes (5): Proposed contract, Unity wiring and checks, IWeaponUser, PhaseEffects, WeaponSandboxUser

### Community 15 - "Merlin – Flow, UI & Audio (Entwurf zur Durchsicht)"
Cohesion: 0.22
Nodes (6): Aktueller Button- und Anzeigeweg, Meine Zuständigkeit, Merlin – Flow, UI & Audio (Entwurf zur Durchsicht), Offene Anschlüsse und nächste Schritte, Technische Fundstellen, Dokumentation

### Community 16 - "com.unity.modules.cloth"
Cohesion: 0.40
Nodes (5): dependencies, depth, source, version, com.unity.modules.cloth

### Community 17 - "MeleeConeIndicator"
Cohesion: 0.10
Nodes (9): MeleeConeIndicator, WeaponPhase, Attack, Charge, Idle, Recover, WeaponVisual, Weapon (+1 more)

### Community 18 - "Weapon"
Cohesion: 0.21
Nodes (7): Reusable attack behaviours, Weapon, CurrentPhase, PhaseElapsed, PhaseProgress, Settings, WeaponAttackBehaviour

### Community 20 - "WeaponSystem/README.md"
Cohesion: 0.35
Nodes (3): Team folder structure, Systemdiagramme, WeaponSystem – Waffen und Progression

### Community 21 - "build_diagrams.py"
Cohesion: 0.24
Nodes (4): box(), decision(), edge(), text()

### Community 22 - "com.unity.modules.physics2d"
Cohesion: 0.18
Nodes (11): dependencies, depth, source, url, version, dependencies, depth, source (+3 more)

### Community 23 - "dependencies"
Cohesion: 0.33
Nodes (6): dependencies, depth, source, url, version, com.unity.visualscripting

### Community 24 - "com.unity.collections"
Cohesion: 0.20
Nodes (10): depth, source, url, version, dependencies, depth, source, version (+2 more)

### Community 25 - "com.unity.modules.uielements"
Cohesion: 0.13
Nodes (15): dependencies, depth, source, url, version, depth, source, version (+7 more)

### Community 26 - "com.unity.test-framework"
Cohesion: 0.05
Nodes (46): dependencies, dependencies, depth, source, version, dependencies, depth, source (+38 more)

### Community 27 - "Erklärung zur KI-Nutzung"
Cohesion: 0.20
Nodes (9): Angaben zum Projekt, Eigene Leistung, Einen Merge-Konflikt lösen, Erklärung, Erklärung zur KI-Nutzung, Fehlermeldungen verstehen, Syntax und Konzepte erklären lassen, Verwendetes KI-Werkzeug (+1 more)

### Community 28 - "Impfany rule-set and overview"
Cohesion: 0.25
Nodes (8): Arena:, Enemy's:, Impfany rule-set and overview, Items:, Other Information, Player:, Soul Lantern, Weapons:

### Community 29 - "dependencies"
Cohesion: 0.18
Nodes (11): dependencies, depth, source, version, dependencies, depth, source, version (+3 more)

### Community 31 - "Jannis — Weapons & upgrades"
Cohesion: 0.20
Nodes (7): Building separately in Unity, How an attack works, How this connects to your work, Jannis — Weapons & upgrades, Planned weapon attributes, Upgrades come next, WeaponSandboxTester

### Community 32 - "WeaponSettings"
Cohesion: 0.18
Nodes (10): WeaponSettings, Attack, AttackAngle, Charge, Damage, DisplayName, KnockbackDistance, Range (+2 more)

### Community 33 - "com.unity.modules.ui"
Cohesion: 0.20
Nodes (10): dependencies, depth, source, version, dependencies, depth, source, version (+2 more)

### Community 34 - "Waffensystem – Systemdiagramme"
Cohesion: 0.25
Nodes (8): 01 · Aufrufweg – Vom Klick zum Lebenspunkte-Abzug, 02 · Zustände – Ein Aufruf, ein kompletter Ablauf, 03 · Schaden – Ein Gegner bekommt nur einen Treffer, 04 · Geometrie – Eine Form, zwei Verwendungen, 05 · Darstellung – Zielen, treffen, Trefferort zeigen, 06 · Unity & Zusammenarbeit – Zusammenbauen und unabhängig testen, Pflege, Waffensystem – Systemdiagramme

### Community 36 - "Weapons & progression — gameplay decisions"
Cohesion: 0.25
Nodes (8): Agreed weapon behaviour, Cards — from the Overview, Current melee scope — agreed with Hugo, Input buffer and dash — agreed, Player/weapon integration — updated design, Proposed details, Souls and lantern — agreed, Weapons & progression — gameplay decisions

### Community 38 - "com.unity.modules.subsystems"
Cohesion: 0.20
Nodes (10): dependencies, depth, source, version, dependencies, depth, source, version (+2 more)

### Community 39 - "MonoBehaviour"
Cohesion: 0.36
Nodes (4): EnemyHealth, IEnemyDamageable, WeaponDamageDummy, WeaponHit

### Community 40 - "com.unity.modules.terrain"
Cohesion: 0.20
Nodes (10): dependencies, depth, source, version, dependencies, depth, source, version (+2 more)

### Community 41 - "EnemyMovement"
Cohesion: 0.18
Nodes (3): AttackPlayer, EnemyMovement, IsStunned

### Community 43 - "com.unity.collab-proxy"
Cohesion: 0.33
Nodes (6): dependencies, depth, source, url, version, com.unity.collab-proxy

### Community 44 - "com.unity.ide.rider"
Cohesion: 0.33
Nodes (6): dependencies, depth, source, url, version, com.unity.ide.rider

### Community 45 - "Hugo — Übergabe Waffen ↔ Spieler"
Cohesion: 0.25
Nodes (8): Ablauf, Dash, Hugo — Übergabe Waffen ↔ Spieler, Kleine gemeinsame Schnittstelle, Prefab und Zielen, Vereinbarter Nahkampf — aktueller Umfang, Vorhandene Settings, Wer baut was?

### Community 46 - "com.unity.modules.audio"
Cohesion: 0.13
Nodes (16): dependencies, depth, source, version, dependencies, depth, source, version (+8 more)

### Community 48 - "com.unity.modules.unitywebrequestaudio"
Cohesion: 0.40
Nodes (5): dependencies, depth, source, version, com.unity.modules.unitywebrequestaudio

### Community 49 - "com.unity.modules.imgui"
Cohesion: 0.18
Nodes (11): dependencies, depth, source, version, dependencies, depth, source, version (+3 more)

### Community 50 - "com.unity.modules.ui"
Cohesion: 0.33
Nodes (6): dependencies, depth, source, version, dependencies, com.unity.modules.hierarchycore

### Community 51 - "com.unity.mathematics"
Cohesion: 0.17
Nodes (13): dependencies, dependencies, depth, source, url, version, dependencies, depth (+5 more)

### Community 52 - "dependencies"
Cohesion: 0.33
Nodes (6): dependencies, depth, source, url, version, com.unity.2d.aseprite

### Community 53 - "com.unity.modules.ai"
Cohesion: 0.40
Nodes (5): dependencies, depth, source, version, com.unity.modules.ai

### Community 54 - "com.unity.modules.physics"
Cohesion: 0.40
Nodes (5): dependencies, depth, source, version, com.unity.modules.physics

### Community 55 - "com.unity.modules.umbra"
Cohesion: 0.40
Nodes (5): dependencies, depth, source, version, com.unity.modules.umbra

### Community 56 - "Next implementation — weapon-owned phases"
Cohesion: 0.33
Nodes (6): Build in small steps, Documentation entry point, Next implementation — weapon-owned phases, Resume checkpoint — 13 November 2026, Saved work we keep, What we are building

### Community 57 - "com.unity.modules.unitywebrequest"
Cohesion: 0.20
Nodes (10): dependencies, depth, source, version, dependencies, depth, source, version (+2 more)

### Community 58 - "com.unity.modules.vehicles"
Cohesion: 0.40
Nodes (5): dependencies, depth, source, version, com.unity.modules.vehicles

### Community 59 - "com.unity.modules.wind"
Cohesion: 0.40
Nodes (5): dependencies, depth, source, version, com.unity.modules.wind

### Community 60 - "com.unity.modules.vr"
Cohesion: 0.40
Nodes (5): dependencies, depth, source, version, com.unity.modules.vr

### Community 61 - "ItemDebugPanel"
Cohesion: 0.07
Nodes (9): ItemDebugPanel, ItemBuff, IModifiable, Stat, MoveSpeed, Modifier, StatModifiers, PlayerMovement (+1 more)

### Community 64 - "com.unity.modules.xr"
Cohesion: 0.40
Nodes (5): dependencies, depth, source, version, com.unity.modules.xr

### Community 65 - "com.unity.timeline"
Cohesion: 0.40
Nodes (5): depth, source, url, version, com.unity.timeline

### Community 67 - "Impfany · Lerngraph für Experience Engineering"
Cohesion: 0.25
Nodes (6): Der kürzeste Lernweg, Impfany · Lerngraph für Experience Engineering, Quizwissen mit wenig Doppelarbeit festigen, So bearbeitet ihr den Graphen im Team, Was am Ende abgegeben wird, Was habt ihr schon, was fehlt?

### Community 71 - "com.unity.modules.imageconversion"
Cohesion: 0.20
Nodes (10): dependencies, depth, source, version, dependencies, depth, source, version (+2 more)

## Knowledge Gaps
- **130 isolated node(s):** `IsStunned`, `IsInShop`, `WaitingForShop`, `StartMenu`, `Playing` (+125 more)
  These have ≤1 connection - possible missing edges or undocumented components. (Counts symbols only; 563 node(s) total have ≤1 connection when file, concept and rationale nodes are included.)
- **16 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `dependencies` connect `dependencies` to `com.unity.2d.common`, `com.unity.2d.sprite`, `packages-lock.json`, `dependencies`, `com.unity.2d.tilemap`, `com.unity.render-pipelines.core`, `com.unity.modules.animation`, `com.unity.modules.jsonserialize`, `com.unity.modules.cloth`, `com.unity.modules.physics2d`, `dependencies`, `com.unity.collections`, `com.unity.modules.uielements`, `com.unity.test-framework`, `com.unity.modules.ui`, `com.unity.modules.subsystems`, `com.unity.modules.terrain`, `com.unity.collab-proxy`, `com.unity.ide.rider`, `com.unity.modules.audio`, `com.unity.modules.unitywebrequestaudio`, `com.unity.modules.imgui`, `com.unity.modules.ui`, `com.unity.mathematics`, `dependencies`, `com.unity.modules.ai`, `com.unity.modules.physics`, `com.unity.modules.umbra`, `com.unity.modules.unitywebrequest`, `com.unity.modules.vehicles`, `com.unity.modules.wind`, `com.unity.modules.vr`, `com.unity.modules.xr`, `com.unity.timeline`, `com.unity.modules.imageconversion`?**
  _High betweenness centrality (0.127) - this node is a cross-community bridge._
- **What connects `IsStunned`, `IsInShop`, `WaitingForShop` to the rest of the system?**
  _130 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `unityengine` be split into smaller, more focused modules?**
  _Cohesion score 0.056107539450613676 - nodes in this community are weakly interconnected._
- **Why does `GameFlow` connect `GameFlow` to `unityengine`, `MonoBehaviour`, `EnemyTracker`, `NumericTextPresenter`, `PlayerHealth`, `ItemDebugPanel`, `EnemyTracker`?**
  _High betweenness centrality (0.054) - this node is a cross-community bridge._
- **Should `manifest.json` be split into smaller, more focused modules?**
  _Cohesion score 0.037037037037037035 - nodes in this community are weakly interconnected._
- **Why does `Weapon` connect `Weapon` to `unityengine`, `WeaponSettings`, `MonoBehaviour`, `CameraZoom`, `Weapons & progression — gameplay decisions`, `Hugo — Übergabe Waffen ↔ Spieler`, `Hugo — Übergabe Waffen ↔ Spieler`, `MeleeConeIndicator`, `Next implementation — weapon-owned phases`, `Jannis — Weapons & upgrades`?**
  _High betweenness centrality (0.048) - this node is a cross-community bridge._
- **Should `dependencies` be split into smaller, more focused modules?**
  _Cohesion score 0.037037037037037035 - nodes in this community are weakly interconnected._