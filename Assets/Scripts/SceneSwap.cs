using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwap : MonoBehaviour
{
    // Specify the name of the scene you want to switch to
    public string sceneToLoad;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collision is with the player object
        if (other.CompareTag("Player"))
        {
            // Load the specified scene
            // SceneManager.LoadScene(sceneToLoad);
            GameManager.instance.SceneSwap(sceneToLoad);
        }
    }
}
