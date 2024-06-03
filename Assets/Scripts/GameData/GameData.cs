using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class GameData
{
    public int level;
    public int lastPlayedScene;

    public GameData (GameManager gameManager)
    {
        level = gameManager.currentLevel;
        lastPlayedScene = gameManager.lastPlayedLevel;
    }
}
