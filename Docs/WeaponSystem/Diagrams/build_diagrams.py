from pathlib import Path
from html import escape as esc
import math

OUT=Path(__file__).resolve().parent
OUT.mkdir(parents=True,exist_ok=True)

def text(x,y,lines,cls='label',anchor='middle'):
    if isinstance(lines,str): lines=[lines]
    return '<text x="%s" y="%s" class="%s" text-anchor="%s">%s</text>'%(x,y,cls,anchor,''.join(f'<tspan x="{x}" dy="{0 if i==0 else 22}">{esc(t)}</tspan>' for i,t in enumerate(lines)))
def box(x,y,w,h,title,sub='',kind='mine'):
    return f'<rect x="{x}" y="{y}" width="{w}" height="{h}" rx="10" class="node {kind}"/>'+text(x+w/2,y+30,title,'node-title')+(text(x+w/2,y+55,sub.split('|'),'small') if sub else '')
def edge(points,label='',lx=0,ly=0,dash=False):
    return f'<polyline points="{points}" class="edge {"dashed" if dash else ""}" marker-end="url(#arrow)"/>'+(text(lx,ly,label,'edge-label') if label else '')
def relation(points):
    return f'<polyline points="{points}" class="edge"/>'
def decision(x,y,w,h,lines):
    pts=f'{x},{y-h/2} {x+w/2},{y} {x},{y+h/2} {x-w/2},{y}'
    return f'<polygon points="{pts}" class="node neutral"/>'+text(x,y-(5 if len(lines)>1 else -5),lines,'label')

def svg(body,label,h=460):
    return f'<svg viewBox="0 0 1040 {h}" role="img" aria-label="{esc(label)}" xmlns="http://www.w3.org/2000/svg"><title>{esc(label)}</title><defs><marker id="arrow" viewBox="0 0 10 10" refX="9" refY="5" markerWidth="7" markerHeight="7" orient="auto-start-reverse"><path d="M 0 0 L 10 5 L 0 10 z" fill="#475569"/></marker></defs>{body}</svg>'

# 1. Actual runtime call path above; data and visual support below.
s1=svg(
text(520,18,'Hauptpfad: vom Angriffsklick bis zum Lebenspunkte-Abzug','node-title')+
box(10,65,170,100,'PlayerAttack','Klick erkennen|Slot auswählen','team')+
box(220,65,200,100,'Weapon','Charge → Attack|→ Recover')+
box(460,65,285,100,'MeleeConeAttackBehaviour','Kegel abfragen|Schadensempfänger finden')+
box(800,65,230,100,'EnemyHealth','implementiert IEnemyDamageable|health -= amount','team')+
edge('180,115 220,115','Attack()',200,49)+
edge('420,115 460,115','BeginAttack(settings)',440,49)+
edge('745,115 800,115','TakeDamage(amount)',772,49)+
box(20,255,300,100,'WeaponSettings','Name / Schaden / Kegelform|Dauer jeder Phase')+
edge('170,255 170,210 320,210 320,165','Werte',240,195)+
box(405,255,300,100,'MeleeConeIndicator','reagiert auf Phasenwechsel|stellt den weißen Kegel dar')+
edge('370,165 370,230 555,230 555,255','PhaseChanged',500,214)+
box(770,255,260,100,'MeleeConeShape','berechnet gemeinsame|Eckpunkte der Kegelform')+
edge('900,255 900,210 670,210 670,165','Kegelpunkte',800,195)+
edge('770,305 705,305','Form',738,290)+
text(520,403,'Typbeziehung: MeleeConeAttackBehaviour erbt von WeaponAttackBehaviour.','label')+
text(520,435,'Weapon speichert den Basistyp, ruft aber die zugewiesene konkrete Komponente auf.','small'),
'Durchgängiger Aufrufpfad: PlayerAttack, Weapon, MeleeConeAttackBehaviour und EnemyHealth')

