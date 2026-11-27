using UnityEngine;
using UnityEngine.InputSystem;

// Heilt den Player voll, wenn er im Trigger steht und E drückt. Beliebig oft nutzbar.
// Braucht einen Collider2D mit "Is Trigger" an.
[RequireComponent(typeof(Collider2D))]
public class HealHeart : MonoBehaviour
{
    [SerializeField] private GameObject healPrompt;

    private PlayerHealth playerInRange;

    private void Awake()
    {
        if (healPrompt != null) healPrompt.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        playerInRange = other.GetComponentInParent<PlayerHealth>();
        if (healPrompt != null) healPrompt.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        playerInRange = null;
        if (healPrompt != null) healPrompt.SetActive(false);
    }

    private void Update()
    {
        if (playerInRange == null) return;
        if (Keyboard.current == null || !Keyboard.current.eKey.wasPressedThisFrame) return;
        playerInRange.Heal(playerInRange.MaxHealth);
    }
}
