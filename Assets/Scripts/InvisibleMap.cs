using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class InvisibleMap : MonoBehaviour
{
    private TilemapRenderer tilemapRenderer;
    private Tilemap tilemap;
    private Coroutine fadeOut;
    private Color originalColor;

    void Awake()
    {
        tilemapRenderer = GetComponent<TilemapRenderer>();
        tilemap = GetComponent<Tilemap>();
        originalColor = tilemap.color;
    }

    public void SetActiveAndFade(float time)
    {
        tilemapRenderer.enabled = true;
        fadeOut = StartCoroutine(FadeOut(time));
    }
    public void SetActive()
    {
        tilemapRenderer.enabled = true;
    }

    private IEnumerator FadeOut(float time)
    {
        float elapsedTime = 0f;
        // Color originalColor = tilemap.color;
        Color targetColor = new Color(originalColor.r, originalColor.g, originalColor.b, 0);

        while (elapsedTime < time)
        {
            elapsedTime += Time.deltaTime;
            tilemap.color = Color.Lerp(originalColor, targetColor, elapsedTime / time);
            yield return null;
        }

        tilemap.color = targetColor;
        tilemapRenderer.enabled = false;
    }
    public void End()
    {
        float time = 1f;
        FadeOut(time);
    }

    public void EndFade()
    {
        StopCoroutine(fadeOut);
        tilemap.color = originalColor;
        tilemapRenderer.enabled = false;
    }

}
