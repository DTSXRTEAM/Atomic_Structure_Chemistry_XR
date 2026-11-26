using System.Collections;
using UnityEngine;

public class TransitionManager : MonoBehaviour
{
    [Header("Splash Quad")]
    [SerializeField] private MeshRenderer splashRenderer;

    private Material splashMaterial;

    [SerializeField] private float fadeInDuration = 1f;
    [SerializeField] private float fadeOutDuration = 1.2f;

    private bool transitionStarted;

    [Header("Lab")]
    [SerializeField] private GameObject labRoot;
    [SerializeField] private GameObject placeHandtrigger;

    [SerializeField] private float waitBeforeHide = 0.5f;

    private Renderer[] labRenderers;
    private Renderer[] placeHandRenderers;

    [Header("Skybox")]
    [SerializeField] private SkyboxManager skyboxManager;

    [Header("Bacteria")]
    [SerializeField] private BacteriaSpawner bacteriaSpawner;

    [SerializeField] private UIManager uiManager;

    [Header("Portal")]
    [SerializeField] private PortalEffect portalEffect;

    [SerializeField] private GameObject portalObject;

    private void Awake()
    {
        labRenderers = labRoot.GetComponentsInChildren<Renderer>(true);
        placeHandRenderers = placeHandtrigger.GetComponentsInChildren<Renderer>(true);

        splashMaterial = splashRenderer.material;

        Color color = splashMaterial.color;
        color.a = 0f;
        splashMaterial.color = color;
    }

    public void StartTransition()
    {
        if (transitionStarted)
            return;

        transitionStarted = true;

        StartCoroutine(TransitionSequence());
    }

    private IEnumerator TransitionSequence()
    {
        // Portal activation
        portalEffect.PlayPortalEffect();

        yield return new WaitForSeconds(0.5f);

        // Fade splash in
        yield return StartCoroutine(FadeSplashIn());

        yield return new WaitForSeconds(waitBeforeHide);

        // Hide Lab
        HideLab();

        // Hide Place Hand Trigger
        HidePlaceHandTrigger();

        yield return new WaitForSeconds(0.2f);

        // Change Skybox
        skyboxManager.ChangeToBacteriaSkybox();

        yield return new WaitForSeconds(0.3f);

        // Spawn bacteria
        bacteriaSpawner.SpawnBacteria();

        uiManager.ShowPanels();

        // Fade splash out
        yield return StartCoroutine(FadeSplashOut());
    }

    private IEnumerator FadeSplashIn()
    {
        Color color = splashMaterial.color;

        float timer = 0f;

        while (timer < fadeInDuration)
        {
            timer += Time.deltaTime;

            color.a = Mathf.Lerp(0f, 1f, timer / fadeInDuration);

            splashMaterial.color = color;

            yield return null;
        }

        color.a = 1f;
        splashMaterial.color = color;
    }

    private IEnumerator FadeSplashOut()
    {
        // Hide portal before the fade-out starts
        if (portalObject != null)
        {
            portalObject.SetActive(false);
        }

        Color color = splashMaterial.color;

        float timer = 0f;

        while (timer < fadeOutDuration)
        {
            timer += Time.deltaTime;

            color.a = Mathf.Lerp(1f, 0f, timer / fadeOutDuration);

            splashMaterial.color = color;

            yield return null;
        }

        color.a = 0f;
        splashMaterial.color = color;
    }

    private void HideLab()
    {
        foreach (Renderer renderer in labRenderers)
        {
            renderer.enabled = false;
        }
    }

    private void HidePlaceHandTrigger()
    {
        foreach (Renderer renderer in placeHandRenderers)
        {
            renderer.enabled = false;
        }
    }
}