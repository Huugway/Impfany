# Jannis — Weapons & upgrades

**Build order:** weapons → card upgrades → Soul Lantern → item bonuses. Automatic phases and melee cone detection work in the private sandbox. The cone finds a target inside it and excludes targets beyond its range or angle. The cone calls IDamageable.TakeDamage once per recipient; a logging dummy verifies the call. TestHammer.prefab now contains the complete cone setup. Player effects, outcome events and real enemy integration remain to build; TestDagger is now wired to the melee cone with its own Dagger settings. TestFist and TestMetalGlove remain unconfigured.

**Updated design:** the player triggers one use; the weapon runs **Charge → Attack → Recover**, requests its phase effects and removes them when finished. Hugo owns input, slots, dash and the player component that applies effects. This replaces the previous player-owned phase timers. See the [German handoff](Handoff-Hugo-Waffen.md) and [step-by-step migration](WeaponImplementationNext.md). `Weapon.Attack()` already starts the whole use; `CurrentPhase` is readable. The effect-recipient API and outcome events are planned. Hugo updates his caller from `BeginAttack()` to `Attack()`; no compatibility wrapper is kept. Attack behaviours retain their internal `BeginAttack(settings)` method.

## Planned weapon attributes

The current implementation is limited to name, damage, melee range/angle and phase settings. Burst, continuous fire, upgrades and on-hit bonuses below are future work.

| Attribute | What it controls |
| --- | --- |
| Name, icon & appearance | How the weapon looks in-game and in menus |
| Weapon slot | Defense (1), Effect (2) or Precision (3) |
| Attack shape | What it hits: e.g. a swing, thrust, projectile or cone |
| Firing mode | One shot, burst, continuous fire or melee swing |
| Damage | Damage per hit, bullet or continuous damage tick |
| Range & width/angle | How far and how wide the attack reaches |
| Burst count & shot interval | How many shots a burst fires and the time between them |
| Continuous damage interval | How often continuous fire deals damage |
| Enemy stun duration | How long a hit interrupts the enemy |
| Knockback distance | How far a hit pushes the enemy |
| Soul gain | Amount of souls spawned from damage actually dealt |
| Lifesteal | Optional direct healing from damage; starts at zero |

**Each weapon also has these three phases, with independent settings:**

| Phase | Duration | Movement speed | Lock aim? |
| --- | --- | --- | --- |
| Charge | Delay from click until the attack starts | Adjustable multiplier | Yes / no |
| Attack | Instantaneous for current melee; duration remains available for future behaviours | Adjustable multiplier | Yes / no |
| Recover | Wait after attacking before the next weapon can start | Adjustable multiplier | Yes / no |

Movement: **0 = cannot walk, 0.5 = half speed, 1 = normal (default), 1.5 = faster**. Aim is unlocked by default. Dash permission is separate.

Examples: railgun = stationary Charge; hammer = stationary Attack/Recover; dagger = faster Recover.

## Reusable attack behaviours

**Current scope: one shared melee cone behaviour for all melee weapons.** No coded weapon swing is needed. Hammer and dagger use the same hit logic with different settings; their sprite animations are presentation.

| Melee setting | Meaning |
| --- | --- |
| `Damage` | Damage to each enemy hit, once per attack |
| `Range` | Distance from the weapon root to the cone's outer edge |
| `AttackAngle` | Total cone width in degrees: 90° = 45° on each side of aim |

`Damage`, `Range` and `AttackAngle` exist in `WeaponSettings`. The earlier word “radius” meant angle; use `AttackAngle` so degrees cannot be confused with distance. A dagger could use 1° and a hammer 90°; range and damage remain editable balance values.

After Charge, the weapon checks the cone **once** along the slot's current local +X direction, damages all overlapping enemies once each, and immediately enters Recover. For this melee behaviour, Attack is instantaneous (`Attack.Duration = 0`), not a collider left active until the next frame. Sprite animation duration does not determine damage timing.

Use an explicit 2D overlap query against a sector-shaped collider, rather than relying on a one-frame trigger callback. Collider-edge overlaps count; checking enemy centres alone is insufficient. Multiple colliders on one enemy still produce one hit. Reuse the query shape instead of spawning/destroying it every attack. Overlap detection and one damage call per recipient are implemented and tested with sandbox dummies.

