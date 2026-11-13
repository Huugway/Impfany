using TMPro;
using UnityEngine;

public class WaveTextPresenter : NumericTextPresenter
{
    [SerializeField] private GameFlow gameFlow;
    [SerializeField] private TMP_Text waveText;

    protected override TMP_Text TargetText => waveText;
    protected override string Format(int number) => $"Wave: {number}";

    protected override void Subscribe()
    {
        if (gameFlow != null)
            gameFlow.OnWaveStarted += ShowNumber;
    }

    protected override void Unsubscribe()
    {
        if (gameFlow != null)
            gameFlow.OnWaveStarted -= ShowNumber;
    }
}
