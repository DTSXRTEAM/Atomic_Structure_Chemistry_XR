using UnityEngine;

public class ZoomManager2 : MonoBehaviour
{
    [SerializeField] private ZoomTarget1 firstTarget;
    [SerializeField]
    private WarpEffect warpEffect;

    public void StartZoom()
    {
        warpEffect.PlayWarp();
        firstTarget.StartZoom();
    }
}
