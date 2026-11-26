using UnityEngine;
using System;

public class ZoomTarget : MonoBehaviour
{
    [Header("Zoom")]
    [SerializeField] private float duration = 0.6f;

    [SerializeField] private float maxScale = 80f;

    [SerializeField] private AnimationCurve zoomCurve;

    private bool zooming;

    private float timer;

    public bool IsFinished { get; private set; }

    private Renderer meshRenderer;
    private Material materialInstance;

    private Color originalColor;

    [SerializeField]
    private float fadeDuration = 0.25f;

    private bool fading;
    private float fadeTimer;

    [Header("Next Object")]
    [SerializeField] private GameObject nextObject;

    public event Action<ZoomTarget> OnZoomFinished;

    private void Awake()
    {
        meshRenderer = GetComponent<Renderer>();

        materialInstance = meshRenderer.material;

        originalColor = materialInstance.color;
    }

    public void StartZoom()
    {
        zooming = true;

        timer = 0;

        IsFinished = false;

        transform.localScale = Vector3.one;
    }

    private void Update()
    {
        if (!zooming)
            return;

        timer += Time.deltaTime;

        float t =
            Mathf.Clamp01(
                timer / duration);

        float curve =
            zoomCurve.Evaluate(t);

        transform.localScale =
            Vector3.Lerp(
                Vector3.one,
                Vector3.one * maxScale,
                curve);

        if (t >= 0.85f && !fading)
        {
            fading = true;

            fadeTimer = 0;
        }

        if (fading)
        {
            fadeTimer += Time.deltaTime;

            float fadeProgress =
                Mathf.Clamp01(
                    fadeTimer / fadeDuration);

            Color color = originalColor;

            color.a = Mathf.Lerp(
                1f,
                0f,
                fadeProgress);

            materialInstance.color = color;

            if (fadeProgress >= 1f)
            {
                fading = false;

                zooming = false;

                IsFinished = true;
                OnZoomFinished?.Invoke(this);

                meshRenderer.enabled = false;
            }
        }
    }

    public void ResetTarget()
    {
        transform.localScale = Vector3.one;

        materialInstance.color = originalColor;

        meshRenderer.enabled = true;

        fading = false;

        fadeTimer = 0;

        zooming = false;

        IsFinished = false;
    }

    public GameObject NextObject
    {
        get
        {
            return nextObject;
        }
    }
}