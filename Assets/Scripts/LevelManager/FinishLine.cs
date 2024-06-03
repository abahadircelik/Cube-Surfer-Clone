using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    public GameObject winCanvas;
    public AnimationStateController playerAnimationController;
    public float celebrationDelay = 1.0f;
    public RunnerMovementController runnerMovementController;
    public Renderer finishLineRenderer;
    public RunnerStackController runnerStackController;
    //public Runner runner;
    
    private void Awake()
    {
        if (winCanvas != null)
        {   
            winCanvas.SetActive(false);
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (winCanvas != null)
        {
            if (finishLineRenderer != null)
            {
                finishLineRenderer.enabled = false;
            }
            //runner.transform.position = new Vector3(runner.transform.position.x, runner.transform.position.y + 0.3f, runner.transform.position.z);
            GameManager.Instance.currentLevel++;
            Debug.Log("Before Coroutine: Celebration Delay = " + celebrationDelay);
            StartCoroutine(ShowWinCanvasWithDelay());
            runnerStackController.gameObject.transform.SetParent(null);
            runnerMovementController.StopMovement();
        }
        GameManager.Instance.lastPlayedLevel = 0;
    }
    
    private IEnumerator ShowWinCanvasWithDelay()
    {
        Transform runner = runnerStackController.transform.GetChild(0);
        runner.position = new Vector3(runner.position.x, runner.position.y + 0.35f, runner.position.z);
        // Trigger the celebration animation if the player has the AnimationStateController
        if (playerAnimationController != null)
        {
            playerAnimationController.StartCelebrationAnimation();
        }

        // Wait for the celebrationDelay before activating the winCanvas
        yield return new WaitForSeconds(celebrationDelay);

        // Activate the winCanvas after the delay
        winCanvas.SetActive(true);
        

    }
}
