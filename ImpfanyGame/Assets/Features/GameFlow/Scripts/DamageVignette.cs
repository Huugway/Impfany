using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

// Steuert die Vignette des Volumes: kurzer Puls bei Schaden,
// dauerhaft leicht bei 2 HP und stark bei 1 HP.
// Das Volume-Profil braucht einen Vignette-Override (Farbe/Smoothness dort einstellen).
[RequireComponent(typeof(Volume))]
public class DamageVignette : MonoBehaviour
{
    [SerializeField, Range(0f, 1f)] private float lowHealthIntensity = 0.3f;      // 2 HP
    [SerializeField, Range(0f, 1f)] private float criticalHealthIntensity = 0.5f; // 1 HP
    [SerializeField, Range(0f, 1f)] private float hitBoost = 0.25f;
    [SerializeField, Min(0f)] private float fadeSpeed = 1f;

    private Vignette vignette;
    private PlayerHealth playerHealth;
    private int lastHealth;
    private float baseIntensity;
    private float currentIntensity;

    private void Awake()
    {
        // .profile = Laufzeit-Kopie, das Profil-Asset bleibt unverändert
        if (!GetComponent<Volume>().profile.TryGet(out vignette))
        {
            Debug.LogError("Volume-Profil hat keinen Vignette-Override.", this);
            enabled = false;
            return;
        }
        vignette.intensity.overrideState = true;
        vignette.intensity.value = 0f;
    }

    private void Update()
    {
        if (playerHealth == null)
        {
            PlayerHealth newPlayerHealth = FindFirstObjectByType<PlayerHealth>();
            if (newPlayerHealth != null)
            {
                playerHealth = newPlayerHealth;
                lastHealth = playerHealth.CurrentHealth;
                playerHealth.OnHealthChanged += PlayerHealth_OnHealthChanged;
                playerHealth.PublishHealth();
            }
        }

        currentIntensity = Mathf.MoveTowards(currentIntensity, baseIntensity, fadeSpeed * Time.deltaTime);
        vignette.intensity.value = currentIntensity;
    }

    private void PlayerHealth_OnHealthChanged(int currentHealth, int maxHealth)
    {
        if (currentHealth == 1) baseIntensity = criticalHealthIntensity;
        else if (currentHealth == 2) baseIntensity = lowHealthIntensity;
        else baseIntensity = 0f;

        if (currentHealth < lastHealth)
            currentIntensity = Mathf.Min(baseIntensity + hitBoost, 1f);

        lastHealth = currentHealth;
    }

    private void OnDestroy()
    {
        if (playerHealth != null)
            playerHealth.OnHealthChanged -= PlayerHealth_OnHealthChanged;
    }
}
