using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    // Lets us set the player as the target
    public Transform target;

    public Vector3 offset;
    // A bunch of values controlling the camera
    public float zoomSpeed = 4f;
    public float minZoom = 2f;
    public float maxZoom = 15f;

    // The angle of the camera
    public float pitch = 2f;
    
    // How fast the camera will rotate
    public float yawSpeed = 100f;

    // The start values for the cameras position
    private float currentZoom = 10f;
    private float currentYaw = 0f;

    void Update ()
    {
        // Allows us to zoom in and out with scroll wheel
        currentZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        // Clamp makes it so we cant go above or below our values we set
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);

        // This is what rotates the camera
        // Horizontal is a unity function that means A and D along the X axis
        currentYaw -= Input.GetAxis("Horizontal") * yawSpeed * Time.deltaTime;
    }

    void LateUpdate ()
    {
        // Makes the camera keep its same distance when it moves with us
        transform.position = target.position - offset * currentZoom;
        // Is what points the camera at the player
        transform.LookAt(target.position + Vector3.up * pitch);

        // Is what spins the camera around the player
        transform.RotateAround(target.position, Vector3.up, currentYaw);
    }
}
