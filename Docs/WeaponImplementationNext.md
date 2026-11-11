# Next implementation — weapon-owned phases

**Status: planned, not implemented · 11 November 2026.** This is the continuation guide for Jannis and the next tutoring model. Design: [Jannis.md](Jannis.md). Player integration: [German handoff](Handoff-Hugo-Waffen.md).

**Tutor mode:** Jannis types the code and makes Unity changes. Explain one small, compilable step and why it exists, then wait for him. Inspect saved files before continuing; “done” sometimes means the editor buffer is still unsaved. Do not dump full scripts, silently implement the plan, or edit Hugo's player files. Docs and Git changes need their own request. Do not push as part of this checklist.

## What we are building

One player call starts **Idle → Charge → Attack → Recover → Idle**. `Weapon` owns the timing and requests the movement/aim effects for each phase. Its attached behaviour performs the firing. The player's effect component applies those requests; the weapon releases its own effects when use ends.

Hugo keeps input, slots, buffering, chain reset, aiming and dash movement. The weapon reports whether it finished, was cancelled during Charge, skipped Recover or was forcibly stopped. Hugo uses that outcome to decide which slot comes next.

**No blanket dash cleanse.** Dash requests an interruption. Accepted cancellation/early recovery completion already removes this weapon use's effects. Attack cannot be interrupted by dash. Whether the player may still dash while Attack continues is a separate, undecided player rule; do not silently forbid or enable it here.

## Saved work we keep

All weapon files are under `ImpfanyGame/Assets/Features/Progression/`.

| Existing file/setup | What changes |
| --- | --- |
| `Scripts/WeaponSettings.cs` | Keep name, damage and the three phase settings. They already contain duration, movement multiplier and aim lock. No data redesign needed. |
| `WeaponData/TestHammer.asset` | Keep the asset and values: Charge 0.4 s / speed 1 / aim free; Attack 0.2 s / speed 0 / aim locked; Recover 0.7 s / speed 0.5 / aim free. |
| `Scripts/Weapon.cs` | Main rewrite: replace the manual attack flag/forwarding API with whole-use entry, current phase, one phase clock, effect requests, interruption and completion notification. |
| `Scripts/WeaponAttackBehaviour.cs` | Keep the common base and Begin/End for the actual Attack phase. Add a progress-update method called by `Weapon`. Hugo will not call behaviour methods. |
| `Scripts/SwingAttackBehaviour.cs` | Removed along with the prefab component. Start with a new projectile-firing behaviour; the weapon sprite remains static. |
| `Scripts/WeaponSandboxTester.cs` | Replace manual Begin/End tests with whole-use, dash-interruption and forced-abort tests. Show/log phase and end reason. |
| `Prefabs/TestHammer.prefab` | Keep root → Visual → Handle/Head, colours, dimensions, and Settings reference. Its attack behaviour field is currently empty and must later reference a projectile-firing component. No rebuild or reparenting needed. |
| `Scenes/WeaponSandbox.unity` | Keep this private scene. Add a simple effect-recipient component for testing; wire it to the tester. No shared scene/player-prefab changes. |

**Current behaviour:** `Weapon.BeginAttack()` and `EndAttack()` exist, but TestHammer has no attack behaviour after removing the swing component. Calling BeginAttack on it currently logs a configuration error. The new sequence and projectile firing are not implemented.

Do not rename `WeaponSettings` again or add `FormerlySerializedAs`. Preserve the existing serialized field names (`settings`, `attackBehaviour`) and Unity `.meta` files. Create new scripts through Unity so they receive metadata.

## Proposed contract

**The ownership is agreed. The following names and implementation choices are the suggested starting contract, not existing API or an additional agreement from Hugo.** Keep the same names in code and handoff if adopted; coordinate any change with Hugo before integration.

New small files belong beside the existing weapon scripts, namespace `Impfany.Weapons`:

| File | Purpose / members |
| --- | --- |
| `WeaponPhase.cs` | Enum: `Idle`, `Charge`, `Attack`, `Recover`. |
| `WeaponUseResult.cs` | Enum: `Completed`, `ChargeCancelled`, `RecoverySkipped`, `Aborted`. |
| `DashInterruptionResult.cs` | Enum: `NotUsing`, `ChargeCancelled`, `RecoverySkipped`, `AttackBlocked`. |
| `IWeaponUser.cs` | Shared interface implemented by Hugo's player adapter and our sandbox recipient. |
| `WeaponSandboxUser.cs` | Sandbox-only implementation; initially records/displays the requested speed and aim state. It is not a production player controller. |

Proposed public members on `Weapon`:

| Member | Meaning |
| --- | --- |
| `bool TryUse(IWeaponUser user)` | Start Charge if idle and configured. False means nothing started; do not emit an end event. |
| `WeaponPhase CurrentPhase { get; }` | Read-only current phase; Idle when available. |
| `event Action<Weapon, WeaponUseResult> UseEnded` | One notification per accepted use after cleanup. |
| `DashInterruptionResult TryInterruptForDash()` | Charge: cancel; Recover: finish early; Attack: refuse; Idle: no change. |
| `void AbortUse()` | Forced cleanup for unequip/death/disable, including during Attack. Idempotent; no event if already idle. |
| `WeaponSettings Settings { get; }` | Keep the existing read-only property. |

