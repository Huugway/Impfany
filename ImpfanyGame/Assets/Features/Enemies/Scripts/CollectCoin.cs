using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class CollectCoin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerWallet wallet))
        {
            wallet.AddCoin();
            Destroy(gameObject);
        }
    }
}
