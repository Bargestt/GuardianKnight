﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Killable {

	[SerializeField]
	private AbilityOverdrivable _MovementAbility;
	[SerializeField]
	private AbilityOverdrivable _SwordAbility;

	public Animator Animation;
	

	[SerializeField]
	private bool _IsInAir = true;

	private Rigidbody2D _Rb;
	public SpriteRenderer SpriteRenderer;

	private PlayerController _Controller;

	public bool LookingRight{ get; private set;}
 
	private void Awake() {
		_Rb = GetComponent<Rigidbody2D>();	
		_Controller = FindObjectOfType<PlayerController>();
		_Controller.OnMoveRight += _Flip;
		_Controller.OnOverdrive += _EnableOverdrive;
	}
	
	// Update is called once per frame
	void Update () { 
		Animation.SetBool("OnGround", !_IsInAir);		
		Animation.SetBool("Walking", _Rb.velocity.x != 0);			
	}

	private void _Flip(float scale)
	{
		if(scale < -0.1f) LookingRight = true; 
		if(scale > 0.1f) LookingRight = false;	

		transform.eulerAngles = new Vector3(0, LookingRight?180:0, 0);	
	}

	private void _EnableOverdrive(OverdriveType type)
	{		
		if(type == OverdriveType.Movement) 
			_MovementAbility.StartOverdrive();
		else if(type == OverdriveType.Sword) 
			_SwordAbility.StartOverdrive();
		//else if(type == OverdriveType.Magic) 		
	}

	protected override void OnDie()
	{
		foreach(Transform tr in transform)
		{
			tr.gameObject.SetActive(false);			
		}	

		Animation.gameObject.SetActive(true);
		Animation.SetTrigger("Dead");

		_Controller.OnMoveRight -= _Flip;
		_Controller.OnOverdrive -= _EnableOverdrive;

		Destroy(gameObject, 1f);
	}


	
	public bool IsInAir(){
		return _IsInAir;
	}
	
	private void OnCollisionEnter2D(Collision2D other) {
		if(other.gameObject.CompareTag("Ground"))
			_IsInAir = false;
	}
	private void OnCollisionExit2D(Collision2D other) {
		if(other.gameObject.CompareTag("Ground"))
			_IsInAir = true;
	}
}