s2=svg(
box(30,70,210,100,'Idle','bereit|Attack() wird angenommen')+
box(285,70,210,100,'Charge','Verzögerung|noch kein Schaden')+
box(540,70,210,100,'Attack','BeginAttack(settings)|ein Treffercheck')+
box(795,70,210,100,'Recover','Nachlauf|noch nicht wieder bereit')+
edge('240,120 285,120','Attack() angenommen',262,53)+edge('495,120 540,120','Charge-Zeit vorbei',518,53)+edge('750,120 795,120','Attack-Zeit vorbei',772,53)+
edge('900,170 900,235 135,235 135,170','Recover-Zeit vorbei → Weapon.EndAttack()',520,223)+
box(285,310,465,85,'Abbruch: Weapon.EndAttack()','jede aktive Phase → Idle; OnDisable ruft ebenfalls auf','neutral')+
edge('390,170 390,310','Abbruch',436,278,True)+edge('645,170 645,310','Abbruch',691,278,True)+edge('795,137 770,137 770,352 750,352',dash=True)+
edge('285,352 135,352 135,235',dash=True)+
text(520,425,'Beim Verlassen von Attack: attackBehaviour.EndAttack(); dann Recover oder bei Abbruch Idle.','small'),
'Zustandsdiagramm der Waffennutzung: Idle, Charge, Attack und Recover',450)

s3=svg(
text(520,18,'BeginAttack: einmal vorbereiten, dann die gefundenen Collider durchgehen','node-title')+
box(15,45,325,95,'Kegel und Trefferliste vorbereiten','Overlap; Collider wieder aus|HashSet vor der Schleife leeren')+
decision(545,92,280,100,['Weiterer Collider?'])+
box(820,45,205,95,'Fertig','alle Collider geprüft')+
edge('340,92 405,92')+
edge('685,92 820,92','nein',752,75)+
edge('545,142 545,195','ja',570,178)+
box(355,195,380,85,'target = Empfänger suchen','GetComponentInParent<IEnemyDamageable>()')+
edge('545,280 545,320')+
decision(545,370,430,100,['Empfänger vorhanden UND','neu im HashSet?'])+
edge('545,420 545,465','ja',570,448)+
box(345,465,400,95,'target.TakeDamage(damage)','EnemyHealth: health -= amount|WeaponDamageDummy: Log-Ausgabe')+
edge('760,370 905,370 905,465','nein',833,354)+
box(790,465,235,95,'Überspringen','kein Schadensaufruf')+
relation('545,560 545,595 280,595')+
relation('907,560 907,595 545,595')+
edge('280,595 280,170 375,170 375,92 405,92')+
text(450,620,'Schleife: mit dem nächsten Collider fortfahren','small'),
'Treffer-Schleife mit vollständigen Ja- und Nein-Pfaden und einem Ende',640)

# 4. Spatial geometry: actual 16-segment fan.
ox,oy,r=150,240,180
arc=[(ox+math.cos(math.radians(a))*r,oy-math.sin(math.radians(a))*r) for a in [-45+i*90/16 for i in range(17)]]
poly=' '.join(f'{x:.2f},{y:.2f}' for x,y in [(ox,oy)]+arc)
geom=f'<polygon points="{poly}" fill="#dbeafe" stroke="#2563eb" stroke-width="2"/>'
for x,y in arc: geom+=f'<line x1="{ox}" y1="{oy}" x2="{x:.2f}" y2="{y:.2f}" stroke="#93c5fd" stroke-width="1"/>'
geom+=f'<circle cx="{ox}" cy="{oy}" r="5" fill="#0f172a"/>'
geom+=edge('150,240 395,240','lokal +X / Aim',360,225)+text(125,263,'Griffpunkt','small')
geom+=text(280,75,'Range: Abstand zum Bogen','small')+text(205,350,'90° gesamt = ±45°','small')
s4=svg(geom+
box(470,30,530,75,'MeleeConeShape.CreatePoints','Range + AttackAngle → Vector2[18]')+
box(470,155,245,100,'PolygonCollider2D','physische Trefferfläche|18 Eckpunkte')+
box(755,155,245,100,'Mesh','sichtbare Fläche|18 Vertices, 16 Dreiecke')+
edge('620,105 620,155')+edge('880,105 880,155')+
text(735,305,['Vector2 = Punkt mit x und y','Vector3 = Punkt mit x, y und z (hier z = 0)','16 Dreiecke × 3 Indizes = int[48]'],'label')+
text(735,405,'Beide verwenden dieselbe Formberechnung.','node-title'),
'Gemeinsame Kegelform für Collider und sichtbares Mesh')

