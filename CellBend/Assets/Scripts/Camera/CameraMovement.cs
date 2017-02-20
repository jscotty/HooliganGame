// @author: Justin Scott Bieshaar
// please contact me at contact@justinbieshaar.com if you have any issues!
// or read the comments ;)

using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

	private const string MOUSE_X = "Mouse X", MOUSE_Y = "Mouse Y";

	[SerializeField] private GameObject _target;

	[SerializeField] private float _speed = 1f;
	[SerializeField] private float _distanceFromTarget = 10f;
	[SerializeField] private float _addYPos = 2f;

	private float _angleAroundTarget = 0f;
	private float _pitch = 20f;
	private float _yaw;
	private float _roll;

	void Update(){
		if(_target == null) return;
		CalculateZoom();
		CalculatePitchAndAngle();
		CalculateCameraPosition(calculateHorizontalDistance, calculateVerticalDistance);
	}

	public void InvertPitch(){
		_pitch = -_pitch;
	}

#region calculations
	/// <summary>
	/// Calculates the zoom factor.
	/// </summary>
	private void CalculateZoom(){
		float zoomLevel = (float)(Input.mouseScrollDelta.y * (0.1f *_speed));
		_distanceFromTarget -= zoomLevel;
	}

	/// <summary>
	/// Calculates the pitch and angle.
	/// </summary>
	private void CalculatePitchAndAngle(){
		if(!Input.GetMouseButton(0)) return;
		float mouseX = Input.GetAxis(MOUSE_Y), mouseY = Input.GetAxis(MOUSE_X);

		_angleAroundTarget -= mouseY * _speed;
		_pitch -= mouseX * _speed;
	}

	private void CalculateCameraPosition(float horDist, float vertDist){
		float theta =   _angleAroundTarget;

		//offset calculations
		float offsetX = (float) (horDist * Mathf.Sin(theta * Mathf.Deg2Rad));
		float offsetZ = (float) (horDist * Mathf.Cos(theta * Mathf.Deg2Rad));

		// calculate new position
		Vector3 newPos;
		newPos.x = _target.transform.position.x - offsetX;
		newPos.y = _target.transform.position.y + vertDist + _addYPos;
		newPos.z = _target.transform.position.z - offsetZ;
		transform.position = newPos;

		_yaw = _angleAroundTarget;

		// rotate
		Vector3 newRot = transform.eulerAngles;
		newRot.x = _pitch;
		newRot.y = _yaw;
		transform.eulerAngles = newRot;

	}

	/// <summary>
	/// Calucating horizontal distance for positioning  
	/// </summary>
	/// <value>The calculate horizontal distance.</value>
	private float calculateHorizontalDistance {
		get{
			return (float) (_distanceFromTarget * Mathf.Cos(_pitch * Mathf.Deg2Rad));
		}
	}

	/// <summary>
	/// Calucating vertical distance for positioning 
	/// </summary>
	/// <value>The calculate vertical distance.</value>
	private float calculateVerticalDistance {
		get{
			return (float) (_distanceFromTarget * Mathf.Sin(_pitch * Mathf.Deg2Rad));
		}
	}
#endregion
}
