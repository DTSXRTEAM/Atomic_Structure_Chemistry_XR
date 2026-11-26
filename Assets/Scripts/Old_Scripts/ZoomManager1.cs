using System.Collections;
using UnityEngine;

public class ZoomManager1 : MonoBehaviour
{
    [Header("Current Target")]
    [SerializeField] private ZoomTarget currentTarget;

    [Header("Flash")]
    [SerializeField] private FlashController flashController;

    private void Start()
    {
        currentTarget.OnZoomFinished += HandleFinished;
    }

    public void StartZoom()
    {
        currentTarget.StartZoom();
    }

    private void HandleFinished(ZoomTarget target)
    {
        GameObject next = target.NextObject;

        if (next == null)
        {
            Debug.Log("No Next Object Assigned.");
            return;
        }

        StartCoroutine(Transition(target.gameObject, next));
    }

    private IEnumerator Transition(GameObject currentObject, GameObject nextObject)
    {
        // Flash to white
        yield return StartCoroutine(flashController.FlashIn());

        // Hide current object
        currentObject.SetActive(false);

        // Show next object
        nextObject.SetActive(true);

        // Update current target
        currentTarget = nextObject.GetComponent<ZoomTarget>();

        if (currentTarget != null)
        {
            currentTarget.OnZoomFinished += HandleFinished;
        }

        // Flash back to normal
        yield return StartCoroutine(flashController.FlashOut());
    }
}