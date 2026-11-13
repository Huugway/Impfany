using TMPro;
using UnityEngine;

public class LevelTextPresenter : NumericTextPresenter
{
    [SerializeField] private GameFlow gameFlow;
    [SerializeField] private TMP_Text levelText;

    protected override TMP_Text TargetText => levelText;
    protected override string Format(int number) => $"Level: {number}";

    protected override void Subscribe()
    {
        if (gameFlow != null)
            gameFlow.OnLevelStarted += ShowNumber;
    }

    protected override void Unsubscribe()
    {
        if (gameFlow != null)
            gameFlow.OnLevelStarted -= ShowNumber;
    }
}
