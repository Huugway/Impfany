using UnityEngine;

public class PlayerEnterShopScript : MonoBehaviour
{
    [SerializeField] private GameFlow gameFlow;

    private void Awake()
    {
        if (gameFlow == null) gameFlow = FindFirstObjectByType<GameFlow>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            gameFlow.PlayerEnteredShop();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            gameFlow.PlayerLeftShop();
        }
    }
}
