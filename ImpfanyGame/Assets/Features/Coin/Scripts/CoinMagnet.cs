using UnityEngine;

public class CoinMagnet : MonoBehaviour
{
    [SerializeField] private float pickupRadius = 1.5f;
    [SerializeField] private float flySpeed = 8f;
    private Transform target;

    private void Update()
    {
        if (target == null)
        {
            PlayerWallet wallet = FindFirstObjectByType<PlayerWallet>();
            if (wallet == null) return;
            target = wallet.transform;
        }

        float distance = Vector2.Distance(transform.position, target.position);
        if (distance > pickupRadius) return;

        transform.position = Vector2.MoveTowards(transform.position, target.position, flySpeed * Time.deltaTime);
    }
}