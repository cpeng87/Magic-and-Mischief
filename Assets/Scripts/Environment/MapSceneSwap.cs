using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapSceneSwap : MonoBehaviour
{
    // Specify the name of the scene you want to switch to
    public string sceneToLoad;
    public string spawnpointName;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collision is with the player object
        if (other.CompareTag("Player"))
        {
            SwapScene();
        }
    }

    public void SwapScene()
    {
        if (GameManager.instance.sceneSwapManager == null)
        {
            Debug.Log("scene swap mananger is null");
        }
        GameManager.instance.sceneSwapManager.SceneSwap(sceneToLoad, spawnpointName);
    }
}
