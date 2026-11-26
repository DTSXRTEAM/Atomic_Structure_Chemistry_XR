using System.Collections;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("UI Panels")]
    [SerializeField] private GameObject introductionPanel;
    [SerializeField] private GameObject informationPanel;

    [Header("Settings")]
    [SerializeField] private float delay = 2f;

    private void Start()
    {
        HidePanels();
        //ShowPanels();
    }

    public void ShowPanels()
    {
        StartCoroutine(ShowPanelsRoutine());
    }

    private IEnumerator ShowPanelsRoutine()
    {
        yield return new WaitForSeconds(delay);

        if (introductionPanel != null)
            introductionPanel.SetActive(true);

        if (informationPanel != null)
            informationPanel.SetActive(true);
    }

    public void HidePanels()
    {
        if (introductionPanel != null)
            introductionPanel.SetActive(false);

        if (informationPanel != null)
            informationPanel.SetActive(false);
    }
}