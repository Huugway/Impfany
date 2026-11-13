# WeaponSystem – Waffen und Progression

Hier liegt die gepflegte Dokumentation zu Jannis' Systemen. Die Diagramme erklären den aktuellen Code; die Designtexte enthalten zusätzlich geplante Funktionen.

| Dokument | Wofür? |
| --- | --- |
| [Jannis.md](Jannis.md) | Überblick, Waffenattribute und Zuständigkeiten |
| [Diagrams](Diagrams/README.md) | Aufrufwege, Phasen, Schaden, Kegelform, Darstellung und Prefab-Aufbau |
| [Handoff-Hugo-Waffen.md](Handoff-Hugo-Waffen.md) | Übergabe und Schnittstellen zum Player; vorhandene und geplante API |
| [WeaponsAndProgression.md](WeaponsAndProgression.md) | Vereinbarte Spielregeln; Karten, Seelen und Laterne als spätere Systeme |
| [WeaponImplementationNext.md](WeaponImplementationNext.md) | Implementierungsstand, offene Tests und nächste Arbeitsschritte |

**Implementiert:** Settings-Assets, automatische Phasen, Nahkampf-Kegel, Schadensaufrufe mit Duplikatschutz, weißer Indikator, Waffen-Prefabs und Sandbox-Testhelfer.

**Noch offen:** Gameplay-Prüfung mit bewegtem Player und echten Gegnern; Phaseneffekte am Player, Endmeldungen und spezielle Dash-API. Upgrades und Soul Lantern sind noch geplant.

**Zusammenarbeit:** Hugo steuert Input, Slots, Ausrichtung und Bewegung; Jannis liefert die Waffe und ihre Trefferlogik; Maxim verarbeitet Gegnerschaden. Die allgemeine [Ordnerstruktur](../FeatureFolders.md) gilt weiterhin.

Bei Systemänderungen die betroffenen Texte und Diagramme mitpflegen. Bestehende API, vereinbartes Design und noch offene Arbeit deutlich auseinanderhalten.