# 5. Opacity curve using test hammer durations, with instant Attack.
cx,cy,cw,ch=75,245,900,165
xhit=cx+cw*1.5/3.5
curve=[]
for i in range(81):
    p=i/80; curve.append((cx+(xhit-cx)*p,cy-ch*.25*p**2.5))
curve.append((xhit,cy-ch))
for i in range(81):
    p=i/80; curve.append((xhit+(cx+cw-xhit)*p,cy-ch*(1-p)**2.5))
curve_points=' '.join(f'{x:.2f},{y:.2f}' for x,y in curve)
s5=svg(
text(250,30,'Charge · 1,5 s','node-title')+text(xhit,30,'Attack · 0 s','node-title')+text(740,30,'Recover · 2 s','node-title')+
f'<line x1="{cx}" y1="{cy}" x2="{cx+cw}" y2="{cy}" class="axis"/>'+f'<line x1="{cx}" y1="{cy}" x2="{cx}" y2="{cy-ch}" class="axis"/>'+
f'<polyline points="{curve_points}" fill="none" stroke="#2563eb" stroke-width="3"/>'+
text(58,85,'100 %','small','end')+text(58,cy-ch*.25+5,'25 %','small','end')+text(58,250,'0 %','small','end')+
text(cx,275,'0 s','small')+text(xhit,275,'1,5 s','small')+text(cx+cw,275,'3,5 s','small')+
text(245,120,'0,25 × progress²·⁵','label')+text(780,120,'(1 − progress)²·⁵','label')+
text(520,302,'Idealisierte Zeiten: Attack mit 0 s hat kein garantiert eigenes sichtbares Vollweiß-Bild.','small')+
box(45,325,290,95,'Folgt der Waffe','Charge und Attack|Parent bleibt Weapon')+
box(375,325,290,95,'Bleibt am Trefferort','Recover: SetParent(null, true)|Weltposition bleibt erhalten')+
box(705,325,290,95,'Beim nächsten Angriff','Charge: wieder unter Weapon|lokaler Transform = Identität')+
edge('335,372 375,372')+edge('665,372 705,372'),
'Indikator: aufladen, kurz volle Deckkraft, am Trefferort ausblenden')

s6=svg(
text(520,18,'A · Aufbau einer Waffeninstanz — Linien bedeuten Parent / Kind','node-title')+
box(310,45,400,85,'Waffen-Root','Weapon + MeleeConeAttackBehaviour')+
box(790,45,235,85,'Settings-Asset','z. B. Dagger.asset')+
edge('710,87 790,87','settings-Referenz',750,40,True)+
relation('510,130 510,160 170,160 170,195')+
relation('510,160 520,160 520,195')+
relation('510,160 870,160 870,195')+
box(20,195,300,85,'Visuals','SpriteRenderer am Kind|oder bei anderen Waffen am Root')+
box(370,195,300,85,'HitArea · Kind','PolygonCollider2D|Trigger an; Collider sonst aus')+
box(720,195,300,85,'HitAreaIndicator · Kind','gemeinsames verschachteltes Prefab|MeshFilter / Renderer / Indicator')+
text(520,335,'B · Testablauf — Pfeile bedeuten Methodenaufrufe','node-title')+
box(10,385,220,95,'WeaponSandboxTester','Kontextmenü in Play Mode|Test/Attack')+
box(270,385,170,95,'Weapon','derselben Instanz|führt die Phasen aus')+
box(480,385,285,95,'MeleeConeAttackBehaviour','fragt HitArea ab|ruft Empfänger auf')+
box(810,385,220,95,'WeaponDamageDummy','Schadensempfänger|loggt den Schadensbetrag')+
edge('230,430 270,430','Attack()',250,369)+
edge('440,430 480,430','BeginAttack(settings)',460,369)+
edge('765,430 810,430','TakeDamage(amount)',787,369)+
text(520,520,'Der Tester ruft die Waffe auf. Die Melee-Komponente ruft den Dummy auf. Der Collider ruft nichts auf.','small'),
'Getrennte Ansichten: Aufbau mit Kind-Linien und durchgängiger Testablauf mit Aufruf-Pfeilen',550)

