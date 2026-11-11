# Next implementation — weapon-owned phases

**Status: partially implemented in tutoring mode · 11 November 2026.** This is the continuation guide for Jannis and the next tutoring model. Design: [Jannis.md](Jannis.md). Player integration: [German handoff](Handoff-Hugo-Waffen.md).

**Tutor mode:** Jannis types the code and makes Unity changes. Explain one small, compilable step and why it exists, then wait for him. Inspect saved files before continuing; “done” sometimes means the editor buffer is still unsaved. Do not dump full scripts, silently implement the plan, or edit Hugo's player files. Docs and Git changes need their own request. Do not push as part of this checklist.

## Resume checkpoint — read this before the older plan

**Next action: teach IWeaponUser and the sandbox effect receiver. Do not restart at step 1.** Read this checkpoint, the melee scope in Jannis.md, and the latest saved `Weapon.cs`, `WeaponSettings.cs`, `MeleeConeAttackBehaviour.cs` and sandbox scene references before continuing. The checklist below is the original roadmap; this checkpoint overrides its outdated implementation-status statements.

- Tutor style: begin each step with **“Now we’re doing X because Y; do Z.”** Explain the purpose before procedures. Small, understandable code additions; Jannis writes/saves them. He declined having the geometry helper written for him. Do not edit scripts/assets autonomously; the previous repair was a specifically authorized exception.
- `WeaponPhase.cs` exists: Idle, Charge, Attack, Recover. `Weapon.CurrentPhase` is public/read-only externally, initialized Idle. The redundant `isAttacking` flag was removed.
- `Weapon.BeginAttack()` now starts Charge; `Update()` advances through all phases automatically; `EndAttack()` stops an active Attack behaviour if needed and returns Idle. Existing public method names remain for Hugo/tester compatibility. `OnDisable()` calls EndAttack.
- Jannis manually verified the whole sequence, repeat use, and zero-duration Attack with the logging behaviour. Timings now saved: Charge 0.4, Attack 0, Recover 0.7. Name Test Hammer, Damage 10, Range 2, AttackAngle 90. Range and AttackAngle fields/getters exist in WeaponSettings. Unused VisualScripting/Rendering imports were removed.
- Sandbox TestHammer now uses `MeleeConeAttackBehaviour`, replacing the attached debug component. `WeaponDebugAttackBehaviour.cs` still exists for testing. `WeaponSandboxTester` still has manual Begin/End ContextMenu actions, referencing the hammer instance.
- Sandbox HitArea is a direct child of TestHammer root, alongside Visual. Local position/rotation zero, scale one. PolygonCollider2D is a trigger and disabled. It is assigned to the melee component. Jannis applied the setup to TestHammer.prefab. Its Weapon settings/behaviour and Melee hitArea references are verified; hitArea belongs to the prefab, disabled/trigger=true, local identity; prefab root rotation is zero. Three StartingWeapons remain unconfigured.
- Latest saved melee code: BeginAttack guards against a missing hitArea, calls BuildHitArea(settings), calls FindTargets(), calls ApplyDamage(settings.Damage), and logs hitResults.Count. EndAttack is empty. BuildHitArea uses 16 segments, an 18-element Vector2 array, origin at index 0, and a loop i=0..16. progress=i/(float)segments; angle=Lerp(-halfAngle,halfAngle,progress); radians=angle*Deg2Rad; points[i+1]=Vector2(Cos(radians),Sin(radians))*Range. After loop: pathCount=1; SetPath(0,points).
- Cone points inspected in Play Mode: 18 points, range 2, total angle 90°. FindTargets uses a reusable List<Collider2D>, ContactFilter2D.SetLayerMask(targetLayers), useTriggers=true, enables hitArea, SyncTransforms, Overlap(filter, hitResults), and disables it in finally. Sandbox targetLayers is Default; the old Player object is disabled. TestTarget is a square with BoxCollider2D at (1.25, 0), scale (0.3, 0.3, 1). Jannis confirmed 1 result there, 0 at (3, 0), and 0 at (1, 1.6). IDamageable.cs and WeaponDamageDummy.cs exist and compile. ApplyDamage clears HashSet<IDamageable>, resolves each hit.GetComponentInParent<IDamageable>(), and calls TakeDamage only if non-null and newly added to the set. Dummy logs incoming damage; it does not reduce health. Jannis verified: 10 damage once for one collider; once for two colliders on one target; twice total for two separate targets/four colliders; rotated weapon (90°) hits the upper target only; target centre at (0, 2.05) still hit because colliders overlap range 2. Scene currently retains two targets with two colliders each, hammer rotation 90°, original target at (0, 2.05), duplicate at (3.82, 1.08). IWeaponUser code was supplied but no file created yet.
- Player effect requests, completion/outcome notifications, explicit dash API and production player integration are not implemented. Existing EndAttack can stop any phase. Dash may cancel **every phase**, including Attack; the weapon reports phase/outcome later, and Hugo decides slot/buffer handling. No need to ask Jannis for that player decision. An executed instantaneous hit cannot be undone.
- Technical follow-ups: the current phase clock resets elapsed time at transitions and loses frame overshoot (roadmap calls for preserving it later). The 360° angle case needs a full-circle outline without a centre seam/duplicate endpoints; current tested target is 90°, and 1° should also be tested. Collider-edge overlap and deduplication by damage recipient are verified. Overlap and SyncTransforms API confirmed against Unity 6000.3 docs; collider is disabled again in finally. Child-collider recipient lookup and the 1°/360° cases remain untested.
- Maxim's current EnemyHealth.Damage(float) is private. Do not bypass enemy ownership by writing its health field. Use our sandbox damage dummy first or integrate through his callable entry point when available.
- Last fresh Unity status: not compiling, compilationFailed=false, zero current errors/warnings. Scripts compile; damage dispatch to logging dummies is manually verified. Real enemy integration remains.
- Git: main. This checkpoint accompanies the phase/cone prototype sync requested by Jannis; consult git log for its commit. Preserve unrelated `Docs/Impfany/Overview.md` edits, .DS_Store files and .vscode folder. Do not push unless requested. Old duplicate docs under Docs/Impfany now link to the maintained Docs/ versions. Prior stashes remain untouched.

