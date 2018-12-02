using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityOverdrivable : Ability {
	public float OverdriveDuration = 10f;
	private float _CurTime;

	[SerializeField]
	private OverdriveState _State = OverdriveState.Available;

	private OverdriveMode _LastLevelMode;

	public void StartOverdrive()
	{
		TryOverdrive();
	}

	public bool TryOverdrive()
	{
		if(_State == OverdriveState.Available)
		{			
			_ApplyOverdrive();
			StartCoroutine(_countDown(OverdriveDuration));
			_State = OverdriveState.InProgress;				
			return true;
		}
		return false;
	}

	private void _ApplyOverdrive()
	{	
		_LastLevelMode = CurrentLevel.Mode;

		if(_LastLevelMode == OverdriveMode.Level)
		{
			if(CanNextLevel) NextLevel();
			else 
			{ //Make multiplier if cant step forward
				_LastLevelMode =  OverdriveMode.Multiplier;
				CurrentLevel.Mode = _LastLevelMode;
				CurrentLevel.Overdrive();
			}
		}
		else if(_LastLevelMode == OverdriveMode.Multiplier)
		{
			CurrentLevel.Overdrive();
		}
	}

	private void _EndOverdrive()
	{	
		_State = OverdriveState.Available;
		if(_LastLevelMode == OverdriveMode.Level)
		{		
			PrevLevel();	
			CurrentLevel.Mode = OverdriveMode.Multiplier;			
		}
		else if(_LastLevelMode == OverdriveMode.Multiplier)
		{
			CurrentLevel.Normalize();	
			if(CanPrevLevel) PrevLevel();
			else
			{
				CurrentLevel.Cripple();
				_State = OverdriveState.Unavailable;
			}
		}
	}	


	private IEnumerator _countDown(float time)
	{
		yield return new WaitForSecondsRealtime(OverdriveDuration);
		_EndOverdrive();
	}

	public OverdriveState GetState()
	{
		return _State;
	}
}
