using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    // The name or index of the next scene to load
    public string nextSceneName;

    void Update()
    {
        // Check if any key is pressed
        if (Input.anyKeyDown)
        {
            // Load the next scene
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