## What we are building

One player call starts **Idle → Charge → Attack → Recover → Idle**. `Weapon` owns the timing and requests the movement/aim effects for each phase. Its attached behaviour performs the attack mechanic. Verify timing briefly with a logging-only behaviour, then build one shared melee cone behaviour. All current melee weapons use Range + AttackAngle; damage is one instantaneous overlap check at Attack entry, followed immediately by Recover. Sprite animation is presentation. No other behaviours are in scope. The player's effect component applies those requests; the weapon releases its own effects when use ends.

Hugo keeps input, slots, buffering, chain reset, aiming and dash movement. The weapon reports completion or interruption, including whether it was in Charge, Attack or Recover. Hugo uses that outcome to decide which slot comes next.

**No blanket dash cleanse.** Dash requests an interruption. Accepted cancellation/early recovery completion already removes this weapon use's effects. **Dash now cancels every active phase, including Attack.** The weapon reports the interrupted phase; Hugo owns the next-slot and buffer decisions. Do not ask Jannis to define Hugo’s slot policy as a prerequisite for implementing the weapon.

## Saved work we keep

All weapon files are under `ImpfanyGame/Assets/Features/Progression/`.

| Existing file/setup | What changes |
| --- | --- |
| `Scripts/WeaponSettings.cs` | Keep name, damage and the three phase settings. They already contain duration, movement multiplier and aim lock. Add `Range` (distance) and `AttackAngle` (total degrees) for the agreed melee cone. Do not call the angle Radius. |
| `WeaponData/TestHammer.asset` | Keep the asset and values: Charge 0.4 s / speed 1 / aim free; Attack currently 0.2 s / speed 0 / aim locked; change duration to 0 when wiring instantaneous melee; Recover 0.7 s / speed 0.5 / aim free. |
| `Scripts/Weapon.cs` | Main rewrite: replace the manual attack flag/forwarding API with whole-use entry, current phase, one phase clock, effect requests, interruption and completion notification. |
| `Scripts/WeaponAttackBehaviour.cs` | Keep the common base and Begin/End for the actual Attack phase. No generic animation-progress method is required now. Hugo will not call behaviour methods. |
| `Scripts/SwingAttackBehaviour.cs` | Removed, including leftover references on TestHammer and the three StartingWeapons prefabs. Keep it removed; presentation does not define damage mechanics. |
| `Scripts/WeaponSandboxTester.cs` | Replace manual Begin/End tests with whole-use, dash-interruption and forced-abort tests. Show/log phase and end reason. |
| `Prefabs/TestHammer.prefab` | Keep root → Visual → Handle/Head, colours, dimensions, and Settings reference. It now contains MeleeConeAttackBehaviour and HitArea with assigned internal references. No rebuild or reparenting needed. |
| `Scenes/WeaponSandbox.unity` | Keep this private scene. Add a simple effect-recipient component for testing; wire it to the tester. No shared scene/player-prefab changes. |

