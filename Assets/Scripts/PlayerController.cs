using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	public delegate void MoveRight(float scale);
	public delegate void MoveUp(float scale);
	public delegate void Jump();
	public delegate void Fire(int type);

	public MoveRight OnMoveRight;
	public MoveUp OnMoveUp;
	public Jump OnJump;
	public Fire OnFire;

	public float HorizontalScale = 1;
	public float VerticalScale = 1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		float hScale = Input.GetAxis("Horizontal") * HorizontalScale;
		float vScale = Input.GetAxis("Vertical") * VerticalScale;
				
		if(OnMoveRight != null) OnMoveRight(hScale);
		if(OnMoveUp != null) OnMoveUp(vScale);
		

		if( Input.GetAxis("Jump") != 0 )
		{
			if(OnJump != null) OnJump();
		}

		if( Input.GetAxis("Fire1")  != 0 )
		{
			if(OnFire != null) OnFire(0);			
		}	
		if( Input.GetAxis("Fire2")  != 0 )
		{
			if(OnFire != null) OnFire(1);			
		}	
	}
}
