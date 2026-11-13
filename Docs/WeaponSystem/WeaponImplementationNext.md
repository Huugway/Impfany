# Next implementation — weapon-owned phases

**Status: partially implemented in tutoring mode · 11 November 2026.** This is the continuation guide for Jannis and the next tutoring model. Design: [Jannis.md](Jannis.md). Player integration: [German handoff](Handoff-Hugo-Waffen.md).

**Tutor mode:** Jannis types the code and makes Unity changes. Explain one small, compilable step and why it exists, then wait for him. Inspect saved files before continuing; “done” sometimes means the editor buffer is still unsaved. Do not dump full scripts, silently implement the plan, or edit Hugo's player files. Docs and Git changes need their own request. Do not push as part of this checklist.

## Resume checkpoint — 13 November 2026

**After compaction:** reread this checkpoint, Jannis.md, Handoff-Hugo-Waffen.md and ../FeatureFolders.md, then inspect the current weapon scripts, starting prefabs and fresh Git/Unity status. Prioritize the temporary player timer replacement below; do not restart earlier implementation steps.

**Priority update — 13 November 2026: replace Hugo's temporary timer ASAP next week (from 16 November).** Confirmed in PlayerAttack.cs after pulling `df6b22f` (Hugo's cooldown commit `5598d6b`): `cooldown = Time.time + Charge.Duration + Attack.Duration + Recover.Duration`; clicks before that timestamp are ignored. The slot index still advances immediately through ProgressWeaponChain after the start attempt; only subsequent attack input is delayed. This is a temporary bridge, not the agreed final contract. Preserve Hugo's work while replacing the timer with the whole-use completion contract.

- Jannis supplies `Weapon.UseEnded` for the end of the **complete use**, after Recover or an interruption. WeaponAttackBehaviour must not notify the player when only the Attack phase ends.
- The player keeps one active weapon across all slots and waits for that weapon's end notification. Hugo then handles slot advancement, the idle-reset timer and still-valid buffered input. Do not gate this with a second duration timer.
- Change `Attack()` to report whether starting was accepted. Hugo subscribes before starting; a rejected start must not leave the player waiting for an event that will never arrive.
- Include a completion/interruption reason. Emit once per accepted use, after cleanup and entering Idle; rejected starts emit nothing. Hugo owns the chain response to cancellation.
- Build this completion contract next without waiting for the unfinished movement/aim effect system. Jannis implements in tutoring mode; Hugo replaces his timer and updates his player integration. Verify normal completion, rejected starts, cancellation/disable and buffered input together.

**Tutor mode remains: Jannis implements; the agent reads and explains small steps.** Jannis explicitly reaffirmed that the agent must not edit prefabs. Explain the intended result and reason before the Unity steps; give the complete setup goal before splitting it into small steps. Preserve unrelated Overview edits and machine files. Git actions require a request.

**Git checkpoint:** weapon code/docs were pushed as `3ecc9e1`; starting prefab/settings/docs setup was pushed as `a4dd9f8`. Latest team sync is `a72e69b`. The team renamed the damage interface to `IEnemyDamageable` and updated the melee behaviour, sandbox dummy and EnemyHealth together. GameFlow now spawns the player on start; coin collection and level UI were added. Real enemy damage and moving-player integration still need a gameplay check. Hugo still owns input, slot policy, dash and movement.

**Editor status:** refreshed Unity's asset database and regenerated the local C# project files after the pull; the stale `IDamageable.cs` project reference caused VS Code's red errors. VS Code now reports zero errors/warnings. Unity compiles successfully; its remaining warning is the unused GameFlow.OnRestart event. No gameplay code or prefab edits were needed for this repair. Current local balance changes: Fist range 1.5; MetalGlove damage 50.

**Current weapon code:** `Weapon.Attack()` runs Charge → Attack → Recover → Idle. `PhaseElapsed` exposes the phase clock read-only; `PhaseChanged` announces each phase entry, including zero-duration Attack. EndAttack cancels any active phase. Movement/aim requests, completion outcomes and a dedicated dash API are not wired yet.

**Melee code:** `Scripts/Weapon/AttackBehaviours/MeeleCone/` contains MeleeConeAttackBehaviour, MeleeConeShape and MeleeConeIndicator. The folder spelling is currently `MeeleCone`. The moved behaviour retains its GUID. Collider and visible mesh use the same 16-segment cone points; the overlap deals damage once per IEnemyDamageable, even with several colliders.

