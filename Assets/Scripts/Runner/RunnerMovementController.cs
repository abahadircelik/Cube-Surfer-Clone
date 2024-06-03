using UnityEngine;

public class RunnerMovementController : MonoBehaviour
{

    [SerializeField] private RunnerInputController runnerInputController;
    [SerializeField] public float runnerForwardMovementSpeed;
    [SerializeField] private float runnerHorizontalMovementSpeed;
    [SerializeField] private float horizontalLimitValue;
    private Transform _transform;
    private bool _isMoving = true;

    private float _newPositionX;

    private void Start()
    {
        _transform = transform;
    }
    
    void Update()
    {
        if (_isMoving)
        {
            SetRunnerForwardMovementSpeed();
            SetRunnerHorizontalSpeed();   
        }
    }

    private void SetRunnerForwardMovementSpeed() {
        transform.Translate(Vector3.forward * (runnerForwardMovementSpeed * Time.deltaTime));

    }

    private void SetRunnerHorizontalSpeed() {
        Vector3 currentPosition = _transform.position;
        _newPositionX = currentPosition.x + (runnerInputController.HorizontalValue * runnerHorizontalMovementSpeed);
        _newPositionX = Mathf.Clamp(_newPositionX, -horizontalLimitValue, horizontalLimitValue);
        _transform.position = new Vector3(_newPositionX, currentPosition.y, currentPosition.z);
    }
    
    public void StopMovement()
    {
        _isMoving = false;
    }

}
