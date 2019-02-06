using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelHandler : MonoBehaviour {
    
    public static LevelHandler instance;
    public RawImage overlay;

    public float fadeOutTime = 0.5f;
    public float fadeInTime = 0.2f;

    public MovieTexture cutscene1;
    public MovieTexture cutscene2;
    public AudioClip backgroundMusic;

    public static bool Fading {
        get { return instance.isFading; }
    }

    private Color startColor;
    private AudioSource audioSource;
    private float fadeProgress;
    private bool isFading;
    private int levelIndex;
    private bool inCountDown;
    private bool hasRestarted;
    private GameObject overlayObject;

    private void Awake() {
        //Check if instance already exists
        if (instance == null) {
            //if not, set instance to this
            instance = this;
        }
        //If instance already exists and it's not this:
        else if (instance != this) {
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);
        }

        audioSource = GetComponent<AudioSource>();
        PlayThenDoSomething(cutscene1, FadeAndContinue);
    }


    private void Start() {
        startColor = overlay.color;
        overlayObject = GameObject.FindGameObjectWithTag("FadingOverlay");
    }


    private void Update() {
        if (Input.GetKeyUp(KeyCode.Escape)) {
            StopCutscene();
        }
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.R)) {
            Scene currentScene = SceneManager.GetActiveScene();

            if (currentScene.name == "Intro") {
                Debug.LogError("Cannot reload this scene");
            } else {
                int sceneToReload = currentScene.buildIndex;
                LoadLevel(sceneToReload);
            }
        }
        if (hasRestarted) {
            Scene currentScene = SceneManager.GetActiveScene();
            if (currentScene.name == "Credits") {
                // Destroy objects who won't destroy on load
                Destroy(overlayObject);
                Destroy(this.gameObject);
            }
            if (currentScene.name == "Intro") {
                audioSource.Play();
            }
        }
    }


    /***** LOAD SCENES *****/


    public void LoadNextLevel() {
        int sceneToLoad = SceneManager.GetActiveScene().buildIndex + 1;

        if (sceneToLoad < SceneManager.sceneCountInBuildSettings) {
            {
                LoadLevel(sceneToLoad);
            }
        } else {
            Debug.LogWarning("NO NEXT SCENE DEFINED");
        }
    }


    public void LoadEnding() {
        isFading = true;
        overlay.color = startColor;
        PlayThenDoSomething(cutscene2, GoToCreditsScene);
    }

    private void GoToCreditsScene() {
        audioSource.Stop();
        Time.timeScale = 1.0f;
        hasRestarted = true;
        SceneManager.LoadScene("Credits");
    }

    private static void LoadLevel(int aLevelIndex) {
        if (Fading) return;
        instance.levelIndex = aLevelIndex;
        instance.StartFade();
    }


    /***** FADE IN / OUT *****/


    private void StartFade() {
        isFading = true;
        StartCoroutine(Fade());
    }


    private IEnumerator ManualFade(Color fromColor, Color toColor) {
        float fadeRate = 1f / 1f;
        fadeProgress = 0f;

        while (fadeProgress < 1f) {
            overlay.color = Color.Lerp(fromColor, toColor, fadeProgress);

            fadeProgress += fadeRate * Time.deltaTime;

            yield return null;
        }
        overlay.color = toColor;
    }


    private IEnumerator Fade() {
        fadeProgress = 0f;

        while (fadeProgress < 1.0f) {
            yield return new WaitForEndOfFrame();
            fadeProgress = Mathf.Clamp01(fadeProgress + Time.deltaTime / fadeOutTime);
            overlay.color = Color.Lerp(Color.clear, startColor, fadeProgress);
        }

        SceneManager.LoadScene(levelIndex);

        while (fadeProgress > 0f) {
            yield return new WaitForEndOfFrame();
            fadeProgress = Mathf.Clamp01(fadeProgress - Time.deltaTime / fadeInTime);
            overlay.color = Color.Lerp(Color.clear, startColor, fadeProgress);
        }

        isFading = false;
    }


    /***** CUTSCENES *****/


    public void PlayThenDoSomething(MovieTexture movieTexture, Action callback) {
        if (movieTexture != null) {
            movieTexture.Stop();
            movieTexture.Play();
            audioSource.clip = movieTexture.audioClip;
            audioSource.Play();
            Screen.fullScreen = true;
            Time.timeScale = 0f;

            StartCoroutine(FindEnd(movieTexture, callback));
        }
    }


    private void StopCutscene() {
        Screen.fullScreen = false;
        audioSource.Stop();

        if (cutscene1 != null && cutscene1.isPlaying) {
            cutscene1.Stop();
            FadeAndContinue();
        }
        if (cutscene2 != null && cutscene2.isPlaying) {
            cutscene2.Stop();
            GoToCreditsScene();
        }
    }


    private void FadeAndContinue() {
        audioSource.clip = backgroundMusic;
        audioSource.Play();
        StartCoroutine(ManualFade(startColor, Color.clear));
        Time.timeScale = 1.0f;
    }

    
    private IEnumerator FindEnd(MovieTexture movieTexture, Action callback) {
        while (movieTexture.isPlaying) {
            yield return 0;
        }

        callback();
        yield break;
    }


    private void OnGUI() {
        if (cutscene1 != null && cutscene1.isPlaying) {
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), cutscene1, ScaleMode.StretchToFill);
        }

        if (cutscene2 != null && cutscene2.isPlaying) {
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), cutscene2, ScaleMode.StretchToFill);
        }
    }


}