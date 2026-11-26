using UnityEngine;

public class ZoomTarget1 : MonoBehaviour
{
    [Header("Zoom Settings")]
    [SerializeField] private float zoomDuration = 0.6f;

    [SerializeField] private float zoomMultiplier = 80f;

    [SerializeField] private AnimationCurve zoomCurve;

    [Header("Next Target")]
    [SerializeField] private ZoomTarget1 nextTarget;

    [Range(0f, 1f)]
    [SerializeField]
    private float startNextAt = 0.4f;

    [Header("Skybox")]
    [SerializeField] private SkyboxTransition skyboxManager;

    [SerializeField]
    [Range(0f, 1f)]
    private float skyboxTrigger = 0.7f;

    private bool skyboxChanged;

    private bool nextStarted;

    private Vector3 originalScale;

    private bool zooming;

    private float timer;

    public bool IsZooming => zooming;

    public float Progress { get; private set; }

    private void Awake()
    {
        // Remember the imported model scale
        originalScale = transform.localScale;
    }

    public void StartZoom()
    {
        zooming = true;

        timer = 0f;

        Progress = 0f;

        // Reset to imported scale
        transform.localScale = originalScale;

        nextStarted = false;

        skyboxChanged = false;
    }

    private void Update()
    {
        if (!zooming)
            return;

        timer += Time.deltaTime;

        Progress = Mathf.Clamp01(timer / zoomDuration);

        if (!nextStarted &&
    Progress >= startNextAt)
        {
            nextStarted = true;

            if (nextTarget != null)
            {
                nextTarget.gameObject.SetActive(true);

                nextTarget.StartZoom();
            }
        }

        if (!skyboxChanged && Progress >= skyboxTrigger)
        {
            skyboxChanged = true;

            if (skyboxManager != null)
            {
                skyboxManager.ChangeSkybox();
            }
        }

        float curve = zoomCurve.Evaluate(Progress);

        transform.localScale =
            Vector3.Lerp(
                originalScale,
                originalScale * zoomMultiplier,
                curve);

        if (Progress >= 1f)
        {
            zooming = false;
        }
    }

    public void ResetZoom()
    {
        zooming = false;

        timer = 0f;

        Progress = 0f;

        transform.localScale = originalScale;
    }
}