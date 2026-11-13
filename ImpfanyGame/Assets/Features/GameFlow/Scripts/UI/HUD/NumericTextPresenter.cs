using TMPro;
using UnityEngine;

// Event source and label remain the responsibility of each concrete presenter.
public abstract class NumericTextPresenter : MonoBehaviour
{
    protected abstract TMP_Text TargetText { get; }
    protected abstract string Format(int value);
    protected abstract void Subscribe();
    protected abstract void Unsubscribe();

    protected virtual void OnEnable()
    {
        Subscribe();
    }

    protected virtual void OnDisable()
    {
        Unsubscribe();
    }

    protected void ShowNumber(int value)
    {
        if (TargetText != null)
            TargetText.text = Format(value);
    }
}
