using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class TitleUIBehaviour : MonoBehaviour
{
    public Button playButton;
    public Button controlButton;


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

    }

}
