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

**Nächste Priorität, Stand 13.11.2026:** Hugos vorläufiger Player-Timer aus den drei Phasendauern ist nach Pull von `df6b22f` im Code bestätigt. Er blockiert weitere Angriffe; der Slot-Index wird noch direkt weitergeschaltet. Nächste Woche ab 16.11. schnellstmöglich durch `Weapon.UseEnded` plus Start-Rückmeldung ersetzen. Die Waffe meldet das tatsächliche Ende; Hugo verwaltet Slot und Input-Buffer. Details stehen im [Resume-Checkpoint](WeaponImplementationNext.md#resume-checkpoint--13-november-2026) und in der [Übergabe](Handoff-Hugo-Waffen.md).

**Zusammenarbeit:** Hugo steuert Input, Slots, Ausrichtung und Bewegung; Jannis liefert die Waffe und ihre Trefferlogik; Maxim verarbeitet Gegnerschaden. Die allgemeine [Ordnerstruktur](../FeatureFolders.md) gilt weiterhin.

Bei Systemänderungen die betroffenen Texte und Diagramme mitpflegen. Bestehende API, vereinbartes Design und noch offene Arbeit deutlich auseinanderhalten.
