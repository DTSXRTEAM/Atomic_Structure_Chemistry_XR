using UnityEngine;

public class ZoomTriggerZone : MonoBehaviour
{
    [SerializeField] private ZoomManager2 zoomManager;

    private bool triggered;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entered Trigger : " + other.name);

        if (triggered)
            return;

        if (other.CompareTag("MainCamera"))
        {
            Debug.Log("Main Camera Entered");

            triggered = true;

            zoomManager.StartZoom();
        }
    }
}