DIAGRAMS = [
{'id': 'overview',
 'title': 'Vom Klick zum Lebenspunkte-Abzug',
 'tag': '01 · Aufrufweg',
 'summary': 'Oben steht der tatsächliche Aufrufweg. Darunter stehen die unterstützenden Daten und die '
            'Darstellung.',
 'mermaid': 'flowchart LR\n'
            '    P["PlayerAttack: Klick und Slot"] -->|"Attack()"| W["Weapon: Phasen"]\n'
            '    W -->|"BeginAttack(settings) beim Eintritt in Attack"| M["MeleeConeAttackBehaviour: Treffer '
            'prüfen"]\n'
            '    M -->|"TakeDamage(amount) über IEnemyDamageable"| E["EnemyHealth: health -= amount"]\n'
            '    S["WeaponSettings: Werte"] -->|"Daten"| W\n'
            '    W -->|"PhaseChanged"| V["MeleeConeIndicator: Darstellung"]\n'
            '    G["MeleeConeShape: gemeinsame Eckpunkte"] --> M\n'
            '    G --> V',
 'files': ['Weapon/Weapon.cs',
           'Weapon/WeaponSettings.cs',
           'Weapon/WeaponAttackBehaviour.cs',
           'Weapon/AttackBehaviours/MeeleCone/MeleeConeAttackBehaviour.cs'], 'svg': s1},
{'id': 'phases',
 'title': 'Ein Aufruf, ein kompletter Ablauf',
 'tag': '02 · Zustände',
 'summary': 'Weapon verwaltet genau eine aktuelle Phase und eine Uhr für diese Phase.',
 'mermaid': 'stateDiagram-v2\n'
            '    [*] --> Idle\n'
            '    Idle --> Charge: Attack angenommen\n'
            '    Charge --> Attack: Charge-Dauer erreicht\n'
            '    Attack --> Recover: Attack-Dauer erreicht\n'
            '    Recover --> Idle: Recover-Dauer erreicht\n'
            '    Charge --> Idle: EndAttack oder OnDisable\n'
            '    Attack --> Idle: EndAttack oder OnDisable\n'
            '    Recover --> Idle: EndAttack oder OnDisable\n'
            '    note right of Attack\n'
            '        BeginAttack(settings) beim Eintritt\n'
            '        EndAttack() beim Verlassen\n'
            '        Dauer 0: Treffer und Recover im selben Update\n'
            '    end note',
 'files': ['Weapon/Weapon.cs', 'Weapon/WeaponPhase.cs'], 'svg': s2},
{'id': 'damage',
 'title': 'Ein Gegner bekommt nur einen Treffer',
 'tag': '03 · Schaden',
 'summary': 'Die Physik findet Collider. Pro Collider wird ein Empfänger gesucht, geprüft und höchstens '
            'einmal aufgerufen.',
 'mermaid': 'flowchart TD\n'
            '    A["BeginAttack: Kegel und Filter vorbereiten"] --> B["Overlap abfragen; Collider danach '
            'aus"]\n'
            '    B --> C["HashSet der Empfänger leeren"]\n'
            '    C --> D{"Weiterer Collider?"}\n'
            '    D -->|"Nein"| Z["Treffercheck fertig"]\n'
            '    D -->|"Ja"| E["target über GetComponentInParent suchen"]\n'
            '    E --> F{"target vorhanden UND neu im HashSet?"}\n'
            '    F -->|"Ja"| G["target.TakeDamage: EnemyHealth zieht Lebenspunkte ab; Dummy loggt"]\n'
            '    F -->|"Nein"| H["Diesen Collider überspringen"]\n'
            '    G --> D\n'
            '    H --> D',
 'files': ['Weapon/AttackBehaviours/MeeleCone/MeleeConeAttackBehaviour.cs',
           'Interfaces/IEnemyDamageable.cs',
           'Test/WeaponDamageDummy.cs'], 'svg': s3},
{'id': 'shape',
 'title': 'Eine Form, zwei Verwendungen',
 'tag': '04 · Geometrie',
 'summary': 'MeleeConeShape berechnet gemeinsame Eckpunkte für Trefferfläche und sichtbaren Kegel.',
 'mermaid': 'flowchart TD\n'
            '    R["Range: Entfernung"] --> G["MeleeConeShape.CreatePoints"]\n'
            '    A["AttackAngle: gesamte Öffnung"] --> G\n'
            '    G --> P["18 Vector2-Punkte: Mittelpunkt + 17 Bogenpunkte"]\n'
            '    P --> C["PolygonCollider2D.SetPath: Trefferfläche"]\n'
            '    P --> V["Vector3-Punkte mit z = 0"]\n'
            '    V --> T["16 Dreiecke mit 48 Indizes"]\n'
            '    T --> M["Mesh: sichtbare Fläche"]',
 'files': ['Weapon/AttackBehaviours/MeeleCone/MeleeConeShape.cs',
           'Weapon/AttackBehaviours/MeeleCone/MeleeConeIndicator.cs'], 'svg': s4},
{'id': 'indicator',
 'title': 'Zielen, treffen, Trefferort zeigen',
 'tag': '05 · Darstellung',
 'summary': 'Der Indikator folgt beim Aufladen und bleibt während Recover am alten Trefferort.',
 'mermaid': 'flowchart TD\n'
            '    E["Weapon.PhaseChanged"] --> F{"Welche Phase?"}\n'
            '    F -->|"Charge"| C["Parent = Weapon; lokaler Transform zurücksetzen; Deckkraft 0"]\n'
            '    F -->|"Attack"| A["Deckkraft auf 1 setzen"]\n'
            '    F -->|"Recover"| R["Parent = null; Welt-Transform erhalten; Deckkraft 1"]\n'
            '    F -->|"Idle"| I["Deckkraft auf 0 setzen"]\n'
            '    P["LateUpdate: nur Charge oder Recover mit Dauer größer 0"] --> L["PhaseElapsed / '
            'Phasendauer: Fade-Kurve berechnen"]\n'
            '    L --> M["MaterialPropertyBlock setzt Deckkraft"]',
 'files': ['Weapon/AttackBehaviours/MeeleCone/MeleeConeIndicator.cs'], 'svg': s5},
{'id': 'prefabs',
 'title': 'Zusammenbauen und unabhängig testen',
 'tag': '06 · Unity & Zusammenarbeit',
 'summary': 'Zwei getrennte Ansichten: oben die Struktur, unten der Aufrufweg eines Sandbox-Tests.',
 'mermaid': 'flowchart TD\n'
            '    subgraph Aufbau["A: Struktur einer Waffeninstanz; Linien sind Kind-Beziehungen"]\n'
            '        P["Root mit Weapon und MeleeConeAttackBehaviour"] ---|"Kind"| H["HitArea: '
            'PolygonCollider2D"]\n'
            '        P ---|"Kind"| V["HitAreaIndicator: verschachteltes Prefab"]\n'
            '        P ---|"Kind oder Darstellung am Root"| X["Visuals: SpriteRenderer"]\n'
            '        P -.->|"settings-Referenz"| S["Eigenes WeaponSettings-Asset"]\n'
            '    end\n'
            '    subgraph Test["B: Testablauf; Pfeile sind Methodenaufrufe"]\n'
            '        T["WeaponSandboxTester"] -->|"Attack()"| W["Weapon"]\n'
            '        W -->|"BeginAttack(settings)"| M["MeleeConeAttackBehaviour"]\n'
            '        M -->|"TakeDamage(amount)"| D["WeaponDamageDummy: loggt Betrag"]\n'
            '    end',
 'files': ['Test/WeaponSandboxTester.cs', 'Test/WeaponDebugAttackBehaviour.cs', 'Test/WeaponDamageDummy.cs'], 'svg': s6}
]

