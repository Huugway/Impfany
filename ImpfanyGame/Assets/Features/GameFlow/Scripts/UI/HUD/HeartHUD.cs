using UnityEngine;
using UnityEngine.UI;

public class HeartHUD : MonoBehaviour
{
    [SerializeField] private Image heartPrefab;
    [SerializeField] private Sprite fullHeartSprite;
    [SerializeField] private Sprite emptyHeartSprite;
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
        RefreshHearts();
    }

    private void Start()
    {
        BuildHearts();
    }
    private void BuildHearts()
    {
        for (int i = 0; i < maxHealth; i++)
        {
            Image heart = Instantiate(heartPrefab, transform);

            if (i < currentHealth)
            {
                heart.sprite = fullHeartSprite;
            }
            else
            {
                heart.sprite = emptyHeartSprite;
            }
        }
    }

    private void RefreshHearts()
    {

    }
}
