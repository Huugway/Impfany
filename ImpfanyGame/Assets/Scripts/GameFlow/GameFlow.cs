using UnityEngine;

public class GameFlow : MonoBehaviour
{
    [SerializeField] private GameObject startPanel;
    [SerializeField] private GameObject hudPanel;

    private GameStatus currentStatus = GameStatus.StartMenu;
    private int waveCounter = 0;
    public event System.Action<GameStatus> OnStatusChanged;
    public event System.Action<int> OnWaveStarted;


    private GameStatus GetGameStatus()
    {
        return currentStatus;
    }

    private int GetWaveCounter()
    {
        return waveCounter;
    }

    private void SetGameStatus(GameStatus status)
    {
        if (currentStatus == status) return;

        currentStatus = status;
        OnStatusChanged?.Invoke(status);
    }

    public void StartWave()
    {
        if (currentStatus != GameStatus.Playing) return;

        waveCounter++;
        OnWaveStarted?.Invoke(waveCounter);
    }

    public void StartGame()
    {
        HidePanel(startPanel);
        ShowPanel(hudPanel);
        SetGameStatus(GameStatus.Playing);
        StartWave();
    }

    private void HidePanel (GameObject panel)
    {
        panel.SetActive(false);
    }

    private void ShowPanel(GameObject panel)
    {
        panel.SetActive(true);
    }
}
