using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

public class CameraZoom : MonoBehaviour
{
    [SerializeField] private InputActionReference zoomCameraAction;
    private PixelPerfectCamera pixelPerfectCamera;
    private Vector2Int zoomedIn = new Vector2Int(640, 360);
    private Vector2Int zoomedOut = new Vector2Int(930, 540);
    
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
        float scroll = zoomCameraAction.action.ReadValue<float>();

        if (scroll > 0f) SetResolution(zoomedIn);
        else if (scroll < 0f) SetResolution(zoomedOut);
    }

    private void SetResolution(Vector2Int resolution)
    {
        pixelPerfectCamera.refResolutionX = resolution.x;
        pixelPerfectCamera.refResolutionY = resolution.y;
    }
}
