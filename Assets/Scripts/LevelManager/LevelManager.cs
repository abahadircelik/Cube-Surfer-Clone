// LevelManager.cs

using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class LevelManager : MonoBehaviour
{
    private int _level;
    public Text levelText;

    public void Update()
    {
        if (GameManager.Instance != null)
        {
            _level = GameManager.Instance.currentLevel;
            //levelText.text = "Level : " + _level;
        }
    }

    public void LoadNext()
    {
        if (GameManager.Instance.currentLevel < 3)
        {
            GameManager.Instance.currentSceneIndex++;
            GameManager.Instance.LoadNewSceneByIndex(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            
            if (GameManager.Instance.lastPlayedLevel != 0)
            {
                GameManager.Instance.LoadNewSceneByIndex(GameManager.Instance.lastPlayedLevel);
            }
            else
            {
                int randomSceneIndex = GetRandomSceneIndex(currentSceneIndex, 1, 3);
                GameManager.Instance.lastPlayedLevel = randomSceneIndex;
                GameManager.Instance.LoadNewSceneByIndex(randomSceneIndex);
                Debug.Log(GameManager.Instance.lastPlayedLevel);
            }
        }
        Debug.Log("Current level is: " + GameManager.Instance.currentLevel);
        SaveSystem.SaveGame(GameManager.Instance);
    }

    private int GetRandomSceneIndex(int currentSceneIndex, int minSceneIndex, int maxSceneIndex)
    {
        int randomSceneIndex = currentSceneIndex;

        while (randomSceneIndex == currentSceneIndex)
        {
            randomSceneIndex = Random.Range(minSceneIndex, maxSceneIndex + 1);
        }

        return randomSceneIndex;
    }

}