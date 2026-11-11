# Jannis — Weapons & upgrades

**Build order:** weapons → card upgrades → Soul Lantern → item bonuses. Settings, the hammer prefab and a private sandbox exist. Manual Begin/End was previously tested with a static pose. The swing component and script have since been removed; the hammer currently has no attack behaviour. Full phase automation, player effects, hit detection, damage and upgrades are still to build.

**Updated design:** the player triggers one use; the weapon runs **Charge → Attack → Recover**, requests its phase effects and removes them when finished. Hugo owns input, slots, dash and the player component that applies effects. This replaces the previous player-owned phase timers. See the [German handoff](Handoff-Hugo-Waffen.md) and [step-by-step migration](WeaponImplementationNext.md). The new API is planned, not implemented yet.

## Planned weapon attributes

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
| Attack | How long the swing/firing lasts | Adjustable multiplier | Yes / no |
| Recover | Wait after attacking before the next weapon can start | Adjustable multiplier | Yes / no |

Movement: **0 = cannot walk, 0.5 = half speed, 1 = normal (default), 1.5 = faster**. Aim is unlocked by default. Dash permission is separate.

Examples: railgun = stationary Charge; hammer = stationary Attack/Recover; dagger = faster Recover.

## Reusable attack behaviours

**A weapon prefab combines appearance + settings + a reusable attack behaviour.** Behaviours describe how an attack works and can be shared by different weapons.

| Behaviour | What it does | Example weapons |
| --- | --- | --- |
| Swing | Sweeps a hit area through an arc | Hammer, sword, axe |
| Thrust | Extends a hit area forward | Dagger, spear |
| Projectile firing | Spawns projectiles; supports single shots, bursts or repeated fire | Pistol, shotgun, bow |
| Beam / cone | Hits along a beam or within a cone, once or repeatedly | Laser, flamethrower |
| Area attack | Hits within an area around a chosen point | Ground slam, explosion |

These are planned starting behaviours; build them as needed. **First: a static weapon that fires a projectile.** A different attack mechanic may need a different behaviour, even if the weapon has the same name or category.

- **`WeaponSettings`** supplies the weapon's base settings (formerly `WeaponDefinition`). Behaviour-specific parameters can live on the appropriate behaviour component.
- **`Weapon`** will run the complete phase sequence, request/release effects and report when use ends. Currently it only forwards manual Begin/End calls; this is the main script to change.
- **`WeaponAttackBehaviour`** handles the attack mechanic. A projectile-firing behaviour is the next planned behaviour. The earlier swing behaviour was removed; attacks and hit detection remain to build.

A hammer and an axe can share Swing with different visuals, damage, timing, reach and angle. Melee / Ranged can be descriptive categories for UI or upgrades; they do not choose the attack code.

Create a prefab per weapon using the components it needs. Use prefab variants when weapons share a similar hierarchy; there is no required Melee → Hammer or Ranged → Pistol prefab inheritance tree. Each copy keeps its own later upgrades rather than changing the shared settings asset.

## How an attack works

One click starts **Charge → Attack → Recover**. Further clicks cycle **slot 1 → 2 → 3 → 1**. Waiting **2–3 seconds after Recover** resets the next attack to slot 1.

During Attack: a pistol fires once, a burst fires several shots, a flamethrower deals repeated damage.

- **Input buffer:** an early click is remembered for about **0.5 seconds**. It fires when attacking becomes possible; otherwise it expires. This value will be balanced later.
- **Dash:** cancels Charge (retry the same weapon), cannot interrupt Attack, and ends Recover immediately (next weapon ready).
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

- My files and `WeaponSandbox` scene live in `Assets/Features/Progression/`; a dummy enemy will be added later.
- I deliver weapon prefabs with a `Weapon` component and editable `WeaponSettings`. Each weapon copy will keep its own upgrades.
- Hugo puts my weapon instances under his `WeaponSlot1`–`WeaponSlot3` objects and triggers them. I edit my weapons/settings; he edits the player/scenes.
- Hugo triggers the complete use and listens for its outcome. Finished/Recovery skipped means next slot; cancelled Charge means retry this slot. Every weapon uses the same `Weapon` component.
- Hugo rotates the slot toward the aim direction and holds it fixed during aim-locked phases. My weapon reads that orientation and animates its `Visual` child separately.
- Update `WeaponSandboxTester` from manual Begin/End to whole-use and interruption tests. Add a sandbox effect recipient so testing does not depend on Hugo’s player. Keep both in the private scene. Coordinate edits to shared prefabs, scenes and settings.

## Upgrades come next

**Spade = damage · Heart = souls · Diamond = stun · Club = knockback.** An ace unlocks its suit’s stack. I build upgrade rules/stats; Merlin builds the screens.

The full lantern heals missing health. At full player health, it stays charged and grants a small movement-speed and damage bonus. Charge threshold and bonus amounts need balancing.

Open rules: [Gameplay decisions](WeaponsAndProgression.md).
