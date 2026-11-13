# KI-Nutzung – Merlin (Impfany)

## Verwendetes Tool

Ich habe für meinen Teil von Impfany nur Hermes Agent in unseren Chats verwendet. Mein Bereich umfasst Spielablauf, Menü, HUD und Endbildschirme. Weitere KI-Tools habe ich dafür nicht selbst genutzt. Diese Erklärung bezieht sich nur auf meinen Beitrag.

## Mein Arbeitsablauf

Meist habe ich Hermes als Tutor und Prüfhilfe genutzt. Ich habe Fragen zu Unity und C# gestellt, mir kleine Schritte für die Bedienung des Editors erklären lassen und bei Fehlern Screenshots, Fehlermeldungen oder den gespeicherten Projektstand prüfen lassen. Ich habe selbst Entscheidungen zum Aufbau getroffen und Schritte im Unity-Editor umgesetzt.

Hermes hat außerdem den Stand von Code und Szenen mit meinen Beschreibungen abgeglichen, auf offene Verbindungen oder Fehler hingewiesen und mir bei Checklisten, einem UI-Ablaufdiagramm sowie Formulierungen für meine Dokumentation geholfen.

## Direkte Code-Unterstützung

Eine Ausnahme von der überwiegend tutoriellen Nutzung: Für die gemeinsame Basis der Wave-, Level- und Coin-Anzeigen hat Hermes auf meinen Auftrag hin vorhandenen Code refaktoriert und die Wallet-Anzeige ergänzt. Dazu gehören die abstrakte Basisklasse `NumericTextPresenter`, Anpassungen an den davon abgeleiteten Klassen `WaveTextPresenter` und `LevelTextPresenter`, der neue `WalletTextPresenter` sowie die Ergänzung von `OnCoinsChanged` und `PublishCoins` in `PlayerWallet`. Diese Änderungen sind in Commit `7312ade` auf `main` enthalten. Anschließend habe ich mir insbesondere die Coroutine zum Anbinden der Wallet erklären lassen.
