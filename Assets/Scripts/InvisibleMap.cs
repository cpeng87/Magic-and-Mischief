using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class InvisibleMap : MonoBehaviour
{
    private TilemapRenderer tilemapRenderer;
    private Tilemap tilemap;

    void Awake()
    {
        tilemapRenderer = GetComponent<TilemapRenderer>();
        tilemap = GetComponent<Tilemap>();
    }

    public void SetActiveAndFade(float time)
    {
        tilemapRenderer.enabled = true;
        StartCoroutine(FadeOut(time));
    }

    private IEnumerator FadeOut(float time)
    {
        float elapsedTime = 0f;
        Color originalColor = tilemap.color;
        Color targetColor = new Color(originalColor.r, originalColor.g, originalColor.b, 0);

        while (elapsedTime < time)
        {
            elapsedTime += Time.deltaTime;
            tilemap.color = Color.Lerp(originalColor, targetColor, elapsedTime / time);
            yield return null;
        }

        tilemap.color = targetColor;
        tilemapRenderer.enabled = false; // Optionally disable the renderer after fade out
    }
}
