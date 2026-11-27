using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

public class CameraZoom : MonoBehaviour
{
    [SerializeField] private InputActionReference zoomCameraAction;
    private PixelPerfectCamera pixelPerfectCamera;
    private Vector2Int zoomedIn = new Vector2Int(640, 360);
    private Vector2Int zoomedOut = new Vector2Int(960, 540);
    private bool locked;
    private Vector2Int resolutionBeforeLock;

    private void Awake()
    {
        pixelPerfectCamera = GetComponent<PixelPerfectCamera>();
    }

    private void OnEnable()
    {
        zoomCameraAction.action.Enable();
    }

    private void Update()
    {
        if (locked) return;

        float scroll = zoomCameraAction.action.ReadValue<float>();

        if (scroll > 0f) SetResolution(zoomedIn);
        else if (scroll < 0f) SetResolution(zoomedOut);
    }

    public void LockZoomedIn()
    {
        if (locked) return;
        locked = true;
        resolutionBeforeLock = new Vector2Int(pixelPerfectCamera.refResolutionX, pixelPerfectCamera.refResolutionY);
        SetResolution(zoomedIn);
    }

    public void UnlockZoom()
    {
        if (!locked) return;
        locked = false;
        SetResolution(resolutionBeforeLock);
    }

    private void SetResolution(Vector2Int resolution)
    {
        pixelPerfectCamera.refResolutionX = resolution.x;
        pixelPerfectCamera.refResolutionY = resolution.y;
    }
}