**Shared visual:** Prefabs/HitAreaIndicator.prefab is nested under TestHammer and all three StartingWeapons. It is a separate child with MeleeConeIndicator, MeshFilter, MeshRenderer and Materials/HitAreaWhite (Sprite-Unlit-Default). Its local Transform is identity. Charge opacity is `0.25 * progress^2.5`; Attack is 100%; Recover is `(1-progress)^2.5`; Idle is invisible. On Recover it detaches preserving its world transform; the next Charge reattaches and resets its local transform. A detached indicator destroys itself if its weapon is destroyed. Renderer property blocks keep opacity independent per instance.

**Verification:** Jannis verified the solid cone, phase visibility and charge/recovery fades in WeaponSandbox using WeaponTester. Detachment/reattachment was added and compiles but is not yet confirmed by a moving-weapon test. TestHammer now has longer test timings: Charge 1.5 s, Attack 0, Recover 2 s; damage 10, range 2, angle 90°. Existing melee checks passed for rotation, range/angle, collider edges, multiple enemies and duplicate colliders.

**StartingWeapons configuration complete:** TestDagger, TestFist and TestMetalGlove each have their own settings asset, an assigned MeleeConeAttackBehaviour, an assigned disabled trigger HitArea at local identity, Default target layers and one shared HitAreaIndicator prefab child. Existing weapon GUIDs are preserved. Jannis made the Unity edits. Stats are editable test values; read the assets for current numbers. Fist now uses 360°, which still needs the full-circle collider/visual test noted below.

**Gameplay checks still open:** test the moving player and enemies in SampleScene. Check actual health loss, aim/movement following during Charge and Attack, frozen world position during Recover, repeat use and cancellation. No new movement/aim effect integration is implied by these prefab updates. Do not edit Hugo's player scripts or Maxim's EnemyHealth.

**Later:** resume source tracking in WeaponSandboxUser (PhaseEffects struct exists; methods remain empty), then phase effect requests. Completion outcomes have been promoted to the priority above. Carry phase-clock overshoot across transitions; validate 1°/360° cones and child-collider recipients. Do not restart the older checklist below at step 1.

## Documentation entry point

The maintained weapon docs now live in `Docs/WeaponSystem/`; start at [README.md](README.md). Shared system diagrams live in [Diagrams](Diagrams/README.md). Update their source and implemented/planned status when the corresponding systems change, then regenerate and inspect the Markdown and offline HTML views.

## What we are building

One player call starts **Idle → Charge → Attack → Recover → Idle**. `Weapon` owns the timing and requests the movement/aim effects for each phase. Its attached behaviour performs the attack mechanic. Verify timing briefly with a logging-only behaviour, then build one shared melee cone behaviour. All current melee weapons use Range + AttackAngle; damage is one instantaneous overlap check at Attack entry, followed immediately by Recover. Sprite animation is presentation. No other behaviours are in scope. The player's effect component applies those requests; the weapon releases its own effects when use ends.

Hugo keeps input, slots, buffering, chain reset, aiming and dash movement. The weapon reports completion or interruption, including whether it was in Charge, Attack or Recover. Hugo uses that outcome to decide which slot comes next.

**No blanket dash cleanse.** Dash requests an interruption. Accepted cancellation/early recovery completion already removes this weapon use's effects. **Dash now cancels every active phase, including Attack.** The weapon reports the interrupted phase; Hugo owns the next-slot and buffer decisions. Do not ask Jannis to define Hugo’s slot policy as a prerequisite for implementing the weapon.

## Saved work we keep

All weapon files are under `ImpfanyGame/Assets/Features/Progression/`.

| Existing file/setup | What changes |
| --- | --- |
| `Scripts/Weapon/WeaponSettings.cs` | Keep name, damage and the three phase settings. They already contain duration, movement multiplier and aim lock. Add `Range` (distance) and `AttackAngle` (total degrees) for the agreed melee cone. Do not call the angle Radius. |
| `Scripts/Weapon/WeaponData/TestHammer.asset` | Keep the asset and values: Charge 0.4 s / speed 1 / aim free; Attack currently 0.2 s / speed 0 / aim locked; change duration to 0 when wiring instantaneous melee; Recover 0.7 s / speed 0.5 / aim free. |
| `Scripts/Weapon/Weapon.cs` | Main rewrite: replace the manual attack flag/forwarding API with whole-use entry, current phase, one phase clock, effect requests, interruption and completion notification. |
| `Scripts/Weapon/WeaponAttackBehaviour.cs` | Keep the common base and Begin/End for the actual Attack phase. No generic animation-progress method is required now. Hugo will not call behaviour methods. |
| `Scripts/SwingAttackBehaviour.cs` | Removed, including leftover references on TestHammer and the three StartingWeapons prefabs. Keep it removed; presentation does not define damage mechanics. |
| `Scripts/Test/WeaponSandboxTester.cs` | Replace manual Begin/End tests with whole-use, dash-interruption and forced-abort tests. Show/log phase and end reason. |
| `Prefabs/TestHammer.prefab` | Keep root → Visual → Handle/Head, colours, dimensions, and Settings reference. It now contains MeleeConeAttackBehaviour and HitArea with assigned internal references. No rebuild or reparenting needed. |
| `Scenes/WeaponSandbox.unity` | Keep this private scene. Add a simple effect-recipient component for testing; wire it to the tester. No shared scene/player-prefab changes. |

