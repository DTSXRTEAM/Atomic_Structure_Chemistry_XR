using UnityEngine;
using System.Collections;

public class PortalEffect : MonoBehaviour
{
    [Header("Particles")]
    [SerializeField] private ParticleSystem portalParticles;

    [Header("Glow")]
    [SerializeField] private Renderer portalRenderer;

    [SerializeField] private float glowIntensity = 4f;

    [SerializeField] private float glowDuration = 0.5f;

    private Material portalMaterial;
    private Color originalEmission;

    private void Awake()
    {
        portalMaterial = portalRenderer.material;

        portalMaterial.EnableKeyword("_EMISSION");

        originalEmission = portalMaterial.GetColor("_EmissionColor");
    }

    public void PlayPortalEffect()
    {
        StartCoroutine(PlayEffect());
    }

    private IEnumerator PlayEffect()
    {
        // Enable glow
        portalMaterial.SetColor(
            "_EmissionColor",
            Color.cyan * glowIntensity);

        // Play particles
        if (portalParticles != null)
        {
            portalParticles.Stop(
                true,
                ParticleSystemStopBehavior.StopEmittingAndClear);

            portalParticles.Play();
        }

        yield return new WaitForSeconds(glowDuration);

        // Restore original glow
        portalMaterial.SetColor(
            "_EmissionColor",
            originalEmission);
    }
}