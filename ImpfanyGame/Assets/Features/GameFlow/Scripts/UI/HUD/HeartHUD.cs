using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HeartHUD : MonoBehaviour
{
    [SerializeField] private Image heartPrefab;
    [SerializeField] private Sprite fullHeartSprite;
    [SerializeField] private Sprite emptyHeartSprite;

    private readonly List<Image> hearts = new();

    private PlayerHealth playerHealth;

    private int currentHealth;
    private int maxHealth;
    
    private void Awake()
    {
        playerHealth = FindFirstObjectByType<PlayerHealth>();
        playerHealth.OnHealthChanged += PlayerHealth_OnHealthChanged;
    }

    private void PlayerHealth_OnHealthChanged(int currentPlayerHealth, int maxPlayerHealth)
    {
        currentHealth = currentPlayerHealth;
        maxHealth = maxPlayerHealth;

        if (hearts.Count == 0) BuildHearts();

        RefreshHearts();
    }

    private void BuildHearts()
    {
        for (int i = 0; i < maxHealth; i++)
        {
            Image heart = Instantiate(heartPrefab, transform);

            hearts.Add(heart);
        }
    }

    private void RefreshHearts()
    {
        for (int i = 0; i < hearts.Count; i++)
            if (i < currentHealth)
                hearts[i].sprite = fullHeartSprite;
            else
                hearts[i].sprite = emptyHeartSprite;
    }

    private void OnDestroy()
    {
        if (playerHealth != null)
            playerHealth.OnHealthChanged -= PlayerHealth_OnHealthChanged;
    }
}