CSS = '\n:root{--ink:#122133;--muted:#536477;--line:#cbd5e1;--blue:#2563eb;--surface:#f3f7fc}\n*{box-sizing:border-box}body{margin:0;background:#eef2f6;color:var(--ink);font:17px/1.5 -apple-system,BlinkMacSystemFont,"Segoe UI",sans-serif}header{max-width:1120px;margin:36px auto 20px;padding:0 30px}header h1{font-size:34px;margin:4px 0 10px;letter-spacing:-.8px}header p{max-width:830px;margin:8px 0;color:var(--muted)}nav{display:flex;flex-wrap:wrap;gap:8px;margin:20px 0}nav a,button{font:inherit;color:var(--ink);border:1px solid var(--line);background:white;border-radius:6px;padding:8px 12px;text-decoration:none;cursor:pointer}nav a:hover,button:hover{border-color:var(--blue)}.sheet{max-width:1120px;margin:24px auto;padding:34px 40px;background:white;border-radius:12px;box-shadow:0 3px 18px #14243308;scroll-margin-top:18px;break-after:page}.eyebrow{font-size:13px;font-weight:700;letter-spacing:1.5px;text-transform:uppercase;color:var(--blue)}h2{font-size:30px;line-height:1.15;letter-spacing:-.6px;margin:10px 0}p.lede{color:var(--muted);margin:10px 0 14px}.diagram{margin:16px -12px 20px}svg{display:block;width:100%;height:auto;overflow:visible}.node{fill:#eff6ff;stroke:#bfdbfe;stroke-width:1.2}.node.team{fill:#f0fdf4;stroke:#bbdfc8}.node.neutral{fill:#f8fafc;stroke:#cbd5e1}svg text{fill:#122133;font-family:-apple-system,BlinkMacSystemFont,"Segoe UI",sans-serif;font-size:17px}.node-title{font-weight:650;font-size:17px}.small{font-size:14px;fill:#475569}.edge-label{font-size:13px;fill:#475569;paint-order:stroke;stroke:#fff;stroke-width:5;stroke-linejoin:round}.edge{fill:none;stroke:#475569;stroke-width:1.6}.dashed{stroke-dasharray:6 5}.axis{stroke:#94a3b8;stroke-width:1}.speak{border-left:3px solid var(--blue);padding:2px 0 2px 18px;margin:16px 0 22px;font-size:18px}.speak strong{display:block;font-size:12px;letter-spacing:1px;text-transform:uppercase;color:var(--blue);margin-bottom:5px}.points{display:grid;grid-template-columns:repeat(3,1fr);gap:24px}.points h3{font-size:16px;margin:0 0 5px}.points p{font-size:14px;line-height:1.55;margin:0;color:var(--muted)}.files{border-top:1px solid var(--line);padding-top:10px;margin:22px 0 0;font-size:12px;color:var(--muted);overflow-wrap:anywhere}a{color:#1d4ed8}.status{display:grid;grid-template-columns:220px 1fr;gap:10px 24px;margin:22px 0}.status dt{font-weight:650}.status dd{margin:0;color:var(--muted)}details{border-top:1px solid var(--line);padding:12px 0}summary{cursor:pointer;font-weight:600}details p{margin:8px 0;color:var(--muted)}footer{max-width:1120px;margin:25px auto 50px;padding:0 30px;font-size:13px;color:var(--muted)}.print-note{font-size:13px!important}.print-answer{display:none}\n@media(max-width:760px){header{padding:0 18px}header h1{font-size:27px}.sheet{margin:16px 8px;padding:24px 18px}.points{grid-template-columns:1fr;gap:16px}.status{grid-template-columns:1fr;gap:5px}.status dd{margin-bottom:12px}.diagram{overflow-x:auto}svg{min-width:700px}.speak{font-size:16px}}\n@media print{@page{size:A4 landscape;margin:10mm}body{background:white;font-size:12px}header,nav,footer,.no-print{display:none}.sheet{box-shadow:none;border-radius:0;margin:0;padding:0;max-width:none;break-after:page}.sheet:last-of-type{break-after:auto}h2{font-size:25px;margin:6px 0}.lede{font-size:13px}.eyebrow{font-size:11px}.diagram{margin:5px 0}svg{max-height:99mm;width:100%;min-width:0}.speak{font-size:14px;line-height:1.4;margin:9px 0 14px}.points{gap:20px}.points h3{font-size:12px}.points p{font-size:11px;line-height:1.4}.files{font-size:9px;margin-top:10px}.status{margin:12px 0;font-size:12px;grid-template-columns:190px 1fr}details{display:none}.print-answer{display:block;font-size:11px;columns:2;column-gap:25px}.print-answer p{break-inside:avoid}*{-webkit-print-color-adjust:exact;print-color-adjust:exact}}\n'

