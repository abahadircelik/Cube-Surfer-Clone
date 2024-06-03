using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public void RestartGame()
    {
        GameManager.Instance.LoadNewSceneByIndex(GameManager.Instance.currentSceneIndex);
    }
    
    public void MainMenu()
    {
        SceneManager.LoadScene("LoadScene");
    }
    
}
