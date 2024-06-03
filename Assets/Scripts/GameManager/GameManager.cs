using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState State;
    public static event Action<GameState> OnGameStateChanged;
    public int currentLevel;
    public int currentSceneIndex;
    public int lastPlayedLevel;
    
    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
        EventSystemManagement();
    }

    public void Start()
    {
        Application.targetFrameRate = 60;
        LoadGame();
    }

    private void EventSystemManagement()
    {
        if (FindObjectOfType<EventSystem>() == null)
        {
            var eventSystem = new GameObject("EventSystem", typeof(EventSystem), typeof(StandaloneInputModule));
            DontDestroyOnLoad(eventSystem);
        }
    }

    public void StartGame()
    {
        UpdateGameState(GameState.LoadMenu);
    }

    private void LoadNewSceneByName(String sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    
    public void LoadNewSceneByIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void GameOver(int currentIndex)
    {
        currentSceneIndex = currentIndex;
        UpdateGameState(GameState.Lose);
    }

    private void UpdateGameState(GameState newState)
    {
        State = newState;
        switch (newState)
        {
            case GameState.LoadMenu:
                HandleLoadMenu();
                break;
            case GameState.Win:
                HandleWin();
                break;
            case GameState.Lose:
                HandleLose();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
        
        OnGameStateChanged?.Invoke(newState);
    }

    private void HandleLose()
    {
        //LoadNewSceneByName("GameOverScene");
    }

    private void HandleWin()
    {
        //LoadNewSceneByName(("YouWinScene"));
    }

    private void HandleLoadMenu()
    {
        LoadNewSceneByName("Level 1");
    }

    public void SaveGame()
    {
        SaveSystem.SaveGame(this);
    }

    public void LoadGame()
    {
        GameData data = SaveSystem.LoadGame();
        if (data != null)
        {
            Debug.Log("Ohhh there is a saved data and u are using it!");
            currentLevel = data.level;
            lastPlayedLevel = data.lastPlayedScene;
        }
        else
        {
            // No saved data, use default values
            Debug.Log("// No saved data, use default values");
            currentLevel = 1;
        }
    }
}

public enum GameState
{
    LoadMenu,
    Win,
    Lose
}
