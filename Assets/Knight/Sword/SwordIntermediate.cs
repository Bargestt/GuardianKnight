using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordIntermediate : SwordBasic {

	public GameObject SlashEffectPrefab;
	public Vector3 offset;
	
	private Effect _SlashEffect;

	private void Start() {
		var obj = GameObject.Instantiate( SlashEffectPrefab, 
								transform.position, 
								transform.rotation,
								transform
							  );
		obj.transform.localPosition = offset;
		_SlashEffect = obj.GetComponent<Effect>();
	}

	protected override void _SlashStart()
	{
		base._SlashStart();
		if(_SlashEffect != null)
		{
			_SlashEffect.Activate();		
		}
	}
}
