using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelComplete : MonoBehaviour
{
    [SerializeField]
    private string nextLevel;



    private GameObject levelCompleteInstance;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player entered the trigger. Destroying player and restarting scene...");

            // Destroy the player object
            Destroy(other.gameObject);

            // Shrink the current object
            transform.localScale = new Vector3(0f, 0f, 0f);

            // Instantiate the "LevelComplete" prefab
            levelCompleteInstance = Instantiate(
                Resources.Load<GameObject>("Prefabs/LevelComplete/LevelComplete"),
                new Vector3(transform.position.x, transform.position.y - 0.8f, 0),
                Quaternion.identity
            );

             Invoke("NextLevel", 1.5f);
        }
    }

    // This method will be called by the animation event when the animation finishes

    // Coroutine to move the "LevelComplete" object down

    // Method to load the next level
    void NextLevel()
    {
        // Load the specified level
        SceneManager.LoadScene(nextLevel);
    }
}
