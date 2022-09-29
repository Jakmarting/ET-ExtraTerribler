using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private const float CAMERA_Z = -10f;

    public Transform target;
    public Vector3 offset;
    [Range(1, 10)]
    public float smoothFactor;
    public Vector3 minValues, maxValues;

    private void Start()
    {

    }

    private void FixedUpdate()
    {
        Follow();
    }

    private void Follow()
    {
        // Define minimum and maximum x,y,z vals 

        Vector3 targetPosition = target.position + offset;
        // Verify if the target position is out of bounds or not
        // Limit to the min and max values

        Vector3 boundPosition = new Vector3(
            Mathf.Clamp(targetPosition.x, minValues.x, maxValues.x),
            Mathf.Clamp(targetPosition.y, minValues.y, maxValues.y),
            CAMERA_Z
            );

        Vector3 smoothPosition = Vector3.Lerp(transform.position, boundPosition, smoothFactor * Time.fixedDeltaTime);
        transform.position = smoothPosition;
    }
}
