using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnButtonClick(Button clickedButton)
    {
        // Get the GameObject name of the clicked button
        string buttonName = clickedButton.gameObject.name;
        Debug.Log("Button clicked! Object Name: " + buttonName);
        SceneManager.LoadScene(buttonName);
    }
}
