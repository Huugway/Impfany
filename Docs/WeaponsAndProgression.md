# Weapons & progression — gameplay decisions

The weapon attributes and team handoffs are in [Jannis.md](Jannis.md). This page keeps the remaining gameplay rules together. Nothing is implemented yet.

## Agreed weapon behaviour

- One click activates one weapon. Attacks cycle **1 → 2 → 3 → 1**.
- Every weapon has **Charge → Attack → Recover**, each with its own duration.
- The idle reset starts **after Recover ends**, never during a long attack. Roughly **2–3 seconds** of waiting resets the next attack to slot 1.
- Aim is adjustable by default. Charge, Attack and Recover can each lock it independently.
- Movement can be restricted independently during each phase.

## Proposed details

| Rule | Starting proposal |
| --- | --- |
| Exact idle reset | 2.5 seconds; tune after playing |
| Phase movement | A speed multiplier: 0 stops walking, 1 is normal, above 1 boosts speed |
| Default phase settings | Normal movement and unlocked aim |
| Aim lock | Hold the direction when the phase starts; consecutive locked phases keep the same direction |
| Continuous damage | A fixed damage interval, not damage every frame |
| Repeated hits | A swing hits each enemy once; later burst shots and continuous ticks can hit it again |
| Upgrading a weapon | New stats apply to its next attack |
| End or interruption | Clear that attack's movement/aim restrictions, keeping other buffs and slows |

A four-second flamethrower dealing 2 damage every 0.1 seconds deals **80 total damage** if the enemy stays in range, before resistances. A fired bullet keeps travelling independently of the player's later aim.

## Input buffer and dash — agreed

- **Input buffer: about 0.5 seconds**, adjustable for balancing. Remember one early attack click and use it as soon as an attack is allowed. If it expires first, do not attack.
- Example: ready in **0.4 seconds → attack triggers**. Ready in **0.6 seconds → click expires** unless something, such as a dash, makes attacking possible sooner.
- **Dash during Charge:** cancel the charge without advancing the slot. The next attack retries that weapon, unless the idle timer resets the chain first.
- **Dash during Attack:** cannot interrupt the Attack phase.
- **Dash during Recover:** end recovery immediately, allowing the next weapon to be used.
- **Hurt/cancellation:** do not reset the chain to slot 1. Only the idle timeout resets it.

Movement multiplier zero means **no walking**; it does not automatically forbid a dash. Incoming knockback can still move the player.

## Cards — from the Overview

| Suit | Improves |
| --- | --- |
| Spade | Weapon damage |
| Heart | Soul gain for the lantern |
| Diamond | Enemy stun duration |
| Club | Knockback |

Each weapon has one stack per suit. Its matching ace unlocks the stack and stays on that weapon. A wildcard can become an ace. Normal cards can be redistributed at the Devil's Table.

**Before building cards, settle:**

- Exact stacking order and how stack size increases the bonus.
- The Devil's Table rule: same-suit addition, or exchanging two cards for a chosen card? The Overview describes both.
- What happens to cards when a weapon is replaced or sold.
- What face cards do, and whether wildcards have other uses.

## Souls and lantern — agreed

- Hits make souls pop out of the enemy. They float briefly, then gravitate satisfyingly toward the player, like in *20 Minutes Till Dawn*.
- The amount spawned depends on **damage actually dealt**; Heart upgrades increase soul gain.
- Souls charge the lantern when they reach the player.
- When the lantern fills and health is missing, its charge is spent to heal the player. The Overview specifies **half a heart per full charge**.
- The lantern can stay full only at maximum health. While full, it grants a small **movement-speed and damage bonus**.
- If the player loses health while storing a full lantern, spend that charge to heal them; the full-lantern bonus ends when the charge is spent.
- Direct lifesteal remains a separate weapon effect.

**Balance later:** souls per damage, lantern capacity, heal amount, buff strength and the souls' float/travel timing.

**One remaining healing detail:** is healing always triggered by a full lantern, or can partial charge also be spent when health is missing?

Jannis builds soul spawning/collection, lantern rules and the weapon damage bonus. Maxim supplies actual damage and hit position; Hugo applies healing and the movement-speed bonus; Merlin displays charge, health and the buff.
