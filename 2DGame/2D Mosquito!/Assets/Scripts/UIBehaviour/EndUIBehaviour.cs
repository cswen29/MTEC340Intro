using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

#if UNITY_EDITOR
    using UnityEditor;
#endif

public class EndUIBehaviour : MonoBehaviour
{
    public Button mainMenuButton;
    public Button playAgainButton;
    public Button exitButton;


    public void MainMenu()
    {
        SceneManager.LoadScene("TitleScene");
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("MainScene");
    }

    private void Start()
    {
        mainMenuButton.onClick.AddListener(MainMenu);
        playAgainButton.onClick.AddListener(PlayAgain);
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
