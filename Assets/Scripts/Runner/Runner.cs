using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Runner : MonoBehaviour
{
    public int currentSceneIndex;
    public GameObject loseCanvas;
    public RunnerMovementController movementController;
    
    private void Awake()
    {
        if (loseCanvas != null)
        {
            loseCanvas.SetActive(false);
        }
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            loseCanvas.SetActive(true);
            GameOver();
        }
    }
    
    private void GameOver()
    {   
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        GameManager.Instance.GameOver(currentSceneIndex);
        movementController.StopMovement();
    }
}
