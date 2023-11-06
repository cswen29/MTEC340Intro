using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

#if UNITY_EDITOR
    using UnityEditor;
#endif

public class TitleUIBehaviour : MonoBehaviour
{
    public Button playButton;
    public Button controlButton;
    public Button exitButton;


    public void StartGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void LoadControls()
    {
        SceneManager.LoadScene("ControlScene");
    }

    private void Start()
    {
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
}
