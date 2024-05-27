using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartScreenScript : MonoBehaviour
{
    public GameObject playScreen;
    public GameObject optionsPanel;

    public Button playButton;
    public Button exitButton;
    public Button optionsButton;
    public Button backButton;
    public Slider soundSlider;
    public Slider musicSlider;

    public AudioSource gameMusic;

    // Start is called before the first frame update
    void Start()
    {
        optionsPanel.SetActive(false);
        playButton.onClick.AddListener(PlayGame);
        exitButton.onClick.AddListener(ExitGame);
        optionsButton.onClick.AddListener(OpenOptions);
        backButton.onClick.AddListener(CloseOptions);

        soundSlider.onValueChanged.AddListener(ChangeSound);
        musicSlider.onValueChanged.AddListener(ChangeMusic);
    }

    void PlayGame()
    {
        Debug.Log("Play Game");
    }

    void ExitGame()
    {
        Debug.Log("Exit Game");
    }

    void OpenOptions()
    {
        playScreen.SetActive(false);
        optionsPanel.SetActive(true);
    }

    void CloseOptions()
    {
        optionsPanel.SetActive(false);
        playScreen.SetActive(true);
    }

    void ChangeSound(float value)
    {
        Debug.Log("Sound: " + value);
    }

    void ChangeMusic(float value)
    {
        gameMusic.volume = value;
    }
}
