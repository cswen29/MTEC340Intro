using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GUIPauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    public static bool isPaused;
    public GameObject defaultSelectedButton; // Reference to the default selected button in the pause menu

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);

        if (defaultSelectedButton != null)
        {
            EventSystem.current.SetSelectedGameObject(defaultSelectedButton);

            // Ensure the button is set to be interactable
            defaultSelectedButton.GetComponent<Button>().Select();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) || (Input.GetKeyDown(KeyCode.P))){
            if (!isPaused)
            {
                PauseGame(); 
            }
        }

        // Allow navigation using arrow keys when paused
        if (isPaused)
        {
            //if (Input.GetKeyDown(KeyCode.DownArrow))
            //{
            //    // Move selection to the next selectable UI element
            //    Selectable next = EventSystem.current.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnDown();
            //    if (next != null)
            //    {
            //        EventSystem.current.SetSelectedGameObject(next.gameObject);
            //    }
            //}
            //else if (Input.GetKeyDown(KeyCode.UpArrow))
            //{
            //    // Move selection to the previous selectable UI element
            //    Selectable prev = EventSystem.current.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnUp();
            //    if (prev != null)
            //    {
            //        EventSystem.current.SetSelectedGameObject(prev.gameObject);
            //    }
            //}
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
            {
                // Activate the selected button (simulate a click)
                EventSystem.current.currentSelectedGameObject.GetComponent<Button>().onClick.Invoke();
            }
        }
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;

        // Set the default selected button when pausing the game
        if (defaultSelectedButton != null)
        {
            EventSystem.current.SetSelectedGameObject(defaultSelectedButton);
        }
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1.0f;
        isPaused = false; 
    }

    public void GoToTutorial()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("SampleScene"); 
    }

    public void QuitGame()
    {
        Application.Quit(); 
    }
}
