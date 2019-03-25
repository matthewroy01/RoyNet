using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public float movSpd;
	private Rigidbody rb;

    private float axisMin = 0.1f;

	void Start ()
    {
		rb = GetComponent<Rigidbody>();
	}
	
	void Update ()
    {
		Movement();
        Align();
        Respawn();
	}

	private void Movement()
	{
		// forwards
		if(Input.GetAxis("Vertical") > axisMin)
		{
			rb.AddForce(transform.forward * movSpd);
		}
		// backwards
		else if(Input.GetAxis("Vertical") < axisMin * -1)
		{
			rb.AddForce(transform.forward * -1 * movSpd);
		}
		// right
		if(Input.GetAxis("Horizontal") > axisMin)
		{
			rb.AddForce(transform.right * movSpd);
		}
        // left
		else if (Input.GetAxis("Horizontal") < axisMin * -1)
		{
			rb.AddForce(transform.right * -1 * movSpd);
		}
	}

    private void Align()
    {
        // make the player's forward vector match the camera
        transform.forward = Camera.main.transform.forward;
    }

    private void Respawn()
    {
        // respawn the player if they go out of bounds
        if (transform.position.y < -10.0f)
        {
            transform.position = Vector3.up * 5.0f;
            rb.velocity = Vector3.zero;
        }
    }
}