**Current behaviour:** `Weapon.BeginAttack()` runs automatic phases. TestHammer.prefab has the cone behaviour and dispatches one damage call per IDamageable recipient. WeaponDamageDummy logs those calls. The three StartingWeapons prefabs remain unconfigured placeholders; player effects, end notifications and real enemy integration are pending.

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
| `bool TryUse(IWeaponUser user)` | Start Charge if idle and configured. False means nothing started; do not emit an end event. |
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
- [ ] **2. Introduce whole-use timing and a logging test behaviour.** Create `WeaponDebugAttackBehaviour.cs`, a sandbox-only concrete behaviour that logs Begin/End and performs no damage or animation. Attach it only to the sandbox weapon instance. Leave the production prefabs unconfigured until they receive real behaviours. Add `TryUse`, one phase timer, and automatic Charge → Attack → Recover → Idle transitions. Validate settings, behaviour and recipient before starting. Keep settings assets unchanged. Temporarily have the sandbox supply a simple recipient with no-op effect methods. Change the tester to start a whole use. First check the existing 0.4 / 0.2 / 0.7 sequence, then verify 0.4 / 0 / 0.7 for instantaneous melee and that a second trigger while busy is rejected.
- [ ] **3. Verify behaviour lifetime.** Check Begin runs once on Attack entry and End once on Attack exit. The weapon remains visually static. Preserve the existing public Weapon BeginAttack/EndAttack calls until Hugo’s caller has migrated, so his player still compiles. Do not turn TestHammer into a projectile weapon. The next real behaviour is the shared melee cone described below.
- [ ] **4. Request and release phase effects.** Implement the sandbox recipient's source tracking. On phase entry, replace this use's effect entry. On completion, remove it. Check the hammer's three speed/aim combinations and that a separate test source survives cleanup. A logged multiplier alone does not verify actual player movement; that check comes with Hugo's adapter.
- [ ] **5. Add end outcomes and interruptions.** Implement the result enums, `UseEnded`, dash interruption and forced abort. Clear active behaviour/effects and set Idle before notifying listeners. Subscribe before starting use, including zero-duration uses. Migrate the sandbox tester to the new API. Remove the obsolete public Weapon BeginAttack/EndAttack entry points only after Hugo has migrated PlayerAttack; do not edit his player code here. Keep Begin/End on the behaviour base as internal weapon-to-behaviour calls.
- [ ] **6. Connect the real player with Hugo.** Hugo implements the recipient interface, triggers TryUse and consumes end outcomes. The result identifies completion, a dash-interrupted phase, or forced abort. Hugo decides chain progression. Aborted must not pretend a normal attack completed. He keeps one active weapon across all slots and processes buffered input only when his player state allows it, including dash. The idle reset remains player-owned and starts after normal or skipped Recover. Cancelled Charge does not itself reset the chain to slot 1.

