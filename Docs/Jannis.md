# Jannis — Weapons & upgrades

**Planned work:** weapons → card upgrades → Soul Lantern → item bonuses. Nothing implemented yet.

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

**Proposed division of work:**

| Who | What I provide | What you handle |
| --- | --- | --- |
| Hugo — Player | Equipped weapon, upgraded stats, phase settings | Input, aiming, attack timing/animation, hit detection, movement/aim restrictions |
| Maxim — Enemies | Damage, stun and knockback for each hit | Apply hits to enemies; report damage actually dealt |
| Merlin — UI & flow | Weapon details, card rules, upgrade results | Menus, HUD, shop/table screens, when upgrades are allowed |

**One hit:** Hugo attacks → Maxim reports actual damage → I spawn souls that briefly float, then fly to the player → collected souls charge the lantern → Merlin updates the HUD.

**I define the weapon settings; Hugo’s player controller applies them.**

## Building separately in Unity

- My files, test scene and dummy enemy live in `Assets/Features/Progression/`.
- I deliver a weapon module prefab and editable settings per weapon. Each weapon copy keeps its own upgrades.
- Hugo/Merlin attach the module once. I edit my module/settings; they edit the player/scenes.
- Agree the handoff above before connecting. Coordinate edits to shared prefabs, scenes and settings.

## Upgrades come next

**Spade = damage · Heart = souls · Diamond = stun · Club = knockback.** An ace unlocks its suit’s stack. I build upgrade rules/stats; Merlin builds the screens.

The full lantern heals missing health. At full player health, it stays charged and grants a small movement-speed and damage bonus. Charge threshold and bonus amounts need balancing.

Open rules: [Gameplay decisions](WeaponsAndProgression.md).
