using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	[SerializeField]
	private Ability _MovementAbility;

	[SerializeField]
	private Ability _SwordAbility;

	public Animator Animation;

	

	[SerializeField]
	private bool _IsInAir = true;

	private Rigidbody2D _Rb;
	public SpriteRenderer SpriteRenderer;

	public bool LookingRight{ get; private set;}
 
	private void Awake() {
		_Rb = GetComponent<Rigidbody2D>();		
	}
	
	// Update is called once per frame
	void Update () { 
		Animation.SetBool("OnGround", !_IsInAir);		
		Animation.SetBool("Walking", _Rb.velocity.x != 0);

		if(_Rb.velocity.x < -0.1f) LookingRight = true; 
		if(_Rb.velocity.x > 0.1f) LookingRight = false;		

		SpriteRenderer.flipX = 	LookingRight;
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
