using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeImage : MonoBehaviour
{
    // private float fadeDuration = 1f; // Duration of the fade effect

    private Image fadeImage; // Reference to the UI image used for fading

    private void Awake()
    {
        fadeImage = GetComponent<Image>();
    }

    public void FadeScreen(bool fadeOut, float fadeDuration)
    {
        StartCoroutine(Fade(fadeOut, fadeDuration));
    }

    IEnumerator Fade(bool fadeOut, float fadeDuration)
    {
        gameObject.SetActive(true);
        float elapsedTime = 0.0f;
        Color initialColor = fadeImage.color;
        float initialAlpha = initialColor.a;
        float targetAlpha = fadeOut ? 1.0f : 0.0f;
        if (fadeOut) {
            initialAlpha = 0.0f;
        }
        else
        {
            initialAlpha = 1.0f;
        }

        while (elapsedTime < fadeDuration)
        {
            float newAlpha = Mathf.Lerp(initialAlpha, targetAlpha, elapsedTime / fadeDuration);
            fadeImage.color = new Color(initialColor.r, initialColor.g, initialColor.b, newAlpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        fadeImage.color = new Color(initialColor.r, initialColor.g, initialColor.b, targetAlpha);
        if (!fadeOut)
        {
            gameObject.SetActive(false); 
        }
    }

    public void SetFullOpacity()
    {
        Color initialColor = fadeImage.color;
        fadeImage.color = new Color(initialColor.r, initialColor.g, initialColor.b, 1.0f);
    }
}