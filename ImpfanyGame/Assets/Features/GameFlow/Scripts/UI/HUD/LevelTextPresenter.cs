using TMPro;
using UnityEngine;

public class LevelTextPresenter : MonoBehaviour
{
    [SerializeField] private GameFlow gameFlow;
    [SerializeField] private TMP_Text levelText;

    private void OnEnable()
    {
        gameFlow.OnLevelStarted += ShowLevel;
    }

    private void OnDisable()
    {
        gameFlow.OnLevelStarted -= ShowLevel;
    }

    private void ShowLevel(int number)
    {
        levelText.text = $"Level: {number}";
    }
}
