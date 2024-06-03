using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text scoreText;
    public static int Score;

    void Update()
    {
            scoreText.text = "Score : " + Score;
    }
    
}