- [ ] **7. Build one working melee cone weapon.** Add `Range` and `AttackAngle` to `WeaponSettings`, create `MeleeConeAttackBehaviour.cs`, and assign it to TestHammer. Reuse a sector-shaped 2D query collider rooted at the weapon grip, facing local +X; its vertices use Range and ±AttackAngle/2, with a segmented outer arc. Explicitly query overlaps at Attack entry using current slot orientation and filter enemy layers. Do not rely on waiting for trigger callbacks or checking target centres. Deduplicate by the enemy's damage recipient, not collider. Damage each enemy once, then immediately enter Recover with Attack duration 0. Query shape stays inactive outside the check; it must not physically push enemies. Keep the sprite static in code.
- [ ] **8. Verify real damage without editing teammates' scripts.** Use our sandbox dummy first, then connect to Maxim's damage entry point once exposed. His current `EnemyHealth.Damage(float)` is private, so weapons cannot call it directly; do not silently edit his file or bypass it by writing `health` directly. Keep any temporary dummy damage recipient within our feature folder. Test enemies inside/outside range and angle, collider-edge overlaps, multiple colliders on one enemy, multiple enemies, and rotated weapon slots. Each use must deal exactly one hit per overlapping enemy.

**Timing detail for the tutor:** carry unused frame time into following phases, handle zero-duration phases without division by zero, and finish the active behaviour at Attack end. Only `Weapon` determines phase completion. Zero-duration melee Attack still calls its behaviour once before transitioning to Recover in the same update; do not skip the hit or insert a mandatory extra frame. Burst/projectile behaviours may later maintain shot intervals inside Attack.

**Cleanup detail:** use one cleanup path for normal completion, accepted interruption and forced abort. It stops the active behaviour, removes that use's effects and emits at most one end event. `OnDisable` uses forced cleanup; death/unequip code calls it before removing the recipient. When damage behaviours are added, stop active hit areas, continuous fire and remaining burst shots; do not undo dealt damage or despawn already-fired projectiles. Avoid finishing a new use accidentally if an end listener immediately starts another one.

## Unity wiring and checks

Once the scripts compile, open **WeaponSandbox**, not SampleScene. Keep TestHammer's existing assignments. Add `WeaponSandboxUser` to the separate WeaponTester object and expose a tester reference to it; the tester passes it to TryUse. Save the scene and prefab outside Play Mode. The weapon prefab must not store a reference to this scene object.

Check the phase/effect milestones in the sandbox, then the cone-hit checks in steps 7–8:

- [ ] One trigger completes all phases, returns Idle, clears its effects and can be triggered again.
- [ ] Dash during Charge, Attack and Recover ends the use immediately, returns Idle, clears its effects and reports the interrupted phase exactly once. During Charge, the attack behaviour must never start.
- [ ] Forced abort/disable during each active phase stops the active behaviour and removes its effects exactly once. Re-enabling starts idle.
- [ ] Zero-duration phases and repeated input do not stick or emit duplicate completion. Unconfigured weapons reject use cleanly.
- [ ] Other effect sources survive weapon cleanup. Consecutive aim-locked phases do not briefly unlock.
- [ ] With Hugo's adapter, check actual walking speed/lock, live aim/aim lock, Hugo’s slot handling and the ~0.5 s input buffer. Dash and knockback remain separate from walking restrictions.

**Next tutoring turn: follow the resume checkpoint above.** Phase basics and cone damage dispatch have been verified; teach IWeaponUser and the sandbox effect receiver next. Additional behaviours, progression and visual animation systems are out of scope.