Proposed `IWeaponUser` methods:

- `void ApplyWeaponPhase(object source, float movementSpeedMultiplier, bool lockAim)` — add or replace this source's phase effects together.
- `void RemoveWeaponEffects(object source)` — remove this source's effects; safe to call when already absent.

Create a fresh source token for each accepted use and keep it until that use ends. This lets cleanup target precisely its own entry. Hugo supplies the recipient explicitly; do not search for the player by name/tag or add a scene-player reference to the weapon prefab.

Suggested player-side combination: multiply independent speed modifiers; aim is locked while any source requests a lock. Capture aim when it becomes locked; keep that direction through consecutive locked phases. Removing a weapon entry must reveal any other active effects, rather than blindly reset speed to 1 or unlock aim. Hugo owns the implementation; our sandbox initially verifies the requests only.

## Build in small steps

Each checkbox is a milestone, **not one large paste**. Split it into small teaching turns.

- [ ] **1. Add the phase enum and readable state.** Create `WeaponPhase.cs`; add `CurrentPhase` to `Weapon`, initially Idle. Keep the old API temporarily so the existing tester compiles. Explain that an enum gives names to a fixed set of states. Check Unity compiles.
- [ ] **2. Introduce whole-use timing.** Add `TryUse`, one phase timer, and automatic Charge → Attack → Recover → Idle transitions. Validate settings, behaviour and recipient before starting. Keep settings assets unchanged. Temporarily have the sandbox supply a simple recipient with no-op effect methods. Change the tester to start a whole use. Check the 0.4 / 0.2 / 0.7 sequence and that a second trigger while busy is rejected.
- [ ] **3. Build projectile firing.** Create a reusable firing behaviour and a simple projectile in the private sandbox. `Weapon` begins the behaviour at Attack entry and ends it at Attack exit. Fire in the weapon root’s local +X direction, which follows Hugo’s slot rotation. Keep the weapon sprite static. Test two slot orientations and verify already-fired projectiles continue after the phase ends. Coordinate projectile hits with Maxim later.
- [ ] **4. Request and release phase effects.** Implement the sandbox recipient's source tracking. On phase entry, replace this use's effect entry. On completion, remove it. Check the hammer's three speed/aim combinations and that a separate test source survives cleanup. A logged multiplier alone does not verify actual player movement; that check comes with Hugo's adapter.
- [ ] **5. Add end outcomes and interruptions.** Implement the result enums, `UseEnded`, dash interruption and forced abort. Clear active behaviour/effects and set Idle before notifying listeners. Subscribe before starting use, including zero-duration uses. Remove the obsolete public Weapon BeginAttack/EndAttack entry points and update the tester together. Keep Begin/End on the behaviour base as internal weapon-to-behaviour calls.
- [ ] **6. Connect the real player with Hugo.** Hugo implements the recipient interface, triggers TryUse and consumes end outcomes. Completed/RecoverySkipped advance; ChargeCancelled retries. Aborted releases the busy state without pretending a normal attack completed. He keeps one active weapon across all slots and processes buffered input only when his player state allows it, including dash. The idle reset remains player-owned and starts after normal or skipped Recover. Cancelled Charge does not itself reset the chain to slot 1.

**Timing detail for the tutor:** carry unused frame time into following phases, handle zero-duration phases without division by zero, and finish the active behaviour at Attack end. Only `Weapon` determines phase completion. Burst/projectile behaviours may later maintain shot intervals inside Attack.

**Cleanup detail:** use one cleanup path for normal completion, accepted interruption and forced abort. It stops the active behaviour, removes that use's effects and emits at most one end event. `OnDisable` uses forced cleanup; death/unequip code calls it before removing the recipient. Already-fired projectiles continue independently. Avoid finishing a new use accidentally if an end listener immediately starts another one.

## Unity wiring and checks

Once the scripts compile, open **WeaponSandbox**, not SampleScene. Keep TestHammer's existing assignments. Add `WeaponSandboxUser` to the separate WeaponTester object and expose a tester reference to it; the tester passes it to TryUse. Save the scene and prefab outside Play Mode. The weapon prefab must not store a reference to this scene object.

Check these in the sandbox before adding hit detection:

- [ ] One trigger completes all phases, returns Idle, clears its effects and can be triggered again.
- [ ] Charge interruption produces no projectile and reports retry; Recover interruption reports next slot; Attack interruption leaves the attack running and reports blocked.
- [ ] Forced abort/disable during each active phase stops the active behaviour and removes its effects exactly once. Re-enabling starts idle.
- [ ] Zero-duration phases and repeated input do not stick or emit duplicate completion. Unconfigured weapons reject use cleanly.
- [ ] Other effect sources survive weapon cleanup. Consecutive aim-locked phases do not briefly unlock.
- [ ] With Hugo's adapter, check actual walking speed/lock, live aim/aim lock, slot advancement and the ~0.5 s input buffer. Dash and knockback remain separate from walking restrictions.

**Next tutoring turn starts at step 1:** inspect the latest saved scripts, then guide Jannis through creating `WeaponPhase.cs`. The old swing work has already been removed. No further weapon types or damage system until this complete-use boundary works.
