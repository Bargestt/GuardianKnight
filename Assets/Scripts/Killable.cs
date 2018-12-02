using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killable : MonoBehaviour {

	public delegate void Damaged(float amount);
	public delegate void Died();

	public event Damaged OnDamage;
	public event Died OnDeath;


	public float MaxHP = 100;
	public float CurrentHP{ get; protected set; }
	public bool IsDead{ get{return CurrentHP==0;} }


	private void Start() {
		Respawn();
	}
	public void ApplyDamage(float amount)
	{	
		if(CurrentHP > 0) 
		{
			CurrentHP -= amount;
			OnDamaged();
			if(OnDamage != null) OnDamage(amount);
			if(CurrentHP <= 0)
			{
				CurrentHP = 0;
				OnDie();
				if(OnDeath != null) OnDeath();
			}
		}
	}

	public void Die()
	{
		ApplyDamage(MaxHP);
	}

	public void Respawn()
	{
		CurrentHP = MaxHP;
	}

	protected virtual void OnDie()
	{ 		
	}
	protected virtual void OnDamaged()
	{		
	}
}