def build():
    md = ["# Waffensystem – Systemdiagramme", "",
          "Stand: 13.11.2026 · Codebasis: `cf29e0b`. [Offline-Ansicht](WeaponSystems.html) · [Dokumentationsübersicht](../README.md).", "",
          "Die Diagramme beschreiben den gespeicherten Code. Bewegungseffekte, Ergebnis-Events und die spezielle Dash-API sind noch geplant. Weitergehende Spielregeln stehen in den verlinkten Systemdokumenten.", "",
          "**Leserichtung:** Aufrufpfeile führen vom Aufrufer zur ausgeführten Methode. Datenpfeile sind entsprechend beschriftet. In Bild 6 bedeuten einfache Linien Parent/Kind-Beziehungen; der gestrichelte Pfeil ist eine Asset-Referenz. Vererbung ist kein Ausführungsschritt.", ""]
    html = ['<!doctype html><html lang="de"><head><meta charset="utf-8"><meta name="viewport" content="width=device-width, initial-scale=1"><title>Impfany – Waffensystem</title><style>'+CSS+'</style></head><body>',
            '<header><div class="eyebrow">Impfany · Technische Dokumentation</div><h1>Waffensystem – Systemdiagramme</h1><p>Stand 13.11.2026 · Codebasis cf29e0b. Aufrufwege, Zustände, Schaden, Geometrie, Darstellung und Prefab-Aufbau.</p><p>Die Diagramme zeigen den aktuellen Code. Bewegungseffekte, Ergebnis-Events und spezielle Dash-API sind noch geplant.</p><nav aria-label="Diagramme">'+''.join(f'<a href="#{p["id"]}">{esc(p["tag"])}</a>' for p in DIAGRAMS)+'<a href="../README.md">Dokumentation</a><button type="button" onclick="window.print()">Drucken / PDF</button></nav></header>']
    for page in DIAGRAMS:
        links = ', '.join(f'[{Path(f).name}](../../../ImpfanyGame/Assets/Features/Progression/Scripts/{f})' for f in page['files'])
        md += ['## '+page['tag']+' – '+page['title'], '', page['summary'], '', '```mermaid', page['mermaid'], '```', '', '**Im Code:** '+links, '']
        html += ['<section class="sheet" id="'+page['id']+'"><div class="eyebrow">'+esc(page['tag'])+'</div><h2>'+esc(page['title'])+'</h2><p class="lede">'+esc(page['summary'])+'</p><div class="diagram">'+page['svg']+'</div><p class="files">Im Code: '+', '.join('<a href="../../../ImpfanyGame/Assets/Features/Progression/Scripts/'+f+'">'+Path(f).name+'</a>' for f in page['files'])+'</p></section>']
    md += ['## Pflege', '', 'Diagramme und Beschriftungen in `build_diagrams.py` ändern, dann vom Repository-Root `python3 Docs/WeaponSystem/Diagrams/build_diagrams.py` ausführen. Markdown und HTML gemeinsam aktuell halten; gegen den Code und im Browser prüfen. Die Generierung ändert keine Unity-Dateien.', '']
    html += ['<footer>Quelle: build_diagrams.py. Regenerieren erzeugt diese Ansicht und die Mermaid-Dokumentation. Keine externen Ressourcen erforderlich.</footer></body></html>']
    (OUT/'WeaponSystems.md').write_text('\n'.join(md))
    (OUT/'WeaponSystems.html').write_text('\n'.join(html))
    print('Generated shared system diagrams in', OUT)

if __name__ == '__main__':
    build()
