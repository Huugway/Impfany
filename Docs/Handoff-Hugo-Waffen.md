# Hugo — Übergabe Waffen ↔ Spieler

**Neues Design · 11.11.2026: Ein Aufruf startet die komplette Waffennutzung.** Die Waffe läuft selbst durch **Charge → Attack → Recover** und meldet anschließend das Ende. Diese Aufteilung ersetzt die frühere Übergabe, in der dein Player alle drei Phasen zeitlich steuert.

**Aktueller Code:** `Attack()` startet Charge → Attack → Recover; `CurrentPhase` und `PhaseElapsed` sind lesbar. `PhaseChanged` meldet Phasenwechsel für die Darstellung. `EndAttack()` bricht die Nutzung ab; ein normaler End-Aufruf durch den Player ist nicht nötig. TestDagger, TestFist und TestMetalGlove sind jetzt mit eigenem Settings-Asset, Nahkampfverhalten, HitArea und gemeinsamem weißem Indikator konfiguriert. Der Kegel trifft jeden IDamageable-Empfänger einmal; Maxims EnemyHealth implementiert diese Schnittstelle inzwischen. Die Verbindung wurde mit Dummys geprüft; Tests mit dem bewegten Player und echten Gegnern stehen noch aus. `IWeaponUser` ist angelegt; Effektanbindung, Endmeldungen und die gesonderte Dash-API fehlen noch.

## Wer baut was?

| Hugo — Spieler | Jannis — Waffen |
| --- | --- |
| Input, ca. 0,5-s-Buffer, Slots 1–3 und Ketten-Reset | Waffen-Prefabs, Settings und spätere Upgrades |
| Eine ausgewählte Waffe auslösen | Charge → Attack → Recover vollständig ausführen |
| Effektkomponente: Bewegung und Aim tatsächlich beeinflussen | Pro Phase Bewegungs-/Aim-Effekte anfordern und danach entfernen lassen |
| Dash ausführen und Unterbrechung anfragen | In jeder aktiven Phase abbrechen und zuverlässig aufräumen |
| Auf das Ende reagieren und den passenden Slot freigeben | Schwung/Schüsse/Treffer sowie zuverlässiges Aufräumen |

Maxim nimmt Treffer entgegen und meldet tatsächlich verursachten Schaden. Seine Treffer-Schnittstelle ist noch abzustimmen.

## Ablauf

1. Dein Player wählt die Waffe und startet ihre Nutzung einmal. Er übergibt dabei einen Zugriff auf seine Effektschnittstelle.
2. Die Waffe startet Charge und fordert die dazugehörigen Effekte an.
3. Sie wechselt selbst zu Attack, ersetzt ihre Phaseneffekte und führt den Angriff aus.
4. Sie beendet den Angriff und wechselt mit neuen Phaseneffekten zu Recover.
5. Nach Recover lässt sie ihre Effekte entfernen und meldet „fertig“. Dein Player gibt den nächsten Slot frei und startet den Idle-Reset-Timer.

**Dein Player braucht keine eigenen Charge-/Attack-/Recover-Timer und keinen normalen EndAttack-Aufruf.** Er verhindert, dass mehrere Slots gleichzeitig benutzt werden. Bereits abgefeuerte Projektile dürfen weiterfliegen.

## Kleine gemeinsame Schnittstelle

