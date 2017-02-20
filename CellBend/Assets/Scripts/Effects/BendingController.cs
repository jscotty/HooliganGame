// @author: Justin Scott Bieshaar
// please contact me at contact@justinbieshaar.com if you have any issues!
// or read the comments ;)

using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class BendingController : MonoBehaviour {

	[SerializeField] private Transform _curveOrigin;
	[SerializeField] private float _curvature = 0f;
	[SerializeField] private float _flatMargin = 0f;

	[Range(0.5f,2f)]
	[SerializeField] private float _xScale = 1f;
	[Range(0.5f,2f)]
	[SerializeField] private float _zScale = 1f;

#region uniform locations

	private int _location_curveOrignin;
	private int _location_curvature;
	private int _location_flatMargin;
	private int _location_scale;

	private void InitLocations(){
		// generating location ID's
		_location_curveOrignin = Shader.PropertyToID(ShaderProperties.CURVE_ORIGIN);
		_location_curvature = Shader.PropertyToID(ShaderProperties.CURVATURE);
		_location_flatMargin = Shader.PropertyToID(ShaderProperties.FLAT_MARGIN);
		_location_scale = Shader.PropertyToID(ShaderProperties.SCALE);
	}

#endregion

	private Vector3 _scale;

	void Start () {
		InitLocations();
	}

	void Update(){
		_scale.x = _xScale;
		_scale.z = _zScale;

		Shader.SetGlobalVector(_location_curveOrignin, _curveOrigin.position);
		Shader.SetGlobalFloat(_location_curvature, _curvature * 0.0001f);
		Shader.SetGlobalFloat(_location_flatMargin, _flatMargin);
		Shader.SetGlobalVector(_location_scale, _scale);
	}
}
