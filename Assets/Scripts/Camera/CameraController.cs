using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField] private Transform runnerTransform;
    [SerializeField] private float lerpValue;
    private Vector3 offset;
    private Vector3 newPosition;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - runnerTransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        SetCameraFollow();
    }

    private void SetCameraFollow() {
        newPosition = Vector3.Lerp(transform.position, new Vector3(0f, runnerTransform.position.y, runnerTransform.position.z) + offset, lerpValue * Time.deltaTime);
        transform.position = newPosition;
    }
}
