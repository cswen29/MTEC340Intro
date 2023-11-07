using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

#if UNITY_EDITOR
    using UnityEditor;
#endif

public class EndUIBehaviour : MonoBehaviour
{
    public Button mainMenuButton;
    public Button playAgainButton;
    public Button exitButton;
    [SerializeField] TextMeshProUGUI endLevelText;


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

        DisplayAchievedLevel();
    }

    void DisplayAchievedLevel() //Taken from ChatGPT 
    {
        if (PlayerPrefs.HasKey("AchievedLevel"))
        {
            int achievedLevel = PlayerPrefs.GetInt("AchievedLevel");
            endLevelText.text = "The Mosquitos beat you at level " + achievedLevel + " :( !!?!?!?";
        }
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
