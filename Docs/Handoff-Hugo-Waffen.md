# Hugo — Übergabe Waffen ↔ Spieler

**Neues Design · 11.11.2026: Ein Aufruf startet die komplette Waffennutzung.** Die Waffe läuft selbst durch **Charge → Attack → Recover** und meldet anschließend das Ende. Diese Aufteilung ersetzt die frühere Übergabe, in der dein Player alle drei Phasen zeitlich steuert.

**Noch umzusetzen:** Im Code gibt es bisher `Weapon.BeginAttack()`, `EndAttack()` und `Settings`. Begin/End steuern nur den eigentlichen Angriff. Die neue Phasensteuerung und Effektschnittstelle existieren noch nicht. Der Swing-Code wurde entfernt; dem Testhammer fehlt aktuell ein Attack Behaviour. Der bestehende Aufruf `BeginAttack()` meldet bei diesem Prefab deshalb einen Konfigurationsfehler. Projektilverhalten, Treffer und Schaden fehlen noch.

## Wer baut was?

| Hugo — Spieler | Jannis — Waffen |
| --- | --- |
| Input, ca. 0,5-s-Buffer, Slots 1–3 und Ketten-Reset | Waffen-Prefabs, Settings und spätere Upgrades |
| Eine ausgewählte Waffe auslösen | Charge → Attack → Recover vollständig ausführen |
| Effektkomponente: Bewegung und Aim tatsächlich beeinflussen | Pro Phase Bewegungs-/Aim-Effekte anfordern und danach entfernen lassen |
| Dash ausführen und Unterbrechung anfragen | Je nach Phase abbrechen, Recover beenden oder Unterbrechung ablehnen |
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

**Vorgeschlagene Namen, noch nicht implementiert.** Der genaue technische Vertrag steht im [Umbauplan](WeaponImplementationNext.md#proposed-contract).

| Zugriff | Zweck |
| --- | --- |
| `weapon.TryUse(user)` | Ganze Nutzung starten; meldet, ob sie angenommen wurde |
| `weapon.CurrentPhase` | Idle, Charge, Attack oder Recover lesen |
| `weapon.UseEnded` | Ende mit Grund erhalten: fertig, Charge abgebrochen, Recover übersprungen oder anderweitig abgebrochen |
| `weapon.TryInterruptForDash()` | Phasenabhängige Unterbrechung anfragen |
| `weapon.AbortUse()` | Beim Weglegen/Tod zuverlässig aufräumen; kein normaler Dash-Aufruf |
| `weapon.Settings` | Vorhandene Werte für Anzeige und Vorschau lesen |

**Effekte:** Deine Komponente nimmt Bewegungsmultiplikator und Aim-Lock mit einer Quellenkennung entgegen. Ein Phasenwechsel ersetzt den Eintrag dieser Quelle. Am Ende entfernt die Waffe nur diesen Eintrag über deine Komponente. Andere Buffs oder Slows bleiben bestehen. Die Waffe setzt niemals direkt deine Bewegungsgeschwindigkeit oder Rigidbody-Geschwindigkeit.

Jannis liefert die kleine Schnittstelle in seinem Bereich; Hugo implementiert sie am Player. Zum unabhängigen Testen verwendet Jannis einen Sandbox-Empfänger. Vor dem Anschließen bitte die vorgeschlagenen Namen abgleichen; die Rollenverteilung ist bereits entschieden.

**Stand von Hugos PlayerAttack:** Der aktuelle Code ruft bereits `BeginAttack()` auf und schaltet den Slot direkt weiter. Dieser Prototyp muss später auf die neue Nutzung und deren Ergebnis umgestellt werden; die neue API existiert noch nicht.

## Dash

| Aktuelle Phase | Reaktion der Waffe | Folge für den Slot |
| --- | --- | --- |
| Charge | Nutzung abbrechen, eigene Effekte entfernen | Gleiche Waffe erneut versuchen |
| Attack | Unterbrechung ablehnen; Angriff läuft weiter | Kein Wechsel |
| Recover | Recovery sofort beenden, eigene Effekte entfernen | Nächster Slot bereit |
| Idle | Nichts abzubrechen | Keine Änderung |

**Dash-Bewegung bleibt bei dir.** Das Ergebnis der Unterbrechung sagt, was mit der Waffe passiert ist. Es definiert nicht automatisch, ob ein Dash während Attack grundsätzlich erlaubt ist; festgelegt ist bisher nur, dass er Attack nicht unterbricht.

Dash braucht kein zusätzliches „alle Effekte löschen“. Die Waffe räumt ihre eigenen Einschränkungen auf. Eine Reinigung von gegnerischen Effekten ist keine vereinbarte Dash-Funktion. Normales Ende, Abbruch und Deaktivieren müssen dieselbe zuverlässige Bereinigung benutzen.

## Prefab und Zielen

Dein Player-Prefab liegt unter `Assets/Prefabs/Player/Player.prefab`. Unter `WaponHolder/WeaponSlot1` bis `WeaponSlot3` kommt jeweils ein Waffen-Prefab mit `Weapon` am Root. Namespace: `Impfany.Weapons`. Leere Slots starten keine Nutzung.

Der Waffenroot ist der Griffpunkt und zeigt bei Rotation 0 nach **lokal +X (rechts)**. Du richtest den Slot auf das aktuelle Aim aus. Deine Effektkomponente hält diese Richtung fest, solange ein Aim-Lock gilt. Wir animieren ausschließlich den `Visual`-Unterbereich.

Jannis' Dateien liegen unter `Assets/Features/Progression/`: `Prefabs/TestHammer.prefab`, `WeaponData/TestHammer.asset`, `Scripts/` und `Scenes/WeaponSandbox.unity`. Die bestehende Prefab-Hierarchie und die Settings-Referenz bleiben; ein neues Attack Behaviour muss später zugewiesen werden. Der Sandbox-Tester gehört nicht auf deinen Player.

## Vorhandene Settings

`weapon.Settings` liefert `DisplayName`, `Damage` und `Charge` / `Attack` / `Recover`. Jede Phase hat `Duration`, `MovementSpeedMultiplier` und `LockAim`.

| Testhammer: 10 Schaden | Sekunden | Bewegung | Aim-Lock |
| --- | --- | --- | --- |
| Charge | 0,4 | 1 | Nein |
| Attack | 0,2 | 0 | Ja |
| Recover | 0,7 | 0,5 | Nein |

0 = kein Laufen, 0,5 = halbes Tempo, 1 = normal, 1,5 = schneller. Ein Lauf-Lock blockiert nicht automatisch Dash oder eingehenden Knockback. Settings-Assets zur Laufzeit nicht verändern; spätere Upgrades gehören zur Waffenkopie.

Weitere Spielregeln: [WeaponsAndProgression.md](WeaponsAndProgression.md). Jannis' nächste Schritte: [Umbauplan](WeaponImplementationNext.md).
