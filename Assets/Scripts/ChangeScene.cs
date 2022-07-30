using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public AudioSource[] sounds;
    public AudioSource click;
    public AudioSource theme;
    static bool AudioBegin = false;
    public AudioMixer mainMixer;

    void Start()
    {
        sounds = GetComponents<AudioSource>();
        click = sounds[0];
        theme = sounds[1];
    }

    
    void Awake()
    {
        if (!AudioBegin)
        {
            theme.Play();
            DontDestroyOnLoad(gameObject);
            AudioBegin = true;
        }
    }


    void Update()
    {
        if (Application.loadedLevelName == "AttackScene" || Application.loadedLevelName == "DefendScene")
        {
            theme.Stop();
            AudioBegin = false;
        }
    }

    public void ClickSound()
    {
        DontDestroyOnLoad(gameObject);
        click.Play();
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName); 
        
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetVolume(float volume)
    {
        mainMixer.SetFloat("volume", volume);
    }
}
