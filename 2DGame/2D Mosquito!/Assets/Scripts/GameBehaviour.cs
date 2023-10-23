using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameBehaviour : MonoBehaviour
{
    public static GameBehaviour Instance;

    public enum State
    {
        Play,
        Pause,
        Prep,
    }

    public State GameState;

    private float _timer;
    public float Timer
    {
        get => _timer;
        set
        {
            _timer = value;

            int minutes = Mathf.FloorToInt(Timer / 60.0f);
            int seconds = Mathf.FloorToInt(Timer % 60.0f);

            TimerGUI.text = $"{minutes:00}:{seconds:00}";
        }
    }

    [SerializeField] TextMeshProUGUI TimerGUI;
    [SerializeField] TextMeshProUGUI _pauseMessage;


    public void GameOver()
    {
        SceneManager.LoadScene("EndScene");
    }

    private void Awake()
    {
        // Singleton pattern
        if (Instance != null && Instance != this)
            Destroy(Instance.gameObject); //should it be destroy instance or instance.gameobject? 
        else
            Instance = this;

        GameState = State.Play;
        Timer = 30.0f;
    }

    private void Update()
    {
        if (GameState == State.Play)
        {
            // Stop timer when the game is paused
            Timer -= Time.deltaTime;

            Time.timeScale = GameState == State.Pause ? 0 : 1;

            if (Timer <= 0)
            {
                Timer = 0;
                GameState = State.Pause; // Pause the game when the timer reaches 0
                GameOver();

            }
        }

        //Pause key input 
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
        {
            GameState = GameState == State.Play ? State.Pause : State.Play;
            _pauseMessage.enabled = !_pauseMessage.enabled; //toggle

        }
    }
}
