using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FlashController : MonoBehaviour
{
    [SerializeField] private Image flashImage;
    [SerializeField] private float flashDuration = 0.15f;

    public IEnumerator FlashIn()
    {
        Color color = flashImage.color;

        float timer = 0f;

        while (timer < flashDuration)
        {
            timer += Time.deltaTime;

            color.a = Mathf.Lerp(0f, 1f, timer / flashDuration);

            flashImage.color = color;

            yield return null;
        }

        color.a = 1f;
        flashImage.color = color;
    }

    public IEnumerator FlashOut()
    {
        Color color = flashImage.color;

        float timer = 0f;

        while (timer < flashDuration)
        {
            timer += Time.deltaTime;

            color.a = Mathf.Lerp(1f, 0f, timer / flashDuration);

            flashImage.color = color;

            yield return null;
        }

        color.a = 0f;
        flashImage.color = color;
    }
}