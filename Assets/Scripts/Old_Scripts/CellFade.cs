using UnityEngine;

public class CellFade : MonoBehaviour
{
    [Header("Hide Settings")]
    [SerializeField] private float hideAtProgress = 0.9f;

    [Header("Reference")]
    [SerializeField] private ZoomTarget1 zoomTarget;

    private Renderer[] renderers;

    private bool hidden;

    private void Awake()
    {
        renderers = GetComponentsInChildren<Renderer>(true);
    }

    private void Update()
    {
        if (hidden)
            return;

        if (zoomTarget.Progress >= hideAtProgress)
        {
            HideModel();
        }
    }

    private void HideModel()
    {
        hidden = true;

        foreach (Renderer renderer in renderers)
        {
            renderer.enabled = false;
        }
    }

    public void ResetModel()
    {
        hidden = false;

        foreach (Renderer renderer in renderers)
        {
            renderer.enabled = true;
        }
    }
}