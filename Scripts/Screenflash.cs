using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenFlash : MonoBehaviour
{
    private Image flashImage;

    private void Awake()
    {
        flashImage = GetComponent<Image>();
    }

    public void Flash(float duration = 0.4f, float holdTime = 0.3f)
    {
        StartCoroutine(DoFlash(duration, holdTime));
    }

    private IEnumerator DoFlash(float duration, float holdTime)
    {
        float half = duration / 2f;
        Color color = flashImage.color;

        // Fade in to white
        for (float t = 0; t < half; t += Time.deltaTime)
        {
            float normalized = t / half;
            color.a = Mathf.Lerp(0f, 1f, normalized);
            flashImage.color = color;
            yield return null;
        }

        // Hold full white
        color.a = 1f;
        flashImage.color = color;
        yield return new WaitForSeconds(holdTime);

        // Fade out back to transparent
        for (float t = 0; t < half; t += Time.deltaTime)
        {
            float normalized = t / half;
            color.a = Mathf.Lerp(1f, 0f, normalized);
            flashImage.color = color;
            yield return null;
        }

        color.a = 0f;
        flashImage.color = color;
    }
}