**Current behaviour:** `Weapon.Attack()` runs automatic phases. TestHammer.prefab has the cone behaviour and dispatches one damage call per IEnemyDamageable recipient. WeaponDamageDummy logs those calls. All three StartingWeapons are configured with independent settings, melee detection and the shared indicator; player effects, end notifications and real enemy integration are pending.

Do not rename `WeaponSettings` again or add `FormerlySerializedAs`. Preserve the existing serialized field names (`settings`, `attackBehaviour`) and Unity `.meta` files. Create new scripts through Unity so they receive metadata.

## Proposed contract

**The ownership is agreed. The following names and implementation choices are the suggested starting contract, not existing API or an additional agreement from Hugo.** Keep the same names in code and handoff if adopted; coordinate any change with Hugo before integration.

New small files belong beside the existing weapon scripts, namespace `Impfany.Weapons`:

| File | Purpose / members |
| --- | --- |
| `WeaponPhase.cs` | Enum: `Idle`, `Charge`, `Attack`, `Recover`. |
| `WeaponUseResult.cs` | Enum: `Completed`, `ChargeCancelled`, `AttackCancelled`, `RecoverySkipped`, `Aborted`. |
| `DashInterruptionResult.cs` | Enum: `NotUsing`, `ChargeCancelled`, `AttackCancelled`, `RecoverySkipped`. |
| `IWeaponUser.cs` | Shared interface implemented by Hugo's player adapter and our sandbox recipient. |
| `WeaponSandboxUser.cs` | Sandbox-only implementation; initially records/displays the requested speed and aim state. It is not a production player controller. |

Proposed public members on `Weapon`:

| Member | Meaning |
| --- | --- |
| `bool Attack(IWeaponUser user)` (planned extension of current `void Attack()`) | Start Charge if idle and configured. False means nothing started; do not emit an end event. |
| `WeaponPhase CurrentPhase { get; }` | Read-only current phase; Idle when available. |
| `event Action<Weapon, WeaponUseResult> UseEnded` | One notification per accepted use after cleanup. |
| `DashInterruptionResult TryInterruptForDash()` | Cancel any active phase and return which one; Idle: no change. |
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
- [ ] **2. Introduce whole-use timing and a logging test behaviour.** Create `WeaponDebugAttackBehaviour.cs`, a sandbox-only concrete behaviour that logs Begin/End and performs no damage or animation. Attach it only to the sandbox weapon instance. Leave the production prefabs unconfigured until they receive real behaviours. Extend `Attack` with the recipient/acceptance result, one phase timer, and automatic Charge → Attack → Recover → Idle transitions. Validate settings, behaviour and recipient before starting. Keep settings assets unchanged. Temporarily have the sandbox supply a simple recipient with no-op effect methods. Change the tester to start a whole use. First check the existing 0.4 / 0.2 / 0.7 sequence, then verify 0.4 / 0 / 0.7 for instantaneous melee and that a second trigger while busy is rejected.
- [ ] **3. Verify behaviour lifetime.** Check Begin runs once on Attack entry and End once on Attack exit. The weapon remains visually static. Use Attack as the public whole-use entry point. Hugo updates his caller; do not retain a BeginAttack compatibility wrapper. EndAttack currently remains the cancellation entry point. Do not turn TestHammer into a projectile weapon. The next real behaviour is the shared melee cone described below.
- [ ] **4. Request and release phase effects.** Implement the sandbox recipient's source tracking. On phase entry, replace this use's effect entry. On completion, remove it. Check the hammer's three speed/aim combinations and that a separate test source survives cleanup. A logged multiplier alone does not verify actual player movement; that check comes with Hugo's adapter.
- [ ] **5. Add end outcomes and interruptions.** Implement the result enums, `UseEnded`, dash interruption and forced abort. Clear active behaviour/effects and set Idle before notifying listeners. Subscribe before starting use, including zero-duration uses. Migrate the sandbox tester to the new API. Keep Attack as the whole-use name. Coordinate later recipient/return-value and cancellation API changes with Hugo; he migrates his player code himself. Do not introduce legacy wrappers. Keep Begin/End on the behaviour base as internal weapon-to-behaviour calls.
- [ ] **6. Connect the real player with Hugo.** Hugo implements the recipient interface, triggers Attack and consumes end outcomes. The result identifies completion, a dash-interrupted phase, or forced abort. Hugo decides chain progression. Aborted must not pretend a normal attack completed. He keeps one active weapon across all slots and processes buffered input only when his player state allows it, including dash. The idle reset remains player-owned and starts after normal or skipped Recover. Cancelled Charge does not itself reset the chain to slot 1.

