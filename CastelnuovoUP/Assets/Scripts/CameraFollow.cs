using UnityEngine;

public class CameraFollow : MonoBehaviour {

    [SerializeField]
    private Transform followedObject;
    Vector3 velocity = Vector3.zero;

    [SerializeField]
    private float cameraDelay = .3f;
    [SerializeField]
    private float cameraHeight = 8f;
    [SerializeField]
    private float cameraDepth = 3f;

    void Start()
    {
        if (followedObject == null) Debug.LogError("Camera doesn't have a target.");
    }

	void LateUpdate () {
        Vector3 targetPosition = followedObject.position;
        targetPosition.y += cameraHeight;
        targetPosition.z -= cameraDepth;

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, cameraDelay);
    }
}
