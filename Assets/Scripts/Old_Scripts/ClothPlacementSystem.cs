using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class ClothPlacementSystem : MonoBehaviour
{
    public GameObject clothObject;
    public GameObject cube1;
    public GameObject cube2;

    private bool isTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (isTriggered) return;

        Debug.Log("Triggered by: " + other.name);

        // 🔥 IMPORTANT: Check parent (since collider is on child)
        XRGrabInteractable grab = other.GetComponentInParent<XRGrabInteractable>();

        if (grab != null && grab.gameObject == clothObject)
        {
            Debug.Log("Cloth detected correctly!");

            isTriggered = true;

            // Enable Cube 2
            cube2.SetActive(true);

            // Disable Cloth
            clothObject.SetActive(false);

            // Disable Cube 1
            cube1.SetActive(false);
        }
    }
}