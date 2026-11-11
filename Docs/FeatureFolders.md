# Team folder structure

Each developer's systems live together under `ImpfanyGame/Assets/Features/`, named by responsibility.

| Folder | Owner | Contents |
| --- | --- | --- |
| `Player/` | Hugo | Player and camera scripts, player prefab, animations and sprites |
| `Enemies/` | Maxim | Enemy/spawn scripts, enemy prefab and `Scenes/MaximScene.unity` |
| `Progression/` | Jannis | Weapons, settings, sandbox helpers and `Scenes/WeaponSandbox.unity`; upgrades and lantern follow |
| `GameFlow/` | Merlin | Flow/status scripts, UI scripts, UI prefabs/sprites and `Scenes/MerlinScene.unity`; audio follows |

Use `Scripts/`, `Prefabs/`, `Scenes/` and asset folders such as `Sprites/` or `Animations/` inside each feature as needed. Create folders when they have content.

**Shared files:** `Assets/Scenes/SampleScene.unity` remains the team integration scene. Arena art and palettes remain in `Assets/Art/`; weapon prototype sprites currently remain there too. Input/render settings stay in `Assets/Settings/`, import tooling in `Assets/Editor/`, and the TextMesh Pro and Thaleah font packages keep their existing locations.

**Working without conflicts:** develop in your own feature and test scene. Deliver prefabs and public script methods for teammates to use. Coordinate edits to SampleScene, shared settings and another owner's prefab. Separate folders reduce accidental overlap; they do not prevent conflicts when two people edit the same asset.

**Moving assets:** move through Unity's Project window, or keep each asset and its `.meta` together. The metadata holds the GUID used by scenes and prefabs. Moving a script does not require changing its class or namespace.

Migration on 12 November 2026 moved teammate assets without changing their code or serialized contents. The old empty `PlayerAnimation.cs` is retained in `Player/Scripts/Legacy/`. Jannis's existing settings move is preserved at `Progression/Scripts/Weapon/WeaponData/TestHammer.asset`.

Checks preserved every asset GUID and found no missing scripts in seven feature prefabs and four team scenes. Existing missing sprites remain on Arena in MaximScene and Player in MerlinScene/WeaponSandbox; those scene files match their committed originals.
