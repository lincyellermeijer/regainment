using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public class MenuButtons : MonoBehaviour
{

    // Load assets in inspector
    [SerializeField] private AudioSource backgroundMusic;
    [SerializeField] private GameObject controllerConnected;
    [SerializeField] private GameObject controllerDisconnected;
    [SerializeField] private GameObject audioEnabled;
    [SerializeField] private GameObject audioDisabled;


    private AudioSource audioSource;
    private bool xboxController;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        controllerConnected.SetActive(false);
        controllerDisconnected.SetActive(false);

        SetSoundState();
    }


    private void Update()
    {
        string[] names = Input.GetJoystickNames();
        for (int x = 0; x < names.Length; x++)
        {
            // print(names[x].Length);
            if (names[x].Length == 51) // xbox 360 controller
            {
                print("XBOX CONTROLLER IS CONNECTED");
                xboxController = true;
            }
            else {
                xboxController = false;
            }
        }

        // Display icons when controller connects / disconnects
        if (xboxController == true)
        {
            controllerDisconnected.SetActive(false);
            controllerConnected.SetActive(true);
        }
        else if (xboxController == false)
        {
            controllerDisconnected.SetActive(true);
            controllerConnected.SetActive(false);
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
        audioSource.Play();
        backgroundMusic.Stop();
        Application.Quit();
    }


    public void ToggleSound()
    {
        audioSource.Play();

        if (PlayerPrefs.GetInt("Muted", 0) == 0)
        {
            PlayerPrefs.SetInt("Muted", 1);
        }
        else
        {
            PlayerPrefs.SetInt("Muted", 0);
        }
        SetSoundState();
    }


    private void SetSoundState()
    {
        if (PlayerPrefs.GetInt("Muted", 0) == 0)
        {
            AudioListener.volume = 1;
            audioEnabled.SetActive(true);
            audioDisabled.SetActive(false);
        }
        else
        {
            AudioListener.volume = 0;
            audioEnabled.SetActive(false);
            audioDisabled.SetActive(true);
        }
    }


}