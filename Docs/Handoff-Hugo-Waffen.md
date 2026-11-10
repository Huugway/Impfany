# Hugo — Übergabe Waffen ↔ Spieler

**Mit Hugo abgestimmtes Design · Stand: 10.11.2026.** Der Spieler bestimmt, wann eine Waffe angreift; die Waffe führt ihren eigenen Angriff aus und liefert dem Spieler ihre Phasenwerte.

**Implementierungsstand:** `TestHammer`, `Weapon` und die Einstellungen existieren. Der Hammer greift noch nicht an. `BeginAttack()` und `EndAttack()` sind vereinbart, aber noch nicht implementiert.

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

Dein Player-Prefab hat bereits diese Struktur (`WaponHolder` ist der aktuelle Objektname):

```text
Player
└── WaponHolder
    ├── WeaponSlot1 → Whip
    ├── WeaponSlot2 → Hammer
    └── WeaponSlot3 → Sniper
```

Whip, Hammer und Sniper sind derzeit Platzhalter. In jeden Slot kommt eine Waffen-Prefab-Instanz mit einer **`Weapon`-Komponente am Root**. Dein bisheriger Zugriff auf das erste Kind des Slots passt dazu; bei leerem Slot wird kein Angriff gestartet.

Der Waffenroot ist der **Griff-/Befestigungspunkt**; bei Rotation 0 zeigt die Waffe nach **lokal +X (rechts)**. Die Waffe liest keinen Input und sucht sich keinen Spieler.

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

## Vereinbarte Aufrufe

| Zeitpunkt | Dein Spieler | Unsere Waffe |
| --- | --- | --- |
| Klick / Charge | Waffe wählen, Einstellungen lesen, Charge starten | Liefert Werte über `weapon.Definition` |
| Beginn von Attack | Einmal `weapon.BeginAttack()` aufrufen | Schwung, Schüsse oder Dauerfeuer starten |
| Ende von Attack | Einmal `weapon.EndAttack()` aufrufen | Aktive Trefferbereiche und Dauerfeuer beenden |
| Recover | Recovery steuern, danach nächsten Slot freigeben | Keine eigene Spieler-Phasensteuerung |

Du verwendest für jede Waffenart dieselbe `Weapon`-Komponente: **`weapon.BeginAttack()`**, unabhängig davon, ob das Prefab TestHammer oder Flamethrower heißt. Dein Player braucht keine Fallunterscheidung für einzelne Waffenarten. Die Methoden nehmen keine Zielrichtung als Parameter entgegen; diese kommt über die Slot-Rotation.

Die Waffe bekommt keine zweite Charge/Attack/Recover-Zeitsteuerung. Eigene Burst-, Schwung- und Trefferintervalle **innerhalb von Attack** gehören zu uns. Bereits abgefeuerte Projektile fliegen nach `EndAttack()` weiter.

## Zielen und Animation

- **Du richtest den Slot laufend auf die Zielrichtung aus**, solange die aktuelle Phase Aim erlaubt. Bei Aim-Lock hältst du die Richtung fest.
- Unsere Waffe verwendet diese Ausrichtung beim jeweiligen Schlag/Schuss. Dauerfeuer und Bursts können so dem Aim folgen; ein abgefeuertes Projektil behält seine Flugrichtung.
- Wir animieren den **`Visual`-Unterbereich** der Waffe. Dein Slot bleibt die Zielausrichtung, auch während der Hammer schwingt.

## Dash und Aufräumen

- **Charge:** Dash bricht ab; derselbe Slot wird beim nächsten Angriff erneut versucht. `BeginAttack()` wurde noch nicht aufgerufen.
- **Attack:** Dash kann diese Phase nicht unterbrechen.
- **Recover:** Dash beendet die Phase sofort; der nächste Slot ist bereit.
- **Weglegen/Deaktivieren:** Unsere Waffe beendet ihre laufenden Trefferbereiche/Effekte zuverlässig.

## Parallel weiterbauen

**Hugo:** Player, Slots, Phasentiming, Aim und Bewegung bauen. Die Trigger erst anschließen, sobald die Methoden in `Weapon` vorhanden sind.

**Jannis:** `BeginAttack()` / `EndAttack()` und zuerst das Hammer-Verhalten in den eigenen Dateien und `WeaponSandbox` umsetzen. Wir ändern deine Spielerdateien nicht; Änderungen an unseren Waffen erfolgen am Waffen-Prefab.

Weitere vereinbarte Spielregeln: [WeaponsAndProgression.md](WeaponsAndProgression.md).
