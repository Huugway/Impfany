using TMPro;
using UnityEngine;

// Schreibt beim Einblenden des Game-Over-Panels das erreichte Level und die Welle in den Text.
public class GameOverTextPresenter : MonoBehaviour
{
    [SerializeField] private GameFlow gameFlow;
    [SerializeField] private TMP_Text resultText;

    private void OnEnable()
    {
        if (gameFlow == null || resultText == null) return;
        resultText.text = $"You got to Level {gameFlow.CurrentLevel} Wave {gameFlow.CurrentWave}";
    }
}