**Jetzt verwendbar:** `void weapon.Attack()` startet die ganze Nutzung. `CurrentPhase`, `Settings` und `EndAttack()` existieren ebenfalls. `BeginAttack()` wurde auf Weapon entfernt; PlayerAttack ruft jetzt `weapon.Attack()` auf. Es gibt keinen Kompatibilitäts-Wrapper. Die unten beschriebene Empfängerübergabe und Endmeldungen sind noch geplant. Der genaue technische Vertrag steht im [Umbauplan](WeaponImplementationNext.md#proposed-contract).

| Zugriff | Zweck |
| --- | --- |
| `weapon.Attack(user)` (geplante Erweiterung) | Ganze Nutzung starten; meldet, ob sie angenommen wurde |
| `weapon.CurrentPhase` | Idle, Charge, Attack oder Recover lesen |
| `weapon.UseEnded` | Ende mit Grund erhalten: fertig, Charge/Attack/Recover durch Dash beendet oder anderweitig abgebrochen |
| `weapon.TryInterruptForDash()` | In jeder aktiven Phase abbrechen; unterbrochene Phase zurückmelden |
| `weapon.AbortUse()` | Beim Weglegen/Tod zuverlässig aufräumen; kein normaler Dash-Aufruf |
| `weapon.Settings` | Vorhandene Werte für Anzeige und Vorschau lesen |

**Effekte:** Deine Komponente nimmt Bewegungsmultiplikator und Aim-Lock mit einer Quellenkennung entgegen. Ein Phasenwechsel ersetzt den Eintrag dieser Quelle. Am Ende entfernt die Waffe nur diesen Eintrag über deine Komponente. Andere Buffs oder Slows bleiben bestehen. Die Waffe setzt niemals direkt deine Bewegungsgeschwindigkeit oder Rigidbody-Geschwindigkeit.

Jannis liefert die kleine Schnittstelle in seinem Bereich; Hugo implementiert sie am Player. Zum unabhängigen Testen verwendet Jannis einen Sandbox-Empfänger. Vor dem Anschließen bitte die vorgeschlagenen Namen abgleichen; die Rollenverteilung ist bereits entschieden.

**Stand von Hugos PlayerAttack:** Der Aufruf wurde auf Jannis’ Wunsch zu `weapon.Attack()` korrigiert. Die übrige Player-Logik bleibt bei dir. Der Slot wird bisher direkt weitergeschaltet. Die spätere Empfängerübergabe und Ergebnisverarbeitung bleiben weitere Integrationsschritte. `BeginAttack(settings)` auf WeaponAttackBehaviour bleibt bestehen: Das startet intern nur die eigentliche Attack-Phase.

## Dash

| Aktuelle Phase | Reaktion der Waffe |
| --- | --- |
| Charge | Nutzung abbrechen, eigene Effekte entfernen, Phase melden |
| Attack | Angriff sofort stoppen, eigene Effekte entfernen, Phase melden |
| Recover | Recovery sofort beenden, eigene Effekte entfernen, Phase melden |
| Idle | Nichts abzubrechen |

**Neue Regel: Dash kann jede aktive Phase unterbrechen, auch Attack.** Bereits verursachter Schaden bleibt bestehen; bereits abgefeuerte Projektile fliegen weiter. Aktive Trefferbereiche, Dauerfeuer und noch nicht abgefeuerte Burst-Schüsse werden gestoppt.

**Dash-Bewegung und die anschließende Slot-/Buffer-Entscheidung bleiben bei dir.** Unsere Waffe meldet das Ergebnis mit der unterbrochenen Phase. Die Waffenimplementierung legt keinen Slotwechsel fest.

Dash braucht kein zusätzliches „alle Effekte löschen“. Die Waffe räumt ihre eigenen Einschränkungen auf. Eine Reinigung von gegnerischen Effekten ist keine vereinbarte Dash-Funktion. Normales Ende, Abbruch und Deaktivieren müssen dieselbe zuverlässige Bereinigung benutzen.

## Prefab und Zielen

Dein Player-Prefab liegt unter `Assets/Features/Player/Prefabs/Player.prefab`. Unter `WaponHolder/WeaponSlot1` bis `WeaponSlot3` kommt jeweils ein Waffen-Prefab mit `Weapon` am Root. Namespace: `Impfany.Weapons`. Leere Slots starten keine Nutzung.

Der Waffenroot ist der Griffpunkt und zeigt bei Rotation 0 nach **lokal +X (rechts)**. Du richtest den Slot auf das aktuelle Aim aus. Deine Effektkomponente hält diese Richtung fest, solange ein Aim-Lock gilt. Die Waffen können vorerst statisch bleiben. Spätere Sprite-Animationen sind von der Trefferlogik getrennt; ein vorhandener `Visual`-Unterbereich bleibt für die Darstellung nutzbar.

Jannis' Dateien liegen unter `Assets/Features/Progression/`: `Prefabs/TestHammer.prefab`, `Scripts/Weapon/WeaponData/TestHammer.asset`, `Scripts/` und `Scenes/WeaponSandbox.unity`. TestHammer.prefab enthält Weapon, MeleeConeAttackBehaviour und den deaktivierten Trigger-Collider HitArea mit internen Referenzen. Target Layers steht für den Sandbox-Test auf Default; bei der Gegnerintegration auf die vereinbarten Gegner-Layer einstellen. Der Sandbox-Tester gehört nicht auf deinen Player.

## Vereinbarter Nahkampf — aktueller Umfang

Alle Nahkampfwaffen verwenden vorerst denselben **Kegel-Treffercheck**:

- `Range`: Entfernung vom Waffenroot bis zum äußeren Kegelrand.
- `AttackAngle`: gesamte Kegelbreite in Grad, z. B. Dolch 1°, Hammer 90°. 90° bedeutet ±45° um die Slot-Richtung. Der Winkel heißt nicht „Radius“.
- Nach Charge erfolgt **ein sofortiger Overlap-Check**, der jedes überlappende Enemy einmal trifft. Mehrere Collider desselben Enemy verursachen keinen Mehrfachschaden.
- Danach sofort Recover. Für diese Waffen ist `Attack.Duration = 0`; wir warten keinen zusätzlichen Frame auf Physik-Callbacks. Sprite-Animation und Schadenszeitpunkt sind getrennt.
- Dash kann jede aktive Phase beenden. Ein bereits ausgeführter Treffer wird nicht rückgängig gemacht; bei einem sofortigen Angriff ist danach bereits Recover aktiv.

`Range`, `AttackAngle` und die explizite Kegel-Overlap-Abfrage sind implementiert. Im Sandbox-Test: Ziel im Kegel erkannt; Ziele außerhalb von Reichweite oder Winkel ausgeschlossen. Der Schadensaufruf und Schutz vor Mehrfachtreffern sind mit Dummys getestet; Maxim muss seine Gegner noch über IDamageable oder einen passenden Adapter anbinden. Andere Angriffsverhalten sind aktuell nicht im Umfang.

## Vorhandene Settings

`weapon.Settings` liefert `DisplayName`, `Damage`, `Range`, `AttackAngle` und `Charge` / `Attack` / `Recover`. Jede Phase hat `Duration`, `MovementSpeedMultiplier` und `LockAim`.

| Testhammer: 10 Schaden | Sekunden | Bewegung | Aim-Lock |
| --- | --- | --- | --- |
| Charge | 0,4 | 1 | Nein |
| Attack | 0 | 0 | Ja |
| Recover | 0,7 | 0,5 | Nein |

0 = kein Laufen, 0,5 = halbes Tempo, 1 = normal, 1,5 = schneller. Ein Lauf-Lock blockiert nicht automatisch Dash oder eingehenden Knockback. Settings-Assets zur Laufzeit nicht verändern; spätere Upgrades gehören zur Waffenkopie.

Weitere Spielregeln: [WeaponsAndProgression.md](WeaponsAndProgression.md). Jannis' nächste Schritte: [Umbauplan](WeaponImplementationNext.md).
