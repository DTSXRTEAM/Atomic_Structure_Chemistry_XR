using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

[RequireComponent(typeof(XRSimpleInteractable))]
public class PlaceHandTrigger : MonoBehaviour
{
    [SerializeField] private CountdownTrigger countdownTrigger;

    private XRSimpleInteractable interactable;
    private bool handSelected;

    private void Awake()
    {
        interactable = GetComponent<XRSimpleInteractable>();
        interactable.selectEntered.AddListener(OnSelected);
    }

    private void OnSelected(SelectEnterEventArgs args)
    {
        if (handSelected)
            return;

        handSelected = true;

        Debug.Log("Place Hand Selected");

        countdownTrigger.HandSelected();
    }

    private void OnDestroy()
    {
        interactable.selectEntered.RemoveListener(OnSelected);
    }
}