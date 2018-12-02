using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordBasic : MonoBehaviour {

	protected PlayerController _Controller;
	protected Player _Player;


	public float SlashTimeout = 0.5f;
	private float _TimeFromLastSlash;

	protected virtual void Awake() 
	{
		_Controller = FindObjectOfType<PlayerController>();	
		_Controller.OnFire += _OnFire;
		
		
		_Player = FindObjectOfType<Player>();		
		
	}

	private void Update() {		
		if(_TimeFromLastSlash > SlashTimeout)
		{
			_Player.Animation.ResetTrigger("Slash");
		}
		else
		{
			_TimeFromLastSlash += Time.deltaTime;
		}
	}
	protected void _Slash()
	{
		if(_TimeFromLastSlash < SlashTimeout) return;

		_Player.Animation.SetTrigger("Slash");	
		_TimeFromLastSlash = 0;	 
	}

	private void _OnFire(int i)
	{
		if(i == 0) _Slash();		
	}
}
