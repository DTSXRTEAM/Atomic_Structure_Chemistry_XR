using UnityEngine;

public class SkyboxTransition : MonoBehaviour
{
    [Header("Skyboxes")]
    [SerializeField] private Material firstSkybox;
    [SerializeField] private Material secondSkybox;

    public void ChangeSkybox()
    {
        RenderSettings.skybox = secondSkybox;

        DynamicGI.UpdateEnvironment();
    }

    public void ResetSkybox()
    {
        RenderSettings.skybox = firstSkybox;

        DynamicGI.UpdateEnvironment();
    }
}