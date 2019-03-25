using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// written by Matthew Roy
// apply this script to an object that has the MainCamera as a child
public class FirstPersonCamera : MonoBehaviour
{
	[Header("Camera")]
	public float camSpeedHorizontal; // horizontal camera sensitivity
    public float camSpeedVertical; // vertical camera sensitivity

    // private components
	private Camera cam;

	// yaw and pitch control the horizontal and vertical angle of the camera
    private float yaw = 0.0f;
    private float pitch = 0.0f;

	void Start ()
	{
		cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
	}

	void Update ()
	{
		CameraRotation();
	}

	private void CameraRotation()
	{
		// code found here: https://gamedev.stackexchange.com/questions/104693/how-to-use-input-getaxismouse-x-y-to-rotate-the-camera
		yaw += camSpeedHorizontal * Input.GetAxis("Mouse X");
        pitch -= camSpeedVertical * Input.GetAxis("Mouse Y");

        // prevent the player from looking up or down infinitely
        if (pitch > 90)
        {
        	pitch = 90;
        }
        else if (pitch < -90)
        {
        	pitch = -90;
        }

        // apply the rotation
        cam.transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
	}
}
