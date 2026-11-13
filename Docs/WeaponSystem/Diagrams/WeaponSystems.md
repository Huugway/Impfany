# Waffensystem – Systemdiagramme

Stand: 13.11.2026 · Codebasis: `cf29e0b`. [Offline-Ansicht](WeaponSystems.html) · [Dokumentationsübersicht](../README.md).

Die Diagramme beschreiben den gespeicherten Code. Bewegungseffekte, Ergebnis-Events und die spezielle Dash-API sind noch geplant. Weitergehende Spielregeln stehen in den verlinkten Systemdokumenten.

**Leserichtung:** Aufrufpfeile führen vom Aufrufer zur ausgeführten Methode. Datenpfeile sind entsprechend beschriftet. In Bild 6 bedeuten einfache Linien Parent/Kind-Beziehungen; der gestrichelte Pfeil ist eine Asset-Referenz. Vererbung ist kein Ausführungsschritt.

## 01 · Aufrufweg – Vom Klick zum Lebenspunkte-Abzug

Oben steht der tatsächliche Aufrufweg. Darunter stehen die unterstützenden Daten und die Darstellung.

```mermaid
flowchart LR
    P["PlayerAttack: Klick und Slot"] -->|"Attack()"| W["Weapon: Phasen"]
    W -->|"BeginAttack(settings) beim Eintritt in Attack"| M["MeleeConeAttackBehaviour: Treffer prüfen"]
    M -->|"TakeDamage(amount) über IEnemyDamageable"| E["EnemyHealth: health -= amount"]
    S["WeaponSettings: Werte"] -->|"Daten"| W
    W -->|"PhaseChanged"| V["MeleeConeIndicator: Darstellung"]
    G["MeleeConeShape: gemeinsame Eckpunkte"] --> M
    G --> V
```

**Im Code:** [Weapon.cs](../../../ImpfanyGame/Assets/Features/Progression/Scripts/Weapon/Weapon.cs), [WeaponSettings.cs](../../../ImpfanyGame/Assets/Features/Progression/Scripts/Weapon/WeaponSettings.cs), [WeaponAttackBehaviour.cs](../../../ImpfanyGame/Assets/Features/Progression/Scripts/Weapon/WeaponAttackBehaviour.cs), [MeleeConeAttackBehaviour.cs](../../../ImpfanyGame/Assets/Features/Progression/Scripts/Weapon/AttackBehaviours/MeeleCone/MeleeConeAttackBehaviour.cs)

## 02 · Zustände – Ein Aufruf, ein kompletter Ablauf

Weapon verwaltet genau eine aktuelle Phase und eine Uhr für diese Phase.

```mermaid
stateDiagram-v2
    [*] --> Idle
    Idle --> Charge: Attack angenommen
    Charge --> Attack: Charge-Dauer erreicht
    Attack --> Recover: Attack-Dauer erreicht
    Recover --> Idle: Recover-Dauer erreicht
    Charge --> Idle: EndAttack oder OnDisable
    Attack --> Idle: EndAttack oder OnDisable
    Recover --> Idle: EndAttack oder OnDisable
    note right of Attack
        BeginAttack(settings) beim Eintritt
        EndAttack() beim Verlassen
        Dauer 0: Treffer und Recover im selben Update
    end note
```

**Im Code:** [Weapon.cs](../../../ImpfanyGame/Assets/Features/Progression/Scripts/Weapon/Weapon.cs), [WeaponPhase.cs](../../../ImpfanyGame/Assets/Features/Progression/Scripts/Weapon/WeaponPhase.cs)

## 03 · Schaden – Ein Gegner bekommt nur einen Treffer

Die Physik findet Collider. Pro Collider wird ein Empfänger gesucht, geprüft und höchstens einmal aufgerufen.

```mermaid
flowchart TD
    A["BeginAttack: Kegel und Filter vorbereiten"] --> B["Overlap abfragen; Collider danach aus"]
    B --> C["HashSet der Empfänger leeren"]
    C --> D{"Weiterer Collider?"}
    D -->|"Nein"| Z["Treffercheck fertig"]
    D -->|"Ja"| E["target über GetComponentInParent suchen"]
    E --> F{"target vorhanden UND neu im HashSet?"}
    F -->|"Ja"| G["target.TakeDamage: EnemyHealth zieht Lebenspunkte ab; Dummy loggt"]
    F -->|"Nein"| H["Diesen Collider überspringen"]
    G --> D
    H --> D
```

