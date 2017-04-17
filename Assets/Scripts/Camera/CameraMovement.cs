using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour 
{
	// ----------------------------------------------- Data members ----------------------------------------------
	public Transform target;			// What the camera will be looking at
	public float distance = 15.0f;		// How far the camera is from the target
	public float xSpeed = 10.0f;		// Longtitudal speed of camera
	public float ySpeed = 10.0f;		// Latitudal speed of camera
	public float zoomSpeed = 5.0f;

	// yMinLimit and yMaxLimit are used to clamp the y angle of the camera.
	public float yMinLimit = 10f;
	public float yMaxLimit = 80f;
	
	public float distanceMin = 10f;		// Minimum distance camera can be from target
	public float distanceMax = 30f;		// Maximum distance camera can be from target
	
	private Rigidbody rigidbody;		// Camera rigidbody, required because we're going to be (line)casting

	private Quaternion rotation;		// Local reference to rotation
	private Vector3 position;			// Local reference to position

	public LayerMask layerMask;
	
	float x = 0.0f;						// x angle of camera
	float y = 0.0f;						// y angle of camera
	// I hate Quaternions.
	// ----------------------------------------------- End Data members -------------------------------------------

	// --------------------------------------------------- Methods ------------------------------------------------
	// --------------------------------------------------------------------
	// Use this for initialization
	void Start () 
	{
		// Going to use a default Unity functionality for this.
		Vector3 angles = this.transform.eulerAngles;
		x = angles.y;
		y = angles.x;
		
		rigidbody = GetComponent<Rigidbody>();
		
		// Make the rigid body not change rotation
		if (rigidbody != null)
		{
			rigidbody.freezeRotation = true;
		}
	}
	// --------------------------------------------------------------------
	void LateUpdate () 
	{
		if (target)		// Does target exist? (Not Null)
		{
			CameraMove();
			// Returns a rotation that rotates z degrees around the z axis,
			// x degrees around the x axis, and y degrees around the y axis (in that order).
			rotation = Quaternion.Euler(y, x, 0);	

			// Scroll wheel can be used to zoom Camera in and out. 
			// This is clamped by the values declared at the top of this file to prevent camera going through Bip, or getting too far away.
			distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel")*zoomSpeed, distanceMin, distanceMax);

			Vector3 distanceVector = new Vector3(0.0f, 0.0f, -distance);
			// Camera follows target
			Vector3 position = rotation * distanceVector + target.position;
			
			transform.rotation = rotation;
			transform.position = position;
		}
	}
	// --------------------------------------------------------------------
	public void CameraMove()
	{
		// Offset the angles by the mouse, when the mouse is moved.
		x += Input.GetAxis("Mouse X") * xSpeed;
		y -= Input.GetAxis("Mouse Y") * ySpeed;

		y = ClampAngle(y, yMinLimit, yMaxLimit);
	}
	// --------------------------------------------------------------------
	public float ClampAngle(float angle, float min, float max)
	{
		// Ensure that angle is between -360 and 360, because it is a float
		if (angle < -360F)
		{
			angle += 360F;
		}

		if (angle > 360F)
		{
			angle -= 360F;
		}
		// Then call Mathf.Clamp to actually clamp the angle.
		return Mathf.Clamp(angle, min, max);
	}
	// --------------------------------------------------- End Methods ---------------------------------------------
}