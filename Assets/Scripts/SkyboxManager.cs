using UnityEngine;

public class SkyboxManager : MonoBehaviour
{
    [Header("Skyboxes")]
    [SerializeField] private Material labSkybox;

    [SerializeField] private Material bacteriaSkybox;

    public void ChangeToBacteriaSkybox()
    {
        RenderSettings.skybox = bacteriaSkybox;

        DynamicGI.UpdateEnvironment();

        Debug.Log("Skybox Changed");
    }

    public void ChangeToLabSkybox()
    {
        RenderSettings.skybox = labSkybox;

        DynamicGI.UpdateEnvironment();
    }
}