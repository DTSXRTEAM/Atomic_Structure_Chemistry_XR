using UnityEngine;

public class PortalTrigger : MonoBehaviour
{
    [SerializeField] private TransitionManager transitionManager;

    private bool triggered;

    private void OnTriggerEnter(Collider other)
    {
        if (triggered)
            return;

        if (other.CompareTag("MainCamera"))
        {
            triggered = true;

            transitionManager.StartTransition();
        }
    }
}