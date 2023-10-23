using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameUIBehaviour : MonoBehaviour
{
    public Button exitButton;

    public void MainMenu()
    {
        SceneManager.LoadScene("TitleScene");
    }

    private void Start()
    {
        exitButton.onClick.AddListener(MainMenu);
    }
}
