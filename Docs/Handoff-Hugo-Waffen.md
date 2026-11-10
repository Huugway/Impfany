# Hugo — Übergabe Waffen ↔ Spieler

**Stand: 10.11.2026.** Das Hammer-Prefab und seine Einstellungen sind vorhanden. **Es greift noch nicht an; eine Trigger-Methode gibt es noch nicht.** Bitte bestätige den Vorschlag unten oder schreib deine gewünschte Schnittstelle dazu, bevor wir die Angriffe implementieren.

## Wer baut was?

| Hugo — Spieler | Jannis — Waffen |
| --- | --- |
| Waffen halten, ausrüsten und zwischen Slot 1–3 wechseln | Waffen-Prefabs, Aussehen und Werte |
| Input, 0,5-s-Inputbuffer und Ketten-Reset | Waffenspezifische Angriffe: Schwung, Projektil, Flammen usw. |
| Charge → Attack → Recover zeitlich steuern | Auf deine Aufrufe reagieren; Treffer erkennen bzw. Projektile erzeugen |
| Zielen, Aim-Locks, Bewegung und Dash-Regeln anwenden | Phasenwerte liefern; später Upgrades je Waffenkopie |

Maxim nimmt Treffer entgegen und meldet den tatsächlich verursachten Schaden zurück. Seine konkrete Treffer-Schnittstelle ist noch abzustimmen.

## Was du jetzt verwenden kannst

Alles liegt unter `ImpfanyGame/Assets/Features/Progression/`:

- `Prefabs/TestHammer.prefab`: Root mit `Weapon`-Komponente, darunter `Visual/Handle` und `Visual/Head`.
- `WeaponData/TestHammer.asset`: gemeinsame Grundwerte dieser Waffenart.
- `Scripts/Weapon.cs` und `Scripts/WeaponDefinition.cs`: Namespace **`Impfany.Weapons`**.
- `Scenes/WeaponSandbox.unity`: Jannis' eigene Testszene.

Der Waffenroot ist der **Griff-/Befestigungspunkt**; die Grafik zeigt bei Rotation 0 nach **lokal +X (rechts)**. Vorschlag für deine Hierarchie:

```text
Player
├── WeaponSlot1
│   └── TestHammer (Prefab-Instanz)
├── WeaponSlot2
└── WeaponSlot3
```

Die Waffe liest keinen Input und sucht sich keinen Spieler. Du setzt ihre Position und Zielausrichtung über den Slot bzw. Waffenroot.

**Bereits lesbar** über die `Weapon`-Komponente am Waffenroot:

| Zugriff | Bedeutung |
| --- | --- |
| `weapon.Definition.DisplayName` | Anzeigename |
| `weapon.Definition.Damage` | Grundschaden pro Treffer |
| `weapon.Definition.Charge`, `.Attack`, `.Recover` | Die drei Phasen |
| Je Phase: `.Duration` | Dauer in Sekunden |
| Je Phase: `.MovementSpeedMultiplier` | 0 = kein Laufen, 0,5 = halbe Geschwindigkeit, 1 = normal |
| Je Phase: `.LockAim` | Zielrichtung während dieser Phase festhalten |

Der Testhammer hat aktuell **10 Schaden** und diese frei änderbaren Testwerte:

| Phase | Sekunden | Bewegung | Aim-Lock |
| --- | --- | --- | --- |
| Charge | 0,4 | 1 | Nein |
| Attack | 0,2 | 0 | Ja |
| Recover | 0,7 | 0,5 | Nein |

Reichweite, Knockback, Stun, Seelengewinn und Lifesteal sind **geplant, aber noch nicht im Script**. Die Settings-Assets im Spiel nicht verändern: spätere Upgrades gehören zur einzelnen Waffenkopie.

## Vorschlag für den Trigger — noch nicht implementiert

1. Du startest Charge und verwendest die Phaseneinstellungen.
2. **Beim Beginn von Attack rufst du `weapon.BeginAttack()` auf.** Die Waffe führt ihren Angriff aus.
3. **Am Ende von Attack rufst du `weapon.EndAttack()` auf.** Laufende Trefferbereiche bzw. Dauerfeuer werden beendet; bereits abgefeuerte Projektile dürfen weiterfliegen.
4. Du steuerst Recover und gibst anschließend den nächsten Slot frei.

**Diese beiden Methodennamen sind nur ein Vorschlag und noch nicht aufrufbar.** Die Waffe bekommt keine zweite Charge/Attack/Recover-Zeitsteuerung. Eigene Burst-/Trefferintervalle innerhalb von Attack gehören zum Waffenverhalten.

Vorschlag fürs Zielen: Du richtest Slot/Waffenroot laufend aus, sofern die aktuelle Phase Aim erlaubt. Die Waffe verwendet diese Ausrichtung beim jeweiligen Schlag/Schuss. So bleiben auch Dauerfeuer und Bursts nachführbar. Ein bereits abgefeuertes Projektil behält seine Flugrichtung.

Die vereinbarten Dash-Regeln bleiben bei dir: Charge abbrechen und denselben Slot erneut versuchen; Attack nicht unterbrechen; Recover sofort beenden und nächsten Slot freigeben. Beim Weglegen/Deaktivieren muss unsere Waffe ihre laufenden Effekte aufräumen.

## Bitte kurz zurückmelden

- Passt diese Aufteilung und `BeginAttack()` / `EndAttack()`, oder welche Aufrufe möchtest du verwenden? Brauchst du zusätzlich Aufrufe für Charge-/Recover-Effekte oder Abbruch?
- Kommt die laufende Zielrichtung über die Rotation des Slots/Waffenroots, oder möchtest du sie ausdrücklich als Richtung übergeben?

Deinen `PlayerAttack`-Slot-Prototyp haben wir gesehen. Du kannst Haltung, Slots und Spielerablauf weiterbauen; wir ändern deine Spielerdateien nicht. Jannis baut das eigentliche Waffenverhalten nach deiner Rückmeldung passend dazu.

Weitere vereinbarte Spielregeln: [WeaponsAndProgression.md](WeaponsAndProgression.md).
