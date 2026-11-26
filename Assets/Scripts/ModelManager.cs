using UnityEngine;

public class ModelManager : MonoBehaviour
{
    [Header("Objects To Hide")]
    [SerializeField] private GameObject uiCanvas;
    [SerializeField] private GameObject swarmParent;
    [SerializeField] private GameObject largeBacteriaParent;

    [Header("3D Model")]
    [SerializeField] private GameObject modelPrefab;
    [SerializeField] private Transform spawnPoint;

    private GameObject currentModel;

    private void Start()
    {
        currentModel = null;
    }

    private void Update()
    {
        // Press A to test
        if (Input.GetKeyDown(KeyCode.A))
        {
            ShowModel();
        }
    }

    public void ShowModel()
    {
        // Hide existing objects
        if (uiCanvas != null)
            uiCanvas.SetActive(false);

        if (swarmParent != null)
            swarmParent.SetActive(false);

        if (largeBacteriaParent != null)
            largeBacteriaParent.SetActive(false);

        // Show model
        //if (modelPrefab != null)
        //{
        //    modelPrefab.SetActive(true);

        //    if (spawnPoint != null)
        //    {
        //        modelPrefab.transform.position = spawnPoint.position;
        //        modelPrefab.transform.rotation = spawnPoint.rotation;
        //    }
        //}

        if (currentModel == null && modelPrefab != null)
        {
            currentModel = Instantiate(
                modelPrefab,
                spawnPoint.position,
                spawnPoint.rotation,
                spawnPoint);

            currentModel.SetActive(true);
        }
    }
}