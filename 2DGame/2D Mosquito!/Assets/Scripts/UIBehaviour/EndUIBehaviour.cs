using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class EndUIBehaviour : MonoBehaviour
{
    public Button mainMenuButton;
    public Button playAgainButton;

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

    }
}
