using System.Collections;
using UnityEngine;

public class EnemyHitFlash : MonoBehaviour
{
    [SerializeField] private Material flashMaterial;
    [SerializeField, Min(0f)] private float duration = 0.08f;

    private SpriteRenderer spriteRenderer;
    private Material originalMaterial;
    private Coroutine flashRoutine;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalMaterial = spriteRenderer.sharedMaterial;
    }

    public void Play()
    {
        if (flashRoutine != null) StopCoroutine(flashRoutine);
        flashRoutine = StartCoroutine(Flash());
    }

    private IEnumerator Flash()
    {
        spriteRenderer.sharedMaterial = flashMaterial;
        yield return new WaitForSeconds(duration);
        spriteRenderer.sharedMaterial = originalMaterial;
        flashRoutine = null;
    }

    private void OnDisable()
    {
        if (spriteRenderer != null) spriteRenderer.sharedMaterial = originalMaterial;
        flashRoutine = null;
    }
}