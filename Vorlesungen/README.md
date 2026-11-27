# Vorlesungen – Experience Engineering (DT2, WS26, Prof. Dr. Jonas Schild)

Kursunterlagen und Aufgabenstand für **Impfany** im Modul Experience Engineering (EE).
Pro Aufgabe EE1–EE7 gibt es einen Ordner mit Vorlesungs-PDF, Screenshot der Aufgabenfolie und `Aufgabe_EEx.md` mit abhakbaren Checkboxen.

**Abgabe: Mi 02.12.2026 · Präsentation: Do 03.12.2026** → [00_Organisation/Abgabe-Checkliste.md](00_Organisation/Abgabe-Checkliste.md)

## Status (Stand 27.11.2026)

| Aufgabe | Thema | Status | Checkliste |
|---|---|---|---|
| EE1 | Beobachtung, 3 offene Fragen, Usability-/UX-Hypothese | 🔴 offen | [Aufgabe_EE1.md](EE1_Beobachtung-Offene-Fragen/Aufgabe_EE1.md) |
| EE2 | Studie T1 planen + durchführen (PXI, SoSci) | 🟡 fast fertig | [Aufgabe_EE2.md](EE2_Studie-T1-planen/Aufgabe_EE2.md) |
| EE3 | Auswertung T1 (Statistik, Boxplots) | 🟢 erledigt | [Aufgabe_EE3.md](EE3_Auswertung-T1/Aufgabe_EE3.md) |
| EE4 | Interface: Gulfs, Shneiderman, Gestalt, mentale Modelle | 🔴 offen | [Aufgabe_EE4.md](EE4_Interface-Analyse/Aufgabe_EE4.md) |
| EE5 | Decision Walkthrough + Information Balancing | 🔴 offen | [Aufgabe_EE5.md](EE5_Decision-Walkthrough/Aufgabe_EE5.md) |
| EE6 | Feedback-Systeme (4 Linsen, SDT, Motivationseffekte) | 🔴 offen | [Aufgabe_EE6.md](EE6_Feedback-Systeme/Aufgabe_EE6.md) |
| EE7 | Studie T2 (Prototyp A vs. B) + Reflexion | 🟡 geplant (SoSci + Hypothesen), Tests offen | [Aufgabe_EE7.md](EE7_Studie-T2-Reflexion/Aufgabe_EE7.md) |

Kursweite Lernhilfe mit Lernpfad: [Docs/Impfany/EE-Lerngraph.md](../Docs/Impfany/EE-Lerngraph.md)

## Struktur

```
Vorlesungen/
├── README.md                       ← diese Datei
├── 00_Organisation/                ← Vorlesung 00 (Orga + HCI), Zeitplan, Bewertung, Abgabe
│   ├── Abgabe-Checkliste.md
│   └── Update_*.png                ← aktualisierte Folien (neue Deadline, Präsentationsslots)
├── EE1_Beobachtung-Offene-Fragen/  ← Vorlesung 01
├── EE2_Studie-T1-planen/           ← Vorlesung 02 + Studiendesign, Einwilligungen
│   └── Kursmaterial/               ← PXI-Vorlage (SoSci-XML), material_eval.zip
├── EE3_Auswertung-T1/              ← Vorlesung 03 + Ergebnisse.xlsx
│   └── Rohdaten/                   ← Einzelscores + SoSci-Export
├── EE4_Interface-Analyse/          ← Vorlesung 04
├── EE5_Decision-Walkthrough/       ← Vorlesung 05
├── EE6_Feedback-Systeme/           ← Vorlesung 06
├── EE7_Studie-T2-Reflexion/        ← Aufgabenfolie (Screenshot), T2-Plan: SoSci-Link, Hypothesen
└── Quiz/                           ← Quiz-PDF Woche 1 + Quizfragen.md (alle Fragen EE0–EE6)
```

Jeder EE-Ordner enthält: `EE_WS26_0x_*.pdf` (Vorlesung) · `Aufgabe_EEx.png` (Aufgabenfolie) · `Aufgabe_EEx.md` (Checkliste + was schon vorliegt).

## Vollständigkeit der Vorlesungen
Alle Impulse aus dem Zeitplan (Folie 10 im Orga-PDF) sind als PDF vorhanden: 00 Organisation/HCI, 01 Intro GUR, 02 Evaluating Experiences, 03 Data Analysis, 04 Understanding the User, 05 Information Balancing, 06 Feedback Systems.

**Was fehlt:**
- Das PDF mit den **aktualisierten Folien** (neue EE7-Aufgabe, Abgabe Mi 02.12., Präsentationsslots). Diese Folien gibt es nur als Screenshots in `00_Organisation/` und `EE7_.../Aufgabe_EE7.png`. Falls im Moodle/Sciebo ein aktualisiertes Foliendeck liegt, hier ergänzen.
- Quizfragen-PDFs gibt es nur für Woche 1 (EE0–EE3). EE4–EE6 stehen direkt in den Vorlesungs-PDFs und sind in `Quiz/Quizfragen.md` gesammelt.
- **EE8** („Plan Experiment T2“) aus dem alten Zeitplan hat keine eigene Folie, sie ist in der neuen EE7-Aufgabe aufgegangen.

## Hinweise für Agenten
- **Sprache:** Deutsch. Folientexte der Aufgaben bleiben englisch (Originalwortlaut).
- **Status pflegen:** Wenn eine Teilaufgabe erledigt ist, Checkbox in `Aufgabe_EEx.md` abhaken **und** Status-Tabelle oben anpassen. Nur abhaken, wenn ein Nachweis existiert (Datei, Commit, Folie). Bei Unsicherheit offen lassen und eine Notiz dazuschreiben.
- **Neue Ergebnisse** (Protokolle, Analysen, Folienentwürfe) in den passenden EE-Ordner legen, nicht in den Root.
- **Datenschutz:** Keine Namen von Testpersonen und keine unterschriebenen Einwilligungen ins Repo. Anonyme SoSci-Exporte sind ok und kommen nach `EEx_.../Rohdaten/`.
- **Dateinamen für die Abgabe:** Präfix `EE26_group#_` (siehe Abgabe-Checkliste).
- **PDFs lesen:** `pdftotext -layout` und `pdftoppm -png -r 110 -f N -l N -singlefile` sind installiert. Aufgabenfolie suchen: nach „Task EE“ greppen.
- **Spielbezug:** Das Spiel liegt in `ImpfanyGame/` (Unity, Testszene `Assets/Scenes/SampleScene.unity`). Für EE4–EE6 passende Commits mit `git log` finden und in der jeweiligen Checkliste verlinken.
- **Team:** Hugo (Player, Animation), Maxim (Gegner, Waves), Jannis (Waffen), Merlin (UI, Game States). Rollen: `Docs/Abgabe/Team-roles-and-responsibilities.md`.
- `Docs/superpowers/` wird nie gepusht.
