using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TransitionManager1 : MonoBehaviour
{
    [Header("Splash")]
    [SerializeField] private Image splashImage;

    [SerializeField] private float fadeInDuration = 1f;

    private bool transitionStarted;

    [Header("Lab")]
    [SerializeField] private GameObject labRoot;

    [SerializeField] private float waitBeforeHide = 0.5f;

    private Renderer[] labRenderers;

    [Header("Skybox")]
    [SerializeField] private SkyboxManager skyboxManager;

    [Header("Bacteria")]
    [SerializeField] private BacteriaController bacteriaController;

    [SerializeField] private float fadeOutDuration = 1.2f;

    [SerializeField]
    private PortalEffect portalEffect;

    private void Awake()
    {
        labRenderers = labRoot.GetComponentsInChildren<Renderer>(true);
    }

    public void StartTransition()
    {
        if (transitionStarted)
            return;

        transitionStarted = true;

        //StartCoroutine(FadeSplashIn());
        StartCoroutine(TransitionSequence());
    }


    private IEnumerator TransitionSequence()
    {
        // Play portal particles first
        portalEffect.PlayPortalEffect();

        // Let the player see the portal activate
        yield return new WaitForSeconds(0.5f);

        // Now start the transition
        yield return StartCoroutine(FadeSplashIn());
    }

    private IEnumerator FadeSplashIn()
    {
        Color color = splashImage.color;

        float timer = 0f;

        while (timer < fadeInDuration)
        {
            timer += Time.deltaTime;

            color.a = Mathf.Lerp(0f, 1f, timer / fadeInDuration);

            splashImage.color = color;

            yield return null;
        }

        color.a = 1f;
        splashImage.color = color;

        yield return new WaitForSeconds(waitBeforeHide);

        // Hide the lab
        HideLab();

        yield return new WaitForSeconds(0.2f);

        skyboxManager.ChangeToBacteriaSkybox();

        yield return new WaitForSeconds(0.3f);

        Debug.Log("Calling Bacteria");

        bacteriaController.StartTransition();

        //Debug.Log("Lab Hidden");

        //Debug.Log("Splash Fade In Complete");

        yield return StartCoroutine(FadeSplashOut());
    }

    private void HideLab()
    {
        foreach (Renderer renderer in labRenderers)
        {
            renderer.enabled = false;
        }
    }

    private IEnumerator FadeSplashOut()
    {
        Color color = splashImage.color;

        float timer = 0f;

        while (timer < fadeOutDuration)
        {
            timer += Time.deltaTime;

            color.a = Mathf.Lerp(1f, 0f, timer / fadeOutDuration);

            splashImage.color = color;

            yield return null;
        }

        color.a = 0f;
        splashImage.color = color;
    }
}