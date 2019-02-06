using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour {

    public AudioSource backgroundMusic;

    private AudioSource audioSource;

    private void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    public void LoadIntro() {
        audioSource.Play();
        backgroundMusic.Stop();
        SceneManager.LoadScene("Intro");
    }


    public void GoToCredits() {
        audioSource.Play();
        backgroundMusic.Stop();
        SceneManager.LoadScene("Credits");
    }


    public void doExitGame() {
        Application.Quit();
    }


    private void Update() {
        if (Input.GetKeyUp(KeyCode.Escape)) {
            Scene currentScene = SceneManager.GetActiveScene();
            if (currentScene.name == "Credits") {
                backgroundMusic.Stop();
                SceneManager.LoadScene("StartScreen");
            }
        }
    }

}
