using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class StartScreenScript : MonoBehaviour
{
    public GameObject playScreen;
    public GameObject optionsPanel;
    public GameObject selectPlayMode;

    public Button playButton;
    public Button exitButton;
    public Button optionsButton;
    public Button optionsBackButton;
    public Button oneVOneButton;
    public Button twoVTwoButton;
    public Button playModeBackButton;

    public Slider soundSlider;
    public Slider musicSlider;

    public AudioSource gameMusic;

    public AudioMixerGroup music;
    public AudioMixerGroup sfx;
    public AudioMixer audioMixer;

    // Start is called before the first frame update
    void Start()
    {
        optionsPanel.SetActive(false);
        selectPlayMode.SetActive(false);

        playButton.onClick.AddListener(PlayGame);
        exitButton.onClick.AddListener(ExitGame);
        optionsButton.onClick.AddListener(OpenOptions);
        optionsBackButton.onClick.AddListener(BackToMainMenu);
        oneVOneButton.onClick.AddListener(StartGameOneVOne);
        twoVTwoButton.onClick.AddListener(StartGameTwoVTwo);
        playModeBackButton.onClick.AddListener(BackToMainMenu);

        soundSlider.onValueChanged.AddListener(ChangeSound);
        musicSlider.onValueChanged.AddListener(ChangeMusic);
    }

    void StartGameTwoVTwo()
    {
        SceneManager.LoadScene("Game2v2");
    }

    void StartGameOneVOne()
    {
        SceneManager.LoadScene("Game1v1");
    }

    void PlayGame()
    {
        Debug.Log("Play Game");
        playScreen.SetActive(false);
        selectPlayMode.SetActive(true);
    }

    void ExitGame()
    {
        Debug.Log("Exit Game");

        Application.Quit();
    }

    void OpenOptions()
    {
        playScreen.SetActive(false);
        optionsPanel.SetActive(true);
    }

    void BackToMainMenu()
    {
        optionsPanel.SetActive(false);
        selectPlayMode.SetActive(false);
        playScreen.SetActive(true);
    }

    void ChangeSound(float value)
    {
        //Debug.Log("Sound: " + value);
        audioMixer.SetFloat("volumeSFX", value);
    }

    void ChangeMusic(float value)
    {
        //gameMusic.volume = value;
        audioMixer.SetFloat("volumeMusic", value);
    }
}