**Im Code:** [MeleeConeAttackBehaviour.cs](../../../ImpfanyGame/Assets/Features/Progression/Scripts/Weapon/AttackBehaviours/MeeleCone/MeleeConeAttackBehaviour.cs), [IEnemyDamageable.cs](../../../ImpfanyGame/Assets/Features/Progression/Scripts/Interfaces/IEnemyDamageable.cs), [WeaponDamageDummy.cs](../../../ImpfanyGame/Assets/Features/Progression/Scripts/Test/WeaponDamageDummy.cs)

## 04 · Geometrie – Eine Form, zwei Verwendungen

MeleeConeShape berechnet gemeinsame Eckpunkte für Trefferfläche und sichtbaren Kegel.

```mermaid
flowchart TD
    R["Range: Entfernung"] --> G["MeleeConeShape.CreatePoints"]
    A["AttackAngle: gesamte Öffnung"] --> G
    G --> P["18 Vector2-Punkte: Mittelpunkt + 17 Bogenpunkte"]
    P --> C["PolygonCollider2D.SetPath: Trefferfläche"]
    P --> V["Vector3-Punkte mit z = 0"]
    V --> T["16 Dreiecke mit 48 Indizes"]
    T --> M["Mesh: sichtbare Fläche"]
```

**Im Code:** [MeleeConeShape.cs](../../../ImpfanyGame/Assets/Features/Progression/Scripts/Weapon/AttackBehaviours/MeeleCone/MeleeConeShape.cs), [MeleeConeIndicator.cs](../../../ImpfanyGame/Assets/Features/Progression/Scripts/Weapon/AttackBehaviours/MeeleCone/MeleeConeIndicator.cs)

## 05 · Darstellung – Zielen, treffen, Trefferort zeigen

Der Indikator folgt beim Aufladen und bleibt während Recover am alten Trefferort.

```mermaid
flowchart TD
    E["Weapon.PhaseChanged"] --> F{"Welche Phase?"}
    F -->|"Charge"| C["Parent = Weapon; lokaler Transform zurücksetzen; Deckkraft 0"]
    F -->|"Attack"| A["Deckkraft auf 1 setzen"]
    F -->|"Recover"| R["Parent = null; Welt-Transform erhalten; Deckkraft 1"]
    F -->|"Idle"| I["Deckkraft auf 0 setzen"]
    P["LateUpdate: nur Charge oder Recover mit Dauer größer 0"] --> L["PhaseElapsed / Phasendauer: Fade-Kurve berechnen"]
    L --> M["MaterialPropertyBlock setzt Deckkraft"]
```

**Im Code:** [MeleeConeIndicator.cs](../../../ImpfanyGame/Assets/Features/Progression/Scripts/Weapon/AttackBehaviours/MeeleCone/MeleeConeIndicator.cs)

## 06 · Unity & Zusammenarbeit – Zusammenbauen und unabhängig testen

Zwei getrennte Ansichten: oben die Struktur, unten der Aufrufweg eines Sandbox-Tests.

```mermaid
flowchart TD
    subgraph Aufbau["A: Struktur einer Waffeninstanz; Linien sind Kind-Beziehungen"]
        P["Root mit Weapon und MeleeConeAttackBehaviour"] ---|"Kind"| H["HitArea: PolygonCollider2D"]
        P ---|"Kind"| V["HitAreaIndicator: verschachteltes Prefab"]
        P ---|"Kind oder Darstellung am Root"| X["Visuals: SpriteRenderer"]
        P -.->|"settings-Referenz"| S["Eigenes WeaponSettings-Asset"]
    end
    subgraph Test["B: Testablauf; Pfeile sind Methodenaufrufe"]
        T["WeaponSandboxTester"] -->|"Attack()"| W["Weapon"]
        W -->|"BeginAttack(settings)"| M["MeleeConeAttackBehaviour"]
        M -->|"TakeDamage(amount)"| D["WeaponDamageDummy: loggt Betrag"]
    end
```

**Im Code:** [WeaponSandboxTester.cs](../../../ImpfanyGame/Assets/Features/Progression/Scripts/Test/WeaponSandboxTester.cs), [WeaponDebugAttackBehaviour.cs](../../../ImpfanyGame/Assets/Features/Progression/Scripts/Test/WeaponDebugAttackBehaviour.cs), [WeaponDamageDummy.cs](../../../ImpfanyGame/Assets/Features/Progression/Scripts/Test/WeaponDamageDummy.cs)

## Pflege

Diagramme und Beschriftungen in `build_diagrams.py` ändern, dann vom Repository-Root `python3 Docs/WeaponSystem/Diagrams/build_diagrams.py` ausführen. Markdown und HTML gemeinsam aktuell halten; gegen den Code und im Browser prüfen. Die Generierung ändert keine Unity-Dateien.
