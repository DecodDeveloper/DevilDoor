using UnityEngine;
using UnityEngine.SceneManagement; // For scene management

public class SceneLoader : MonoBehaviour
{
    // This method takes the scene name as input
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
