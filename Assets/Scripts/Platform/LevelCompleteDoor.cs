using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCompleteDoor : MonoBehaviour
{
    [SerializeField]
    private float levelCompleteMoveSpeed = 50f; // Speed for downward movement

    void Start() { }

    // Update is called once per frame
    void Update() { }

    public void OnAnimationComplete()
    {
        // Start moving the LevelComplete object down once the animation is complete
        Debug.Log("================ animation complete");
        StartCoroutine(MoveLevelCompleteDown());

        // Invoke the NextLevel method after 0.5 seconds
    }

    IEnumerator MoveLevelCompleteDown()
    {
        while (true)
        {
            // Move the object downwards
            transform.position += new Vector3(0, -levelCompleteMoveSpeed * Time.deltaTime, 0);

            // Yield until the next frame
            yield return null;
        }
    }
}
