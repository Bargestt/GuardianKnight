using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Run only
public class MovementBasic : Overdrivable {
	public float WalkAcceleration = 40;
	public float MaxWalkSpeed = 5;
	public float DecelerationSpeed = 0.4f;

	protected PlayerController _Controller;
	protected Player _Player;
	protected Rigidbody2D _Rb;

	protected Vector2 _WalkDir = Vector2.zero;



	protected virtual void Awake() 
	{
		_Controller = FindObjectOfType<PlayerController>();
		_Controller.OnMoveRight += _SetMoveRight;
		
		
		_Player = FindObjectOfType<Player>();
		_Rb = _Player.GetComponent<Rigidbody2D>();
		
		_SavedDecelerationSpeed = DecelerationSpeed;
	}

	protected virtual void FixedUpdate() 
	{
		if(_Player.IsInAir()) return;
		if(_WalkDir.x != 0)
		{						
			if(Mathf.Abs(_Rb.velocity.x) < MaxWalkSpeed)
			{
				_Rb.AddForce(Vector2.right * _WalkDir.x * WalkAcceleration);
			}
			else
			{			
				_Rb.velocity = new Vector2( Mathf.Clamp(_Rb.velocity.x, -MaxWalkSpeed, MaxWalkSpeed), _Rb.velocity.y);
			}		
		} 
		else{			
			_Rb.velocity = new Vector2(Mathf.Lerp(_Rb.velocity.x, 0, DecelerationSpeed), _Rb.velocity.y);
		}			
	}
	


	private float _SavedDecelerationSpeed;
	public override void Overdrive()
	{
		WalkAcceleration *= OverdriveMultiplier;
		MaxWalkSpeed *= OverdriveMultiplier;
		DecelerationSpeed = 1;
	}
	public override void Normalize()
	{
		WalkAcceleration /= OverdriveMultiplier;
		MaxWalkSpeed /= OverdriveMultiplier;
		DecelerationSpeed = _SavedDecelerationSpeed;
	}
	public override void Cripple()
	{
		WalkAcceleration /= CrippleMultiplier;
		MaxWalkSpeed /= CrippleMultiplier;
		DecelerationSpeed = 1;

		_Player.Animation.SetFloat("WalkSpeed", 0.5f);
	}





	private void _SetMoveRight(float scale)
	{
		if(scale != 0)
		{
			_WalkDir.x = Mathf.Sign(scale);
		}
		else
		{
			_WalkDir.x = 0;
		}	
	}



}

