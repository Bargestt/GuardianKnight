using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementFly : MovementJump{

	public float FlySpeed = 100;
	public float FlyStr = 1;


	private bool _Flying = false;
	private float _TimeFromLastFly =0;
	private float _InitGravity;
	private float _SavedWalkSpeed;	

	protected override void Awake() {
		base.Awake();
		_InitGravity = _Rb.gravityScale;
		_SavedWalkSpeed = MaxWalkSpeed;
	}

	protected override void _Jump()
	{		
		if(!isActiveAndEnabled)return;
		
		if(_Player.IsInAir())
		{
			_Flying = true;			
			_TimeFromLastFly = 0;			
		}
		else
		{
			_Rb.AddForce(new Vector2(0, JumpStr), ForceMode2D.Impulse);
		}
	}

	private void OnDisable() {
		_Rb.gravityScale = _InitGravity;
	}

	protected override void FixedUpdate() 
	{	
		base.FixedUpdate();

		if(_Flying) 
		{	
			if(_Rb.velocity.y < 0){
				_Rb.velocity.Set(_Rb.velocity.x, 0);				
			}

			_Rb.AddForce( Vector2.up * FlyStr );
			_TimeFromLastFly += Time.fixedDeltaTime;


			if(_TimeFromLastFly > 0.1f)	{
				_Flying = false;	
				_Rb.gravityScale = _InitGravity;
			}
			else{
				_Rb.gravityScale = 0;
			}						
		}	

		if(_Player.IsInAir())
		{
			MaxWalkSpeed = FlySpeed;
		}	
		else
		{
			MaxWalkSpeed = _SavedWalkSpeed;
		}
	}
	
	public override void Overdrive()
	{		
		AirControl = OverdriveFloat(AirControl);
		FlySpeed = OverdriveFloat(FlySpeed);
	}
	public override void Normalize()
	{
		
		AirControl = NormalizeFloat(AirControl);
		FlySpeed = NormalizeFloat(FlySpeed);
	}
	public override void Cripple()
	{		
		AirControl = CrippleFloat(AirControl);
		FlySpeed = CrippleFloat(FlySpeed);		
	}
}
