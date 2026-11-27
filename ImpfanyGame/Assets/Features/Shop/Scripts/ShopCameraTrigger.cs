using UnityEngine;

public class ShopCameraTrigger : MonoBehaviour
{
    [SerializeField] private Transform cameraPoint;
    [SerializeField] private CameraFollow cameraFollow;
    [SerializeField] private CameraZoom cameraZoom;

    private void Awake()
    {
        if (cameraFollow == null) cameraFollow = FindFirstObjectByType<CameraFollow>();
        if (cameraZoom == null) cameraZoom = FindFirstObjectByType<CameraZoom>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        cameraFollow.LockTo(cameraPoint);
        cameraZoom.LockZoomedIn();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        cameraFollow.Unlock();
        cameraZoom.UnlockZoom();
    }
}