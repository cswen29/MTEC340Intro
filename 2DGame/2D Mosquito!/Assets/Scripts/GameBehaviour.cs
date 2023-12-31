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
        Idle
    }

    public State GameState;

    [SerializeField] TextMeshProUGUI LevelGUI;
    [SerializeField] TextMeshProUGUI _pauseMessage;
    [SerializeField] TextMeshProUGUI _pauseMessage1;
    [SerializeField] TextMeshProUGUI _pauseMessage2;

    [SerializeField] TextMeshProUGUI _instructionsMessage;
    public int score = 0;
    public int level = 1;


    public void GameOver()
    {
        // Save the achieved level to PlayerPrefs
        PlayerPrefs.SetInt("AchievedLevel", level);
        SceneManager.LoadScene("EndScene");
    }

    private void Awake()
    {
        // Singleton pattern
        if (Instance != null && Instance != this)
            Destroy(Instance.gameObject); //should it be destroy instance or instance.gameobject? 
        else
            Instance = this;

        GameState = State.Prep;
        Debug.Log("Prep State");
    }

    private void Update()
    {
        if (GameState == State.Prep)
        {
            if(_instructionsMessage !=null)
                _instructionsMessage.enabled = true;

            if (Input.GetKeyDown(KeyCode.Return))
            {
                _instructionsMessage.enabled = false;
                GameState = State.Play;
                Debug.Log("Advanced to Play State");
            }
        }

        if (GameState == State.Play)
        {
            Time.timeScale = GameState == State.Pause ? 0 : 1;

        }
        //Pause key input 
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
        {
            GameState = GameState == State.Play ? State.Pause : State.Play;
            _pauseMessage.enabled = !_pauseMessage.enabled; //toggle
            _pauseMessage1.enabled = !_pauseMessage1.enabled; //toggle
            _pauseMessage2.enabled = !_pauseMessage2.enabled; //toggle
            Debug.Log("Pause State");

        }
    }

    public void UpdateScore(int points)
    {
        score += points;
        UpdateLevel();
    }

    void UpdateLevel()
    {
        if (score % 5 == 0) // Increase level every 5 points
        {
            level++;
            LevelGUI.text = ("Level:" + level); 
        }
    }
}
