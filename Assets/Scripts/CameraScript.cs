using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

	public Transform Target;
	public Transform Origin;

	public float FalloffStart;
	public float MaxDist;

	public AnimationCurve StrengthCurve;

	private float _FalloffStart;
	private float _MaxDist;


	private void Awake() {
		_FalloffStart = Mathf.Pow(FalloffStart, 2);
		_MaxDist = Mathf.Pow(MaxDist, 2);		
	}

	private void LateUpdate() {
		if(Target == null || Origin == null) return;

		Vector2 offset = Target.position - Origin.position;

		float strength = Mathf.Clamp01( (offset.sqrMagnitude - _FalloffStart) / _MaxDist) ;	
		


		transform.position = Vector3.Lerp(Origin.position, Target.position, StrengthCurve.Evaluate(strength));
		transform.position = new Vector3( transform.position.x, transform.position.y, -1);
	}
}
