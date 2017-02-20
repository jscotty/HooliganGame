// @author: Justin Scott Bieshaar
// please contact me at contact@justinbieshaar.com if you have any issues!
// or read the comments ;)

using UnityEngine;
using System.Collections;

public class DetectorRay : MonoBehaviour {

	[SerializeField] private LayerMask _detectionLayer;
	[SerializeField] private float _checkRate = .5f, _range = 5f;

	[SerializeField] private bool _update = true;

	private float _nextCheck;
	private RaycastHit _hit;

	#region initializers

	/// <summary>
	/// Initialize WITHOUT update
	/// </summary>
	/// <param name="range">Range.</param>
	/// <param name="detectionLayer">Detection layer.</param>
	public DetectorRay Init(float range, LayerMask detectionLayer){
		this._update = false;
		this._range = range;
		this._detectionLayer = detectionLayer;

		return this;
	}

	/// <summary>
	/// Initialize WITH update
	/// </summary>
	/// <param name="checkRate">Check rate.</param>
	/// <param name="range">Range.</param>
	/// <param name="detectionLayer">Detection layer.</param>
	public DetectorRay Init(float checkRate, float range, LayerMask detectionLayer){
		this._update = true;
		this._checkRate = checkRate;
		this._range = range;
		this._detectionLayer = detectionLayer;

		return this;
	}
	#endregion

	void Update(){
		if(!_update) return; // no need to update
		Detect();
	}

	#region detectors
	/// <summary>
	/// Detect with interval
	/// </summary>
	private void Detect(){
		if(Time.time < _nextCheck) return; // no need to check
		_nextCheck = Time.time + _checkRate;

		if(Physics.Raycast(transform.position,transform.forward, out _hit, _detectionLayer)){
			//TODO do stuff with detection
		}
	}

	/// <summary>
	/// Detects object
	/// </summary>
	/// <returns>The object.</returns>
	private GameObject DetectObject(){
		if(Physics.Raycast(transform.position,transform.forward, out _hit, _range, _detectionLayer)){
			return _hit.transform.gameObject;
		} // else
		return null; 
	}
	#endregion
}
