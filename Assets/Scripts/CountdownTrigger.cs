using System.Collections;
using TMPro;
using UnityEngine;

public class CountdownTrigger : MonoBehaviour
{
    [SerializeField] private GameObject portal;
    [SerializeField] private GameObject countdownUI;
    [SerializeField] private TMP_Text countdownText;

    private bool handSelected;
    private bool countdownStarted;

    public void HandSelected()
    {
        handSelected = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (countdownStarted)
            return;

        if (!handSelected)
            return;

        if (!other.CompareTag("MainCamera"))
            return;

        countdownStarted = true;

        StartCoroutine(CountdownRoutine());
    }

    private IEnumerator CountdownRoutine()
    {
        countdownUI.SetActive(true);

        countdownText.text = "3";
        yield return new WaitForSeconds(1);

        countdownText.text = "2";
        yield return new WaitForSeconds(1);

        countdownText.text = "1";
        yield return new WaitForSeconds(1);

        countdownUI.SetActive(false);

        portal.SetActive(true);
    }
}