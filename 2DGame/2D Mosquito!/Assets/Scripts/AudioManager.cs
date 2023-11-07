using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource sharedMusic;
    //public AudioSource mainSceneMusic;

    private bool hasPlayedSharedMusic = false; // Flag to track if shared music has been played


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        PlaySharedMusic();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {

        if (scene.name == "MainScene")
        {
            StopSharedMusic();
            //PlayMainSceneMusic();
        }

    }

    private void PlaySharedMusic()
    {
        if (sharedMusic != null && !hasPlayedSharedMusic)
        {
            sharedMusic.Play();
            hasPlayedSharedMusic = true;
        }
    }

    private void StopSharedMusic()
    {
        if (sharedMusic != null)
        {
            sharedMusic.Stop();
        }
    }
    //private void PlayMainSceneMusic()
    //{
    //    AudioManager mainSceneAudioManager = FindObjectOfType<AudioManager>();
    //    if (mainSceneAudioManager != null)
    //    {
    //        mainSceneMusic = mainSceneAudioManager.mainSceneMusic;
    //        mainSceneMusic.Play();
    //    }
    //}
}
