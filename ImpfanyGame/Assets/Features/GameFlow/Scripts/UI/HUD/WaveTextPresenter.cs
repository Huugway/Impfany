using TMPro;
using UnityEngine;

public class WaveTextPresenter : MonoBehaviour
{
    [SerializeField] private GameFlow gameFlow;
    [SerializeField] private TMP_Text waveText;

    private void OnEnable()
    {
        gameFlow.OnWaveStarted += ShowWave;
    }

    private void OnDisable()
    {
        gameFlow.OnWaveStarted -= ShowWave;
    }

    private void ShowWave(int number)
    {
        waveText.text = $"Wave {number}";
    }
}
