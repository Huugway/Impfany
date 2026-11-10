# Jannis — Weapons & upgrades

**Build order:** weapons → card upgrades → Soul Lantern → item bonuses. Weapon settings, a placeholder hammer prefab and a private sandbox scene exist. Attacks and upgrades are not implemented yet.

**Player/weapon integration:** [German handoff for Hugo](Handoff-Hugo-Waffen.md). The ownership split is agreed with Janis; Hugo still needs to confirm the proposed trigger calls and aim handoff.

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

## How an attack works

One click starts **Charge → Attack → Recover**. Further clicks cycle **slot 1 → 2 → 3 → 1**. Waiting **2–3 seconds after Recover** resets the next attack to slot 1.

During Attack: a pistol fires once, a burst fires several shots, a flamethrower deals repeated damage.

- **Input buffer:** an early click is remembered for about **0.5 seconds**. It fires when attacking becomes possible; otherwise it expires. This value will be balanced later.
- **Dash:** cancels Charge (retry the same weapon), cannot interrupt Attack, and ends Recover immediately (next weapon ready).
- **Chain reset:** only the idle timer resets the chain, not getting hurt or cancelling an attack.

## How this connects to your work

**Player/weapon responsibilities agreed with Janis:**

| Who | What I provide | What you handle |
| --- | --- | --- |
| Hugo — Player | Weapon prefabs, stats, phase settings and weapon-specific attack behaviour | Holding/equipping, slots and cycling, input/buffer, aiming, phase timing, dash and movement/aim restrictions |
| Maxim — Enemies | Damage, stun and knockback for each hit | Apply hits to enemies; report damage actually dealt |
| Merlin — UI & flow | Weapon details, card rules, upgrade results | Menus, HUD, shop/table screens, when upgrades are allowed |

**One hit:** Hugo triggers the weapon → my weapon detects a hit (or spawns its projectile) → Maxim applies the hit and reports actual damage → I spawn souls that briefly float, then fly to the player → collected souls charge the lantern → Merlin updates the HUD.

**I define the weapon settings; Hugo’s player controller applies them.**

## Building separately in Unity

- My files and `WeaponSandbox` scene live in `Assets/Features/Progression/`; a dummy enemy will be added later.
- I deliver weapon prefabs with a `Weapon` component and editable `WeaponDefinition` settings. Each weapon copy will keep its own upgrades.
- Hugo puts my weapon instances under his `WeaponSlot1`–`WeaponSlot3` objects and triggers them. I edit my weapons/settings; he edits the player/scenes.
- Confirm trigger calls and aim handoff before implementing attacks. Coordinate edits to shared prefabs, scenes and settings.

## Upgrades come next

**Spade = damage · Heart = souls · Diamond = stun · Club = knockback.** An ace unlocks its suit’s stack. I build upgrade rules/stats; Merlin builds the screens.

The full lantern heals missing health. At full player health, it stays charged and grants a small movement-speed and damage bonus. Charge threshold and bonus amounts need balancing.

Open rules: [Gameplay decisions](WeaponsAndProgression.md).
