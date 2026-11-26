using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class ModelTransition : MonoBehaviour
{
    [Header("Scaling")]
    [SerializeField] private float scaleMultiplier = 20f;
    [SerializeField] private float duration = 3f;
    [SerializeField] private AnimationCurve scaleCurve;

    [Header("Fade")]
    [Range(0f, 1f)]
    [SerializeField] private float fadeStart = 0.7f;

    private Vector3 startScale;
    private Vector3 targetScale;

    private bool isAnimating;

    private Material[] materials;

    private XRBaseInteractable interactable;

    private void Awake()
    {
        interactable = GetComponent<XRBaseInteractable>();

        startScale = transform.localScale;
        targetScale = startScale * scaleMultiplier;

        Renderer[] renderers = GetComponentsInChildren<Renderer>();

        List<Material> mats = new List<Material>();

        foreach (Renderer r in renderers)
        {
            mats.AddRange(r.materials);
        }

        materials = mats.ToArray();
    }

    private void OnEnable()
    {
        interactable.selectEntered.AddListener(OnSelected);
    }

    private void OnDisable()
    {
        interactable.selectEntered.RemoveListener(OnSelected);
    }

    private void OnSelected(SelectEnterEventArgs args)
    {
        if (!isAnimating)
            StartCoroutine(ScaleRoutine());
    }

    IEnumerator ScaleRoutine()
    {
        isAnimating = true;

        float timer = 0f;

        while (timer < duration)
        {
            timer += Time.deltaTime;

            float progress = timer / duration;

            transform.localScale =
                Vector3.Lerp(startScale,
                             targetScale,
                             scaleCurve.Evaluate(progress));

            if (progress >= fadeStart)
            {
                float fade =
                    Mathf.InverseLerp(fadeStart, 1f, progress);

                Fade(fade);
            }

            yield return null;
        }

        Destroy(gameObject);
    }

    void Fade(float t)
    {
        foreach (Material mat in materials)
        {
            if (mat.HasProperty("_BaseColor"))
            {
                Color c = mat.color;
                c.a = Mathf.Lerp(1, 0, t);
                mat.color = c;
            }
        }
    }
}