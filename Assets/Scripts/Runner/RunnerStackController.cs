using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerStackController : MonoBehaviour
{
    [SerializeField] private RunnerMovementController _runnerMovementController;
    public List<GameObject> blockList = new List<GameObject>();
    private GameObject _lastBlockObject;
    private readonly float _blockAddConstant = 1.0f;
    private Transform _transform;
    private int _decreaseBlock = 0;
    
    public AnimationStateController animationStateController;

    private void Start()
    {
        _transform = transform;
        UpdateLastBlockObject();
    }
    void Update()
    {
        UpdateTheScore();
    }

    public void IncreaseBlockStack(GameObject go)
    {
        if (!blockList.Contains(go))
        {
            if (go.transform.position.y > 1.5)
            {
                go.transform.position = new Vector3(_lastBlockObject.transform.position.x, _lastBlockObject.transform.position.y, _lastBlockObject.transform.position.z);
            }
            else
            {
                go.transform.position = new Vector3(_lastBlockObject.transform.position.x, go.transform.position.y, _lastBlockObject.transform.position.z);
            }
            _transform.position = new Vector3(_transform.position.x, _transform.position.y + _blockAddConstant, _transform.position.z);
            go.transform.SetParent(transform);
            blockList.Add(go);
            UpdateLastBlockObject();
        }
    }

    public void DecreaseBlockStack(GameObject go)
    {
        if (blockList.Contains(go))
        {
            _decreaseBlock++;
            go.transform.parent = null;
            blockList.Remove(go);
            StartCoroutine(DecreaseWithDelay(go, _runnerMovementController.runnerForwardMovementSpeed));
            UpdateLastBlockObject();
        }
    }
    private IEnumerator DecreaseWithDelay(GameObject go, float forwardSpeed)
{
    float delayConstant = Mathf.Clamp(4.1f / forwardSpeed, 0.1f, 1.0f);
    float delay = 0.41f * delayConstant; // it was previously 0.42, now it is more fast-paced.

    yield return new WaitForSeconds(delay);

    if (transform.childCount > 0)
    {
        Transform mainCubeFirstChild = transform.GetChild(0);
        StartCoroutine(LandFirstChild(mainCubeFirstChild));
    }

    // Continue with the remaining code
    float initialY = _transform.position.y;
    float targetY = initialY - (_blockAddConstant * _decreaseBlock);

    float finalElapsedTime = 0f;
    float finalDuration = 0.2f;

    while (finalElapsedTime < finalDuration)
    {
        float newY = Mathf.Lerp(initialY, targetY, finalElapsedTime / finalDuration);
        _transform.position = new Vector3(_transform.position.x, newY, _transform.position.z);
        
        finalElapsedTime += Time.deltaTime;
        yield return null;
    }
    _transform.position = new Vector3(_transform.position.x, targetY, _transform.position.z);
    _decreaseBlock = 0;
}

    private IEnumerator LandFirstChild(Transform child)
    {
        animationStateController.StartFallingAnimation();
        // Store the initial position of the first child
        Vector3 initialPosition = child.position;

        // Get the target position
        Vector3 targetPosition = initialPosition - new Vector3(0f, _blockAddConstant * _decreaseBlock, 0f);

        // Set up animation parameters
        float elapsedTime = 0f;
        float duration = 0.3f;

        // Perform the landing animation
        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
        
            // Update only the y position, allowing movement in x and z directions
            child.position = new Vector3(child.position.x, Mathf.Lerp(initialPosition.y, targetPosition.y, t), child.position.z);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the final position is precisely the target position
        child.position = targetPosition;

        // Reset the local position to maintain relative position to the parent
        child.localPosition = new Vector3(0f, 0.3f, 0f);
        
        animationStateController.FinishFallingAnimation();
        UpdateLastBlockObject(); // Ensure _lastBlockObject is updated
    }
    
    private void UpdateLastBlockObject()
    {
        if (blockList.Count > 0)
        {
            _lastBlockObject = blockList[^1];
        }
    }

    private void UpdateTheScore()
    {
        ScoreManager.Score = blockList.Count;
    }
}