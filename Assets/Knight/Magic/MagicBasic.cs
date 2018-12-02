using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBasic : MonoBehaviour {

	protected PlayerController _Controller;
	protected Player _Player;

	public float CastDelay = 0.2f;


	public float CastTimeout = 2f;
	private float _TimeFromLastCast = 100;

	public GameObject MagicPrefab;

	protected virtual void Awake() 
	{
		_Controller = FindObjectOfType<PlayerController>();	
		_Controller.OnFire += _OnFire;
		
		
		_Player = FindObjectOfType<Player>();		
		
	}

	private void Update() {		
		if(_TimeFromLastCast > CastTimeout)
		{
			_Player.Animation.ResetTrigger("Cast");
		}
		else
		{
			_TimeFromLastCast += Time.deltaTime; 
		}
	}
	protected void _Cast()
	{
		if(_TimeFromLastCast < CastTimeout) return;

		StartCoroutine(_DelayedCast());

		_Player.Animation.SetTrigger("Cast");	 
		_TimeFromLastCast = 0;	 
	}

	private IEnumerator _DelayedCast()
	{
		yield return new WaitForSeconds(CastDelay);
		GameObject.Instantiate( MagicPrefab, 
								transform.position, 
								Quaternion.Euler(0, _Player.LookingRight? 180:0, 0)
							  );		
	}

	private void _OnFire(int i)
	{
		if(i == 1) _Cast();		
	}
}
