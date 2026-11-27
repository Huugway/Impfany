using UnityEngine;
using UnityEngine.Rendering.Universal;

// Setzt die Light2D-Intensität passend zum aktuellen Frame von heartbeating.anim.
[RequireComponent(typeof(Animator), typeof(Light2D))]
public class HeartLightPulse : MonoBehaviour
{
    // Ein Wert pro Frame der Animation (4 Frames)
    private static readonly float[] FrameIntensities = { 1.5f, 2.2f, 1.8f, 1.2f };

    private Animator animator;
    private Light2D heartLight;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        heartLight = GetComponent<Light2D>();
    }

    private void Update()
    {
        float loopTime = Mathf.Repeat(animator.GetCurrentAnimatorStateInfo(0).normalizedTime, 1f);
        int frame = (int)(loopTime * FrameIntensities.Length);
        heartLight.intensity = FrameIntensities[frame];
    }
}
