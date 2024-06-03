using System;
using UnityEngine;

public class RunnerInputController : MonoBehaviour
{
    private float _horizontalValue;
    private readonly float _sensitivityConstant = 250.0f;
    public float HorizontalValue => _horizontalValue;

    void Update()
    {
        HandleRunnerHorizontalInput();
    }

    private void HandleRunnerHorizontalInput()
    {
        if (Input.GetMouseButton(0))
        {
            _horizontalValue = ((Input.GetAxis("Mouse X") / Screen.dpi) * _sensitivityConstant);
        }
        else
        {
            _horizontalValue = 0;
        }
    }
}