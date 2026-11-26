using UnityEngine;

public class BacteriaController : MonoBehaviour
{
    [Header("Zoom")]
    [SerializeField] private float zoomDuration = 3f;

    [SerializeField] private float maxScale = 20f;

    [SerializeField] private AnimationCurve zoomCurve;

    private Vector3 originalScale;

    private bool zooming;

    private float timer;

    private void Awake()
    {
        originalScale = transform.localScale;

        gameObject.SetActive(false);
    }

    public void StartTransition()
    {
        Debug.Log("Bacteria Transition Started");

        gameObject.SetActive(true);

        transform.localScale = originalScale;

        timer = 0f;

        zooming = true;
    }

    private void Update()
    {
        if (!zooming)
            return;

        timer += Time.deltaTime;

        float t = Mathf.Clamp01(timer / zoomDuration);

        float curve = zoomCurve.Evaluate(t);

        transform.localScale =
            Vector3.Lerp(
                originalScale,
                originalScale * maxScale,
                curve);

        if (t >= 1f)
        {
            zooming = false;
        }
    }
}