using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

#if UNITY_EDITOR
    using UnityEditor;
#endif
[RequireComponent(typeof(AudioSource))]

public class TitleUIBehaviour : MonoBehaviour
{
    public Button playButton;
    public Button controlButton;
    public Button exitButton;

    [SerializeField] AudioClip _smashButton1;
    [SerializeField] AudioClip _smashButton2;

    AudioSource _source;

    public void StartGame()
    {
        PlayButtonSound(_smashButton1);
        SceneManager.LoadScene("MainScene");
    }

    public void LoadControls()
    {
        PlayButtonSound(_smashButton2);
        SceneManager.LoadScene("ControlScene");
    }

    private void Start()
    {
        _source = GetComponent <AudioSource>();

        playButton.onClick.AddListener(StartGame);
        controlButton.onClick.AddListener(LoadControls);
        exitButton.onClick.AddListener(Quit);
    }

    void Quit()
    {
        #if UNITY_EDITOR
            EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
    void PlayButtonSound(AudioClip clip)
    {
        if (_source != null && clip != null)
        {
            _source.PlayOneShot(clip);
        }
    }
}
