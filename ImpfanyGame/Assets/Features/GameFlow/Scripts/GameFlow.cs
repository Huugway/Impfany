using UnityEngine;

public class GameFlow : MonoBehaviour
{
    [SerializeField] private GameObject startPanel;
    [SerializeField] private GameObject hudPanel;
    [SerializeField] private GameObject gameOverPanel;
    private PlayerHealth playerHealth;
    [SerializeField] private EnemyTracker enemyTracker;


    private GameStatus currentStatus = GameStatus.StartMenu;
    private int waveCounter = 0;
    private int levelCounter = 0;
    public event System.Action<GameStatus> OnStatusChanged;
    public event System.Action<int> OnWaveStarted;
    public event System.Action<int> OnLevelStarted;
    public event System.Action OnRestart;


    private void Awake()
    {
        playerHealth = FindFirstObjectByType<PlayerHealth>();
        playerHealth.OnDeath += Player_OnDeath;
        enemyTracker.OnWaveEnd += EnemyTracker_OnWaveEnd;
    }

    private void EnemyTracker_OnWaveEnd()
    {
        StartWave();
    }

    private void Player_OnDeath()
    {
        SetGameStatus(GameStatus.GameOver);
        HidePanel(hudPanel);
        ShowPanel(gameOverPanel);
    }

    private GameStatus GetGameStatus()
    {
        return currentStatus;
    }

    private int GetWaveCounter()
    {
        return waveCounter;
    }

    private int GetLevelCounter()
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

    public void StartLevel()
    {
        if (currentStatus != GameStatus.Playing) return;

        levelCounter++;
        OnLevelStarted?.Invoke(levelCounter);
    }

    public void StartGame()
    {
        HidePanel(startPanel);
        ShowPanel(hudPanel);
        playerHealth.PublishHealth();
        SetGameStatus(GameStatus.Playing);
        StartLevel();
        StartWave();
    }

    public void RestartGame()
    {
        HidePanel(gameOverPanel);
        SetGameStatus(GameStatus.StartMenu);
        levelCounter = 0;
        waveCounter = 0;

        ShowPanel(startPanel);
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
