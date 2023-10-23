using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControlUIBehaviour : MonoBehaviour
{
    public Button backButton;

    public void MainMenu()
    {
        SceneManager.LoadScene("TitleScene");
    }

    private void Start()
    {
        backButton.onClick.AddListener(MainMenu);
    }

}
