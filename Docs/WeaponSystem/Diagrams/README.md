# Systemdiagramme

[Alle Diagramme mit Mermaid-Quellen](WeaponSystems.md) · [Offline-Ansicht im Browser](WeaponSystems.html) · [Zur Systemdokumentation](../README.md)

| Bild | Zeigt |
| --- | --- |
| 01 – Aufrufweg | PlayerAttack → Weapon → MeleeConeAttackBehaviour → EnemyHealth |
| 02 – Zustände | Idle, Charge, Attack, Recover und Abbruch |
| 03 – Schaden | Empfänger suchen, Duplikate ausschließen und alle Collider durchgehen |
| 04 – Geometrie | Gemeinsame Eckpunkte für Collider und Mesh |
| 05 – Darstellung | Fade-Kurven und Abkoppeln während Recover |
| 06 – Unity | Prefab-Aufbau getrennt vom Testablauf |

**Pflege:** Diagramme und Beschriftungen stehen in `build_diagrams.py`. Nach einer Änderung vom Repository-Root ausführen:

```sh
python3 Docs/WeaponSystem/Diagrams/build_diagrams.py
```

Die Generierung aktualisiert die Markdown- und HTML-Ansicht. Beide gehören ins Repo. Danach Aufrufwege, Fallunterscheidungen, Beschriftungen und Darstellung gegen den aktuellen Code prüfen. Private Sprechtexte und Prüfungsfragen gehören nicht in diesen Ordner.
