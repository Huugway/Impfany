using System;
using UnityEngine;

public class GameFlow : MonoBehaviour
{
    [SerializeField] private GameObject startPanel;
    [SerializeField] private GameObject hudPanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject gameWonPanel;
    [SerializeField] private GameObject bigWaveMessage;
    [SerializeField] private GameObject bigLevelMessage;
    [SerializeField] private int prepTime = 5;
    [SerializeField] private int panelTime = 2;
    [SerializeField] private GameObject bigCompletedMessage;
    [SerializeField] private GameObject playerPrefab;

    private PlayerHealth playerHealth;
    [SerializeField] private EnemyTracker enemyTracker;

    [SerializeField] private int waveCountPerLevel = 10;
    [SerializeField] private int maxLevel = 3;
    bool isInShop = false;
    bool waitingForShop = false;

    public bool IsInShop => isInShop;
    public bool WaitingForShop => waitingForShop;

    private GameStatus currentStatus = GameStatus.StartMenu;
    private int waveCounter = 0;
    private int levelCounter = 0;
    public event System.Action OnShopOpened;
    public event System.Action<GameStatus> OnStatusChanged;
    public event System.Action<int> OnWaveStarted;
    public event System.Action<int> OnLevelStarted;
    public event System.Action OnRestart;


    private void subscribeEvents()
    {
        playerHealth.OnDeath += Player_OnDeath;
        enemyTracker.OnWaveEnd += EnemyTracker_OnWaveEnd;
    }

    private void LateUpdate()
    {
        if (playerHealth == null)
        {
            PlayerHealth newPlayerHealth = FindFirstObjectByType<PlayerHealth>();
            if (newPlayerHealth == null) return;
            playerHealth = newPlayerHealth;
            subscribeEvents();
            playerHealth.PublishHealth();
        }
    }

    private void Start()
    {
        ShowPanel(startPanel);
    }

    private void EnemyTracker_OnWaveEnd()
    {
        if (waitingForShop) return;
        if (waveCounter == 0) return;
        if (waveCounter >= waveCountPerLevel)
        {
            EndLevel();
            if (currentStatus != GameStatus.Playing) return;

            waitingForShop = true;
            OnShopOpened?.Invoke();
            return;
        }

        StartWave();
    }

    public void PlayerEnteredShop()
    {
        if (!waitingForShop) return;
        isInShop = true;
    }

    public void PlayerLeftShop()
    {
        if (!isInShop) return;

        isInShop = false;
        waitingForShop = false;
        StartLevel();
    }

    private void EndLevel()
    {
        if(levelCounter == maxLevel)
        {
            Win();
            return;
        }

        waveCounter = 0;
        PopUpPanel(bigCompletedMessage, prepTime);
    }

    private void Win()
    {
        SetGameStatus(GameStatus.Won);
        HidePanel(hudPanel);
        ShowPanel(gameWonPanel);
    }

    private void Player_OnDeath()
    {
        SetGameStatus(GameStatus.GameOver);
        HidePanel(hudPanel);
        ShowPanel(gameOverPanel);
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
        PopUpPanel(bigWaveMessage, panelTime);
    }

    public async void StartLevel()
    {
        if (currentStatus != GameStatus.Playing) return;

        levelCounter++;
        OnLevelStarted?.Invoke(levelCounter);
        PopUpPanel(bigLevelMessage, prepTime);
        await Awaitable.WaitForSecondsAsync(prepTime);
        StartWave();
    }

    public void StartGame()
    {
        HidePanel(startPanel);
        ShowPanel(hudPanel);

        Instantiate(playerPrefab);   

        SetGameStatus(GameStatus.Playing);
        StartLevel();
    }

    public void RestartGame()
    {
        HidePanel(gameOverPanel);
        HidePanel(gameWonPanel);
        
        SetGameStatus(GameStatus.StartMenu);
        levelCounter = 0;
        waveCounter = 0;
        isInShop = false;
        waitingForShop = false;

        if(playerHealth != null) playerHealth.Disintegrate();
        
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

    private async void PopUpPanel(GameObject panel, int seconds)
    {
        panel.SetActive(true);
        await Awaitable.WaitForSecondsAsync(seconds);
        panel.SetActive(false);
    }
}
