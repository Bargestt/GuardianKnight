using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementJump : MovementBasic {

	public float JumpStr = 10;
	public float AirControl = 0.2f;	
	protected override void Awake() {
		base.Awake();

		_Controller.OnJump += _Jump;
	}
	private void OnDestroy() {
		_Controller.OnJump -= _Jump; 
	}

	protected virtual void _Jump()
	{		
		if(!isActiveAndEnabled)return;
		
		if(!_Player.IsInAir())
			_Rb.AddForce(new Vector2(0, JumpStr), ForceMode2D.Impulse); 
	}

	protected override void FixedUpdate() 
	{		
		if(_Player.IsInAir())
		{
			if(_WalkDir.x != 0)
			{						
				if(Mathf.Abs(_Rb.velocity.x) < MaxWalkSpeed || Mathf.Sign(_Rb.velocity.x) != Mathf.Sign(_WalkDir.x))
				{
					_Rb.AddForce(Vector2.right * _WalkDir.x * WalkAcceleration * AirControl);
				}					
			} 
		}
		else
		{
			base.FixedUpdate();
		}
	}
	
	public override void Overdrive()
	{
		base.Overdrive();
		AirControl = OverdriveFloat(AirControl);	
	}
	public override void Normalize()
	{
		base.Normalize();
		AirControl = NormalizeFloat(AirControl);		
	}
	public override void Cripple()
	{
		base.Cripple();
		JumpStr = CrippleFloat(JumpStr);
		AirControl = 0;
	}

}
