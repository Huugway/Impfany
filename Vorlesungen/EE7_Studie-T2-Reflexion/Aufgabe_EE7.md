# EE7 – Evaluate resulting prototype 2 in a user study and reflect on your work

**Quelle:** aktualisierte Folie, nur als Screenshot vorhanden: [Aufgabe_EE7.png](Aufgabe_EE7.png). Das zugehörige PDF fehlt, siehe README.
Ältere Fassung: `EE_WS26_03_Game User Data Analysis.pdf`, Folie 25 → [Aufgabe_EE7_alte-Version.png](Aufgabe_EE7_alte-Version.png)
**Status:** 🟡 in Arbeit. T2 ist geplant (SoSci-Fragebogen, Hypothesen), Durchführung, Auswertung und Reflexion stehen noch aus.

> Im ursprünglichen Zeitplan (00_Organisation, Folie 10) gab es noch **EE8 „Plan Experiment T2“**. Die neue EE7-Folie fasst EE7 + EE8 zusammen: T2 planen, durchführen, auswerten, reflektieren.

## Aufgaben (Folientext, aktuelle Fassung)

1. Evaluate the differences between your first (IA) and second (improved by EE4–6) prototypes:
   - [x] Make assumptions and formulate hypotheses using H0/H1 schemes, acknowledging measurability → [Hypothesen T2](#hypothesen-t2-vorschlag-auf-basis-der-commits-im-team-bestätigen) (H1 Progress Feedback, H2 Ease of Control, H3 Clarity of Goals, plus Messbarkeit/Grenzen)
   - [ ] Plan, conduct and analyze a user study similar to the one you did in EE2+3. *Plan* steht (SoSci `base01`, gleiche Items wie T1), Durchführung und Analyse fehlen.
   - [ ] Analyze differences between the results of the two studies
   - [ ] Discuss results in regard to your hypotheses and to the content of this course
2. - [ ] Reflect on your work and group organization: how do you plan to use the content of this course in the future?

## Vorbereitung T2
- [x] Build A (Stand T1) liegt dem Prof bereits vor (an ihn geschickt) → Abgabe „Type A before improvement“
- [ ] Build B (nach EE4–6) festhalten → Abgabe „Type B after improvement“
- [ ] Gleiche Aufgabe, Dauer, Instruktion, PXI-Items und Kodierung wie T1 verwenden. PXI-Items und Kodierung sind in SoSci identisch, Aufgabe, Dauer (10 Min.) und Instruktion beim Testen aus `EE2-Studiendesign.docx` übernehmen.
- [ ] Neue Erstspielende rekrutieren
- [x] SoSci-Fragebogen für T2 angelegt (27.11.2026), siehe unten
- [ ] Auswertung T2 + Vergleich T1/T2 (Boxplots nebeneinander)

## SoSci-Fragebogen T2
Beide Erhebungen laufen im selben Projekt `impfany-ee2`. Im Export trennt die Spalte `QUESTNNR` die beiden Studien: `base` = T1, `base01` = T2.

| Fragebogen | Beschreibung in SoSci | Link |
|---|---|---|
| `base` | T1 – Prototyp A (vor EE4–6) | https://www.soscisurvey.de/impfany-ee2/ (Standard) |
| `base01` | T2 – Prototyp B (nach EE4–6) | https://www.soscisurvey.de/impfany-ee2/?q=base01 |

- `base01` ist eine Kopie von `base`, die Merlin angelegt hat. PXI (PX01–30, zufällige Reihenfolge), Enjoyment (PX31–33), DE01, DE02 und die Texte sind **unverändert**. Damit sind die Daten direkt mit T1 vergleichbar.
- Neu nur in T2: **DE03 „Impfany vorher gespielt?“** (nein/ja, Pflicht) auf Seite 1. T2 testet andere Personen als T1 (between-subjects), also braucht es neue Erstspielende. Fälle mit „ja“ werden nicht ausgewertet.
- Bewusst **keine** Frage nach der Testleitung, damit die Umfrage anonym bleibt. Den Nachweis „jede Person ≥ 2 Tests“ führt das Team außerhalb von SoSci, z. B. mit einer Strichliste pro Person ohne Teilnehmerdaten.
- [ ] Vor dem Start einen Probelauf über den Link `?q=base01` machen und den Testfall danach unter „Erhobene Daten“ löschen.
- [ ] Optional: In den Einstellungen von `base01` die Option „Vollständigen Interview-Link in der Adresszeile anzeigen“ abschalten, wenn mehrere Personen am selben Laptop antworten.

## Hypothesen T2 (Vorschlag auf Basis der Commits, im Team bestätigen)

**Design:** Prototyp A (T1, n = 10) gegen Prototyp B (T2). Verschiedene Erstspielende pro Studie, also unabhängige Stichproben (between-subjects). Gleiche Aufgabe (10 Min. Arena), gleiche PXI-Items, gleiche Kodierung (−3 bis +3). Die Hypothesen werden **vor** den T2-Tests festgelegt.

### Was sich zwischen A und B geändert hat (Commits ab 24.11.2026)
| Bereich | Änderungen | Commits |
|---|---|---|
| Fortschritt | Große Pop-ups „Level X“, „Wave X“, „Completed“ mit Vorbereitungszeit, Arena-Deckel/Türen während der Welle, Shop zwischen den Levels (Items für 15 Coins, Kauf mit E), Stat-Buffs durch Items, Münz-Magnet, Heilraum mit Herz | c31ff78, 4b34024, 103c559, 71056bb, 4e79bbe, d4fb826, 32aa17a, ea23c96, cbf06ac |
| Treffer-/Kampffeedback | Knockback, Stun und weißes Aufblitzen bei Treffern, Gegner fallen um, Todesanimationen, Waffenanimationen und Schwung-Effekt, Schadens-Vignette | dd719d7, be3b2b8, 8480392, 107166d, 7362581 |
| Steuerung | Bewegungsgeschwindigkeit und Animationen angepasst, Gegner greifen erst auf kurze Distanz an, Gegner schneller (z. B. movementSpeed 1,25 → 1,66) | 3eb3a5f, 24023b1, cd5c36b |
| Ziele/Spielablauf | Neue Sieg- und Game-over-Screens, Siegbedingung angepasst | 059a90c |
| Optik/Inhalt | Big Demon, Lava-Bloom, dynamisches Licht, Gegneranimationen | eb830b5, 57aed03, 7ecf885, e807f8e |

### Haupthypothesen (gerichtet, B > A)
| # | Subskala | T1 (A): M / Median | H₁ | H₀ | Begründung |
|---|---|---|---|---|---|
| H1 | **Progress Feedback** (PX22–24) | −0,47 / −0,33 | B > A | B ≤ A | Niedrigster Wert nach Challenge. Pop-ups für Level/Welle/Abschluss und der Shop machen den Fortschritt sichtbar. |
| H2 | **Ease of Control** (PX16–18) | 1,30 / 2,17 | B > A | B ≤ A | Trefferfeedback und Waffenanimationen zeigen, ob ein Angriff sitzt (EE3: PX17 „Klarheit der Aktionen“ nur 0,60). Die Steigerung fällt vermutlich klein aus, weil A schon im positiven Bereich lag. |
| H3 | **Clarity of Goals** (PX28–30) | 1,10 / 1,67 | B > A | B ≤ A | „Level/Wave“-Pop-ups, Sieg-/Game-over-Screens und die neue Siegbedingung sagen klarer, was zu tun ist. |

### Explorativ (ohne feste Richtung, nur beschreiben)
- **Challenge** (A: −0,80): Big Demon, schnellere Gegner, Heilraum und Shop-Buffs verändern die Balance in beide Richtungen. Eine Richtung lässt sich nicht seriös vorhersagen, deshalb zweiseitig: H₁ B ≠ A.
- **Autonomy** (A: −0,03): Der Shop bietet erstmals echte Wahlmöglichkeiten. Ein Anstieg ist möglich, war aber nicht das Ziel der Änderungen.
- **Audiovisual Appeal** (A: 1,53) und **Enjoyment** (A: 1,07): Beide liegen schon hoch, ein Deckeneffekt ist wahrscheinlich.

### Messbarkeit und Grenzen (für die Diskussion)
- Pro Gruppe etwa 10 Personen. Die Auswertung bleibt wie in EE3 **deskriptiv**: M, Median, SD und Boxplots A/B nebeneinander. Optional einseitiger Mann-Whitney-U-Test für H1–H3, der kleine Stichproben und ordinale Daten verträgt. Bei drei Tests auf Mehrfachtestung hinweisen.
- B ändert viele Dinge gleichzeitig. Ein Unterschied lässt sich darum nur der Summe der Änderungen zuschreiben, nicht einem einzelnen Feature.
- Unterschiedliche Stichproben: Altersgruppe und Actionspiel-Erfahrung (DE01/DE02) zwischen T1 und T2 vergleichen. In T1 spielten 8 von 10 Personen mindestens wöchentlich Actionspiele.
- B ist inhaltlich größer (Shop, Heilraum). In 10 Minuten sehen Testpersonen davon unterschiedlich viel. Notieren, welches Level erreicht wurde (nur auf Papier, nicht in SoSci).

### Zu T1 nachträglich
Für T1 keine neuen Hypothesen formulieren. T1 hatte vorab eine Hypothese (Ease of Control > 0, siehe `EE2-Studiendesign.docx`). Hypothesen erst nach Sicht der Daten aufzustellen gilt als HARKing und wäre in der Prüfung angreifbar. T1 ist die Baseline, und die T2-Hypothesen oben stützen sich legitim auf die T1-Ergebnisse. Nachtragen lassen sich nur die Lücken *Build oder Commit* und *Testgerät/Startszene* im Studiendesign.

## Reflexion (Teil der Abgabe)
- [ ] Individueller Reflexionsbericht je Person (0,5–1,5 Seiten): Inhalt, Stunden, Teambeitrag, KI-Nutzung mit Prompts, Anwendung im weiteren Studium
- [ ] KI-Erklärung, Entwürfe liegen in `Docs/Abgabe/*-ki-use.md`
