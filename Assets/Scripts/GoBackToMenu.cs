using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoBackToMenu : MonoBehaviour
{
    void Update()
    {
        // If cancel (ESC or B) is pressed during the credits, go back to startscreen
        if (Input.GetButtonDown("Cancel"))
        {
            Scene currentScene = SceneManager.GetActiveScene();
            if (currentScene.name == "Credits")
            {
                SceneManager.LoadScene("StartScreen");
            }
        }
    }


}