- **`WeaponSettings`** holds name, damage, phase settings and melee range/angle.
- **`Weapon`** runs phases. Player effect requests and completion/interruption notifications are planned.
- **`WeaponAttackBehaviour`** is the common base; `MeleeConeAttackBehaviour` currently detects overlapping colliders.

Timing, damage calls, duplicate-hit prevention, multiple targets, rotation and collider-edge overlaps are verified with sandbox dummies. Next: phase effect requests and player integration. Projectile, continuous-fire, sweeping and other mechanics are future work, outside this implementation scope. Keep the component-based prefab approach; each weapon can have its own visuals/settings without a Hammer/Pistol class hierarchy.

## How an attack works

One click starts **Charge → Attack → Recover**. Further clicks cycle **slot 1 → 2 → 3 → 1**. Waiting **2–3 seconds after Recover** resets the next attack to slot 1.

For the current melee scope, Attack performs one cone hit check and transitions immediately to Recover. Future weapon behaviours may have a longer Attack phase.

- **Input buffer:** an early click is remembered for about **0.5 seconds**. It fires when attacking becomes possible; otherwise it expires. This value will be balanced later.
- **Dash:** can cancel the weapon during Charge, Attack or Recover. The weapon stops its active behaviour, removes its own effects and reports which phase was interrupted. Hugo owns the subsequent slot decision. An instantaneous melee hit cannot be undone once its overlap query has executed.
- **Chain reset:** only the idle timer resets the chain, not getting hurt or cancelling an attack.

## How this connects to your work

**Player/weapon responsibilities agreed with Hugo:**

| Who | What I provide | What you handle |
| --- | --- | --- |
| Hugo — Player | Weapons that run their phases, request/release effects, accept interruption requests and report completion | Holding/equipping, slots, input/buffer, idle reset, dash movement and a player component that applies effects |
| Maxim — Enemies | Damage, stun and knockback for each hit | Apply hits to enemies; report damage actually dealt |
| Merlin — UI & flow | Weapon details, card rules, upgrade results | Menus, HUD, shop/table screens, when upgrades are allowed |

**One hit:** Hugo triggers the weapon → my weapon detects a hit (or spawns its projectile) → Maxim applies the hit and reports actual damage → I spawn souls that briefly float, then fly to the player → collected souls charge the lantern → Merlin updates the HUD.

**My weapon controls when its phase effects apply; Hugo’s effect component changes actual movement and aiming.** Each use has its own effect source. Phase changes replace that source’s effects; completion, cancellation or disabling removes them. Other buffs/slows stay intact. Dash requests interruption; it does not clear every player effect.

## Building separately in Unity

- My files and `WeaponSandbox` scene live in `Assets/Features/Progression/`. Sandbox damage dummies stay here too. See the [team folder structure](FeatureFolders.md) for the other areas.
- I deliver weapon prefabs with a `Weapon` component and editable `WeaponSettings`. Each weapon copy will keep its own upgrades.
- Hugo puts my weapon instances under his `WeaponSlot1`–`WeaponSlot3` objects and triggers them. I edit my weapons/settings; he edits the player/scenes.
- Hugo triggers the complete use and listens for its outcome. The outcome identifies completion or the interrupted phase; Hugo owns how his chain responds. Every weapon uses the same `Weapon` component.
- Hugo rotates the slot toward the aim direction and holds it fixed during aim-locked phases. My weapon uses that orientation for the cone. Sprite animation is separate from hit detection; no scripted swing is needed.
- Update `WeaponSandboxTester` from manual Begin/End to whole-use and interruption tests. Add a sandbox effect recipient so testing does not depend on Hugo’s player. Keep both in the private scene. Coordinate edits to shared prefabs, scenes and settings.

## Upgrades come next

**Spade = damage · Heart = souls · Diamond = stun · Club = knockback.** An ace unlocks its suit’s stack. I build upgrade rules/stats; Merlin builds the screens.

The full lantern heals missing health. At full player health, it stays charged and grants a small movement-speed and damage bonus. Charge threshold and bonus amounts need balancing.

Open rules: [Gameplay decisions](WeaponsAndProgression.md).
