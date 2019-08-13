using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuButtons : MonoBehaviour
{

    // Load backgroundMusic in inspector
    [SerializeField] private AudioSource backgroundMusic;
    private AudioSource audioSource;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }


    private void Update()
    {
        // If escape is pressed during the credits, go back to startscreen
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Scene currentScene = SceneManager.GetActiveScene();
            if (currentScene.name == "Credits")
            {
                backgroundMusic.Stop();
                SceneManager.LoadScene("StartScreen");
            }
        }
    }


    public void StartGame()
    {
        audioSource.Play();
        backgroundMusic.Stop();
        SceneManager.LoadScene("Intro");
    }


    public void GoToCredits()
    {
        audioSource.Play();
        backgroundMusic.Stop();
        SceneManager.LoadScene("Credits");
    }


    public void ExitGame()
    {
        backgroundMusic.Stop();
        Application.Quit();
    }

}
