using System;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    [SerializeField] private RunnerStackController runnerStackController;
    
    private bool _isStack = false;
    private bool _hasCollided = false;

    void Start()
    {
        runnerStackController = GameObject.FindObjectOfType<RunnerStackController>();
    }

    void Update()
    {
        if (ShouldCollectCube())
        {
            if (!_isStack)
            {
                _isStack = true;
                runnerStackController.IncreaseBlockStack(gameObject);
            }
        }
    }
    
    private bool ShouldCollectCube()
    {
        float minCollectDistance = 1.5f;

        Vector3 lastBlockPosition = runnerStackController.blockList[^1].transform.position;
        Vector3 cubePosition = transform.position;

        // Ignore y distance and only consider x and z distances
        float distanceXZ = Vector2.Distance(new Vector2(lastBlockPosition.x, lastBlockPosition.z), new Vector2(cubePosition.x, cubePosition.z));

        return distanceXZ < minCollectDistance;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (!_hasCollided && other.gameObject.CompareTag("Obstacle") && other.gameObject.transform.position.y == gameObject.transform.position.y)
        {
            runnerStackController.DecreaseBlockStack(gameObject);
            _hasCollided = true;
        }
    }
}