- [ ] **7. Build one working melee cone weapon.** Add `Range` and `AttackAngle` to `WeaponSettings`, create `MeleeConeAttackBehaviour.cs`, and assign it to TestHammer. Reuse a sector-shaped 2D query collider rooted at the weapon grip, facing local +X; its vertices use Range and ±AttackAngle/2, with a segmented outer arc. Explicitly query overlaps at Attack entry using current slot orientation and filter enemy layers. Do not rely on waiting for trigger callbacks or checking target centres. Deduplicate by the enemy's damage recipient, not collider. Damage each enemy once, then immediately enter Recover with Attack duration 0. Query shape stays inactive outside the check; it must not physically push enemies. Keep the sprite static in code.
- [ ] **8. Verify real damage without editing teammates' scripts.** Use our sandbox dummy first, then connect to Maxim's damage entry point once exposed. EnemyHealth now implements IDamageable explicitly; verify the resulting health changes in gameplay without editing his file or writing `health` directly. Keep any temporary dummy damage recipient within our feature folder. Test enemies inside/outside range and angle, collider-edge overlaps, multiple colliders on one enemy, multiple enemies, and rotated weapon slots. Each use must deal exactly one hit per overlapping enemy.

**Timing detail for the tutor:** carry unused frame time into following phases, handle zero-duration phases without division by zero, and finish the active behaviour at Attack end. Only `Weapon` determines phase completion. Zero-duration melee Attack still calls its behaviour once before transitioning to Recover in the same update; do not skip the hit or insert a mandatory extra frame. Burst/projectile behaviours may later maintain shot intervals inside Attack.

**Cleanup detail:** use one cleanup path for normal completion, accepted interruption and forced abort. It stops the active behaviour, removes that use's effects and emits at most one end event. `OnDisable` uses forced cleanup; death/unequip code calls it before removing the recipient. When damage behaviours are added, stop active hit areas, continuous fire and remaining burst shots; do not undo dealt damage or despawn already-fired projectiles. Avoid finishing a new use accidentally if an end listener immediately starts another one.

## Unity wiring and checks

Once the scripts compile, open **WeaponSandbox**, not SampleScene. Keep TestHammer's existing assignments. Add `WeaponSandboxUser` to the separate WeaponTester object and expose a tester reference to it; the tester passes it to Attack. Save the scene and prefab outside Play Mode. The weapon prefab must not store a reference to this scene object.

Check the phase/effect milestones in the sandbox, then the cone-hit checks in steps 7–8:

- [ ] One trigger completes all phases, returns Idle, clears its effects and can be triggered again.
- [ ] Dash during Charge, Attack and Recover ends the use immediately, returns Idle, clears its effects and reports the interrupted phase exactly once. During Charge, the attack behaviour must never start.
- [ ] Forced abort/disable during each active phase stops the active behaviour and removes its effects exactly once. Re-enabling starts idle.
- [ ] Zero-duration phases and repeated input do not stick or emit duplicate completion. Unconfigured weapons reject use cleanly.
- [ ] Other effect sources survive weapon cleanup. Consecutive aim-locked phases do not briefly unlock.
- [ ] With Hugo's adapter, check actual walking speed/lock, live aim/aim lock, Hugo’s slot handling and the ~0.5 s input buffer. Dash and knockback remain separate from walking restrictions.

**Next tutoring turn: follow the priority update in the resume checkpoint above.** Implement the whole-use completion/acceptance contract so Hugo can replace his temporary timer; source tracking in WeaponSandboxUser follows later. Additional attack mechanics, progression and sprite animation systems remain future work; the melee indicator is implemented as described above.
